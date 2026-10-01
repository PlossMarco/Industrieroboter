namespace Industrieroboter.Domain;

/// <summary>Spezifische Schweißverfahren (zusätzlich zum Freitext-Attribut "art" aus Werkzeug).</summary>
public enum SchweisserArt { Punktschweissen, Schutzgasschweissen, WigSchweissen, Laserschweissen }

/// <summary>
/// Ein Schweisser ist ein Werkzeug ohne weitere eigene Maße.
/// </summary>
public class Schweisser : Werkzeug
{
    /// <summary>Schweißverfahren des Schweissers.</summary>
    private SchweisserArt schweisserArt;

    /// <summary>
    /// Erzeugt einen Schweisser. Art und Verschleiß gehen per base(...) an Werkzeug.
    /// </summary>
    /// <param name="art">Freitext-Bezeichnung der Werkzeugart.</param>
    /// <param name="verschleiss">Startverschleiß in Prozent (0–100).</param>
    /// <param name="schweisserArt">Schweißverfahren. Standardwert Punktschweissen.</param>
    public Schweisser(string art, int verschleiss, SchweisserArt schweisserArt = SchweisserArt.Punktschweissen) : base(art, verschleiss)
    {
        this.schweisserArt = schweisserArt;
    }

    /// <summary>Gibt z. B. "Laserschweissen (Verschleiss y %)." aus.</summary>
    public override void ausgeben()
    {
        Console.WriteLine($"{schweisserArt} (Verschleiss {Verschleiss} %).");
    }
}
