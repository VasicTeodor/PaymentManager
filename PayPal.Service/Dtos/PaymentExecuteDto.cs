namespace PayPal.Service.Dtos
{
    public class PaymentExecuteDto
    {
        public string PaymentId { get; set; }
        public string Token { get; set; }
        public string PayerId { get; set; }
    }
}