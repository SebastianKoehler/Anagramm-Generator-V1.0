using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Anagramm_Generator_V1._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         * Klasse MainWindow
         * initialisiert die Komponenten des GUI-Fensters
         */
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Methode - EventHandler bei Eingabe des Nutzers in das Eingabefeld
        /// nimmt die Eingabe des Nutzers entgegen bzw. registriert die Veränderung des Textes im Eingabefeld
        /// sollte das Eingabefeld leer sein, sind die Button deaktiviert
        /// sollte das Eingabefeld mit Zeichen gefüllt, folglich der Inhalt von Text geändert werden,
        /// werden die Button aktiviert und man kann mit ihnen interagieren
        /// @param1:    object sender =  TextBox-Element, Eingabefeld
        /// @param2:    TextChangedEventArgs e = EventHandler, wird aufgerufen wenn der Inhalt vom Textfeld geändert wird
        /// </summary>
        /// <returns>
        /// void = kein Rückgabewert;
        /// Funktion = registriert eine Veränderung des Text-Feldes im Eingabefeld und damit eine Eingabe durch den Nutzer
        /// ebenso ist es möglich eines Drag & Drops durch den Nutzer.
        /// wird eine gültige Eingabe festgestellt, werden die Button freigeschaltet/ aktiviert.
        /// </returns>
        private void EingabefeldUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Try-Catch Block für den Fall das eine falsche Eingabe getätigt wird
            // soll verhindern dass das Programm abbricht
            try
            {
                // empfangen der Eingabe des Users, vorheriges bearbeiten durch entfernen der Leerzeichen am Anfang/am Ende der Eingabe
                // einspeichern der verarbeiteten Eingabe in der Variable "EingabeUser"
                string EingabeUser = EingabefeldUser.Text.Trim();
                // Zählen der vorhandenen Zeichen in der Eingabe des Nutzers
                // abspeichern der Anzahl in der Variable "CharAnzahl"
                int CharAnzahl = EingabeUser.Length;
                // prüfen der Anzahl der Zeichen ob diese größer wie 0 ist
                // Sollte sie größer wie 0 sein, werden die Button freigeschaltet
                if (CharAnzahl > 0)
                {
                    ButtonCreate.IsEnabled = true;
                    ButtonReset.IsEnabled = true;
                }
                // Ist die Anzahl der Zeichen 0, folglich keine Eingabe getätigt worden,
                // sind die Button standardmäßig deaktiviert
                else
                {
                    ButtonCreate.IsEnabled = false;
                    ButtonReset.IsEnabled = false;
                }
            }
            // im Falle einer falschen Eingabe wird das abbrechen des Programms verhindert
            // fängt eine mögliche Exception ab, gibt beim Abfangen einen Signalton aus.
            catch
            {
                Console.Beep();
            }
        }
        /// <summary>
        /// Methode - EventHandler bei Betätigung des Create-Button
        /// registriert die Betätigung des Buttons per Button-Click-Event
        /// per Default ist der Button deaktiviert
        /// wird erst freigeschaltet sobald das Eingabefeld mit mindestens einem Zeichen(kein Leerzeichen) gefüllt wurde
        /// bei Betätigung des Buttons wird der Shuffle-Algorithmus aufgerufen und ausgeführt
        /// Der Algorithmus wird insgesamt 5x ausgeführt, jeweils 1x für jede der 5 Anzeigeboxen 
        /// @param1:    object sender =  Button-Element, Create-Button
        /// @param2:    RoutedEventArgs e = EventHandler, wird aufgerufen wenn der Button betätigt wird
        /// </summary>
        /// <returns>
        /// void = kein Rückgabewert;
        /// Funktion = aufrufen der Shuffle-Methode & generieren 
        /// der Anagramme/Ausgabe für den Nutzer, aus seiner Eingabe
        /// </returns>
        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            // AnzeigeBlock 1-5 mit je einem generiertem Anagramm als Ausgabeergebnis
            AnzeigeTextBox1.Text = Shuffle();
            AnzeigeTextBox2.Text = Shuffle();
            AnzeigeTextBox3.Text = Shuffle();
            AnzeigeTextBox4.Text = Shuffle();
            AnzeigeTextBox5.Text = Shuffle();
        }
        /// <summary>
        /// Methode - EventHandler bei Betätigung des Reset-Button
        /// registriert die Betätigung des Buttons per Button-Click-Event
        /// per Default ist der Button deaktiviert
        /// wird erst freigeschaltet sobald das Eingabefeld mit mindestens einem Zeichen(kein Leerzeichen) gefüllt wurde
        /// bei Betätigung des Buttons wird der Inhalt der Text-Felder der Anzeigeboxen sowie des Eingabefeldes gelöscht/zurückgesetzt
        /// dadurch dass das Eingabefeld wieder leer ist, werden auch die Button deaktiviert.
        /// @param1:    object sender =  Button-Element, Create-Button
        /// @param2:    RoutedEventArgs e = EventHandler, wird aufgerufen wenn der Button betätigt wird
        /// </summary>
        /// <returns>
        /// void = kein Rückgabewert;
        /// Funktion = zurücksetzen des Text-Feldes der Ausgabefelder(TextBlock)
        /// </returns>
        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            // TextBox Eingabefeld sowie TextBlock Ausgabefelder 1-5 werden geleert/ zurückgesetzt
            EingabefeldUser.Text = "";
            AnzeigeTextBox1.Text = "";
            AnzeigeTextBox2.Text = "";
            AnzeigeTextBox3.Text = "";
            AnzeigeTextBox4.Text = "";
            AnzeigeTextBox5.Text = "";
        }
        /// <summary>
        /// Methode - Algorithmus - wird durch das betätigen des Create-Button aufgerufen
        /// nimmt die Eingabe des Nutzers im Eingabefeld entgegen, speichert diese in der Variablen "EingabeUser"
        /// erstellt ein Objekt der Random-Klasse "zufallsgenerator", sowie 2 Listen
        /// Die Eingabe des Nutzers wird entgegen genommen, verarbeitet, die Anzahl der Zeichen gezählt
        /// daraufhin wird der erste & letzte Index festgestellt
        /// alle Werte werden in Variablen gespeichert.
        /// 
        /// In der For-Schleife wird die Eingabe des Nutzers(String) in die einzelnen Zeichen zerlegt
        /// und alle in der Liste "EingabeZerlegt" gespeichert
        /// es wird nochmal eine Variable mit der Anzahl der Elemente erstellt, diese ist die 
        /// Grundlage für die While Schleife, solange der Wert dieser Variable größer 0 ist, wird die while-Schleife ausgeführt
        /// 
        /// In der While-Schleife wird die der Wert der Variable "RestlicheCharacter" dekrementiert, 
        /// eine Variable Index erstellt, welche mit einem Integer Wert initialisiert wird, 
        /// welche durch das Random-Objekt "zufallsgenerator" generiert wird
        /// über diesen Index-Nummer, wird durch "Zufall" auf ein Character aus der Liste "EingabeZerlegt" ausgewählt
        /// dieser wird über die ToString()-Methode in seinen String-Wert gewandelt und der Liste "EingabeNeuErstellt" hinzugefügt
        /// danach wird der gleiche Wert aus der vorherigen Liste "EingabeZerlegt" entfernt
        /// 
        /// Nach dem letzten Durchlauf der While-Schleife, werden mit einer ForEach-Schleife
        /// alle Elemente in der Liste "EingabeNeuErstellt" durchlaufen und in der string-Variable "Ausgabe"
        /// eingefügt 
        /// 
        /// welche nach durchlaufen der ForEach-Schleife per Return-Statement
        /// die Variable Ausgabe als String Rückgabewert des Shuffle-Algorithmus
        /// in AnzeigeTextBox.Text des jeweiligen Elements (TextBlock 1-5) ausgibt.
        /// </summary>
        /// <returns>
        /// String Ausgabe = 1x 1 generiertes Anagramm aus der Nutzereingabe
        /// </returns>
        private string Shuffle()
        {
            // erstellen des Objekts "zufallsgenerator" der Random-Klasse
            // dieses Objekt wird genutzt um eine Zufallszahl zwischen 0 und dem letzten Zeichen zu generieren
            // welche als Index fungiert, zum aufrufen eines zufälligen Zeichens
            Random zufallsgenerator = new();

            // erstellen des Objekts "EingabeZerlegt" der List-Klasse
            // Diese speichert die einzelnen Zeichen der Nutzereingabe ab -> Datentyp: Char
            List<char> EingabeZerlegt = new();

            // erstellen des Objekts "EingabeNeuErstellt" der List-Klasse
            // Diese speichert die neu generierte Zeichenfolge ab  -> Datentyp: string
            List<string> EingabeNeuErstellt = new();

            // erstellen der string-Variablen "EingabeUser" welche den, durch die Trim()-Methode"
            // verarbeiteten, Wert aus dem Eingabefeld und die Nutzereingabe abspeichert
            string EingabeUser = EingabefeldUser.Text.Trim();

            // erstellen der Integer-Variablen "AnzahlCharacter" 
            int AnzahlCharacter = EingabeUser.Length;

            // erstellen der Integer-Variablen "FirstIndex"
            // festlegen des ersten Indexes
            // Grundlage für die Bedingung der For-Schleife
            int FirstIndex = 0;

            // aufrufen der for-Schleife, welche Die Eingabe des Nutzers durchläuft,
            // den index-basierten Wert von 0 bis zum letzten Index liest
            // und in der Liste "EingabeZerlegt" mit der Add()-Methode ablegt
            // @param1: Startbedingung -> int i -> Wert aus der Integer-Variablen "FirstIndex" = 0
            // @param2: Abbruchbedingung -> int i -> Wert stellt den die maximale Anzahl an Zeichen dar
            // @param3: bei jedem Durchlauf wird der Wert von i inkrementiert - um 1 erhöht
            // @param4: bei jedem Durchlauf wird der Wert von FirstIndex inkrementiert - um 1 erhöht
            //          dieser stellt den Index des Zeichens dar auf den zugegriffen wird
            // return:  liest nacheinander Character aus "EingabeUser" aufgrund ihres Indexes aus
            //          und speichert diese einzeln, nacheinander in der Liste "EingabeZerlegt" ab
            for (int i = FirstIndex; i <= AnzahlCharacter - 1; i++, FirstIndex++)
            {
                // füllen der Liste "EingabeZerlegt" über die Add()-Methode der List-Klasse
                // param1:  String-Variable EingabeUser mit der Eingabe durch den Nutzer
                // param2:  FirstIndex - welche den Index der Zeichen von 0 bis zum letzten möglichen Index durchläuft
                EingabeZerlegt.Add(EingabeUser[FirstIndex]);
            }

            // erstellen der Integer-Variablen "RestlicheCharacter" 
            // die Elemente(Character) in der Liste "EingabeZerlegt" werden gezählt,
            // die Anzahl der Elemente wird in der Integer-Variablen "RestlicheCharacter" gespeichert
            // die Variable stellt die Bedingung für die While-Schleife dar.
            int RestlicheCharacter = EingabeZerlegt.Count;

            // aufrufen der while-Schleife, welche alle Werte in der Liste "EingabeNeuErstellt"
            // durchläuft und diese in der String-Variable "Ausgabe" speichert und so deren Wert ("") überschreibt
            // @param1: Integer Variable "RestlicheCharacter" welche die Anzahl der übrigen Elemente in der Liste "EingabeZerlegt" darstellt
            //          wird bei jedem Durchlauf der Schleife dekrementiert - also um 1 verringert
            // @param2: 0
            // return:  durchläuft solange die Liste "EingabeZerlegt", wie der Wert von "RestlicheCharacter" größer wie 0 ist
            //          wählt die Were aus der ersten Liste aus, fügt sie der Liste "EingabeNeuErstellt" und entfernt sie aus der Liste "EingabeZerlegt"        
            while (RestlicheCharacter > 0)
            {
                // dekrementieren des Werts der Integer-Varable "RestlicheCharacter"
                // Abbruchbedingung der Schleife
                RestlicheCharacter--;
                // generieren des Zufallswertes, gespeichert in der Integer-Variablen "index"
                // inkrementieren des Wertes aus "RestlicheCharacter" damit man die Auswahl
                // aller verfügbaren Zeichen und deres Indexes hat
                int index = zufallsgenerator.Next(RestlicheCharacter + 1);
                // Zugriff über den Index auf die Werte in der Liste "EingabeZerlegt",
                // konvertieren von Char in String-Datentyp über die ToString()-Methode
                // ablegen des ausgewählten Wertes in der Liste "EingabeNeuErstellt" über die Add()-Methode
                EingabeNeuErstellt.Add(EingabeZerlegt[index].ToString());
                // Entfernen des ausgewählten Wertes in der vorherigen Liste "EingabeZerlegt"
                // mithilfe der RemoveAt()-Methode, ihr wird der zufällig generierte Index übergeben
                EingabeZerlegt.RemoveAt(index);
            }

            // erstellen der string-Variablen "Ausgabe",
            // welche vorerst mit einem leeren String-Wert initialisiert wird
            string Ausgabe = "";
            // aufrufen der ForEach-Schleife, welche alle Werte in der Liste "EingabeNeuErstellt"
            // durchläuft und diese in der String-Variable "Ausgabe" speichert und so deren Wert ("") überschreibt
            // @param1: string "Character"
            // @param2: Liste "EingabeNeuErstellt"
            // return:  überschreibt den leeren String-Wert der string-Variable "Ausgabe"
            foreach (string Character in EingabeNeuErstellt)
            {
                // Character-Werte werden in Ausgabe hintereinander eingespeichert
                Ausgabe += Character;
            }
            // ausgeben des Rückgabewertes(String) der Shuffle-Methode,
            // auslesen des Wertes der String-Variable "Ausgabe"
            return Ausgabe;
        }
    }
}
