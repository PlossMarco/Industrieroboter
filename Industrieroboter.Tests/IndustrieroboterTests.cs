using Industrieroboter.Domain;
// Alias: löst die Namenskollision zwischen Namespace "Industrieroboter" und Klasse "Industrieroboter".
using Roboter = Industrieroboter.Domain.Industrieroboter;

/// <summary>
/// Automatisierte Unit-Tests (xUnit) für Industrieroboter und Werkzeug.
/// Jeder Test ist unabhängig und erzeugt seinen eigenen Roboter (Arrange – Act – Assert).
/// Geprüft wird über Rückgabewerte, Verschleißwerte und erwartete Exceptions:
/// ungültiger Platz -> Exception, belegter/leerer Platz -> bool.
/// </summary>
public class IndustrieroboterTests
{
    // ---------- Hinzufügen ----------

    [Fact]
    public void Hinzufuegen_FreierPlatz_GibtTrue()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();

        // Act: ausführen
        bool ergebnis = roboter.werkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        // Assert: Ergebnis prüfen
        Assert.True(ergebnis);
    }

    [Fact]
    public void Hinzufuegen_BelegterPlatz_GibtFalse()
    {
        // Arrange: Platz 5 vorbelegen
        Roboter roboter = new Roboter();
        roboter.werkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        // Act: ausführen
        bool ergebnis = roboter.werkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        // Assert: Ergebnis prüfen
        Assert.False(ergebnis);
    }

    [Fact]
    public void Hinzufuegen_PlatzOutOfRange_Exception()
    {
        // Arrange: alles vorbereiten
        int ungueltigerPlatz = 10;
        Roboter roboter = new Roboter();

        // Act + Assert: ausführen und erwartete Exception prüfen
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.werkzeugHinzufuegen(ungueltigerPlatz, new Bohrer("bohrer", 0, 10)));
    }

    [Fact]
    public void Hinzufuegen_PlatzNegativ_Exception()
    {
        // Arrange: alles vorbereiten
        int ungueltigerPlatz = -1;
        Roboter roboter = new Roboter();

        // Act + Assert: ausführen und erwartete Exception prüfen
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.werkzeugHinzufuegen(ungueltigerPlatz, new Bohrer("bohrer", 0, 10)));
    }

    // ---------- Entfernen ----------

    [Fact]
    public void Entfernen_BelegterPlatz_GibtTrue()
    {
        // Arrange: Platz 5 vorbelegen
        Roboter roboter = new Roboter();
        roboter.werkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        // Act: ausführen
        bool ergebnis = roboter.werkzeugEntfernen(5);

        // Assert: Ergebnis prüfen
        Assert.True(ergebnis);
    }

    [Fact]
    public void Entfernen_FreierPlatz_GibtFalse()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();

        // Act: ausführen
        bool ergebnis = roboter.werkzeugEntfernen(5);

        // Assert: Ergebnis prüfen
        Assert.False(ergebnis);
    }

    [Fact]
    public void Entfernen_PlatzOutOfRange_Exception()
    {
        // Arrange: alles vorbereiten
        int ungueltigerPlatz = 10;
        Roboter roboter = new Roboter();

        // Act + Assert: ausführen und erwartete Exception prüfen
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.werkzeugEntfernen(ungueltigerPlatz));
    }

    [Fact]
    public void Entfernen_PlatzNegativ_Exception()
    {
        // Arrange: alles vorbereiten
        int ungueltigerPlatz = -1;
        Roboter roboter = new Roboter();

        // Act + Assert: ausführen und erwartete Exception prüfen
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.werkzeugEntfernen(ungueltigerPlatz));
    }

    // ---------- Benutzen ----------

    [Fact]
    public void Benutzen_Plus50_TrueVerschleiss50()
    {
        // Arrange: Werkzeug auf Platz 5 legen
        Roboter roboter = new Roboter();
        roboter.werkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        // Act: ausführen
        bool ergebnis = roboter.werkzeugBenutzen(5, 50);

        // Assert: Ergebnis prüfen
        Assert.True(ergebnis);
    }

    [Fact]
    public void Benutzen_Plus120_Verschleiss100()
    {
        // Arrange: Bohrer in einer Variable merken, um danach seinen Verschleiß abzufragen
        Roboter roboter = new Roboter();
        Bohrer bohrer = new Bohrer("bohrer", 0, 10);
        roboter.werkzeugHinzufuegen(5, bohrer);

        // Act
        roboter.werkzeugBenutzen(5, 120);

        // Assert: muss bei 100 gedeckelt sein
        Assert.Equal(100, bohrer.Verschleiss);
    }

    [Fact]
    public void Benutzen_Minus10_FalseVerschleissBleibt()
    {
        // Arrange: Bohrer mit 50 % Verschleiß auf Platz 5
        Roboter roboter = new Roboter();
        Bohrer bohrer = new Bohrer("bohrer", 50, 0);
        roboter.werkzeugHinzufuegen(5, bohrer);

        // Act: ausführen
        bool ergebnis = roboter.werkzeugBenutzen(5, -10);

        // Assert: abgelehnt und Verschleiß unverändert
        Assert.Equal(50, bohrer.Verschleiss);
        Assert.False(ergebnis);
    }

    [Fact]
    public void Benutzen_FreierPlatz_False()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();

        // Act: ausführen
        bool ergebnis = roboter.werkzeugBenutzen(5, 50);

        // Assert: Ergebnis prüfen
        Assert.False(ergebnis);
    }

    // ---------- Warten ----------

    [Fact]
    public void Warten_NachBenutzen_TrueVerschleiss0()
    {
        // Arrange: verschlissenes Werkzeug (50 %) auf Platz 5
        Roboter roboter = new Roboter();
        roboter.werkzeugHinzufuegen(5, new Bohrer("bohrer", 50, 0));

        // Act: ausführen
        bool ergebnis = roboter.werkzeugWarten(5);

        // Assert: Ergebnis prüfen
        Assert.True(ergebnis);
    }

    [Fact]
    public void Warten_FreierPlatz_False()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();

        // Act: ausführen
        bool ergebnis = roboter.werkzeugWarten(5);

        // Assert: Ergebnis prüfen
        Assert.False(ergebnis);
    }

    // ---------- Türsteher (Property Verschleiss) ----------

    [Fact]
    public void InstanzierenNeuerBohrer_Verschleiss150_Exception()
    {
        // Arrange: nichts nötig

        // Act + Assert: Konstruktor mit ungültigem Verschleiß muss eine Exception werfen
        Assert.Throws<ArgumentOutOfRangeException>(() => new Bohrer("bohrer", 150, 0));
    }
}
