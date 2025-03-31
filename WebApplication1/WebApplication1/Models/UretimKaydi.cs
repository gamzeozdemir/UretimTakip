namespace UretimTakip.Models;

public class UretimKaydi
{
    public int KayitNo { get; set; }
    public DateTime Baslangic { get; set; }
    public DateTime Bitis { get; set; }
    public string Statu { get; set; } = "ÜRETİM"; // "Statü" yerine "Statu" kullanıldı
    public string? DurusNedeni { get; set; } // Duruş varsa nedeni
}
