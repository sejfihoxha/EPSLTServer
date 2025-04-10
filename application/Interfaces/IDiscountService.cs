namespace EPSLTTaskServer.Application.Interfaces
{
    public interface IDiscountService
    {
        Task<bool> GenerateCodesAsync(int count, byte length);
        Task<bool> UseCodeAsync(string code);

    }
}
