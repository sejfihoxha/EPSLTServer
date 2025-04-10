namespace EPSLTServer.Domain.Entities
{
    public class DiscountCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public bool IsUsed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
