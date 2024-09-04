using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olaksani.Models
{
    public class Terapija
    {
        private string naziv;
        private string opis;
        private Lekar lekar;
        private Pacijent pacijent;

        public string Naziv { get => naziv; set => naziv = value; }
        public string Opis { get => opis; set => opis = value; }
        public Lekar Lekar { get => lekar; set => lekar = value; }
        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }

        public Terapija(string naziv, string opis, Lekar lekar, Pacijent pacijent)
        {
            this.Naziv = naziv;
            this.Opis = opis;
            this.Lekar = lekar;
            Pacijent = pacijent;
        }

        public Terapija()
        {
        }
    }
}