using System;
using System.Collections.Generic;

namespace UP.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public int UserId { get; set; }

    public int? BookId { get; set; }

    public int? ReviewId { get; set; }

    public string Cause { get; set; } = null!;

    public bool IsChecked { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Review? Review { get; set; }

    public virtual User User { get; set; } = null!;
}
