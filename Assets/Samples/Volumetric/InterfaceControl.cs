using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Text;


using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class InterfaceControl : MonoBehaviour {  	

    public Text timeDisplay;
    public Text timeSecondsDisplay;
    public Text audioInText;
    // public string ipAddress;
    public static string data = null;

	#region private members 	
	private TcpClient socketConnection; 	
	private Thread clientReceiveThread; 	
    private int port;
	#endregion  	
	// Use this for initialization 	
	void Start () {
        port = 8888;
		ConnectToTcpServer();     
	}  	
	// Update is called once per frame
	void Update () {         
		// if (Input.GetKeyDown(KeyCode.Space)) {             
		// 	SendMessage();         
		// }   
        String time_hr = DateTime.Now.ToString("HH:mm");
        String time_hr_sec = DateTime.Now.ToString(":ss");
        timeDisplay.text = time_hr;
        timeSecondsDisplay.text = time_hr_sec;
        audioInText.text = "Tranquility Base, Mark speaking...";  
	}  	
	/// <summary> 	
	/// Setup socket connection. 	
	/// </summary> 	
	private void ConnectToTcpServer () { 		
		try {  			
			clientReceiveThread = new Thread (new ThreadStart(ListenForData)); 			
			clientReceiveThread.IsBackground = true; 			
			clientReceiveThread.Start();  		
		} 		
		catch (Exception e) { 			
			Debug.Log("On client connect exception " + e); 		
		} 	
	}  	
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	
	/// </summary>     
	private void ListenForData() { 		
		try { 			
			socketConnection = new TcpClient("localhost", port);  			
			Byte[] bytes = new Byte[1024];             
			while (true) { 				
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream()) { 					
					int length; 					
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 						
						var incommingData = new byte[length]; 						
						Array.Copy(bytes, 0, incommingData, 0, length); 						
						// Convert byte array to string message. 						
						string serverMessage = Encoding.ASCII.GetString(incommingData); 						
						Debug.Log("server message received as: " + serverMessage); 					
					} 				
				} 			
			}         
		}         
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	}  	
	// <summary> 	
	/// Send message to server using socket connection. 	
	//  </summary> 	
	// private void SendMessage() {         
	// 	if (socketConnection == null) {             
	// 		return;         
	// 	}  		
	// 	try { 			
	// 		// Get a stream object for writing. 			
	// 		NetworkStream stream = socketConnection.GetStream(); 			
	// 		if (stream.CanWrite) {                 
	// 			string clientMessage = "This is a message from one of your clients."; 				
	// 			// Convert string message to byte array.                 
	// 			byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage); 				
	// 			// Write byte array to socketConnection stream.                 
	// 			stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);                 
	// 			Debug.Log("Client sent his message - should be received by server");             
	// 		}         
	// 	} 		
	// 	catch (SocketException socketException) {             
	// 		Debug.Log("Socket exception: " + socketException);         
	// 	}     
	// } 
}




// public class InterfaceControl : MonoBehaviour
// {
//     public Text timeDisplay;
//     public Text timeSecondsDisplay;
//     public Text audioInText;
//     // public string ipAddress;
//     public static string data = null;
//     //public int port;
//     // Start is called before the first frame update
//     void Start()
//     {
//         // Data buffer for incoming data.  
//     //     byte[] bytes = new Byte[1024];

//     //     // Establish the local endpoint for the socket.  
//     //     // Dns.GetHostName returns the name of the
//     //     // host running the application.  
//     //     IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
//     //     IPAddress ipAddress = ipHostInfo.AddressList[0];
//     //     IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

//     //     // Create a TCP/IP socket.  
//     //     Socket listener = new Socket(ipAddress.AddressFamily,
//     //         SocketType.Stream, ProtocolType.Tcp);

//     //     // Bind the socket to the local endpoint and
//     //     // listen for incoming connections.  
//     //     try
//     //     {
//     //         listener.Bind(localEndPoint);
//     //         listener.Listen(10);

//     //         // Start listening for connections.  
//     //         while (true)
//     //         {
//     //             Console.WriteLine("Waiting for a connection...");
//     //             // Program is suspended while waiting for an incoming connection.  
//     //             Socket handler = listener.Accept();
//     //             data = null;

//     //             // An incoming connection needs to be processed.  
//     //             while (true)
//     //             {
//     //                 int bytesRec = handler.Receive(bytes);
//     //                 data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
//     //                 if (data.IndexOf("<EOF>") > -1)
//     //                 {
//     //                     break;
//     //                 }
//     //             }

//     //             // Show the data on the console.  
//     //             Console.WriteLine("Text received : {0}", data);

//     //             // Echo the data back to the client.  
//     //             byte[] msg = Encoding.ASCII.GetBytes(data);

//     //             handler.Send(msg);
//     //             handler.Shutdown(SocketShutdown.Both);
//     //             handler.Close();
//     //         }

//     //     }
//     //     catch (Exception e)
//     //     {
//     //         Console.WriteLine(e.ToString());
//     //     }

//     //     Console.WriteLine("\nPress ENTER to continue...");
//     //     //Console.Read();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         String time_hr = DateTime.Now.ToString("HH:mm");
//         String time_hr_sec = DateTime.Now.ToString(":ss");
//         timeDisplay.text = time_hr;
//         timeSecondsDisplay.text = time_hr_sec;
//         audioInText.text = "Tranquility Base, Mark speaking...";
        
        

//         // TODO: Sockets to receive audio in text, and audio out text
//         // TODO: Update audio in text
//     }
// }
