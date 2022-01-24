namespace StoreKeepersAssistant.ViewModels
{
    public class InvoiceItemDTO
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string ItemId { get; set; }
        public double Qty { get; set; }
    }
}
