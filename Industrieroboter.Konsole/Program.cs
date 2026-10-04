// Konsolenanwendung zur Verwaltung eines Industrieroboters (Aufgabe 2).
// Program.cs ist für die Kommunikation mit dem Nutzer zuständig (Eingaben, Menüs, Fehlermeldungen);
// die eigentliche Logik liegt im Domain-Projekt.
using Industrieroboter.Domain;
using Industrieroboter.Konsole;
using Roboter = Industrieroboter.Domain.Industrieroboter;

// Steuert die Menüschleife; wird nur bei MenuePunkt.Beenden auf false gesetzt.
bool checkMenue = true;

// Ein Roboter für die gesamte Laufzeit (vor der Schleife), damit Werkzeuge zwischen den Menüpunkten erhalten bleiben.
// Voller Name nötig, weil "Industrieroboter" sonst als Namespace interpretiert wird.
Roboter industrieroboter1 = new Roboter();

// do-while: Das Menü wird mindestens einmal angezeigt.
do
{
    Console.WriteLine("=== Werkzeugkasten-Verwaltung ===");
    // Der Menütext wird aus dem Enum erzeugt – die Liste der Menüpunkte existiert nur an einer Stelle (DRY).
    int menueAuswahl = ZahlEinlesen(AuswahlText<MenuePunkt>());

    // Ein try/catch um den ganzen switch (DRY): fängt ungültige Plätze aus allen Menüpunkten ab,
    // damit das Programm nicht abstürzt und der Nutzer es erneut versuchen kann.
    try
    {
        // Cast der Zahl in das Enum: ungültige Zahlen (z. B. 9) stürzen nicht ab, sondern landen im default.
        switch ((MenuePunkt)menueAuswahl)
        {
            case MenuePunkt.Hinzufuegen:
                WerkzeugHinzufuegenMenue();
                break;

            case MenuePunkt.Entfernen:
                int platzEntfernen = ZahlEinlesen("Wählen Sie von welchem Platz Sie das Werkzeug entfernen möchten (0 - 9)");
                EntfernenMitMeldung(industrieroboter1, platzEntfernen);
                break;

            case MenuePunkt.Anzeigen:
                WerkzeugkastenAnzeigen(industrieroboter1);
                break;

            case MenuePunkt.Benutzen:
                int platzBenutzen = ZahlEinlesen("Welches Werkzeug soll benutzt werden ? Platz(0 - 9) : ");
                int wert = ZahlEinlesen("Um wie viel % soll der Verschleiss steigen? ");
                bool genutzt = industrieroboter1.WerkzeugBenutzen(platzBenutzen, wert);
                Werkzeug? benutzt = industrieroboter1.WerkzeugAbrufen(platzBenutzen);

                if (genutzt)
                {
                    Console.WriteLine($"Benutztes Werkzeug auf Platz {platzBenutzen}: {benutzt}");
                }
                else
                {
                    Console.WriteLine($"Benutzen nicht möglich, da Platz {platzBenutzen} leer ist.");
                }
                break;

            case MenuePunkt.Warten:
                int platzWarten = ZahlEinlesen("Welches Werkzeug soll gewartet werden? Platz (0 - 9):");
                bool gewartet = industrieroboter1.WerkzeugWarten(platzWarten);
                Werkzeug? neu = industrieroboter1.WerkzeugAbrufen(platzWarten);

                if (gewartet)
                {
                    Console.WriteLine($"Gewartetes Werkzeug auf Platz {platzWarten}: {neu}");
                }
                else
                {
                    Console.WriteLine($"Warten nicht möglich, da Platz {platzWarten} leer ist.");
                }
                break;

            case MenuePunkt.Beenden:
                checkMenue = false;
                break;

            case MenuePunkt.Testprogramm:       // Aufgabe 1.2
                Testprogramm();
                break;

            case MenuePunkt.Statistik:
                StatistikAnzeigen(industrieroboter1);
                break;

            default:                            // ungültige Menüauswahl
                Console.WriteLine($"Die Eingabe muss eine Zahl zwischen 1 und {Enum.GetValues<MenuePunkt>().Length} sein");
                break;
        }
    }
    catch (ArgumentOutOfRangeException ex)
    {
        // Die Meldung steckt bereits in der Exception (formuliert im Domain-Projekt) und wird hier nur ausgegeben.
        Console.WriteLine(ex.Message);
    }
}
while (checkMenue);


