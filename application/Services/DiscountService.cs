using EPSLTServer.Domain.Entities;
using EPSLTServer.Domain.Interfaces;
using EPSLTTaskServer.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace EPSLTTaskServer.Application.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _repository;
        private static readonly char[] _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        public DiscountService(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> GenerateCodesAsync(int count, byte length)
        {
            if (length < 7 || length > 8)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be 7 or 8.");

            var successfulCodes = new List<DiscountCode>();
            var maxAttempts = 5;
            int attempts = 0;

            while (successfulCodes.Count < count && attempts < maxAttempts)
            {
                var codesRemaining = count - successfulCodes.Count;
                var codesToInsert = new List<DiscountCode>();

                for (int i = 0; i < codesRemaining; i++)
                {
                    var code = GenerateRandomCode(length);
                    codesToInsert.Add(new DiscountCode { Code = code });
                }

                try
                {
                    await _repository.SaveCodesAsync(codesToInsert);
                    successfulCodes.AddRange(codesToInsert);

                }
                catch (DbUpdateException)
                {
                    _repository.ClearContextTracker();
                }

                attempts++;
            }

            return successfulCodes.Count == count;
        }

        public async Task<bool> UseCodeAsync(string code)
        {
            var discountCode = await _repository.GetUnusedCodeAsync(code);
            if (discountCode == null)
                return false;

            await _repository.MarkCodeAsUsedAsync(discountCode);
            return true;
        }

        private string GenerateRandomCode(int length)
        {
            return new string(Enumerable.Range(0, length)
                .Select(_ => _chars[RandomNumberGenerator.GetInt32(_chars.Length)]).ToArray());
        }
    }
}
