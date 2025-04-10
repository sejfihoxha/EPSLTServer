using EPSLTServer.Domain.Entities;
using EPSLTServer.Domain.Interfaces;
using EPSLTTaskServer.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace EPSLTTaskServer.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DiscountDbContext _context;

        public DiscountRepository(DiscountDbContext context)
        {
            _context = context;
        }
        public async Task SaveCodesAsync(IEnumerable<DiscountCode> codes)
        {
            await _context.DiscountCodes.AddRangeAsync(codes);
            await _context.SaveChangesAsync();
        }
        public async Task<DiscountCode?> GetUnusedCodeAsync(string code)
        {
            return await _context.DiscountCodes
                .FirstOrDefaultAsync(dicountCode => dicountCode.Code == code && !dicountCode.IsUsed);
        }

        public async Task MarkCodeAsUsedAsync(DiscountCode code)
        {
            code.IsUsed = true;
            _context.DiscountCodes.Update(code);
            await _context.SaveChangesAsync();
        }

        public void ClearContextTracker()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
