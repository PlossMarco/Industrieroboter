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

        private static readonly int _maxAnzWerkzeuge = 10;

        public int AnzahlPlaetze 
        {
            get {
                return _maxAnzWerkzeuge;
        }

        }

        /// <summary>
        /// Der Werkzeugkasten. Plätze 0 bis _maxAnzWerkzeuge-1.
        /// Werkzeug? -> ein Fach darf null sein (= leerer Platz).
        /// Typ Werkzeug -> Bohrer, Greifer und Schweisser passen alle hinein (Polymorphie).
        /// </summary>
        private Werkzeug?[] _werkzeugKasten = new Werkzeug[_maxAnzWerkzeuge];

        /// <summary>
        /// Legt ein Werkzeug auf den angegebenen Platz, sofern dieser frei ist.
        /// </summary>
        /// <param name="platz">Zielplatz (0–9).</param>
        /// <param name="neu">Das abzulegende Werkzeug.</param>
        /// <returns>true bei Erfolg; false, wenn der Platz bereits belegt ist.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Wenn der Platz nicht existiert.</exception>
        public bool WerkzeugHinzufuegen(int platz, Werkzeug neu)
        {
            PlatzPruefen(platz);

            if (_werkzeugKasten[platz] != null)
            {
                return false;
            }
            else
            {
                _werkzeugKasten[platz] = neu;

                return true;
            }
        }

        /// <summary>
        /// Entfernt das Werkzeug vom angegebenen Platz, sofern dort eines liegt.
        /// </summary>
        /// <param name="platz">Platz (0–9).</param>
        /// <returns>true bei Erfolg; false, wenn der Platz leer ist.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Wenn der Platz nicht existiert.</exception>
        public bool WerkzeugEntfernen(int platz)
        {
            PlatzPruefen(platz);

            // Lokale Variable, damit der Compiler die Null-Prüfung nachvollziehen kann.
            Werkzeug? w = _werkzeugKasten[platz];

            if (w == null)
            {
                return false;
            }
            else
            {
                _werkzeugKasten[platz] = null;

                return true;
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
        public bool WerkzeugBenutzen(int platz, int wert)
        {
            PlatzPruefen(platz);

            Werkzeug? w = _werkzeugKasten[platz];

            if (w == null)
            {
                return false;
            }
            else
            {
                w.Benutzen(wert);

                return true;
            }
        }

        /// <summary>
        /// Wartet das Werkzeug auf dem angegebenen Platz (Verschleiß zurück auf 0).
        /// </summary>
        /// <param name="platz">Platz (0–9).</param>
        /// <returns>true bei Erfolg; false, wenn der Platz leer ist.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Wenn der Platz nicht existiert.</exception>
        public bool WerkzeugWarten(int platz)
        {
            PlatzPruefen(platz);

            Werkzeug? w = _werkzeugKasten[platz];

            if (w == null)
            {
                return false;
            }
            else
            {
                w.Warten();

                return true;
            }
        }

        /// <summary>
        /// Zentrale Prüfung, ob ein Platz existiert (DRY – wird von allen Methoden mit Platz-Parameter genutzt).
        /// private, weil sie nur intern als Hilfsmethode gebraucht wird.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Wenn der Platz außerhalb von 0 bis _maxAnzWerkzeuge-1 liegt.</exception>
        private void PlatzPruefen(int platz)
        {
            if (platz < 0 || platz > (_maxAnzWerkzeuge - 1))
                throw new ArgumentOutOfRangeException(nameof(platz), $"Platz muss zwischen 0 und {_maxAnzWerkzeuge - 1} liegen!");
        }

        /// <summary>
        /// Berechnet eine Statistik: durchschnittlicher Verschleiß aller vorhandenen Werkzeuge,
        /// Anzahl belegter/freier Plätze und das am stärksten verschlissene Werkzeug.
        /// </summary>
        public Statistik StatistikBerechnen()
        {
            // Variablen, die beim Durchlaufen fortgeschrieben werden
            int frei = 0;
            int verschleissGesamt = 0;
            Werkzeug? staerkstes = null;

            // Schleife zählt durch alle Plätze des Werkzeugkasten
            for (int i = 0; i < _maxAnzWerkzeuge; i++)
            {
                Werkzeug? w = _werkzeugKasten[i];

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

            int belegt = _maxAnzWerkzeuge - frei;

            double durchschnittVerschleiss = 0;
            

            if (belegt > 0)
            {
                durchschnittVerschleiss = (double)verschleissGesamt / belegt;
            }

            return new Statistik(belegt, frei, staerkstes, durchschnittVerschleiss);



        }

        public Werkzeug? WerkzeugAbrufen(int platz)
        {
            PlatzPruefen(platz);

            return _werkzeugKasten[platz];
        }
    }
}
