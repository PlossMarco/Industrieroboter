using Industrieroboter.Domain;
using Roboter = Industrieroboter.Domain.Industrieroboter;

public class IndustrieroboterTests
{
    [Fact]
    public void Hinzufuegen_FreierPlatz_GibtTrue()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();

        // Act: ausführen
        bool ergebnis = roboter.werkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        //Assert: ausfürhen und das Ergebnis prüfen
        Assert.True(ergebnis);
    }

    [Fact]
    public void Hinzufuegen_BelegterPlatz_GibtFalse()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();
        roboter.werkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        // Act: ausführen
        bool ergebnis = roboter.werkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        //Assert: ausfürhen und das Ergebnis prüfen
        Assert.False(ergebnis);
    }

    [Fact]
    public void Hinzufuegen_PlatzOutOfRange_Exception()
    {
        // Arrange: alles vorbereiten
        int ungueltigerPlatz = 10;
        Roboter roboter = new Roboter();

        //Act + Assert: ausfürhen und das Ergebnis prüfen
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.werkzeugHinzufuegen(ungueltigerPlatz, new Bohrer("bohrer", 0, 10)));
    }

    [Fact]
    public void Hinzufuegen_PlatzNegativ_Exception()
    {
        // Arrange: alles vorbereiten
        int ungueltigerPlatz = -1;
        Roboter roboter = new Roboter();

        //Act + Assert: ausfürhen und das Ergebnis prüfen
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.werkzeugHinzufuegen(ungueltigerPlatz, new Bohrer("bohrer", 0, 10)));
    }

    [Fact]
    public void Entfernen_BelegterPlatz_GibtTrue()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();
        roboter.werkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        // Act:
        bool ergebnis = roboter.werkzeugEntfernen(5);

        //Assert: ausfürhen und das Ergebnis prüfen
        Assert.True(ergebnis);
    }

    [Fact]
    public void Entfernen_FreierPlatz_GibtFalse()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();

        // Act: ausführen
        bool ergebnis = roboter.werkzeugEntfernen(5);

        //Assert: ausfürhen und das Ergebnis prüfen
        Assert.False(ergebnis);
    }

    [Fact]
    public void Entfernen_PlatzOutOfRange_Exception()
    {
        // Arrange: alles vorbereiten
        int ungueltigerPlatz = 10;
        Roboter roboter = new Roboter();

        //Act + Assert: ausfürhen und das Ergebnis prüfen
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.werkzeugEntfernen(ungueltigerPlatz));
    }

    [Fact]
    public void Entfernen_PlatzNegativ_Exception()
    {
        // Arrange: alles vorbereiten
        int ungueltigerPlatz = -1;
        Roboter roboter = new Roboter();

        //Act + Assert: ausfürhen und das Ergebnis prüfen
        Assert.Throws<ArgumentOutOfRangeException>(() => roboter.werkzeugEntfernen(ungueltigerPlatz));
    }

    [Fact]
    public void Benutzen_Plus50_TrueVerschleiss50()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();
        roboter.werkzeugHinzufuegen(5, new Bohrer("bohrer", 0, 10));

        // Act: ausführen
        bool ergebnis = roboter.werkzeugBenutzen(5, 50);

        //Assert: ausfürhen und das Ergebnis prüfen
        Assert.True(ergebnis);
    }

    [Fact]
    public void Benutzen_Plus120_Verschleiss100()
    {
        // Arrange
        Roboter roboter = new Roboter();
        Bohrer bohrer = new Bohrer("bohrer", 0, 10);
        roboter.werkzeugHinzufuegen(5, bohrer);

        // Act
        roboter.werkzeugBenutzen(5, 120);

        // Assert
        Assert.Equal(100, bohrer.Verschleiss);
    }

    [Fact]
    public void Benutzen_Minus10_FalseVerschleissBleibt()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();
        Bohrer bohrer = new Bohrer("bohrer", 50, 0);
        roboter.werkzeugHinzufuegen(5, bohrer);

        // Act: ausführen
        bool ergebnis = roboter.werkzeugBenutzen(5, -10);
        int verschleiss = bohrer.Verschleiss;

        //Assert: ausfürhen und das Ergebnis prüfen
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

        //Assert: ausfürhen und das Ergebnis prüfen
        Assert.False(ergebnis);
    }

    [Fact]
    public void Warten_NachBenutzen_TrueVerschleiss0()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();
        roboter.werkzeugHinzufuegen(5, new Bohrer("bohrer", 50, 0));

        // Act: ausführen
        bool ergebnis = roboter.werkzeugWarten(5);

        //Assert: ausfürhen und das Ergebnis prüfen
        Assert.True(ergebnis);
    }

    [Fact]
    public void Warten_FreierPlatz_False()
    {
        // Arrange: alles vorbereiten
        Roboter roboter = new Roboter();

        // Act: ausführen
        bool ergebnis = roboter.werkzeugWarten(5);

        //Assert: ausfürhen und das Ergebnis prüfen
        Assert.False(ergebnis);
    }

    [Fact]
    public void InstanzierenNeuerBohrer_Verschleiss150_Exception()
    {
        // Arrange: alles vorbereiten

        //Act + Assert: ausfürhen und das Ergebnis prüfen
        Assert.Throws<ArgumentOutOfRangeException>(() => new Bohrer("bohrer", 150, 0));
    }
}
