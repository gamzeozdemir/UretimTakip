using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using UretimTakip.Models;

namespace UretimTakip.Controllers
{
    public class UretimController : Controller
    {
        private static List<UretimKaydi> uretimKayitlari = new List<UretimKaydi>();
        private static readonly List<MolaBilgisi> standartDuruslar = new List<MolaBilgisi>
        {
            new MolaBilgisi { Baslangic = new TimeSpan(10, 0, 0), Bitis = new TimeSpan(10, 15, 0), MolaAdi = "Çay Molası" },
            new MolaBilgisi { Baslangic = new TimeSpan(12, 0, 0), Bitis = new TimeSpan(12, 30, 0), MolaAdi = "Yemek Molası" },
            new MolaBilgisi { Baslangic = new TimeSpan(15, 0, 0), Bitis = new TimeSpan(15, 15, 0), MolaAdi = "Çay Molası" }
        };

        public IActionResult Index()
        {
            var model = new UretimViewModel
            {
                UretimKayitlari = uretimKayitlari,
                StandartDuruslar = standartDuruslar,
                IslenmisKayitlar = new List<UretimKaydi>()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult YeniKayitEkle(UretimKaydi yeniKayit)
        {
            if (yeniKayit != null)
            {
                yeniKayit.KayitNo = uretimKayitlari.Count + 1;
                uretimKayitlari.Add(yeniKayit);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Hesapla()
        {
            var islenmisKayitlar = new List<UretimKaydi>();

            foreach (var kayit in uretimKayitlari)
            {
                DateTime baslangic = kayit.Baslangic;
                DateTime bitis = kayit.Bitis;

                while (baslangic < bitis)
                {
                    var mola = standartDuruslar.FirstOrDefault(m =>
                        baslangic.TimeOfDay < m.Bitis && bitis.TimeOfDay > m.Baslangic);

                    if (mola != null)
                    {
                        if (baslangic.TimeOfDay < mola.Baslangic)
                        {
                            islenmisKayitlar.Add(new UretimKaydi
                            {
                                KayitNo = kayit.KayitNo,
                                Baslangic = baslangic,
                                Bitis = baslangic.Date + mola.Baslangic,
                                Statu = "URETIM"
                            });
                        }

                        islenmisKayitlar.Add(new UretimKaydi
                        {
                            KayitNo = kayit.KayitNo,
                            Baslangic = baslangic.Date + mola.Baslangic,
                            Bitis = baslangic.Date + mola.Bitis,
                            Statu = "DURUŞ",
                            DurusNedeni = mola.MolaAdi
                        });

                        baslangic = baslangic.Date + mola.Bitis;
                    }
                    else
                    {
                        islenmisKayitlar.Add(new UretimKaydi
                        {
                            KayitNo = kayit.KayitNo,
                            Baslangic = baslangic,
                            Bitis = bitis,
                            Statu = "URETIM"
                        });
                        break;
                    }
                }
            }

            var model = new UretimViewModel
            {
                UretimKayitlari = uretimKayitlari,
                StandartDuruslar = standartDuruslar,
                IslenmisKayitlar = islenmisKayitlar
            };

            return View("Index", model);
        }
    }
}
