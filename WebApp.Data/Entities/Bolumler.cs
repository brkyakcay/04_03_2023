using System;
using System.Collections.Generic;

namespace WebApp.Data.Entities;

public partial class Bolumler
{
    public int Id { get; set; }

    public string? Adi { get; set; }

    public int OgretmenId { get; set; }

    public virtual Ogretmenler Ogretmen { get; set; } = null!;
}
