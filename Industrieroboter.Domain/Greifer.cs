namespace Industrieroboter.Domain;

public enum GreiferArt { Parallelgreifer, Vakuumgreifer, Magnetgreifer, Nadelgreifer }

public class Greifer : Werkzeug
{
    private GreiferArt greiferArt;
    public Greifer(string art, int verschleiss, GreiferArt greiferArt = GreiferArt.Parallelgreifer) : base(art, verschleiss)
    {
        this.greiferArt = greiferArt;
    }
    public override void ausgeben()
    {
        Console.WriteLine($"{greiferArt} (Verschleiss {Verschleiss} %).");
    }
}