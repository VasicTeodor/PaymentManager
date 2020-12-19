namespace PaymentManager.Api.Models
{
    public class CompleteTransactionResult
    {
        public string UrlForRedirection { get; set; }
        public bool IsSaved { get; set; }
    }
}