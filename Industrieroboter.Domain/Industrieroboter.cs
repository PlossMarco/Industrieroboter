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
            platzPruefen(platz);

            if (werkzeugKasten[platz] != null)
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
            platzPruefen(platz);

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

        public void werkzeugAnzeigen()
        {
            for (int i = 0; i < maxAnzWerkzeuge; i++)
            {
                Werkzeug? w = werkzeugKasten[i];

                if (w != null)
                {
                    Console.Write($"Platz {i}: ");
                    w.ausgeben();
                }
                else
                {
                    Console.WriteLine($"Platz {i}: leer");
                }
            }
        }

        public void werkzeugBenutzen(int platz, int wert)
        {
            platzPruefen(platz);

            Werkzeug? w = werkzeugKasten[platz];

            if (w == null)
            {
                Console.WriteLine($"Benutzen nicht möglich, da Platz {platz} nicht belegt ist.");

                return;
            }
            else
            {
                if (w.benutzen(wert))
                {
                    Console.Write($"Benutztes Werkzeug von Platz {platz}: ");
                    w.ausgeben();
                }

                return;
            }
        }

        public void werkzeugWarten(int platz)
        {
            platzPruefen(platz);

            Werkzeug? w = werkzeugKasten[platz];

            if (w == null)
            {
                Console.WriteLine($"Warten nicht möglich, da Platz {platz} nicht belegt ist.");

                return;
            }
            else
            {
                Console.Write($"Gewartetes Werkzeug von Platz {platz}: ");
                w.warten();
                w.ausgeben();

                return;
            }
        }
        private void platzPruefen(int platz)
        {
            if (platz < 0 || platz > (maxAnzWerkzeuge - 1))
                throw new ArgumentOutOfRangeException(nameof(platz), $"Platz muss zwischen 0 und {maxAnzWerkzeuge - 1} liegen!");
        }
    }
}