/// <summary>
/// Testprogramm aus Aufgabe 1.2 (manuelle Tests per Sichtprüfung).
/// Nutzt einen eigenen, frischen Roboter, damit der Menü-Roboter unberührt bleibt.
/// </summary>
void Testprogramm()
{
    Werkzeug bohrer1 = new Bohrer("bohrer", 0, 10);
    Werkzeug bohrer2 = new Bohrer("bohrer", 0, 10);

    Industrieroboter.Domain.Industrieroboter industrieroboter1 = new Industrieroboter.Domain.Industrieroboter();

    TesteHinzufuegen(5, bohrer1);   // erwartet: hinzugefügt
    TesteHinzufuegen(5, bohrer2);   // erwartet: belegt
    TesteHinzufuegen(10, bohrer2);  // erwartet: existiert nicht
    TesteHinzufuegen(-1, bohrer2);  // erwartet: existiert nicht

    TesteEntfernen(5);              // erwartet: entfernt
    TesteEntfernen(5);              // erwartet: nicht belegt
    TesteEntfernen(10);             // erwartet: existiert nicht
    TesteEntfernen(-1);             // erwartet: existiert nicht

    // Lokale Hilfsfunktionen innerhalb von Testprogramm():
    // sehen den Test-Roboter und fangen die Exception je Testfall einzeln ab,
    // damit alle 8 Tests durchlaufen. Die Texte entsprechen exakt der Vorgabe aus 1.2.
    void TesteHinzufuegen(int platz, Werkzeug neu)
    {
        try
        {
            HinzufuegenMitMeldung(industrieroboter1, platz, neu);
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
            EntfernenMitMeldung(industrieroboter1, platz);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine($"Entfernen nicht möglich, da Platz {platz} nicht existiert.");
        }
    }
}


/// <summary>
/// Menüpunkt 1: fragt erst den Platz, dann über ein Untermenü die Werkzeugart
/// (beim Bohrer zusätzlich die Größe) sowie die spezifische Bauform ab und legt das Werkzeug ab.
/// </summary>
void WerkzeugHinzufuegenMenue()
{
    int platz = ZahlEinlesen("Geben Sie den Platz ein, an dem Sie das gewünschte Werkzeug hinzufügen möchten (0 - 9)");

    int artAuswahl = ZahlEinlesen("Wählen Sie die Art des Werkzeuges: " + AuswahlText<WerkzeugTyp>());

    // Typ Werkzeug (Basisklasse), damit jede Unterklasse hineinpasst;
    // "?" weil bei ungültiger Auswahl kein Werkzeug entsteht.
    Werkzeug? neu = null;

    switch ((WerkzeugTyp)artAuswahl)
    {
        case WerkzeugTyp.Bohrer:
            int groesse = ZahlEinlesen("Geben Sie die Größe des Bohrers ein (1 - 10).");

            // Die Bauform-Enums der Domain beginnen bei 0, der Nutzer zählt ab 1 -> Versatz 1 bzw. "- 1".
            // Der Cast stürzt bei ungültigen Zahlen nicht ab, deshalb wird anschließend mit Enum.IsDefined geprüft.
            int bohrerArtAuswahl = ZahlEinlesen("Bohrerart: " + AuswahlText<BohrerArt>(1));
            BohrerArt bohrerArt = (BohrerArt)(bohrerArtAuswahl - 1);

            if (!Enum.IsDefined(bohrerArt))
            {
                Console.WriteLine("Ungültige Bohrerart!");
                return;
            }

            neu = new Bohrer("bohrer", 0, groesse, bohrerArt);

            break;

        case WerkzeugTyp.Greifer:
            int greiferArtAuswahl = ZahlEinlesen("Greiferart: " + AuswahlText<GreiferArt>(1));
            GreiferArt greiferArt = (GreiferArt)(greiferArtAuswahl - 1);

            if (!Enum.IsDefined(greiferArt))
            {
                Console.WriteLine("Ungültige Greiferart!");
                return;
            }

            neu = new Greifer("greifer", 0, greiferArt);

            break;

        case WerkzeugTyp.Schweisser:
            int schweisserArtAuswahl = ZahlEinlesen("Schweisserart: " + AuswahlText<SchweisserArt>(1));
            SchweisserArt schweisserArt = (SchweisserArt)(schweisserArtAuswahl - 1);

            if (!Enum.IsDefined(schweisserArt))
            {
                Console.WriteLine("Ungültige Schweisserrart!");
                return;
            }

            neu = new Schweisser("schweisser", 0, schweisserArt);

            break;

        default:
            Console.WriteLine("Ungültige Werkzeugart!");

            break;
    }
    if (neu != null)
    {
        HinzufuegenMitMeldung(industrieroboter1, platz, neu);
    }
}

