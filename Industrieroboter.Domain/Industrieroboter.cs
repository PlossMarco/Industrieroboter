using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Industrieroboter.Domain
{
    public class Industrieroboter
    {
        private static readonly int maxAnzWerkzeuge = 10;
        private Werkzeug?[] werkzeugKasten = new Werkzeug[maxAnzWerkzeuge];

        public bool werkzeugHinzufuegen(int platz, Werkzeug neu)
        {

            if (!(platz >= 0 && platz < maxAnzWerkzeuge))
            {
                Console.WriteLine($"Hinzufügen nicht möglich, da Platz {platz} nicht existiert.");

                return false;
            }
            else if (werkzeugKasten[platz] != null)
            {
                Console.WriteLine($"Hinzufügen nicht möglich, da Platz {platz} belegt ist.");

                return false;
            }
            else
            {
                werkzeugKasten[platz] = neu;
                Console.Write($"Hinzugefügtes Werkzeug auf Platz {platz}: ");
                neu.ausgeben();

                return true;
            }
        }
        public bool werkzeugEntfernen(int platz)
        {
            if (!(platz >= 0 && platz < maxAnzWerkzeuge))
            {
                Console.WriteLine($"Entfernen nicht möglich, da Platz {platz} nicht existiert.");

                return false;
            }

            Werkzeug? w = werkzeugKasten[platz];

            if (w == null)
            {
                Console.WriteLine($"Entfernen nicht möglich, da Platz {platz} nicht belegt ist.");

                return false;
            }
            else
            {
                Console.Write($"Entferntes Werkzeug auf Platz {platz}: ");
                w.ausgeben();
                werkzeugKasten[platz] = null;

                return true;
            }
        }
    }
}
