
using UretimTakip.Models;
public class UretimViewModel
{
    public List<UretimKaydi> UretimKayitlari { get; set; } = new List<UretimKaydi>();
    public List<MolaBilgisi> StandartDuruslar { get; set; } = new List<MolaBilgisi>
    {
        new MolaBilgisi { Baslangic = new TimeSpan(10, 0, 0), Bitis = new TimeSpan(10, 15, 0), MolaAdi = "Çay Molası" },
        new MolaBilgisi { Baslangic = new TimeSpan(12, 0, 0), Bitis = new TimeSpan(12, 30, 0), MolaAdi = "Yemek Molası" },
        new MolaBilgisi { Baslangic = new TimeSpan(15, 0, 0), Bitis = new TimeSpan(15, 15, 0), MolaAdi = "Çay Molası" }
    };
    public List<UretimKaydi> IslenmisKayitlar { get; set; } = new List<UretimKaydi>();
}