/// <summary>
/// Erzeugt aus einem beliebigen Enum einen Auswahltext wie "1 = Hinzufuegen | 2 = Entfernen | …".
/// So muss die Liste der Optionen nirgends von Hand gepflegt werden.
/// </summary>
/// <typeparam name="T">Das Enum, dessen Werte angezeigt werden.</typeparam>
/// <param name="versatz">Wird zur Enum-Zahl addiert – nötig für Enums, die bei 0 beginnen (Nutzer zählt ab 1).</param>
string AuswahlText<T>(int versatz = 0) where T : struct, Enum
{
    return string.Join(" | ", Enum.GetValues<T>().Select(wert => $"{Convert.ToInt32(wert) + versatz} = {wert}"));
}

/// <summary>
/// Hilfsmethode: fragt so lange nach, bis eine ganze Zahl eingegeben wurde, und liefert diese zurück.
/// </summary>
/// <param name="frage">Der Text, der vor der Eingabe angezeigt wird.</param>
int ZahlEinlesen(string frage)
{
    int zahl;
    bool istZahl;
    do
    {
        Console.WriteLine(frage);
        // TryParse liefert true/false, ob die Umwandlung geklappt hat; die Zahl kommt über "out".
        istZahl = int.TryParse(Console.ReadLine(), out zahl);
        if (!istZahl)
        {
            Console.WriteLine("Bitte eine Zahl eingeben: ");
        }
    }
    while (!istZahl);

    return zahl;
}

void HinzufuegenMitMeldung(Roboter roboter, int platz, Werkzeug neu)
{
    bool w = roboter.WerkzeugHinzufuegen(platz, neu);
    if (w)
    {
        Console.WriteLine($"Hinzugefügtes Werkzeug auf Platz {platz}: {neu}");
    }
    else
    {
        Console.WriteLine($"Hinzufügen nicht möglich, da Platz {platz} belegt ist.");
    }
}

void EntfernenMitMeldung(Roboter roboter, int platzEntfernen)
{
    Werkzeug? entfernt = roboter.WerkzeugAbrufen(platzEntfernen);
    bool w = roboter.WerkzeugEntfernen(platzEntfernen);

    if (w)
    {
        Console.WriteLine($"Entferntes Werkzeug auf Platz {platzEntfernen}: {entfernt}");
    }
    else
    {
        Console.WriteLine($"Entfernen nicht möglich, da Platz {platzEntfernen} nicht belegt ist.");
    }
}

void WerkzeugkastenAnzeigen(Roboter roboter)
{
    for (int i = 0; i < roboter.AnzahlPlaetze; i++)
    {
        Werkzeug? w = roboter.WerkzeugAbrufen(i);
        if (w == null)
        {
            Console.WriteLine($"Platz {i}: leer");
        }
        else
        {
            Console.WriteLine($"Platz {i}: {w}");
        }
    }
}

void StatistikAnzeigen(Roboter roboter)
{
    Statistik statistik = roboter.StatistikBerechnen();

        if (statistik.StaerkstesWerkzeug == null)
    {
        Console.WriteLine("Keine Werkzeuge vorhanden.");
    }
    else
    {
        Console.WriteLine($"Der Durchschnittliche Verschleiss beträgt: {statistik.DurchschnittVerschleiss:0.0}");
        Console.WriteLine($"Plätze \tfrei: \t{statistik.AnzahlFrei}\tbelegt: \t{statistik.AnzahlBelegt}");
        Console.WriteLine($"Das Werkzeug mit dem größten Verschleiss ist: {statistik.StaerkstesWerkzeug}");
    }
}