using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Anagramm_Generator_V1._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void EingabefeldUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string EingabeUser = EingabefeldUser.Text.Trim();
                int CharAnzahl = EingabeUser.Length;

                if (CharAnzahl > 0)
                {
                    ButtonCreate.IsEnabled = true;
                    ButtonReset.IsEnabled = true;
                }
                else
                {
                    ButtonCreate.IsEnabled = false;
                    ButtonReset.IsEnabled = false;
                }
            }
            catch
            {
                Console.Beep();
                Console.WriteLine("Falsche Eingabe!");
            }
        }
        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            AnzeigeTextBox1.Text = Shuffle();
            AnzeigeTextBox2.Text = Shuffle();
            AnzeigeTextBox3.Text = Shuffle();
            AnzeigeTextBox4.Text = Shuffle();
            AnzeigeTextBox5.Text = Shuffle();
        }
        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            EingabefeldUser.Text = "";
            AnzeigeTextBox1.Text = "";
            AnzeigeTextBox2.Text = "";
            AnzeigeTextBox3.Text = "";
            AnzeigeTextBox4.Text = "";
            AnzeigeTextBox5.Text = "";
        }

        private string Shuffle()
        {
            Random zufallsgenerator = new();
            var EingabeZerlegt = new List<char>();
            var EingabeNeuErstellt = new List<string>();

            string EingabeUser = EingabefeldUser.Text.Trim();       
            int AnzahlCharacter = EingabeUser.Length;               
            int FirstIndex = 0;
            int LastElement = EingabeUser[^1];                      

            for (int i = FirstIndex; i <= AnzahlCharacter - 1; i++, FirstIndex++)
            {
                EingabeZerlegt.Add(EingabeUser[FirstIndex]);
            }

            int RestlicheCharacter = EingabeZerlegt.Count;

            while (RestlicheCharacter > 0)
            {
                RestlicheCharacter--;
                int index = zufallsgenerator.Next(RestlicheCharacter + 1);
                EingabeNeuErstellt.Add(EingabeZerlegt[index].ToString());
                EingabeZerlegt.RemoveAt(index);
            }

            string Ausgabe = "";
            foreach (string Character in EingabeNeuErstellt)
            {
                Ausgabe += Character;
            }

            return Ausgabe;
        }
    }
}
