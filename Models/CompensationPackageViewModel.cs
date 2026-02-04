namespace ERP.Models
{
    /// <summary>
    /// ViewModel for creating/editing compensation packages
    /// Used for form binding in CompensationPackageController.Save()
    /// </summary>
    public class CompensationPackageViewModel
    {
        public int EmployeeId { get; set; }
        public decimal BaseSalary { get; set; }
        public DateTime EffectiveFrom { get; set; } = DateTime.Now;

        public List<AllowanceDto>? Allowances { get; set; }
        public List<BonusDto>? Bonuses { get; set; }
        public List<AdvantageDto>? Advantages { get; set; }
    }

    public class AllowanceDto
    {
        public int AllowanceTypeId { get; set; }
        public decimal Amount { get; set; }
        public bool IsRecurring { get; set; }
        public bool IsTaxable { get; set; }
        public string? Frequency { get; set; }
        public DateTime EffectiveFrom { get; set; } = DateTime.Now;
        public DateTime? EffectiveTo { get; set; }
    }

    public class BonusDto
    {
        public int BonusTypeId { get; set; }
        public decimal Amount { get; set; }
        public bool IsAutomatic { get; set; }
        public bool IsTaxable { get; set; } = true;
        public bool IsExceptional { get; set; }
        public bool IsPerformanceBased { get; set; }
        public string? BonusRule { get; set; }
        public DateTime AwardedOn { get; set; } = DateTime.Now;
        public DateTime? ValidUntil { get; set; }
    }

    public class AdvantageDto
    {
        public int AdvantageTypeId { get; set; }
        public decimal Value { get; set; }
        public string? Provider { get; set; }
        public string? EligibilityRule { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
    }
}