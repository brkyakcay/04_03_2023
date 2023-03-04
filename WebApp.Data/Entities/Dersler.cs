using System;
using System.Collections.Generic;

namespace WebApp.Data.Entities;

public partial class Dersler
{
    public int Id { get; set; }

    public string? Adi { get; set; }

    public virtual ICollection<Ogrenciler> Ogrencilers { get; } = new List<Ogrenciler>();

    public virtual ICollection<Ogretmenler> Ogretmenlers { get; } = new List<Ogretmenler>();
}
