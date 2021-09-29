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
using System.Windows.Shapes;

namespace AnalizadorLexicoAtomatas.View
{
    /// <summary>
    /// Lógica de interacción para CompiladorView.xaml
    /// </summary>
    public partial class CompiladorView : Window
    {
        public CompiladorView()
        {
            InitializeComponent();
            btnAnalisis.IsEnabled = false;
            btnCompilar.IsEnabled = false;
        }

        //Metodo para scroll de ambos textbox y numeracion
        private void Proof(Object sender, ScrollChangedEventArgs e)
        {
            txtLines.ScrollToVerticalOffset(e.VerticalOffset);
            txtB.ScrollToVerticalOffset(e.VerticalOffset);
        }
        private void txtB_TextChanged(object sender, TextChangedEventArgs e)
        {
            //AnalizadorSintactico();
            var linIzq = txtB.LineCount;
            // Comprobar cuál de los dos textBoxes tiene más líneas
            txtLines.Text = " ";
            for (var i = 1; i <= linIzq; i++)
                // Indentar el texto a la derecha 
                txtLines.Text += i.ToString("0").PadLeft(4) + "\r";
        }

        private void btnLexico_Click(object sender, RoutedEventArgs e)
        {
            btnAnalisis.IsEnabled = true;
            btnCompilar.IsEnabled = true;
        }
    }
}
