namespace Industrieroboter.Domain;

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


