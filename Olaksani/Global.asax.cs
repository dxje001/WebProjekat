using Olaksani.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Olaksani
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            HttpContext.Current.Application["admini"] = new List<Administrator>();
            HttpContext.Current.Application["termini"] = new List<Termin>();
            HttpContext.Current.Application["lekari"] = new List<Lekar>();
            HttpContext.Current.Application["zdravstveniKartoni"] = new List<ZdravstveniKarton>();
            HttpContext.Current.Application["pacijenti"] = new List<Pacijent>();
            HttpContext.Current.Application["terapije"] = new List<Terapija>();




            List<Administrator> admini = (List<Administrator>)HttpContext.Current.Application["admini"];
            List<Termin> ispiti = (List<Termin>)HttpContext.Current.Application["termini"];
            List<Lekar> profesori = (List<Lekar>)HttpContext.Current.Application["lekari"];
            List<ZdravstveniKarton> rezultati = (List<ZdravstveniKarton>)HttpContext.Current.Application["zdravstveniKartoni"];
            List<Pacijent> studenti = (List<Pacijent>)HttpContext.Current.Application["pacijenti"];
            List<Terapija> terapijas = (List<Terapija>)HttpContext.Current.Application["terapije"];





            List<Termin> zakTer = new List<Termin>();
            List<Termin> sloTer = new List<Termin>();
            List<Termin> zakazaniTermin1 = new List<Termin>();
            List<Termin> zakazaniTermin2 = new List<Termin>();
            List<Termin> zakazaniTermin = new List<Termin>();
            List<Terapija> ter2 = new List<Terapija>();
            List<Terapija> ter1 = new List<Terapija>();





            


            profesori.Add(new Lekar
            {
                KorisnickoIme = "lekar1",
                Sifra = "lekar1",
                Ime = "Dragan",
                Prezime = "Stojkovic",
                DatumRodjenja = DateTime.Parse("1969-10-04"),
                ElektronskaPosta = "dragan.stojkovic@gmail.com",
                
            });


            profesori.Add(new Lekar
            {
                KorisnickoIme = "lekar2",
                Sifra = "lekar2",
                Ime = "Daca",
                Prezime = "Peric",
                DatumRodjenja = DateTime.Parse("1969-10-04"),
                ElektronskaPosta = "daca.peric@gmail.com",
                
            });



            Termin n = new Termin
            {
                Lekar = profesori[0],
                StatusTermina = Status.Slobodan,
                VremeOdrzavanja = DateTime.Parse("2031-12-18 07:21:40"),
                OpisTerapije = "Terapija1",
            };

            Termin n1 = new Termin
            {
                Lekar = profesori[0],
                StatusTermina = Status.Slobodan,
                VremeOdrzavanja = DateTime.Parse("2031-12-18 13:42:25"),
                OpisTerapije = "Terapija2",
            };

            Termin n2 = new Termin
            {
                Lekar = profesori[1],
                StatusTermina = Status.Slobodan,
                VremeOdrzavanja = DateTime.Parse("2031-12-18 17:31:45"),
                OpisTerapije = "Terapija3",
            };

            Termin n3 = new Termin
            {
                Lekar = profesori[1],
                StatusTermina = Status.Slobodan,
                VremeOdrzavanja = DateTime.Parse("2031-12-18 20:36:15"),
                OpisTerapije = "Terapija4",
            };

            Termin z2 = new Termin
            {
                Lekar = profesori[0],
                StatusTermina = Status.Zauzet,
                VremeOdrzavanja = DateTime.Parse("2031-12-18 22:26:15"),
                OpisTerapije = "Vazduh",
            };

            Termin z3 = new Termin
            {
                Lekar = profesori[1],
                StatusTermina = Status.Zauzet,
                VremeOdrzavanja = DateTime.Parse("2031-12-18 21:16:15"),
                OpisTerapije = "Vatra",
            };

            ispiti.Add(n);
            ispiti.Add(n1);
            ispiti.Add(n2);
            ispiti.Add(n3);
            ispiti.Add(z2);
            ispiti.Add(z3);
            
            foreach(Termin ts in ispiti)
            {
                if (ts.Lekar.KorisnickoIme.Equals("lekar1") && ts.StatusTermina.Equals(Status.Slobodan))
                {
                    profesori[0].SlobodniTermini.Add(ts);
                }
                else if (ts.Lekar.KorisnickoIme.Equals("lekar1") && ts.StatusTermina.Equals(Status.Zauzet))
                {
                    profesori[0].ZakazaniTermini.Add(ts);
                }

                else if (ts.Lekar.KorisnickoIme.Equals("lekar2") && ts.StatusTermina.Equals(Status.Slobodan))
                {
                    profesori[1].SlobodniTermini.Add(ts);
                }

                else if (ts.Lekar.KorisnickoIme.Equals("lekar2") && ts.StatusTermina.Equals(Status.Zauzet))
                {
                    profesori[1].ZakazaniTermini.Add(ts);
                }
            }


            zakazaniTermin1.Add(z2);

            zakazaniTermin2.Add(z3);

            studenti.Add(new Pacijent
            {
                KorisnickoIme = "pacijent1",
                Jmbg = "1234567812344",
                Sifra = "28814",
                Ime = "Marko",
                Prezime = "Plazarevic",
                DatumRodjenja = DateTime.Parse("2001-10-18"),
                ElektronskaPosta = "markoplezar@gmail.com",
                ZakazaniTermini = zakazaniTermin1,
            }) ;

            studenti.Add(new Pacijent
            {
                KorisnickoIme = "pacijent2",
                Jmbg = "1234567812343",
                Sifra = "28814",
                Ime = "Jarko",
                Prezime = "Plazarevic",
                DatumRodjenja = DateTime.Parse("2002-10-18"),
                ElektronskaPosta = "jarkoplezar@gmail.com",
                ZakazaniTermini = zakazaniTermin,
            });

            studenti.Add(new Pacijent
            {
                KorisnickoIme = "pacijent3",
                Jmbg = "1234567812342",
                Sifra = "28814",
                Ime = "Zarko",
                Prezime = "Plazarevic",
                DatumRodjenja = DateTime.Parse("2001-10-18"),
                ElektronskaPosta = "zarkoplezar@gmail.com",
                ZakazaniTermini = zakazaniTermin2,
            });

            studenti.Add(new Pacijent
            {
                KorisnickoIme = "pacijent4",
                Jmbg = "1234567812341",
                Sifra = "28814",
                Ime = "Darko",
                Prezime = "Plazarevic",
                DatumRodjenja = DateTime.Parse("2001-10-18"),
                ElektronskaPosta = "darkoplezar@gmail.com",
                ZakazaniTermini = zakazaniTermin,
            });

            
            Terapija t = new Terapija
            {
                Lekar = profesori[0],
                Pacijent = studenti[0],
                Opis = "Madagaskarsa vodica",
                Naziv = "Voda",
            };


            Terapija t1 = new Terapija
            {
                Lekar = profesori[0],
                Pacijent = studenti[1],
                Opis = "Madagaskarsa vatra",
                Naziv = "Vatra",
            };

            Terapija t2 = new Terapija
            {
                Lekar = profesori[1],
                Pacijent = studenti[2],
                Opis = "Madagaskarsa zemljica",
                Naziv = "Zemlja",
            };

            Terapija t3 = new Terapija
            {
                Lekar = profesori[1],
                Pacijent = studenti[2],
                Opis = "Madagaskarsi vazduh",
                Naziv = "Vazduh",
            };


            terapijas.Add(t);
            terapijas.Add(t1);
            terapijas.Add(t2);
            terapijas.Add(t3);

            foreach (Terapija tss in terapijas)
            {
                for (int i = 0; i < studenti.Count; i++)
                {
                    if (tss.Pacijent.KorisnickoIme == studenti[i].KorisnickoIme)
                    {
                        studenti[i].Terapijas.Add(tss);
                    }
                }
                for (int i = 0; i < profesori.Count; i++)
                {
                    if (tss.Lekar.KorisnickoIme == profesori[i].KorisnickoIme)
                    {
                        profesori[i].PrepisaneTerapije.Add(tss);
                    }
                }

            }


            foreach ( Pacijent p in studenti)
            {

                ZdravstveniKarton ri = new ZdravstveniKarton
                {
                Pacijent = p,
                Termini = p.ZakazaniTermini,
                };

                rezultati.Add(ri);
            }

