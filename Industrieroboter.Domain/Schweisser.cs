namespace Industrieroboter.Domain;

public enum SchweisserArt { Punktschweissen, Schutzgasschweissen, WigSchweissen, Laserschweissen }

public class Schweisser : Werkzeug
{
    private SchweisserArt schweisserArt;
    public Schweisser(string art, int verschleiss, SchweisserArt schweisserArt = SchweisserArt.Punktschweissen) : base(art, verschleiss)
    {
        this.schweisserArt = schweisserArt;
    }
    public override void ausgeben()
    {
        Console.WriteLine($"{schweisserArt} (Verschleiss {Verschleiss} %).");
    }
}