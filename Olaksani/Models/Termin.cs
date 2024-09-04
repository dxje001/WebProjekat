using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olaksani.Models
{
    public enum Status {  Slobodan, Zauzet }

    public class Termin
    {

        private Lekar lekar;
        private Status statusTermina;
        private DateTime vremeOdrzavanja;
        private string opisTerapije;

        public Termin()
        {
        }

        public Termin(Lekar lekar, Status statusTermina, DateTime vremeOdrzavanja, string opisTerapije)
        {
            this.Lekar = lekar;
            this.StatusTermina = statusTermina;
            this.VremeOdrzavanja = vremeOdrzavanja;
            this.OpisTerapije = opisTerapije;
        }

        public Lekar Lekar { get => lekar; set => lekar = value; }
        public Status StatusTermina { get => statusTermina; set => statusTermina = value; }
        public DateTime VremeOdrzavanja { get => vremeOdrzavanja; set => vremeOdrzavanja = value; }
        public string OpisTerapije { get => opisTerapije; set => opisTerapije = value; }
    }
}