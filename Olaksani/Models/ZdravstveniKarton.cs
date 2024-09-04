using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olaksani.Models
{
    public class ZdravstveniKarton
    {
        private Pacijent pacijent;
        private List<Termin> termini;

        public ZdravstveniKarton()
        {
        }

        public ZdravstveniKarton(Pacijent pacijent, List<Termin> termini)
        {
            this.Pacijent = pacijent;
            this.Termini = termini;
        }

        public Pacijent Pacijent { get => pacijent; set => pacijent = value; }
        public List<Termin> Termini { get => termini; set => termini = value; }
    }
}