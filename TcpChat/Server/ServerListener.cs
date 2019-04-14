using Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerListener
    {
        const int BUFFER_SIZE = 256;
        TcpListener Listener;
        List<User> Users;

        public ServerListener()
        {
            Users = new List<User>();
            Listener = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000));
        }

        public void Start()
        {
            Console.WriteLine("Start Listening");
            Listener.Start();

            Task.Run(() =>
            {
                while(true)
                {
                    var client = Listener.AcceptTcpClient();
                    Task.Run(() =>
                    {
                        Console.WriteLine("Accept Client: " + client.Client.RemoteEndPoint.ToString());
                        var stream = client.GetStream();

                        var msg = ReceiveMessage(stream);

                        var def = new { email = "", pass = "" };
                        var message = JsonConvert.DeserializeAnonymousType(msg, def);

                        User user = new User() { Email = message.email, Password = message.pass,Stream = stream };
                        Users.Add(user);

                        //MailService mailService = new MailService();
                        //string token = mailService.GenerateToken();
                        //mailService.SendConfirmEmail(user.Email);

                        //do
                        //{
                        //    msg = ReceiveMessage(stream);
                        //    if (msg == token)
                        //    {
                        //        user.IsConfirmed = true;
                        //        Console.WriteLine($"User {user.Email} has confirmed his mail");
                        //        SendMessage(stream, "True");
                        //    }
                        //    else
                        //    {
                        //        SendMessage(stream, "False");
                        //    }

                        //} while (msg != token);
                        
                        while(true)
                        {
                            var userMessage = ReceiveMessage(user.Stream);
                            Console.WriteLine("Receive Message: " + userMessage);
                            foreach(var u in Users)
                            {
                                try
                                {
                                    SendMessage(u.Stream, user.Email+ ": "+ userMessage);
                                    Console.WriteLine("Send Message");
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }

                    });
                   

                }
                
            });
            

        }

       
        public string ReceiveMessage(NetworkStream stream)
        {
            byte[] arr = new byte[BUFFER_SIZE];
            int byteCount = stream.Read(arr, 0, BUFFER_SIZE);
            string msg = Helper.BytesToString(arr, byteCount);
            return msg;
        }

        public void SendMessage(NetworkStream stream, string msg)
        {
            byte[] arr = Helper.StringToBytes(msg);
            stream.Write(arr, 0, arr.Length);
        }

    }
}
