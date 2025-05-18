using System;
using System.Collections.Generic;

namespace Variant1.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Article { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Genre { get; set; }

    public string? Description { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int Status { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
