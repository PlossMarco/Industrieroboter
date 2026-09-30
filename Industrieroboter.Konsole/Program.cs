using Industrieroboter.Domain;

bool checkMenue = true;
string? userInput;
int auswahl;

Industrieroboter.Domain.Industrieroboter industrieroboter1 = new Industrieroboter.Domain.Industrieroboter();

do
{
    Console.WriteLine("1 - ");
    Console.WriteLine("2 - ");
    Console.WriteLine("3 - Anzeigen");
    Console.WriteLine("4 - Benutzen");
    Console.WriteLine("5 - Warten");
    Console.WriteLine("6 - Beenden");
    Console.WriteLine("7 - Testprogramm");

    userInput = Console.ReadLine();
    int.TryParse(userInput, out auswahl);

    switch (auswahl)
    {
        case 1:
            Console.WriteLine("Kommt noch!");
            break;

        case 2:
            Console.WriteLine("Kommt noch!");
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