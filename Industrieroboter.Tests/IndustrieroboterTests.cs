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
        bool ergebnis = roboter.WerkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        // Assert: Ergebnis prüfen
        Assert.True(ergebnis);
    }

    [Fact]
    public void Hinzufuegen_BelegterPlatz_GibtFalse()
    {
        // Arrange: Platz 5 vorbelegen
        Roboter roboter = new Roboter();
        roboter.WerkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        // Act: ausführen
        bool ergebnis = roboter.WerkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

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
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.WerkzeugHinzufuegen(ungueltigerPlatz, new Bohrer("bohrer", 0, 10)));
    }

    [Fact]
    public void Hinzufuegen_PlatzNegativ_Exception()
    {
        // Arrange: alles vorbereiten
        int ungueltigerPlatz = -1;
        Roboter roboter = new Roboter();

        // Act + Assert: ausführen und erwartete Exception prüfen
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.WerkzeugHinzufuegen(ungueltigerPlatz, new Bohrer("bohrer", 0, 10)));
    }

    // ---------- Entfernen ----------

    [Fact]
    public void Entfernen_BelegterPlatz_GibtTrue()
    {
        // Arrange: Platz 5 vorbelegen
        Roboter roboter = new Roboter();
        roboter.WerkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        // Act: ausführen
        bool ergebnis = roboter.WerkzeugEntfernen(5);

        // Assert: Ergebnis prüfen
        Assert.True(ergebnis);
    }

    [Fact]
    public void Entfernen_FreierPlatz_GibtFalse()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();

        // Act: ausführen
        bool ergebnis = roboter.WerkzeugEntfernen(5);

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
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.WerkzeugEntfernen(ungueltigerPlatz));
    }

    [Fact]
    public void Entfernen_PlatzNegativ_Exception()
    {
        // Arrange: alles vorbereiten
        int ungueltigerPlatz = -1;
        Roboter roboter = new Roboter();

        // Act + Assert: ausführen und erwartete Exception prüfen
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.WerkzeugEntfernen(ungueltigerPlatz));
    }

    // ---------- Benutzen ----------

    [Fact]
    public void Benutzen_Plus50_TrueVerschleiss50()
    {
        // Arrange: Werkzeug auf Platz 5 legen
        Roboter roboter = new Roboter();
        roboter.WerkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        // Act: ausführen
        bool ergebnis = roboter.WerkzeugBenutzen(5, 50);

        // Assert: Ergebnis prüfen
        Assert.True(ergebnis);
    }

    [Fact]
    public void Benutzen_Plus120_Verschleiss100()
    {
        // Arrange: Bohrer in einer Variable merken, um danach seinen Verschleiß abzufragen
        Roboter roboter = new Roboter();
        Bohrer bohrer = new Bohrer("bohrer", 0, 10);
        roboter.WerkzeugHinzufuegen(5, bohrer);

        // Act
        roboter.WerkzeugBenutzen(5, 120);

        // Assert: muss bei 100 gedeckelt sein
        Assert.Equal(100, bohrer.Verschleiss);
    }

    [Fact]
    public void Benutzen_NegativWert_Exception()
    {
        // Arrange: Bohrer mit 50 % Verschleiß auf Platz 5
        Roboter roboter = new Roboter();
        Bohrer bohrer = new Bohrer("bohrer", 50, 0);
        roboter.WerkzeugHinzufuegen(5, bohrer);


        // Act + Assert: Negativwert bei Verschleiss abgelehnt
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.WerkzeugBenutzen(5, -10));
    }

    [Fact]
    public void Benutzen_FreierPlatz_False()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();

        // Act: ausführen
        bool ergebnis = roboter.WerkzeugBenutzen(5, 50);

        // Assert: Ergebnis prüfen
        Assert.False(ergebnis);
    }

    // ---------- Warten ----------

    [Fact]
    public void Warten_NachBenutzen_TrueVerschleiss0()
    {
        // Arrange: verschlissenes Werkzeug (50 %) auf Platz 5
        Roboter roboter = new Roboter();
        roboter.WerkzeugHinzufuegen(5, new Bohrer("bohrer", 50, 0));

        // Act: ausführen
        bool ergebnis = roboter.WerkzeugWarten(5);

        // Assert: Ergebnis prüfen
        Assert.True(ergebnis);
    }

    [Fact]
    public void Warten_FreierPlatz_False()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();

        // Act: ausführen
        bool ergebnis = roboter.WerkzeugWarten(5);

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

    [Fact]
    public void Konstruktor_Bezeichnung_UeberArtLesbar()
    {
        // Arrange: alles vorbereiten
        Bohrer bohrer = new Bohrer("B-01", 0, 10);

        // Act: ausführen

        // Assert: Ergebnis prüfen
        Assert.Equal("B-01", bohrer.Art);
    }
    // ---------- Statistik ----------


    [Fact]
    public void Statistik_LeererKasten_KeinWerkzeug()
    {
        Roboter roboter = new Roboter();

        Statistik statistik = roboter.StatistikBerechnen();

        Assert.Null(statistik.StaerkstesWerkzeug);
    }

    [Fact]
    public void Statistik_ZweiWerkzeuge_WerteKorrekt()
    {
        Roboter roboter = new Roboter();
        roboter.WerkzeugHinzufuegen(5, new Bohrer("bohrer", 20, 0));
        Bohrer bohrer = new Bohrer("bohrer2", 60, 0);
        roboter.WerkzeugHinzufuegen(2, bohrer);
        Statistik statistik = roboter.StatistikBerechnen();


        Assert.Same(bohrer, statistik.StaerkstesWerkzeug);
        Assert.Equal(40, statistik.DurchschnittVerschleiss);
        Assert.Equal(2, statistik.AnzahlBelegt);
    }
}


