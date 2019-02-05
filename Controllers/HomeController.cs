using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using kimlik.Models;


namespace kimlik.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {

            //ViewBag.Message = "Control Panel Bölümüne Hoşgeldiniz.";

            return View();
        }


        public ActionResult SelectTemplateFirst()
        {
            KimlikDbDataContext db = new KimlikDbDataContext();

            //var selectTasarim = from tasarim in db.KmlikTasarims
            //                    select new
            //                    {
            //                        tasarim.Id,
            //                        tasarim.KimlikAdi
            //                    };


            try
            {
                if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
                {
                    //return View(db.KmlikTasarims.Where(x => x.Id == db.MusteriOnayKimlikTasarims.Single(y => y.MusteriOnayId == Convert.ToInt32(Session["UserId"])).KimlikTasarimId).ToList());
                    return View(db.MusteriOnayKimlikTasarims.Where(x => x.MusteriOnayId == Convert.ToInt32(Session["UserId"])).ToList());
                }
                else {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception e)
            {
                return View();
            }


            
        }


        public ActionResult KimlikTasarimlarim()
        {

            KimlikDbDataContext db = new KimlikDbDataContext();
            try
            {
                if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
                {
                    return View(db.MusteriOnayKimlikTasarims.Where(x => x.MusteriOnayId == Convert.ToInt32(Session["UserId"])).ToList().OrderByDescending(x => x.Id));
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult KimlikTasarla()
        {
            try
            {
                if (String.IsNullOrEmpty(Session["UserId"].ToString()))
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }


        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult KimlikTasarla(FormCollection collection, HttpPostedFileBase ImageLeft, HttpPostedFileBase ImageRight)
        {



            KimlikDbDataContext db = new KimlikDbDataContext();
            KmlikTasarim newKT = new KmlikTasarim();
            newKT.KimlikAdi = collection["KimlikAdi"].ToString();
            newKT.Color = collection["Color"].ToString();
            newKT.ColorPano = collection["ColorPano"].ToString();
            newKT.ColorFont = collection["ColorFont"].ToString();
            newKT.ColorFont2 = collection["ColorFont2"].ToString();
            newKT.ColorFont3 = collection["ColorFont3"].ToString();
            newKT.KimlikBaslik = collection["KimlikBaslik"].ToString();

            try
            {
                if (ImageLeft != null && ImageLeft.ContentLength > 0)
                {
                    newKT.ImageLeft = Utils.ImageUploadAndResize(ImageLeft, 300, 140);
                }
                else
                {
                    newKT.ImageLeft = "";
                }

                if (ImageRight != null && ImageRight.ContentLength > 0)
                {
                    newKT.ImageRight = Utils.ImageUploadAndResize(ImageRight, 300, 140);
                }
                else
                {
                    newKT.ImageRight = "";
                }
            }
            catch (Exception)
            {
                newKT.ImageRight = "";
                newKT.ImageLeft = "";
            }

            



            newKT.Etiket1 = collection["Etiket1"].ToString();
            newKT.Etiket1Eng = collection["Etiket1Eng"].ToString();
            newKT.Etiket2 = collection["Etiket2"].ToString();
            newKT.Etiket2Eng = collection["Etiket2Eng"].ToString();
            newKT.Etiket3 = collection["Etiket3"].ToString();
            newKT.Etiket3Eng = collection["Etiket3Eng"].ToString();
            newKT.Etiket4 = collection["Etiket4"].ToString();
            newKT.Etiket4Eng = collection["Etiket4Eng"].ToString();
            newKT.Etiket5 = collection["Etiket5"].ToString();
            newKT.Etiket5Eng = collection["Etiket5Eng"].ToString();
            newKT.Etiket6 = collection["Etiket6"].ToString();
            newKT.Etiket6Eng = collection["Etiket6Eng"].ToString();
            newKT.Etiket7 = collection["Etiket7"].ToString();
            newKT.Etiket7Eng = collection["Etiket7Eng"].ToString();
            newKT.KimlikAltBaslik = collection["KimlikAltBaslik"].ToString();
            newKT.KimlikOnAraBaslik = collection["KimlikOnAraBaslik"].ToString();

            newKT.KimlikBaslikArka = collection["KimlikBaslikArka"].ToString();
            //newKT.KimlikBaslikArkaEng = collection["KimlikBaslikArkaEng"].ToString();


            newKT.etiketArkaOrtaBaslik = collection["etiketArkaOrtaBaslik"].ToString();
            //newKT.etiketArkaOrtaBaslikEng = collection["etiketArkaOrtaBaslikEng"].ToString();
            newKT.Etiket1Arka = collection["Etiket1Arka"].ToString();
            newKT.Etiket1ArkaEng = collection["Etiket1ArkaEng"].ToString();
            newKT.Etiket2Arka = collection["Etiket2Arka"].ToString();
            newKT.Etiket2ArkaEng = collection["Etiket2ArkaEng"].ToString();
            newKT.Etiket3Arka = collection["Etiket3Arka"].ToString();
            newKT.Etiket3ArkaEng = collection["Etiket3ArkaEng"].ToString();
            newKT.Etiket4Arka = collection["Etiket4Arka"].ToString();
            newKT.Etiket4ArkaEng = collection["Etiket4ArkaEng"].ToString();
            newKT.Etiket5Arka = collection["Etiket5Arka"].ToString();
            newKT.Etiket5ArkaEng = collection["Etiket5ArkaEng"].ToString();
            newKT.Etiket6Arka = collection["Etiket6Arka"].ToString();
            newKT.Etiket6ArkaEng = collection["Etiket6ArkaEng"].ToString();
            newKT.Etiket7Arka = collection["Etiket7Arka"].ToString();
            newKT.Etiket7ArkaEng = collection["Etiket7ArkaEng"].ToString();
            newKT.Etiket8Arka = collection["Etiket8Arka"].ToString();
            newKT.Etiket8ArkaEng = collection["Etiket8ArkaEng"].ToString();
            newKT.Etiket9Arka = collection["Etiket9Arka"].ToString();
            newKT.Etiket9ArkaEng = collection["Etiket9ArkaEng"].ToString();
            newKT.Etiket10Arka = collection["Etiket10Arka"].ToString();
            newKT.Etiket10ArkaEng = collection["Etiket10ArkaEng"].ToString();
            newKT.Etiket11Arka = collection["Etiket11Arka"].ToString();
            newKT.Etiket11ArkaEng = collection["Etiket11ArkaEng"].ToString();
            newKT.Etiket12Arka = collection["Etiket12Arka"].ToString();
            newKT.Etiket12ArkaEng = collection["Etiket12ArkaEng"].ToString();
            //newKT.Etiket13Arka = collection["Etiket13Arka"].ToString();
            //newKT.Etiket13ArkaEng = collection["Etiket13ArkaEng"].ToString();

            db.KmlikTasarims.InsertOnSubmit(newKT);
            db.SubmitChanges();

            MusteriOnayKimlikTasarim newMOK = new MusteriOnayKimlikTasarim();
            newMOK.MusteriOnayId = Convert.ToInt32(Session["UserId"].ToString());
            newMOK.KimlikTasarimId = newKT.Id;
            db.MusteriOnayKimlikTasarims.InsertOnSubmit(newMOK);
            db.SubmitChanges();
            return View("SelectTemplateFirst");




        }





        public ActionResult MainPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MainPage(FormCollection collection, HttpPostedFileBase ImagePath)
        {
            KimlikDbDataContext db = new KimlikDbDataContext();

            //try
            //{
            KisiBilgileri newKisiBilgileri = new KisiBilgileri();

            newKisiBilgileri.MusteriOnayId = Convert.ToInt32(Session["UserId"]);
            newKisiBilgileri.KimlikTasarimId = Convert.ToInt32(collection["KimlikTasarimId"]);
            newKisiBilgileri.TCNo = collection["TCNo"].ToString();
            newKisiBilgileri.Soyadi = collection["Soyadi"].ToString();
            newKisiBilgileri.Adi = collection["Adi"].ToString();
            newKisiBilgileri.BabaAdi = collection["BabaAdi"].ToString();
            newKisiBilgileri.AnaAdi = collection["AnaAdi"].ToString();
            newKisiBilgileri.DogumYeri = collection["DogumYeri"].ToString();
            newKisiBilgileri.DogumTarih = collection["DogumTarih"].ToString();
            newKisiBilgileri.MedeniHali = collection["MedeniHali"].ToString();
            newKisiBilgileri.Din = collection["Din"].ToString();
            newKisiBilgileri.Il = collection["Il"].ToString();
            newKisiBilgileri.Ilce = collection["Ilce"].ToString();
            newKisiBilgileri.Mahalle = collection["Mahalle"].ToString();
            newKisiBilgileri.CiltNo = collection["CiltNo"].ToString();
            newKisiBilgileri.AileSiraNo = collection["AileSiraNo"].ToString();
            newKisiBilgileri.SiraNo = collection["SiraNo"].ToString();
            newKisiBilgileri.VerildigiYer = collection["VerildigiYer"].ToString();
            newKisiBilgileri.VerilisNedeni = collection["VerilisNedeni"].ToString();
            newKisiBilgileri.KanGrubu = "";
            newKisiBilgileri.KayitNo = collection["KayitNo"].ToString();
            newKisiBilgileri.VerilisTarihi = collection["VerilisTarihi"].ToString();
            newKisiBilgileri.ImagePath = Utils.ImageUploadAndResize(ImagePath, 550, 75).ToString();
            db.KisiBilgileris.InsertOnSubmit(newKisiBilgileri);
            db.SubmitChanges();
            //}
            //catch (Exception)
            //{

            //}





            return RedirectToAction("KimlikListeleme", "Home");
        }


        public ActionResult KimlikListeleme()
        {
            KimlikDbDataContext db = new KimlikDbDataContext();
            try
            {
                if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
                {
                    return View(db.KisiBilgileris.Where(x => x.MusteriOnayId == Convert.ToInt32(Session["UserId"])).ToList().OrderByDescending(x => x.Id));
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Home");
            }



        }
        [HttpPost]
        public ActionResult KimlikListelemeAjax(int id) {
            

            KimlikDbDataContext db = new KimlikDbDataContext();

            //try
            //{
                if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
                {
                    return View(db.KisiBilgileris.Where(x => x.MusteriOnayId == Convert.ToInt32(Session["UserId"]) && x.KimlikTasarimId==id).ToList().OrderByDescending(x => x.Id));
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            //}
            //catch (Exception)
            //{
            //   // return RedirectToAction("Login", "Home");
            //    return View();
            //}

            

        }

        public ActionResult Register()
        {

            return View();

        }

        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            KimlikDbDataContext db = new KimlikDbDataContext();

            try
            {
                if (db.Musteris.Where(x => x.EmailAddress.Contains(collection["EmailAddress"].ToString())).Count() > 0)
                {
                    //Session["InfoText"] = "Bu email adresine kayıtlı bir kullanıcı zaten mevcut.";
                    ViewBag.InfoText = "Bu email adresine kayıtlı bir kullanıcı zaten mevcut.";
                    return View();

                }
                else
                {
                    Musteri newMusteri = new Musteri()
                    {
                        SirketAdi = collection["SirketAdi"].ToString(),
                        AdiSoyadi = collection["AdiSoyadi"].ToString(),
                        Telefon = collection["Telefon"].ToString(),
                        EmailAddress = collection["EmailAddress"].ToString(),
                        StartDate = DateTime.Now,
                        IsReaded = false
                    };

                    db.Musteris.InsertOnSubmit(newMusteri);
                    db.SubmitChanges();

                    ViewBag.InfoText = "Kaydınız başarılı bir şekilde tamamlanmıştır. Lütfen, belirttiğiniz email adresine onay email'inin gelmesini bekleyiniz.";
                    //Session["InfoText"] = "Kaydınız başarılı bir şekilde tamamlanmıştır. Lütfen, belirttiğiniz email adresine onay email'inin gelmesini bekleyiniz.";
                    Utils.SendMailTo("Sisteme Kayıt İsteği", "Sisteme" + newMusteri.AdiSoyadi + " - " + newMusteri.SirketAdi + " ismiyle kayıt olmuştur. Lütfen onaylama işlemini yapınız." + "http://kimlikyazdir-admin.technotechbilgisayar.com" + "<br/><br/>", "saygin@technotechbilgisayar.com", "587", "saygin@technotechbilgisayar.com", "mail.technotechbilgisayar.com", "Ssercin01");


                    return View();

                }
            }
            catch (Exception e)
            {
                ViewBag.InfoText = "Bir hata meydana geldi. Lütfen, tekrardan deneyiniz." + e.Message;
                //Session["InfoText"] = "Bir hata meydana geldi. Lütfen, tekrardan deneyiniz." + e.Message;
                return View();

            }



        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            KimlikDbDataContext db = new KimlikDbDataContext();

            if (db.MusteriOnays.Where(x => x.UserName == collection["UserName"].ToString() && x.Pass == collection["Pass"].ToString() && x.IsConfirm == true).Count() > 0)
            {
                Session["UserId"] = db.MusteriOnays.Single(x => x.UserName == collection["UserName"].ToString() && x.Pass == collection["Pass"].ToString() && x.IsConfirm == true).Id;

                Session.Timeout = 600;

                if (db.MusteriOnayKimlikTasarims.Count(x => x.MusteriOnayId == Convert.ToInt32(Session["UserId"])) > 0)
                {
                    return RedirectToAction("SelectTemplateFirst", "Home");
                }
                else
                {
                    return RedirectToAction("KimlikTasarla", "Home");
                }

            }
            else
            {
                return View();
            }
        }

        public ActionResult PrintPage(int id)
        {
            KimlikDbDataContext db = new KimlikDbDataContext();
            try
            {
                if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
                {
                    return View(db.KisiBilgileris.Single(x => x.Id == id));
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Home");
            }

        }

        public void SaveImage()
        {

        }

        public ActionResult About()
        {
            KimlikDbDataContext db = new KimlikDbDataContext();
            return View(db.KisiBilgileris.ToList());
        }

        public ActionResult KimlikDuzenleme(int id)
        {
            KimlikDbDataContext db = new KimlikDbDataContext();
            return View(db.KisiBilgileris.Single(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult KimlikDuzenleme(int id, FormCollection collection, HttpPostedFileBase ImagePath)
        {
            KimlikDbDataContext db = new KimlikDbDataContext();

            //try
            //{
            if (!String.IsNullOrEmpty(Session["UserId"].ToString()))
            {
                KisiBilgileri selected = db.KisiBilgileris.Single(x => x.Id == id);
                selected.MusteriOnayId = Convert.ToInt32(Session["UserId"]);
                selected.TCNo = collection["TCNo"].ToString();
                selected.Soyadi = collection["Soyadi"].ToString();
                selected.Adi = collection["Adi"].ToString();
                selected.BabaAdi = collection["BabaAdi"].ToString();
                selected.AnaAdi = collection["AnaAdi"].ToString();
                selected.DogumYeri = collection["DogumYeri"].ToString();
                selected.DogumTarih = collection["DogumTarih"].ToString();
                selected.MedeniHali = collection["MedeniHali"].ToString();
                selected.Din = collection["Din"].ToString();
                selected.Il = collection["Il"].ToString();
                selected.Ilce = collection["Ilce"].ToString();
                selected.Mahalle = collection["Mahalle"].ToString();
                selected.CiltNo = collection["CiltNo"].ToString();
                selected.AileSiraNo = collection["AileSiraNo"].ToString();
                selected.SiraNo = collection["SiraNo"].ToString();
                selected.VerildigiYer = collection["VerildigiYer"].ToString();
                selected.VerilisNedeni = collection["VerilisNedeni"].ToString();
                //selected.KanGrubu = collection["KanGrubu"].ToString();
                selected.KayitNo = collection["KayitNo"].ToString();
                selected.VerilisTarihi = collection["VerilisTarihi"].ToString();

                if (ImagePath != null && ImagePath.ContentLength > 0)
                {
                    selected.ImagePath = Utils.ImageUploadAndResize(ImagePath, 550, 75).ToString();
                }
                else
                {
                    selected.ImagePath = selected.ImagePath;
                }


                db.SubmitChanges();

                return RedirectToAction("KimlikListeleme", "Home");
            }
            else
            {

                return RedirectToAction("Login", "Home");
            }
            //}
            //catch (Exception)
            //{
            //    return RedirectToAction("Login", "Home");
            //}


        }


        public ActionResult KimlikTasarimEdit(int id)
        {

            KimlikDbDataContext db = new KimlikDbDataContext();

            return View(db.KmlikTasarims.Single(x => x.Id == id));

        }

        [HttpPost]
        public ActionResult KimlikTasarimEdit(int id, FormCollection collection, HttpPostedFileBase ImageLeft, HttpPostedFileBase ImageRight)
        {

            KimlikDbDataContext db = new KimlikDbDataContext();


            KmlikTasarim newKT = db.KmlikTasarims.Single(x => x.Id == id);

            newKT.KimlikAdi = collection["KimlikAdi"].ToString();
            newKT.Color = collection["Color"].ToString();
            newKT.ColorPano = collection["ColorPano"].ToString();
            newKT.ColorFont = collection["ColorFont"].ToString();
            newKT.KimlikBaslik = collection["KimlikBaslik"].ToString();
            try
            {
                if (ImageLeft.ContentLength > 0 && ImageLeft != null)
                {
                    newKT.ImageLeft = Utils.ImageUploadAndResize(ImageLeft, 200, 42);
                }
                else
                {
                    newKT.ImageLeft = newKT.ImageLeft;
                }
                if (ImageRight.ContentLength > 0 && ImageRight != null)
                {
                    newKT.ImageRight = Utils.ImageUploadAndResize(ImageRight, 200, 42);
                }
                else
                {
                    newKT.ImageRight = newKT.ImageRight;
                }
            }
            catch (Exception)
            {
                newKT.ImageLeft = newKT.ImageLeft;
                newKT.ImageRight = newKT.ImageRight;
            }



            newKT.Etiket1 = collection["Etiket1"].ToString();
            newKT.Etiket2 = collection["Etiket2"].ToString();
            newKT.Etiket3 = collection["Etiket3"].ToString();
            newKT.Etiket4 = collection["Etiket4"].ToString();
            newKT.Etiket5 = collection["Etiket5"].ToString();
            newKT.Etiket6 = collection["Etiket6"].ToString();
            newKT.Etiket7 = collection["Etiket7"].ToString();
            newKT.KimlikBaslikArka = collection["KimlikBaslikArka"].ToString();
            newKT.etiketArkaOrtaBaslik = collection["etiketArkaOrtaBaslik"].ToString();
            newKT.Etiket1Arka = collection["Etiket1Arka"].ToString();
            newKT.Etiket2Arka = collection["Etiket2Arka"].ToString();
            newKT.Etiket3Arka = collection["Etiket3Arka"].ToString();
            newKT.Etiket4Arka = collection["Etiket4Arka"].ToString();
            newKT.Etiket5Arka = collection["Etiket5Arka"].ToString();
            newKT.Etiket6Arka = collection["Etiket6Arka"].ToString();
            newKT.Etiket7Arka = collection["Etiket7Arka"].ToString();
            newKT.Etiket8Arka = collection["Etiket8Arka"].ToString();
            newKT.Etiket9Arka = collection["Etiket9Arka"].ToString();
            newKT.Etiket10Arka = collection["Etiket10Arka"].ToString();
            newKT.Etiket11Arka = collection["Etiket11Arka"].ToString();

            db.SubmitChanges();

            MusteriOnayKimlikTasarim newMOK = db.MusteriOnayKimlikTasarims.Single(x => x.MusteriOnayId == Convert.ToInt32(Session["UserId"]));

            newMOK.MusteriOnayId = Convert.ToInt32(Session["UserId"].ToString());
            newMOK.KimlikTasarimId = newKT.Id;

            db.SubmitChanges();

            return View("MainPage");


        }


    }
}
