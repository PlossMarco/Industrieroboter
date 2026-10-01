namespace Industrieroboter.Domain
{
    /// <summary>
    /// Industrieroboter mit einem Werkzeugkasten fester Größe.
    /// Der Roboter ist für die Plätze zuständig (existiert der Platz? ist er belegt?),
    /// die Werkzeuge selbst kümmern sich um ihren Verschleiß.
    /// Regel: ungültiger Platz = Aufrufer-Fehler -> Exception;
    ///        belegter bzw. leerer Platz = normaler Geschäftsfall -> Rückgabewert bool.
    /// </summary>
    public class Industrieroboter
    {
        /// <summary>Anzahl der Plätze im Werkzeugkasten. static: gilt für alle Roboter gemeinsam; readonly: unveränderlich.</summary>
        private static readonly int maxAnzWerkzeuge = 10;

        /// <summary>
        /// Der Werkzeugkasten. Plätze 0 bis maxAnzWerkzeuge-1.
        /// Werkzeug? -> ein Fach darf null sein (= leerer Platz).
        /// Typ Werkzeug -> Bohrer, Greifer und Schweisser passen alle hinein (Polymorphie).
        /// </summary>
        private Werkzeug?[] werkzeugKasten = new Werkzeug[maxAnzWerkzeuge];

        /// <summary>
        /// Legt ein Werkzeug auf den angegebenen Platz, sofern dieser frei ist.
        /// </summary>
        /// <param name="platz">Zielplatz (0–9).</param>
        /// <param name="neu">Das abzulegende Werkzeug.</param>
        /// <returns>true bei Erfolg; false, wenn der Platz bereits belegt ist.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Wenn der Platz nicht existiert.</exception>
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
                // Write + ausgeben(): beide Teile landen in derselben Zeile.
                Console.Write($"Hinzugefügtes Werkzeug auf Platz {platz}: ");
                neu.ausgeben();

                return true;
            }
        }

        /// <summary>
        /// Entfernt das Werkzeug vom angegebenen Platz, sofern dort eines liegt.
        /// </summary>
        /// <param name="platz">Platz (0–9).</param>
        /// <returns>true bei Erfolg; false, wenn der Platz leer ist.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Wenn der Platz nicht existiert.</exception>
        public bool werkzeugEntfernen(int platz)
        {
            platzPruefen(platz);

            // Lokale Variable, damit der Compiler die Null-Prüfung nachvollziehen kann.
            Werkzeug? w = werkzeugKasten[platz];

            if (w == null)
            {
                Console.WriteLine($"Entfernen nicht möglich, da Platz {platz} nicht belegt ist.");

                return false;
            }
            else
            {
                // Erst ausgeben, dann das Fach leeren – danach wäre das Werkzeug nicht mehr greifbar.
                Console.Write($"Entferntes Werkzeug auf Platz {platz}: ");
                w.ausgeben();
                werkzeugKasten[platz] = null;

                return true;
            }
        }

        /// <summary>
        /// Gibt für jeden Platz entweder das Werkzeug (über ausgeben()) oder "Platz x: leer" aus.
        /// </summary>
        public void werkzeugAnzeigen()
        {
            for (int i = 0; i < maxAnzWerkzeuge; i++)
            {
                Werkzeug? w = werkzeugKasten[i];

                if (w != null)
                {
                    Console.Write($"Platz {i}: ");
                    w.ausgeben(); // polymorph: Bohrer, Greifer oder Schweisser gibt sich selbst aus
                }
                else
                {
                    Console.WriteLine($"Platz {i}: leer");
                }
            }
        }

        /// <summary>
        /// Benutzt das Werkzeug auf dem angegebenen Platz, d. h. erhöht dessen Verschleiß.
        /// Die eigentliche Verschleiß-Logik (Deckeln bei 100) liegt im Werkzeug.
        /// </summary>
        /// <param name="platz">Platz (0–9).</param>
        /// <param name="wert">Zuwachs des Verschleißes in Prozent.</param>
        /// <returns>true bei Erfolg; false, wenn der Platz leer ist oder der Wert negativ.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Wenn der Platz nicht existiert.</exception>
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
                // Arbeit an das Werkzeug weiterreichen und nur im Erfolgsfall melden.
                if (w.benutzen(wert))
                {
                    Console.Write($"Benutztes Werkzeug von Platz {platz}: ");
                    w.ausgeben();
                    return true;
                }
                else { return false; }
            }
        }

        /// <summary>
        /// Wartet das Werkzeug auf dem angegebenen Platz (Verschleiß zurück auf 0).
        /// </summary>
        /// <param name="platz">Platz (0–9).</param>
        /// <returns>true bei Erfolg; false, wenn der Platz leer ist.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Wenn der Platz nicht existiert.</exception>
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

        /// <summary>
        /// Zentrale Prüfung, ob ein Platz existiert (DRY – wird von allen Methoden mit Platz-Parameter genutzt).
        /// private, weil sie nur intern als Hilfsmethode gebraucht wird.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Wenn der Platz außerhalb von 0 bis maxAnzWerkzeuge-1 liegt.</exception>
        private void platzPruefen(int platz)
        {
            if (platz < 0 || platz > (maxAnzWerkzeuge - 1))
                throw new ArgumentOutOfRangeException(nameof(platz), $"Platz muss zwischen 0 und {maxAnzWerkzeuge - 1} liegen!");
        }

        /// <summary>
        /// Gibt eine Statistik aus: durchschnittlicher Verschleiß aller vorhandenen Werkzeuge,
        /// Anzahl belegter/freier Plätze und das am stärksten verschlissene Werkzeug.
        /// </summary>
        public void werkzeugStatistik()
        {
            // Variablen, die beim Durchlaufen fortgeschrieben werden
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

                    // Erst auf null prüfen: ist staerkstes noch leer, wird der zweite Teil gar nicht ausgewertet.
                    if (staerkstes == null || w.Verschleiss > staerkstes.Verschleiss)
                        staerkstes = w;
                }
            }

            int belegt = maxAnzWerkzeuge - frei;

            // Sonderfall leerer Kasten: kein Fehler, sondern normaler Zustand -> Meldung statt Division durch 0.
            // (staerkstes == null ist gleichbedeutend mit belegt == 0, aber der Compiler erkennt so,
            //  dass staerkstes weiter unten nicht null sein kann.)
            if (staerkstes == null)
            {
                Console.WriteLine("Keine Werkzeuge vorhanden.");
                        return;
            }

            // Cast auf double, damit nicht ganzzahlig geteilt (abgerundet) wird.
            double durchschnittVerschleiss = (double)verschleissGesamt / belegt;

            Console.WriteLine($"der durchschnittliche Verschleiss aller Werkzeuge liegt bei: \t\t{durchschnittVerschleiss:0.0}");
            Console.WriteLine($"Anzahl der Plätze \tbelegt: {belegt}\tfrei: {frei}");
            Console.Write($"das am stärksten verschlissene Werkzeug ist: ");
            staerkstes.ausgeben();

        }
    }
}
