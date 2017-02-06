﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace MyTcpListener
{
    public static class TCPServer
    {
        private static void AcceptClient(TcpListener server)
        {

            Byte[] bytes = new Byte[256];
            String data = null;
            Console.Write("Waiting for a connection... ");
            // Perform a blocking call to accept requests.
            // You could also user server.AcceptSocket() here.
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Connected!");


            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);

                // Process the data sent by the client.
                string[] dt1 = data.Split(' ');
                double score1 = Convert.ToDouble(dt1[dt1.Length - 1]);
                int score = Convert.ToInt32(score1);
                List<string> dataString = dt1.ToList();
                
                dataString.RemoveAt(dataString.Count - 1);
                //Insert data into database.
                DB_FUNCS.Insert(dataString,score);
                
            }

            // Shutdown and end connection
            client.Close();

        }

        public static void SServer()
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                AcceptClient(server);

                // Buffer for reading data
                // Enter the listening loop.


            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }
    }
}
