using EPSLTServer.Domain.Entities;

namespace EPSLTServer.Domain.Interfaces
{
    public interface IDiscountRepository
    {
        Task SaveCodesAsync(IEnumerable<DiscountCode> codes);
        Task<DiscountCode?> GetUnusedCodeAsync(string code);
        Task MarkCodeAsUsedAsync(DiscountCode code);
        void ClearContextTracker();
    }
}
