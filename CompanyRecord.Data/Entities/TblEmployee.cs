using System;
using System.Collections.Generic;

namespace CompanyRecord.Data.Entities;

public partial class TblEmployee
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public string? DepartmentName { get; set; }

    public string? EmailAddress { get; set; }

    public decimal? Salary { get; set; }
}
