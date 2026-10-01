namespace Industrieroboter.Domain;

public enum BohrerArt { Spiralbohrer, Stufenbohrer, Kernbohrer, Gewindebohrer }
public enum GreiferArt { Parallelgreifer, Vakuumgreifer, Magnetgreifer, Nadelgreifer }
public enum SchweisserArt { Punktschweissen, Schutzgasschweissen, WigSchweissen, Laserschweissen }

public abstract class Werkzeug
{
    private string art;
    private int verschleiss;
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

    public abstract void ausgeben();

    public Werkzeug(string art, int verschleiss)
    {
        this.art = art;
        this.Verschleiss = verschleiss;
    }
    public bool benutzen(int wert)
    {

            if (wert < 0)
            {
                Console.WriteLine("Der Verschleiss muss im positiven Bereich liegen!");

                return false;
            }
        Verschleiss = Math.Min(verschleiss + wert, 100);

        return true;
    }

    public void warten()
    {
        Verschleiss = 0;
    }
}

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
