namespace Industrieroboter.Konsole;

/// <summary>
/// Punkte des Hauptmenüs. Die Zahlen entsprechen der Eingabe des Nutzers
/// (erster Wert = 1, die weiteren zählen automatisch hoch).
/// Gehört ins Konsolen-Projekt, weil das Menü reine Bedienoberfläche ist.
/// </summary>
public enum MenuePunkt
{
    Hinzufuegen = 1,
    Entfernen,
    Anzeigen,
    Benutzen,
    Warten,
    Beenden,
    Testprogramm,
    Statistik
}
