namespace PaymentManager.Api.Dtos
{
    public class PaymentRequestDto
    {
        public string MerchantStoreId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentServiceUrl { get; set; }
    }
}