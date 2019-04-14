using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        TcpClient client;

        public Registration(TcpClient _client)
        {
            client = _client;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextBox.Text;
            string pass = passwordTextBox.Text;
            

            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000));

            var stream = client.GetStream();


            var json = JsonConvert.SerializeObject(new
            {
                email = email,
                pass = pass
            });

            var arr = Helper.StringToBytes(json);

            stream.Write(arr, 0, arr.Length);

            exp.IsExpanded = true;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();

            //string token = code.Text;
            //var stream = client.GetStream();
            //byte[] arr = Helper.StringToBytes(token);
            //stream.Write(arr, 0, arr.Length);

            //arr = new byte[256];
            //int bytesCount = stream.Read(arr, 0, 256);
            //string msg = Helper.BytesToString(arr, bytesCount);
            //if(msg == "True")
            //{
            //    this.DialogResult = true;
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Wrong Code\nTryAgain");
            //}

        }
    }


   

}
