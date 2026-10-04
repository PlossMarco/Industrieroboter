# Industrieroboter – Werkzeugkasten-Verwaltung

Konsolenanwendung in **C# / .NET 9** zur Verwaltung des Werkzeugkastens eines Industrieroboters.
Entstanden als Projektaufgabe in meiner Umschulung – mit Fokus auf objektorientiertem Design,
sauberer Fehlerbehandlung und automatisierten Tests.

## Features

- **Werkzeugkasten mit 10 Plätzen** (0–9), jeder Platz leer oder mit genau einem Werkzeug belegt
- **Drei Werkzeugtypen:** Bohrer, Greifer, Schweisser – jeweils mit eigener Bauform (Enum)
- **Menügeführte Bedienung:**
  1. Werkzeug hinzufügen (mit Untermenü für Typ, Größe und Bauform)
  2. Werkzeug entfernen
  3. Werkzeugkasten anzeigen
  4. Werkzeug benutzen (Verschleiß erhöhen, gedeckelt bei 100 %)
  5. Werkzeug warten (Verschleiß auf 0 % zurücksetzen)
  6. Beenden
  7. Testprogramm (manuelle Testfälle aus der Aufgabenstellung)
  8. Statistik (Ø-Verschleiß, freie/belegte Plätze, am stärksten verschlissenes Werkzeug)
- **18 automatisierte Unit-Tests** mit xUnit

## Klassendiagramm

```mermaid
classDiagram
    class Industrieroboter {
        -int _maxAnzWerkzeuge = 10$
        -Werkzeug?[] _werkzeugKasten
        +int AnzahlPlaetze
        +WerkzeugHinzufuegen(int platz, Werkzeug neu) bool
        +WerkzeugEntfernen(int platz) bool
        +WerkzeugBenutzen(int platz, int wert) bool
        +WerkzeugWarten(int platz) bool
        +WerkzeugAbrufen(int platz) Werkzeug?
        +StatistikBerechnen() Statistik
        -PlatzPruefen(int platz) void
    }
    class Werkzeug {
        <<abstract>>
        -string _art
        -int _verschleiss
        +string Art
        +int Verschleiss
        +ToString() string*
        +Benutzen(int wert) void
        +Warten() void
    }
    class Bohrer {
        -int _groesse
        -BohrerArt _bohrerArt
        +BohrerArt BohrerTyp
        +ToString() string
    }
    class Greifer {
        -GreiferArt _greiferArt
        +ToString() string
    }
    class Schweisser {
        -SchweisserArt _schweisserArt
        +ToString() string
    }
    class Statistik {
        +int AnzahlBelegt
        +int AnzahlFrei
        +Werkzeug? StaerkstesWerkzeug
        +double DurchschnittVerschleiss
    }
    Industrieroboter "1" o-- "0..10" Werkzeug : _werkzeugKasten
    Industrieroboter ..> Statistik : erzeugt
    Werkzeug <|-- Bohrer
    Werkzeug <|-- Greifer
    Werkzeug <|-- Schweisser
```

## Projektstruktur

```
Industrieroboter/
├── Industrieroboter.Domain/     Klassenbibliothek: Werkzeug, Bohrer, Greifer, Schweisser, Industrieroboter, Statistik
├── Industrieroboter.Konsole/    Konsolenanwendung: Menü, Eingaben und sämtliche Ausgaben
├── Industrieroboter.Tests/      xUnit-Tests für das Domänenmodell
└── global.json                  fixiert das .NET 9 SDK
```

Das Domänenmodell kennt weder Konsole noch Tests – Konsole und Tests verweisen beide nur auf `Domain`.
Im Domain-Projekt gibt es keinen einzigen `Console`-Aufruf: Es liefert nur Rückgabewerte, Objekte und Exceptions,
die Konsolenanwendung entscheidet, was angezeigt wird.

## Starten

Voraussetzung: [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

```bash
dotnet run --project Industrieroboter.Konsole
```

Tests ausführen:

```bash
dotnet test
```

## Designentscheidungen

- **Abstrakte Basisklasse & Polymorphie:** `Werkzeug` deklariert `public abstract override string ToString()`.
  Damit muss jede Unterklasse ihre eigene Beschreibung liefern – ein vergessenes oder falsch geschriebenes
  `ToString()` fällt schon beim Kompilieren auf. Der Werkzeugkasten speichert `Werkzeug`-Referenzen,
  jede Unterklasse beschreibt sich selbst.
- **Domain ohne Konsole:** Roboter und Werkzeuge geben nichts aus, sondern liefern Daten. Dadurch ist die Logik
  unabhängig von der Oberfläche und vollständig mit Unit-Tests prüfbar.
- **Verantwortlichkeiten getrennt:** Der Roboter prüft Plätze, das Werkzeug kümmert sich um seinen Verschleiß,
  die Konsole um die Kommunikation mit dem Nutzer.
- **Property als „Türsteher“:** `Verschleiss` hat ein `public get` und ein `private set`, das Werte außerhalb
  von 0–100 mit einer `ArgumentOutOfRangeException` ablehnt – auch schon im Konstruktor.
- **Exceptions vs. Rückgabewert:** Ein nicht existierender Platz oder ein negativer Verschleißzuwachs ist ein
  Aufruferfehler und wirft eine `ArgumentOutOfRangeException`. Ein belegter bzw. leerer Platz ist ein normaler
  Geschäftsfall und wird über den Rückgabewert `bool` gemeldet – so ist jeder Rückgabewert eindeutig.
- **Statistik als Momentaufnahme:** `StatistikBerechnen()` liefert ein unveränderliches `Statistik`-Objekt
  (nur `get`, gesetzt im Konstruktor). Alle Werte stammen aus einer einzigen Berechnung und passen garantiert
  zusammen.
- **Enums mit Standardwert:** Die Bauform ist ein optionaler Konstruktorparameter, damit bestehende Aufrufe
  gültig bleiben.

## Hinweis zur Benennung

Das Klassendiagramm der ursprünglichen Aufgabenstellung gab Methodennamen in camelCase vor
(z. B. `werkzeugHinzufuegen`). Die abgegebene Version hielt sich daran. Nach der Abgabe wurde der Code an die üblichen C#-Konventionen angepasst:

- **Methoden und Properties** in PascalCase, z. B. `WerkzeugHinzufuegen`, `AnzahlPlaetze`
- **private Felder** mit Unterstrich und camelCase, z. B. `_verschleiss`, `_werkzeugKasten`
- **Parameter und lokale Variablen** in camelCase ohne Unterstrich, z. B. `platz`, `neu`

Außerdem wurden die Konsolenausgaben aus dem Domänenmodell herausgelöst.

## Autor

Marco Ploss
