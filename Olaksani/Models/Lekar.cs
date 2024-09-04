using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Olaksani.Models
{


    public class Lekar
    {
        private string korisnickoIme;
        private string sifra;
        private string ime;
        private string prezime;
        private DateTime datumRodjenja;
        private string elektronskaPosta;
        private List<Termin> zakazaniTermini = new List<Termin>();
        private List<Termin> slobodniTermini = new List<Termin>();
        private List<Terapija> prepisaneTerapije = new List<Terapija>();

        public Lekar()
        {
        }

        public Lekar(string korisnickoIme, string sifra, string ime, string prezime, DateTime datumRodjenja, string elektronskaPosta)
        {
            KorisnickoIme = korisnickoIme;
            Sifra = sifra;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
            ElektronskaPosta = elektronskaPosta;
        }

        public Lekar(string korisnickoIme, string sifra, string ime, string prezime, DateTime datumRodjenja, string elektronskaPosta, System.Collections.Generic.List<Termin> zakazaniTermini, System.Collections.Generic.List<Termin> slobodniTermini, List<Terapija> prepisaneTerapije)
        {
            this.KorisnickoIme = korisnickoIme;
            this.Sifra = sifra;
            this.Ime = ime;
            this.Prezime = prezime;
            this.DatumRodjenja = datumRodjenja;
            this.ElektronskaPosta = elektronskaPosta;
            this.ZakazaniTermini = zakazaniTermini;
            this.SlobodniTermini = slobodniTermini;
            this.PrepisaneTerapije = prepisaneTerapije;
        }

        public string KorisnickoIme { get => korisnickoIme; set => korisnickoIme = value; }
        public string Sifra { get => sifra; set => sifra = value; }
        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public DateTime DatumRodjenja { get => datumRodjenja; set => datumRodjenja = value; }
        public string ElektronskaPosta { get => elektronskaPosta; set => elektronskaPosta = value; }
        public List<Termin> ZakazaniTermini { get => zakazaniTermini; set => zakazaniTermini = value; }
        public List<Termin> SlobodniTermini { get => slobodniTermini; set => slobodniTermini = value; }
        public List<Terapija> PrepisaneTerapije { get => prepisaneTerapije; set => prepisaneTerapije = value; }
    }
}