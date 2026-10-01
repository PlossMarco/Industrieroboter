using Industrieroboter.Domain;

bool checkMenue = true;

Industrieroboter.Domain.Industrieroboter industrieroboter1 = new Industrieroboter.Domain.Industrieroboter();

do
{
    Console.WriteLine("=== Werkzeugkasten-Verwaltung ===");
    int menueAuswahl = ZahlEinlesen("1 = Hinzufügen | 2 = Entfernen | 3 = Anzeigen | 4 = Benutzen | 5 = Warten | 6 = Beenden | 7 = Testprogramm | 8 = Statistik");
    try
    {

        switch (menueAuswahl)
        {
            case 1:                             // Hinzufügen
                WerkzeugHinzufuegenMenue();
                break;
            case 2:                             // Entfernen
                int platzEntfernen = ZahlEinlesen("Wählen Sie von welchem Platz Sie das Werkzeug entfernen möchten (0 - 9)");
                industrieroboter1.werkzeugEntfernen(platzEntfernen);
                break;
            case 3:                             // Anzeigen
                industrieroboter1.werkzeugAnzeigen();
                break;

            case 4:                             // Benutzen
                int platzBenutzen = ZahlEinlesen("Welches Werkzeug soll benutzt werden ? Platz(0 - 9) : ");
                int wert = ZahlEinlesen("Um wie viel % soll der Verschleiss steigen? ");
                industrieroboter1.werkzeugBenutzen(platzBenutzen, wert);
                break;

            case 5:                             // Warten
                int platzWarten = ZahlEinlesen("Welches Werkzeug soll gewartet werden? Platz (0 - 9):");
                industrieroboter1.werkzeugWarten(platzWarten);
                break;

            case 6:                             // Beenden
                checkMenue = false;
                break;

            case 7:                             // Testprogramm
                Testprogramm();
                break;

            case 8:
                industrieroboter1.werkzeugStatistik();
                break;

            default:
                Console.WriteLine("Die Eingabe muss eine Zahl zwischen 1 und 8 sein");
                break;
        }
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
while (checkMenue);


void Testprogramm()
{
    Werkzeug bohrer1 = new Bohrer("bohrer", 0, 10);
    Werkzeug bohrer2 = new Bohrer("bohrer", 0, 10);

    Industrieroboter.Domain.Industrieroboter industrieroboter1 = new Industrieroboter.Domain.Industrieroboter();

    TesteHinzufuegen(5, bohrer1);
    TesteHinzufuegen(5, bohrer2);
    TesteHinzufuegen(10, bohrer2);
    TesteHinzufuegen(-1, bohrer2);

    TesteEntfernen(5);
    TesteEntfernen(5);
    TesteEntfernen(10);
    TesteEntfernen(-1);

    void TesteHinzufuegen(int platz, Werkzeug neu)
    {
        try
        {
            industrieroboter1.werkzeugHinzufuegen(platz, neu);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Hinzufügen nicht möglich, da Platz {platz} nicht existiert.");
        }
    }
    void TesteEntfernen(int platz)
    {
        try
        {
            industrieroboter1.werkzeugEntfernen(platz);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Entfernen nicht möglich, da Platz {platz} nicht existiert.");
        }
    }
}


void WerkzeugHinzufuegenMenue()
{
    int platz = ZahlEinlesen("Geben Sie den Platz ein, an dem Sie das gewünschte Werkzeug hinzufügen möchten (0 - 9)");

    int artAuswahl = ZahlEinlesen("Wählen Sie die Art des Werkzeuges 1 = Bohrer | 2 = Greifer | 3 = Schweisser");

    Werkzeug? neu = null;

    switch (artAuswahl)
    {
        case 1:
            int groesse = ZahlEinlesen("Geben Sie die Größe des Bohrers ein (1 - 10).");

            int bohrerArtAuswahl = ZahlEinlesen("Bohrerart: 1 = Spiralbohrer | 2 = Stufenbohrer | 3 = Kernbohrer | 4 = Gewindebohrer");
            BohrerArt bohrerArt = (BohrerArt)(bohrerArtAuswahl - 1);

            neu = new Bohrer("bohrer", 0, groesse, bohrerArt);

            if (!Enum.IsDefined(bohrerArt))
            {
                Console.WriteLine("Ungültige Bohrerart!");
                return;
            }

            break;

        case 2:
            int greiferArtAuswahl = ZahlEinlesen("Greiferart: 1 = Parallelgreifer | 2 = Vakuumgreifer | 3 = Magnetgreifer | 4 = Nadelgreifer ");
            GreiferArt greiferArt = (GreiferArt)(greiferArtAuswahl - 1);

            neu = new Greifer("greifer", 0, greiferArt);

            if (!Enum.IsDefined(greiferArt))
            {
                Console.WriteLine("Ungültige Greiferart!");
                return;
            }

            break;

        case 3:
            int schweisserArtAuswahl = ZahlEinlesen("Schweisserart: 1 = Punktschweissen | 2 = Schutzgasschweissen | 3 = WigSchweissen | 4 = Laserschweissen ");
            SchweisserArt schweisserArt = (SchweisserArt)(schweisserArtAuswahl - 1);

            neu = new Schweisser("schweisser", 0, schweisserArt);

            if (!Enum.IsDefined(schweisserArt))
            {
                Console.WriteLine("Ungültige Schweisserrart!");
                return;
            }

            break;

        default:
            Console.WriteLine("Ungültige Werkzeugart!");

            break;
    }

    if (neu != null)
    {
        industrieroboter1.werkzeugHinzufuegen(platz, neu);
    }
}

int ZahlEinlesen(string frage)
{
    int zahl;
    bool istZahl;
    do
    {
        Console.WriteLine(frage);
        istZahl = int.TryParse(Console.ReadLine(), out zahl);
        if (!istZahl)
        {
            Console.WriteLine("Bitte eine Zahl eingeben: ");
        }
    }
    while (!istZahl);

    return zahl;
}