using System;
using System.Collections.Generic;

namespace BusinessObjects.Entities;

public partial class GenderType
{
    public int GenderId { get; set; }

    public string? GenderName { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
