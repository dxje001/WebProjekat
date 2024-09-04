using Olaksani.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Olaksani.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login()
        {

            

            //premestiti u global, rad sa fajlovima
            //problem kod povratka??

            List<Administrator> admini = (List<Administrator>)HttpContext.Application["admini"];
            List<Termin> termini = (List<Termin>)HttpContext.Application["termini"];
            List<Lekar> lekari = (List<Lekar>)HttpContext.Application["lekari"];
            List<ZdravstveniKarton> zdravstveniKartoni = (List<ZdravstveniKarton>)HttpContext.Application["zdravstveniKartoni"];
            List<Pacijent> pacijenti = (List<Pacijent>)HttpContext.Application["pacijenti"];
            
           

            var korisnickoIme = Request["korisnickoIme"] != null ? Request["korisnickoIme"] : String.Empty;
            var sifra = Request["sifra"] != null ? Request["sifra"] : String.Empty;

            string baseDirectory = @"C:\Users\Dusan\Downloads\WebOlaksani-master (2)\WebOlaksani-master\Olaksani";
            string relativePath = @"Datoteka\ZdravstveniKartoni.csv";
            string absolutePath = Path.Combine(baseDirectory, relativePath);
            foreach (ZdravstveniKarton p in zdravstveniKartoni)
                SaveZdravstveniKartonToCsv(p, absolutePath);


            string baseDirectory1 = @"C:\Users\Dusan\Downloads\WebOlaksani-master (2)\WebOlaksani-master\Olaksani";
            string relativePath1 = @"Datoteka\Lekari.csv";
            string absolutePath1 = Path.Combine(baseDirectory1, relativePath1);
            foreach (Lekar p in lekari)
                SaveLekarToCsv(p, absolutePath1);

            foreach (var s in pacijenti)
            {
                if (s.Sifra == sifra && s.KorisnickoIme == korisnickoIme)
                {
                    foreach (Lekar l in lekari)
                    {
                        foreach(Terapija t in s.Terapijas)
                        {
                        
                            if(l.KorisnickoIme == t.Lekar.KorisnickoIme)
                            {

                                foreach (Termin te in l.SlobodniTermini)
                                {
                                    if (!s.ZakaziTermin.Contains(te))
                                    {
                                        s.ZakaziTermin.Add(te);

                                    }
                                }
        
                            }

                        }

                    }

                    return View("Pacijent", s);
                }

            }

            foreach (var s in lekari)
            {
                if (s.Sifra == sifra && s.KorisnickoIme == korisnickoIme)
                {
                   
                    return View("Lekar", s);
                }

            }

            foreach (var s in admini)
            {
                if (s.Sifra == sifra && s.KorisnickoIme == korisnickoIme)
                {
                    return View("Administrator", admini);
                }

            }

            return View("PogresanLogin");

        }

        

        [HttpPost]
        public ActionResult StudentPrijava()
        {
            //kako ustvari podesiti polje tog prijavljenog ispita

            List<Termin> ispiti = (List<Termin>)HttpContext.Application["termini"];
            List<Lekar> profesori = (List<Lekar>)HttpContext.Application["lekari"];
            List<Pacijent> studenti = (List<Pacijent>)HttpContext.Application["pacijenti"];

            var splitovanje = Request["tip"] != null ? Request["tip"] : String.Empty;
            string[] niz = splitovanje.Split(',');

            var username = Request["username"] != null ? Request["username"] : String.Empty;

            var vreme = Request["vremeOdrzavanja"] != null ? Request["vremeOdrzavanja"] : String.Empty;

            DateTime datum = DateTime.Parse(vreme);

        

            foreach (Pacijent stud in studenti)
            {
                if (stud.KorisnickoIme == username)
                {

                    foreach (Termin t in stud.ZakaziTermin)
                    {

                        if (datum == t.VremeOdrzavanja)
                        {
                            t.Lekar.SlobodniTermini.Remove(t);
                            t.StatusTermina = Status.Zauzet;

                            t.Lekar.ZakazaniTermini.Add(t);
                            stud.ZakazaniTermini.Add(t);
                            stud.ZakaziTermin.Remove(t);
                            break;
                        }
                    }


                    return View("PacijentLista", stud.ZakazaniTermini);
                }

            }
            return View("~/Views/Home/Index.cshtml");
        }


        [HttpPost]
        public ActionResult DodajIspit(string stanjeTermina, DateTime datum, string opisTerapije)
        {
            List<Termin> ispiti = (List<Termin>)HttpContext.Application["termini"];
            List<Lekar> profesori = (List<Lekar>)HttpContext.Application["lekari"];
            var username = Request["username2"] != null ? Request["username2"] : String.Empty;
            var time = Request["appt-time"] != null ? Request["appt-time"] : String.Empty;
            DateTime time2 = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);

            DateTime datum2 = datum.Date.Add(time2.TimeOfDay);




            foreach (Lekar p in profesori)
            {
               

                if (p.KorisnickoIme == username)
                {
                    Olaksani.Models.Status novo;
                    if (stanjeTermina == "Slobodan")
                    {
                        novo = Status.Slobodan;
                    }
                    else
                    {
                        novo = Status.Zauzet;
                    }

                    Termin n = new Termin
                    {
                        Lekar = p,
                        StatusTermina = novo,
                        VremeOdrzavanja = datum2,
                        OpisTerapije = opisTerapije
                        
                    };
                    if (novo == Status.Slobodan)
                    {
                        p.SlobodniTermini.Add(n);
                    }
                    else
                    {
                        p.ZakazaniTermini.Add(n);

                    }
                    ispiti.Add(n);

                    string baseDirectory = @"C:\Users\Dusan\Downloads\WebOlaksani-master (2)\WebOlaksani-master\Olaksani";
                    string relativePath = @"Datoteka\Termini.csv";
                    string absolutePath = Path.Combine(baseDirectory, relativePath);
                    SaveTerminToCsv(ispiti, absolutePath);

                    List<Termin> ukupno = new List<Termin>();
                    foreach (Termin t in p.ZakazaniTermini)
                    {
                        ukupno.Add(t);
                    }
                    foreach (Termin t in p.SlobodniTermini)
                    {
                        ukupno.Add(t);
                    }

                    return View("LekarLista", ukupno);
                }
            }

            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public ActionResult DodajTerapiju(string nazivTerapije, string opisTerapije)
        {
            List<Terapija> terapijas = (List<Terapija>)HttpContext.Application["terapije"];
            List<Lekar> profesori = (List<Lekar>)HttpContext.Application["lekari"];
            List<Pacijent> pacijentinakvadrat = (List<Pacijent>)HttpContext.Application["pacijenti"];

            var username = Request["username2"] != null ? Request["username2"] : String.Empty;
            //var pacijentiIdjs = Request["SelectedPatientIds"] != null ? Request["SelectedPatientIds"] : String.Empty;
            var korime = Request["korime"] != null ? Request["korime"] : String.Empty;

            

            

            foreach (Lekar p in profesori)
            {


                if (p.KorisnickoIme == username )
                {

                    foreach (Terapija t4 in p.PrepisaneTerapije)
                    {
                        if(t4.Pacijent.KorisnickoIme == korime)
                        {
                        Terapija n = new Terapija
                        {
                            Lekar = p,
                            Pacijent = t4.Pacijent,
                            Naziv = nazivTerapije,
                            Opis = opisTerapije

                        };
                        t4.Pacijent.Terapijas.Add(n);
                        p.PrepisaneTerapije.Add(n);
                        terapijas.Add(n);
                            break;

                        }
                    }

                    string baseDirectory = @"C:\Users\Dusan\Downloads\WebOlaksani-master (2)\WebOlaksani-master\Olaksani";
                    string relativePath = @"Datoteka\Terapije.csv";
                    string absolutePath = Path.Combine(baseDirectory, relativePath);
                    SaveTerapijaToCsv(terapijas, absolutePath);

                    

                    return View("LekarLista2", p.PrepisaneTerapije);
                }
            }

            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public ActionResult PregledIspitaStudent()
        {
            List<Pacijent> studenti = (List<Pacijent>)HttpContext.Application["pacijenti"];
            List<Lekar> profesori = (List<Lekar>)HttpContext.Application["lekari"];


            var username = Request["username1"] != null ? Request["username1"] : String.Empty;

            foreach (Pacijent p in studenti)
            {
                if (p.KorisnickoIme == username)
                {

                    List<Termin> ukupno = new List<Termin>();

                    foreach (Lekar l in profesori)
                    {
                        foreach (Termin t in l.SlobodniTermini)
                        {
                            ukupno.Add(t);
                        }


                    }
                    return View("PacijentLista", ukupno);
                }
            }

            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public ActionResult PregledTerapijaStudent()
        {
            List<Pacijent> studenti = (List<Pacijent>)HttpContext.Application["pacijenti"];
            List<Lekar> profesori = (List<Lekar>)HttpContext.Application["lekari"];


            var username = Request["username1"] != null ? Request["username1"] : String.Empty;

            foreach (Pacijent p in studenti)
            {
                if (p.KorisnickoIme == username)
                {

                   
                    return View("PacijentLista2", p.Terapijas);
                }
            }

            return View("~/Views/Home/Index.cshtml");
        }

        [HttpPost]
        public ActionResult PregledTerapija()
        {
            List<Lekar> profesori = (List<Lekar>)HttpContext.Application["lekari"];

            var username = Request["username"] != null ? Request["username"] : String.Empty;

            foreach (Lekar p in profesori)
            {
                if (p.KorisnickoIme == username)
                {
                    
                    return View("LekarLista2", p.PrepisaneTerapije);
                }
            }

            return View("~/Views/Home/Index.cshtml");

        }

        [HttpPost]
        public ActionResult PregledIspita()
        {
            List<Pacijent> studenti = (List<Pacijent>)HttpContext.Application["pacijenti"];
            List<Lekar> profesori = (List<Lekar>)HttpContext.Application["lekari"];

            var username = Request["username"] != null ? Request["username"] : String.Empty;

           

            foreach (Lekar p in profesori)
            {

                if (p.KorisnickoIme == username)
                {
                    List<Termin> ukupno = new List<Termin>();
                    foreach (Termin t in p.ZakazaniTermini)
                    {
                        if (!ukupno.Contains(t) && t.Lekar.KorisnickoIme == username)
                        {
                            ukupno.Add(t);
                        }
                    }
                    foreach (Termin t in p.SlobodniTermini)
                    {
                        if (!ukupno.Contains(t) && t.Lekar.KorisnickoIme == username)
                        {
                            ukupno.Add(t);
                        }
                    }
                    return View("LekarLista", ukupno);
                }
            }

            return View("~/Views/Home/Index.cshtml");

        }

        [HttpPost]
        public ActionResult Pregled()
        {

            List<Pacijent> studenti = (List<Pacijent>)HttpContext.Application["pacijenti"];
            return View("AdminLista", studenti);
        }

        [HttpPost]
        public ActionResult Obrisi()
        {

            var del = Request["del"] != null ? Request["del"] : String.Empty;
            List<Pacijent> studenti = (List<Pacijent>)HttpContext.Application["pacijenti"];
            bool obrisan = false;

            for (int i = 0; i < studenti.Count;)
            {

                if (studenti[i].KorisnickoIme.ToUpper().Equals(del.ToUpper()))
                {
                    studenti.RemoveAt(i);
                    i = 0;
                    obrisan = true;
                    continue;
                }
                i++;

            }

            if (obrisan)
            {
                return View("AdminLista", studenti);
            }
            else
            {
                return View("PogresanAdmin");

            }
        }

        [HttpPost]
        public ActionResult DodajStudenta(String korisnickoime, string jmbg, string sifra, string ime, string prezime, DateTime datum, string elektronskaPosta)
        {
            List<Pacijent> studenti = (List<Pacijent>)HttpContext.Application["pacijenti"];

            List<Termin> ispitiN = new List<Termin>();
            List<Terapija> te = new List<Terapija>();

            Pacijent s = new Pacijent(korisnickoime, jmbg, sifra, ime, prezime, datum, elektronskaPosta, ispitiN,te, ispitiN);

            foreach (Pacijent l in studenti)
            {

                if (l.Jmbg == jmbg || l.ElektronskaPosta.ToUpper() == elektronskaPosta.ToUpper() || l.KorisnickoIme.ToUpper() == korisnickoime.ToUpper())
                {
                    return View("PogresanAdmin");

                }


            }

            studenti.Add(s);

            string baseDirectory = @"C:\Users\Dusan\Downloads\WebOlaksani-master (2)\WebOlaksani-master\Olaksani";
            string relativePath = @"Datoteka\Pacijenti.csv";
            string absolutePath = Path.Combine(baseDirectory, relativePath);
            foreach (Pacijent p in studenti)
                SavePacijentToCsv(s, absolutePath);

            return View("AdminLista", studenti);

        }

        [HttpPost]
        public ActionResult AzurirajStudenta(String korisnickoime, string sifra, string ime, string prezime, DateTime datum, string elektronskaPosta)
        {
            List<Pacijent> studenti = (List<Pacijent>)HttpContext.Application["pacijenti"];

            List<Termin> ispitiN = new List<Termin>();

            foreach (Pacijent l in studenti)
            {
                if (l.ElektronskaPosta.ToUpper() == elektronskaPosta.ToUpper())
                {
                    return View("PogresanAdmin");

                }
            }

            bool found = false;
            foreach (Pacijent l in studenti)
            {
                if (l.KorisnickoIme.ToUpper() == korisnickoime.ToUpper())
                {
                    found = true;
                    l.Sifra = sifra;
                    l.Ime = ime;
                    l.Prezime = prezime;
                    l.DatumRodjenja = datum;
                    l.ElektronskaPosta = elektronskaPosta;
                }
            }

            if (found == false)
            {
                return View("PogresanAdmin");
            }

            //studenti.Add();
            return View("AdminLista", studenti);

        }

        public void SaveTerminToCsv(List<Termin> termins, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the CSV header
                writer.WriteLine("Lekar,StatusTermina,VremeOdrzavanja,opisTerapije");

                // Write each Ispit object as a CSV row
                foreach (Termin ispit in termins)
                {
                    string csvRow = $"{ispit.Lekar.Ime},{ispit.StatusTermina},{ispit.VremeOdrzavanja},{ispit.OpisTerapije}";
                    writer.WriteLine(csvRow);
                }
            }
        }

        public void SaveTerapijaToCsv(List<Terapija> termins, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the CSV header
                writer.WriteLine("Lekar,Pacijent,Naziv,opisTerapije");

                // Write each Ispit object as a CSV row
                foreach (Terapija ispit in termins)
                {
                    string csvRow = $"{ispit.Lekar.Ime},{ispit.Pacijent.Ime},{ispit.Naziv},{ispit.Opis}";
                    writer.WriteLine(csvRow);
                }
            }
        }


        public void SaveLekarToCsv(Lekar profesor, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the CSV header
                writer.WriteLine("KorisnickoIme,Sifra,Ime,Prezime,DatumRodjenja,ElektronskaPosta");

                // Write the Profesor object as a CSV row
                string csvRow = $"{profesor.KorisnickoIme},{profesor.Sifra},{profesor.Ime},{profesor.Prezime},{profesor.DatumRodjenja},{profesor.ElektronskaPosta}";
                writer.WriteLine(csvRow);



                // Write the IspitiPredmeta
                writer.WriteLine("ZakazaniTermini");
                foreach (Termin ispit in profesor.ZakazaniTermini)
                {
                    string ispitCsvRow = $"{ispit.Lekar.Ime},{ispit.StatusTermina} , {ispit.VremeOdrzavanja} , {ispit.OpisTerapije}";
                    writer.WriteLine(ispitCsvRow);
                }

                // Write the IspitiPredmeta
                writer.WriteLine("SlobodniTermini");
                foreach (Termin ispit in profesor.SlobodniTermini)
                {
                    string ispitCsvRow = $"{ispit.Lekar.Ime},{ispit.StatusTermina} , {ispit.VremeOdrzavanja} , {ispit.OpisTerapije}";
                    writer.WriteLine(ispitCsvRow);
                }
            }
        }

        public void SavePacijentToCsv(Pacijent student, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the CSV header
                writer.WriteLine("KorisnickoIme,BrojIndeksa,Sifra,Ime,Prezime,DatumRodjenja,ElektronskaPosta");

                // Write the Student object as a CSV row
                string csvRow = $"{student.KorisnickoIme},{student.Jmbg},{student.Sifra},{student.Ime},{student.Prezime},{student.DatumRodjenja},{student.ElektronskaPosta}";
                writer.WriteLine(csvRow);

                // Write the Ispiti
                writer.WriteLine("ZakazaniTermini");
                foreach (Termin ispit in student.ZakazaniTermini)
                {
                    string ispitCsvRow = $"{ispit.Lekar.Ime},{ispit.StatusTermina} , {ispit.VremeOdrzavanja} , {ispit.OpisTerapije}";
                    writer.WriteLine(ispitCsvRow);
                }

                // Write the Ispiti
                writer.WriteLine("PrepisaneTerapije");
                foreach (Terapija ispit in student.Terapijas)
                {
                    string ispitCsvRow = $"{ispit.Lekar.Ime},{ispit.Opis}";
                    writer.WriteLine(ispitCsvRow);
                }
            }
        }

        public void SaveZdravstveniKartonToCsv(ZdravstveniKarton rezultat, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Write the CSV header
                writer.WriteLine("KorisnickoIme, Jmbg, Ime, Prezime, DatumRodjenja, ElektronskaPosta");
                string csvRow = $"{rezultat.Pacijent.KorisnickoIme},{rezultat.Pacijent.Jmbg},{rezultat.Pacijent.Ime},{rezultat.Pacijent.Prezime},{rezultat.Pacijent.DatumRodjenja},{rezultat.Pacijent.ElektronskaPosta}";
                writer.WriteLine(csvRow);


                writer.WriteLine("Termini");
                foreach (Termin ispit in rezultat.Termini)
                {
                    string ispitCsvRow = $"{ispit.Lekar.Ime},{ispit.StatusTermina} , {ispit.VremeOdrzavanja} , {ispit.OpisTerapije}";
                    writer.WriteLine(ispitCsvRow);
                }

               

            }
        }
    }
}