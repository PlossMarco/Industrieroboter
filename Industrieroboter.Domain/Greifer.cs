namespace Industrieroboter.Domain;

/// <summary>Spezifische Bauformen eines Greifers (zusätzlich zum Freitext-Attribut "art" aus Werkzeug).</summary>
public enum GreiferArt { Parallelgreifer, Vakuumgreifer, Magnetgreifer, Nadelgreifer }

/// <summary>
/// Ein Greifer ist ein Werkzeug ohne weitere eigene Maße.
/// </summary>
public class Greifer : Werkzeug
{
    /// <summary>Bauform des Greifers.</summary>
    private GreiferArt greiferArt;

    /// <summary>
    /// Erzeugt einen Greifer. Art und Verschleiß gehen per base(...) an Werkzeug.
    /// </summary>
    /// <param name="art">Freitext-Bezeichnung der Werkzeugart.</param>
    /// <param name="verschleiss">Startverschleiß in Prozent (0–100).</param>
    /// <param name="greiferArt">Bauform. Standardwert Parallelgreifer.</param>
    public Greifer(string art, int verschleiss, GreiferArt greiferArt = GreiferArt.Parallelgreifer) : base(art, verschleiss)
    {
        this.greiferArt = greiferArt;
    }

    /// <summary>Gibt z. B. "Magnetgreifer (Verschleiss y %)." aus.</summary>
    public override void ausgeben()
    {
        Console.WriteLine($"{greiferArt} (Verschleiss {Verschleiss} %).");
    }
}
