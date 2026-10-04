namespace Industrieroboter.Domain;

public class Statistik
{
public int AnzahlBelegt { get; }
    public int AnzahlFrei { get; }
    public Werkzeug? StaerkstesWerkzeug { get; }
    public double DurchschnittVerschleiss { get; }

    public Statistik (int anzahlBelegt, int anzahlFrei, Werkzeug? staerkstesWerkzeug, double durchschnittVerschleiss)
        {
        AnzahlBelegt = anzahlBelegt;
        AnzahlFrei = anzahlFrei;
        StaerkstesWerkzeug = staerkstesWerkzeug;
        DurchschnittVerschleiss = durchschnittVerschleiss;
    }
}