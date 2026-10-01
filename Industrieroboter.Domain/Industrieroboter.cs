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

        public bool werkzeugBenutzen(int platz, int wert)
        {
            platzPruefen(platz);

            Werkzeug? w = werkzeugKasten[platz];

            if (w == null)
            {
                Console.WriteLine($"Benutzen nicht möglich, da Platz {platz} nicht belegt ist.");

                return false;
            }
            else
            {
                if (w.benutzen(wert))
                {
                    Console.Write($"Benutztes Werkzeug von Platz {platz}: ");
                    w.ausgeben();
                    return true;
                }
                else { return false; }
            }
        }

        public bool werkzeugWarten(int platz)
        {
            platzPruefen(platz);

            Werkzeug? w = werkzeugKasten[platz];

            if (w == null)
            {
                Console.WriteLine($"Warten nicht möglich, da Platz {platz} nicht belegt ist.");

                return false;
            }
            else
            {
                Console.Write($"Gewartetes Werkzeug von Platz {platz}: ");
                w.warten();
                w.ausgeben();

                return true;
            }
        }
        private void platzPruefen(int platz)
        {
            if (platz < 0 || platz > (maxAnzWerkzeuge - 1))
                throw new ArgumentOutOfRangeException(nameof(platz), $"Platz muss zwischen 0 und {maxAnzWerkzeuge - 1} liegen!");
        }

        public void werkzeugStatistik()
        {
            // Variablen
            int frei = 0;
            int verschleissGesamt = 0;
            Werkzeug? staerkstes = null;

            // Schleife zählt durch alle Plätze des Werkzeugkasten
            for (int i = 0; i < maxAnzWerkzeuge; i++)
            {
                Werkzeug? w = werkzeugKasten[i];

                if (w == null)
                    frei++;
                else
                {
                    verschleissGesamt += w.Verschleiss;

                    if (staerkstes == null || w.Verschleiss > staerkstes.Verschleiss)
                        staerkstes = w;
                }
            }

            int belegt = maxAnzWerkzeuge - frei;

            if (staerkstes == null)
            {
                Console.WriteLine("Keine Werkzeuge vorhanden.");
                        return;
            }

            double durchschnittVerschleiss = (double)verschleissGesamt / belegt;

            Console.WriteLine($"der durchschnittliche Verschleiss aller Werkzeuge liegt bei: \t\t{durchschnittVerschleiss:0.0}");
            Console.WriteLine($"Anzahl der Plätze \tbelegt: {belegt}\tfrei: {frei}");
            Console.Write($"das am stärksten verschlissene Werkzeug ist: ");
            staerkstes.ausgeben();

        }
    }
}
