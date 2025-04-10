namespace EPSLTTaskServer.Application.Models
{
    public class DiscountCodeModel
    {
        public string Code { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }

    }

}
