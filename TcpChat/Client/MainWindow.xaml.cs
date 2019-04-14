using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TcpClient TcpClient;
        public MainWindow()
        {
            TcpClient = new TcpClient();
            Registration registration = new Registration(TcpClient);
            if(registration.ShowDialog() != true)
            {
                this.Close();
            }
            InitializeComponent();

            ReceiveMessages();

        }

        void ReceiveMessages()
        {
            Task.Run(() =>
            {
                while(true)
                {
                    var stream = TcpClient.GetStream();
                    byte[] arr = new byte[256];
                    var bytesCount = stream.Read(arr, 0, 256);
                    var msg = Helpers.Helper.BytesToString(arr, bytesCount);
                   
                    this.Dispatcher.Invoke(() =>
                    {
                        Messages.Items.Add(msg);
                    });
                   

                }
                        
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var stream = TcpClient.GetStream();
            var arr = Helpers.Helper.StringToBytes(messageTextBox.Text);
            stream.Write(arr, 0, arr.Length);
        }
    }
}
