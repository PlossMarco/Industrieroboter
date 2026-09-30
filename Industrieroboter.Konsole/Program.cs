using Industrieroboter.Domain;

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