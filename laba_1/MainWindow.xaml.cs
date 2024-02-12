using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace laba_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string result = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"http://localhost:5024/sum?X={X.Text}&Y={Y.Text}");
            request.Method = "POST";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            var stream = response.GetResponseStream();
            if (stream != null)
                result = new StreamReader(stream).ReadToEnd();
            response.Close();

            Sum.Text = result;
        }
    }
}
