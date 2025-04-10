using EPSLTTaskServer.Application.Enums;
using EPSLTTaskServer.Application.Interfaces;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class TcpServer
{
    private readonly IDiscountService _discountService;
    private readonly TcpListener _listener;

    public TcpServer(int port, IDiscountService discountService)
    {
        _discountService = discountService;
        _listener = new TcpListener(IPAddress.Any, port);
    }

    public void Start()
    {
        _listener.Start();
        Console.WriteLine("Server started on port 5000");

        while (true)
        {
            var client = _listener.AcceptTcpClient();
            _ = Task.Run(() => HandleClient(client));
        }
    }

    private async Task HandleClient(TcpClient client)
    {
        using var stream = client.GetStream();

        try
        {
            while (true)
            {
                int messageType = stream.ReadByte();

                if (messageType == (int)RequestTypeEnum.ClientClosedConnection)
                    break;

                switch (messageType)
                {
                    case (int)RequestTypeEnum.Generate:

                        var genBuffer = new byte[3];
                        int genRead = await ReadExactAsync(stream, genBuffer, 0, 3);
                        if (genRead != 3)
                        {
                            Console.WriteLine("Incomplete generate request.");
                            return;
                        }

                        ushort count = BitConverter.ToUInt16(genBuffer, 0);
                        byte length = genBuffer[2];

                        Console.WriteLine($"[Generate] {count} codes of length {length}");

                        var isGeneratedSuccessfully = await _discountService.GenerateCodesAsync(count, length);

                        await stream.WriteAsync(new byte[] { isGeneratedSuccessfully ? (byte)1 : (byte)0 });
                        break;

                    case (int)RequestTypeEnum.Use:
                        var codeBuffer = new byte[8];
                        int useRead = await ReadExactAsync(stream, codeBuffer, 0, 8);
                        if (useRead < 7 || useRead > 8)
                        {
                            Console.WriteLine("Invalid code length received.");
                            return;
                        }

                        string code = Encoding.ASCII.GetString(codeBuffer, 0, useRead).TrimEnd('\0');
                        Console.WriteLine($"[Use] Code: {code}");

                        var result = await _discountService.UseCodeAsync(code);
                        byte resultCode = result ? (byte)1 : (byte)0;

                        await stream.WriteAsync(new byte[] { resultCode });
                        break;

                    default:
                        Console.WriteLine($"Unknown message type received: {messageType}");
                        return;
                }
            }
        }
        catch (IOException ioEx)
        {
            Console.WriteLine($"Client disconnected: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Server error: {ex.Message}");
        }
    }

    private async Task<int> ReadExactAsync(NetworkStream stream, byte[] buffer, int offset, int count)
    {
        int totalRead = 0;

        while (totalRead < count)
        {
            int bytesRead = await stream.ReadAsync(buffer, offset + totalRead, count - totalRead);
            if (bytesRead == 0) break; // Client disconnected

            totalRead += bytesRead;
        }

        return totalRead;
    }
}