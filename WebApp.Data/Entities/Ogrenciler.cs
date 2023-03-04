using System;
using System.Collections.Generic;

namespace WebApp.Data.Entities;

public partial class Ogrenciler
{
    public int Id { get; set; }

    public int No { get; set; }

    public string? Adi { get; set; }

    public string? GobekAdi { get; set; }

    public string? Soyadi { get; set; }

    public string? Eposta { get; set; }

    public virtual ICollection<Dersler> Derslers { get; } = new List<Dersler>();
}