/*
            admini.Add(new Administrator
            {
                KorisnickoIme = "admin1",
                Sifra = "admin1",
                Ime = "Dusan",
                Prezime = "Markovic",
                DatumRodjenja = DateTime.Parse("2001-11-04")
            });
*/



            
            string baseDirectory = @"C:\Users\Dusan\Downloads\WebOlaksani-master (2)\WebOlaksani-master\Olaksani";
            string relativePath = @"Datoteka\Admin.csv";

            string absolutePath = Path.Combine(baseDirectory, relativePath);

            using (FileStream fs = new FileStream(absolutePath, FileMode.OpenOrCreate))
            {
                using (StreamReader streamReader = new StreamReader(fs))
                {
                    if (streamReader.ReadToEnd().Equals(String.Empty))
                    {
                        fs.SetLength(0); // Clear the file content
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine("admin1,admin1,Dusan,Markovic,2001-11-04");
                        }
                    }

                    fs.Position = 0; // Reset the stream position to the beginning of the file

                    string s = streamReader.ReadToEnd();
                    string[] niz = s.Split(',');

                    admini.Add(new Administrator
                    {
                        KorisnickoIme = niz[0],
                        Sifra = niz[1],
                        Ime = niz[2],
                        Prezime = niz[3],
                        DatumRodjenja = DateTime.Parse(niz[4])
                    });
                }
            }

            
        }
    }
}
