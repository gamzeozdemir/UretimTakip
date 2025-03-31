using System;
using System.Collections.Generic;
using UretimTakip.Models;

namespace UretimTakip.Services
{
    public class UretimHesaplamaService
    {
        public List<UretimKaydi> Hesapla(List<UretimKaydi> uretimKayitlari, List<MolaBilgisi> molaBilgileri)
        {
            List<UretimKaydi> yeniKayitlar = new List<UretimKaydi>();

            foreach (var kayit in uretimKayitlari)
            {
                DateTime mevcutBaslangic = kayit.Baslangic;
                DateTime mevcutBitis = kayit.Bitis;
                bool bolundu = false;

                foreach (var mola in molaBilgileri)
                {
                    DateTime molaBaslangic = kayit.Baslangic.Date + mola.Baslangic;
                    DateTime molaBitis = kayit.Baslangic.Date + mola.Bitis;

                    if (mevcutBaslangic < molaBitis && mevcutBitis > molaBaslangic)
                    {
                        bolundu = true;

                        if (mevcutBaslangic < molaBaslangic)
                        {
                            yeniKayitlar.Add(new UretimKaydi
                            {
                                KayitNo = kayit.KayitNo,
                                Baslangic = mevcutBaslangic,
                                Bitis = molaBaslangic,
                                Statu = "URETIM",
                                DurusNedeni = null
                            });
                        }

                        yeniKayitlar.Add(new UretimKaydi
                        {
                            KayitNo = kayit.KayitNo,
                            Baslangic = molaBaslangic,
                            Bitis = molaBitis,
                            Statu = "DURUŞ",
                            DurusNedeni = mola.MolaAdi
                        });

                        mevcutBaslangic = molaBitis;
                    }
                }

                if (!bolundu || mevcutBaslangic < mevcutBitis)
                {
                    yeniKayitlar.Add(new UretimKaydi
                    {
                        KayitNo = kayit.KayitNo,
                        Baslangic = mevcutBaslangic,
                        Bitis = mevcutBitis,
                        Statu = kayit.Statu,
                        DurusNedeni = kayit.DurusNedeni
                    });
                }
            }

            return yeniKayitlar;
        }
    }
}
