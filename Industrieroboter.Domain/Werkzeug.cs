namespace Industrieroboter.Domain;

/// <summary>
/// Abstrakte Basisklasse für alle Werkzeuge, die im Werkzeugkasten eines
/// Industrieroboters liegen können (Bohrer, Greifer, Schweisser).
/// Sie ist abstrakt, weil es kein "allgemeines" Werkzeug gibt – nur konkrete Unterklassen.
/// </summary>
public abstract class Werkzeug
{
    /// <summary>Freitext-Bezeichnung der Werkzeugart (laut Klassendiagramm).</summary>
    private string art;
    public string Art
    {
        get { return art; }
    }

    /// <summary>Eigentliches Feld für den Verschleiß. Wird nur über die Property <see cref="Verschleiss"/> geschrieben.</summary>
    private int verschleiss;

    /// <summary>
    /// Verschleiß in Prozent (0 = neu, 100 = komplett verschlissen).
    /// "Türsteher": Lesen dürfen alle (public get), schreiben nur die Klasse Werkzeug selbst (private set).
    /// Jeder Schreibzugriff wird auf den gültigen Bereich 0–100 geprüft.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Wenn ein Wert außerhalb von 0–100 gesetzt werden soll.</exception>
    public int Verschleiss
    {
        get { return verschleiss; }
        private set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Verschleiss muss zwischen 0 und 100 liegen.");
            }
            verschleiss = value;
        }
    }

    /// <summary>
    /// Gibt eine Beschreibung des Werkzeugs auf der Konsole aus.
    /// Abstrakt: Jede Unterklasse muss selbst festlegen, wie sie sich ausgibt (Polymorphie).
    /// </summary>
    public abstract void ausgeben();

    /// <summary>
    /// Initialisiert Art und Verschleiß. Der Verschleiß läuft dabei über die Property,
    /// d. h. ein ungültiger Startwert (z. B. 150) löst bereits hier eine Exception aus.
    /// </summary>
    /// <param name="art">Freitext-Bezeichnung der Werkzeugart.</param>
    /// <param name="verschleiss">Startverschleiß in Prozent (0–100).</param>
    public Werkzeug(string art, int verschleiss)
    {
        this.art = art;
        this.Verschleiss = verschleiss;
    }

    /// <summary>
    /// Erhöht den Verschleiß um den angegebenen Wert, gedeckelt bei 100.
    /// </summary>
    /// <param name="wert">Zuwachs in Prozent. Negative Werte sind fachlich unsinnig und werden abgelehnt.</param>
    /// <returns>true, wenn der Verschleiß erhöht wurde; false bei negativem Wert.</returns>
    public bool benutzen(int wert)
    {

        // Guard Clause: Durch Benutzen wird ein Werkzeug nie "besser".
        if (wert < 0)
        {
            Console.WriteLine("Der Verschleiss muss im positiven Bereich liegen!");

            return false;
        }
        // Math.Min nimmt den kleineren Wert -> alles über 100 wird auf 100 gedeckelt.
        Verschleiss = Math.Min(verschleiss + wert, 100);

        return true;
    }

    /// <summary>
    /// Wartet das Werkzeug, d. h. setzt den Verschleiß auf 0 zurück.
    /// </summary>
    public void warten()
    {
        Verschleiss = 0;
    }
}


