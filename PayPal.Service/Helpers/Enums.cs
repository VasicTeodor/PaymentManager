namespace PayPal.Service.Helpers
{
    public enum ChargeModelType
    {
        SHIPPING,
        TAX
    }

    public enum FailAction
    {
        CONTINUE,
        CANCEL
    }

    public enum Frequency
    {
        WEEK,
        DAY,
        YEAR,
        MONTH
    }

    public enum PaymentType
    {
        TRIAL,
        REGULAR
    }

    public enum SubscriptionType
    {
        FIXED,
        INFINITE
    }
}