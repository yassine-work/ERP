namespace ERP.Models
{
    public enum CalculationType
    {
        Fixed,
        Percentage,
        Formula
    }

    public enum PaymentFrequency
    {
        Monthly,
        Quarterly,
        Annual,
        OneTime
    }

    public enum AssignmentStatus
    {
        Active,
        PendingApproval,
        Suspended,
        Ended
    }

    public enum BenefitCategory
    {
        Transport,
        Health,
        Technology,
        Wellness,
        Accommodation
    }
}