namespace Industrieroboter.Domain;

public abstract class Werkzeug
{
    private string art;
    protected int verschleiss;
    public abstract void ausgeben();

    public Werkzeug(string art, int verschleiss)
    {
        this.art = art;
        this.verschleiss = verschleiss;
    }
    public void benutzen(int wert)
    {

            if (wert < 0)
            {
                Console.WriteLine("Der Verschleiss muss im positiven Bereich liegen!");
                return;
            }
        verschleiss = Math.Min(verschleiss + wert, 100);
    }

    public void warten()
    {
        verschleiss = 0;
    }
}

public class Bohrer : Werkzeug 
{
    private int groesse;
    public Bohrer(string art, int verschleiss, int groesse) : base(art, verschleiss)
    {
        this.groesse = groesse;
    }

    public override void ausgeben()
    {
        Console.WriteLine($"Bohrer mit Groesse {groesse} (Verschleiss {verschleiss} %).");
    }
}

public class Greifer : Werkzeug
{
    public Greifer(string art, int verschleiss) : base(art, verschleiss)
    {
    }
    public override void ausgeben()
    {
        Console.WriteLine($"Greifer (Verschleiss {verschleiss} %).");
    }
}

public class Schweisser : Werkzeug
{
    public Schweisser(string art, int verschleiss) : base(art, verschleiss)
    {
    }
    public override void ausgeben()
    {
        Console.WriteLine($"Schweisser (Verschleiss {verschleiss} %).");
    }

}
