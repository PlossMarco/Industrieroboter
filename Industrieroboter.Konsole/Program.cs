using Industrieroboter.Domain;

bool checkMenue = true;

Industrieroboter.Domain.Industrieroboter industrieroboter1 = new Industrieroboter.Domain.Industrieroboter();

do
{
    Console.WriteLine("=== Werkzeugkasten-Verwaltung ===");
    int menueAuswahl = ZahlEinlesen("1 = Hinzufügen | 2 = Entfernen | 3 = Anzeigen | 4 = Benutzen | 5 = Warten | 6 = Beenden | 7 = Testprogramm");

    switch (menueAuswahl)
    {
        case 1:
            WerkzeugHinzufuegenMenue();
            break;
        case 2:
            int platz = ZahlEinlesen("Wählen Sie von welchem Platz Sie das Werkzeug entfernen möchten (0 - 9)");
            industrieroboter1.werkzeugEntfernen(platz);
            break;
        case 3:
            industrieroboter1.werkzeugAnzeigen();
            break;

        case 4:
            // Benutzen();
            Console.WriteLine("Kommt noch!");
            break;

        case 5:
            // Warten();
            Console.WriteLine("Kommt noch!");
            break;

        case 6:
            checkMenue = false;
            break;

        case 7:
            Testprogramm();
            break;

        default:
            Console.WriteLine("Die Eingabe muss eine Zahl zwischen 1 und 7 sein");
            break;
    }
}
while (checkMenue);


void Testprogramm() {
    Werkzeug bohrer1 = new Bohrer("bohrer", 0, 10);
    Werkzeug bohrer2 = new Bohrer("bohrer", 0, 10);

    Industrieroboter.Domain.Industrieroboter industrieroboter1 = new Industrieroboter.Domain.Industrieroboter();

    industrieroboter1.werkzeugHinzufuegen(5, bohrer1);
    industrieroboter1.werkzeugHinzufuegen(5, bohrer2);
    industrieroboter1.werkzeugHinzufuegen(10, bohrer2);
    industrieroboter1.werkzeugHinzufuegen(-1, bohrer2);

    industrieroboter1.werkzeugEntfernen(5);
    industrieroboter1.werkzeugEntfernen(5);
    industrieroboter1.werkzeugEntfernen(10);
    industrieroboter1.werkzeugEntfernen(-1);
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
            neu = new Bohrer("bohrer", 0, groesse);

            break;

        case 2:
            neu = new Greifer("greifer", 0);

            break;

        case 3:
            neu = new Schweisser("schweisser", 0);

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