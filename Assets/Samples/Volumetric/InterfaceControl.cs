using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class SynchronousSocketClient
{

    public static void StartClient()
    {
        
    }

    public static int Main(String[] args)
    {
        StartClient();
        return 0;
    }
}

public class InterfaceControl : MonoBehaviour
{
    public Text timeDisplay;
    public Text audioInText;
    // public string ipAddress;
    public static string data = null;
    //public int port;
    // Start is called before the first frame update
    void Start()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket.  
        // Dns.GetHostName returns the name of the
        // host running the application.  
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        // Create a TCP/IP socket.  
        Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and
        // listen for incoming connections.  
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Start listening for connections.  
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                // Program is suspended while waiting for an incoming connection.  
                Socket handler = listener.Accept();
                data = null;

                // An incoming connection needs to be processed.  
                while (true)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }

                // Show the data on the console.  
                Console.WriteLine("Text received : {0}", data);

                // Echo the data back to the client.  
                byte[] msg = Encoding.ASCII.GetBytes(data);

                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        //Console.Read();
    }

    // Update is called once per frame
    void Update()
    {
        String time_hr = DateTime.Now.ToString("HH:mm:ss");
        timeDisplay.text = time_hr;
        audioInText.text = "Tranquility Base, Mark speaking...";
        
        

        // TODO: Sockets to receive audio in text, and audio out text
        // TODO: Update audio in text
    }
}
