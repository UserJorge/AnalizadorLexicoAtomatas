using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace AnalizadorLexicoAtomatas.View
{
    /// <summary>
    /// Lógica de interacción para PagePruebaLinea.xaml
    /// </summary>
    public partial class PagePruebaLinea : Page
    {
        public PagePruebaLinea()
        {
            InitializeComponent();
        }
       // T_SimbolosM Tsimbolos = new T_SimbolosM();

        
       
        //Metodos de disenio y formato
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void OnFocus(object sender, RoutedEventArgs e)
        {
            Button bt = e.Source as Button;
            SolidColorBrush mb = new SolidColorBrush(Color.FromArgb(80, 255, 255, 255));
            bt.Background = mb;
        }
        private void OnFocusR(object sender, RoutedEventArgs e)
        {
            Button bt = e.Source as Button;
            SolidColorBrush mb = new SolidColorBrush(Color.FromArgb(120, 255, 17, 0));
            bt.Background = mb;
        }
        private void LeaveFocus(object sender, RoutedEventArgs e)
        {
            Button bt = e.Source as Button;
            bt.Background = Brushes.Transparent;
        }
        //private void btnMax_Click(object sender, RoutedEventArgs e)
        //{
        //    var iconMin = new PackIcon { Kind = PackIconKind.WindowRestore };
        //    var iconMax = new PackIcon { Kind = PackIconKind.WindowMaximize };
        //    if (WindowState == WindowState.Normal)
        //    {
        //        this.WindowState = WindowState.Maximized;
        //        btnMax.Content = iconMin;
        //    }
        //    else
        //    {
        //        btnMax.Content = iconMax;
        //        this.WindowState = WindowState.Normal;
        //    }
        //}
        //private void btnMin_Click(object sender, RoutedEventArgs e)
        //{
        //    if (WindowState == WindowState.Minimized)
        //    {
        //        this.WindowState = WindowState.Normal;
        //    }
        //    else
        //    {
        //        this.WindowState = WindowState.Minimized;
        //    }
        //}


        //Metodos de usabilidad, guardar, abrir codigo
        //private void Open_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Title = " Abrir archivo      -        CompiladorBuz ";
        //    openFileDialog.Filter = "Archivos CBuz#(*.Buz)|*.Buz";
        //    if (openFileDialog.ShowDialog() == true)
        //        txtB.Text = File.ReadAllText(openFileDialog.FileName);
        //}
        //private void SaveAs_Click(object sender, RoutedEventArgs e)
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.Title = " Abrir archivo      -        CompiladorBuz ";
        //    saveFileDialog.Filter = "Archivos CBuz#(*.Buz)|*.Buz";
        //    if (saveFileDialog.ShowDialog() == true)
        //        File.WriteAllText(saveFileDialog.FileName, txtB.Text);
        //}

      

    }
}
