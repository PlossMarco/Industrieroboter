namespace Industrieroboter.Domain;

public enum BohrerArt { Spiralbohrer, Stufenbohrer, Kernbohrer, Gewindebohrer }

public class Bohrer : Werkzeug
{
    private BohrerArt bohrerArt;
    private int groesse;
    public Bohrer(string art, int verschleiss, int groesse, BohrerArt bohrerart = BohrerArt.Spiralbohrer) : base(art, verschleiss)
    {
        this.bohrerArt = bohrerart;
        this.groesse = groesse;
    }

    public BohrerArt BohrerTyp
    {
        get { return bohrerArt; }
    }

    public override void ausgeben()
    {
        Console.WriteLine($"Bohrer mit Groesse {groesse} (Verschleiss {Verschleiss} %).");
    }
}