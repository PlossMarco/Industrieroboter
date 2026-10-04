namespace Industrieroboter.Domain;

/// <summary>Spezifische Bauformen eines Bohrers (zusätzlich zum Freitext-Attribut "art" aus Werkzeug).</summary>
public enum BohrerArt { Spiralbohrer, Stufenbohrer, Kernbohrer, Gewindebohrer }

/// <summary>
/// Ein Bohrer ist ein Werkzeug mit einer Größe in Millimetern.
/// </summary>
public class Bohrer : Werkzeug
{
    /// <summary>Bauform des Bohrers. Wird einmal im Konstruktor gesetzt und ändert sich danach nicht.</summary>
    private BohrerArt _bohrerArt;

    /// <summary>Größe des Bohrers in Millimetern.</summary>
    private int _groesse;

    /// <summary>
    /// Erzeugt einen Bohrer. Art und Verschleiß werden per base(...) an Werkzeug weitergereicht,
    /// die eigenen Attribute (Größe, Bohrerart) setzt der Bohrer selbst.
    /// </summary>
    /// <param name="art">Freitext-Bezeichnung der Werkzeugart.</param>
    /// <param name="verschleiss">Startverschleiß in Prozent (0–100).</param>
    /// <param name="groesse">Größe in Millimetern.</param>
    /// <param name="bohrerArt">Bauform. Standardwert Spiralbohrer, damit bestehende Aufrufe ohne diesen Parameter gültig bleiben.</param>
    public Bohrer(string art, int verschleiss, int groesse, BohrerArt bohrerArt = BohrerArt.Spiralbohrer) : base(art, verschleiss)
    {
        _bohrerArt = bohrerArt;
        _groesse = groesse;
    }

    /// <summary>Liefert die Bauform (nur lesbar, kein set).</summary>
    public BohrerArt BohrerTyp
    {
        get { return _bohrerArt; }
    }

    /// <summary>
    /// Liefert "Bohrer mit Groesse x (Verschleiss y %)." zurück.
    /// Der Text bleibt bewusst ohne Bohrerart, damit die Ausgabe exakt der Vorgabe aus Aufgabe 1.2 entspricht.
    /// </summary>
    public override string ToString()
    {
        return $"Bohrer mit Groesse {_groesse} (Verschleiss {Verschleiss} %).";
    }
}
