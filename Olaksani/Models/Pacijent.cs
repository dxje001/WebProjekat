using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Olaksani.Models
{
    public class Pacijent
    {
        private string korisnickoIme;
        private string jmbg;
        private string sifra;
        private string ime;
        private string prezime;
        private DateTime datumRodjenja;
        private string elektronskaPosta;
        private List<Termin> zakazaniTermini = new List<Termin>();
        private List<Terapija> terapijas = new List<Terapija>();
        private List<Termin> zakaziTermin = new List<Termin>();


        public Pacijent()
        {
        }

        public Pacijent(string korisnickoIme, string jmbg, string sifra, string ime, string prezime, DateTime datumRodjenja, string elektronskaPosta, List<Termin> zakazaniTermini, List<Terapija> terapijas, List<Termin> zakaziTermin)
        {
            this.KorisnickoIme = korisnickoIme;
            this.Jmbg = jmbg;
            this.Sifra = sifra;
            this.Ime = ime;
            this.Prezime = prezime;
            this.DatumRodjenja = datumRodjenja;
            this.ElektronskaPosta = elektronskaPosta;
            this.ZakazaniTermini = zakazaniTermini;
            this.Terapijas = terapijas;
            this.ZakaziTermin = zakaziTermin;
        }

        public Pacijent(string korisnickoIme, string jmbg, string sifra, string ime, string prezime, DateTime datumRodjenja, string elektronskaPosta, List<Termin> zakazaniTermini)
        {
            KorisnickoIme = korisnickoIme;
            Jmbg = jmbg;
            Sifra = sifra;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            ElektronskaPosta = elektronskaPosta;
            ZakazaniTermini = zakazaniTermini;
        }

        public Pacijent(string korisnickoIme, string jmbg, string sifra, string ime, string prezime, DateTime datumRodjenja, string elektronskaPosta, List<Termin> zakazaniTermini, List<Terapija> terapijas)
        {
            KorisnickoIme = korisnickoIme;
            Jmbg = jmbg;
            Sifra = sifra;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            ElektronskaPosta = elektronskaPosta;
            ZakazaniTermini = zakazaniTermini;
            Terapijas = terapijas;
        }

        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Jmbg { get => jmbg; set => jmbg = value; }
        public string Sifra { get => sifra; set => sifra = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public DateTime DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
        public string ElektronskaPosta { get => elektronskaPosta; set => elektronskaPosta = value; }
        public List<Termin> ZakazaniTermini { get => zakazaniTermini; set => zakazaniTermini = value; }
        public List<Terapija> Terapijas { get => terapijas; set => terapijas = value; }
        public List<Termin> ZakaziTermin { get => zakaziTermin; set => zakaziTermin = value; }
    }
}