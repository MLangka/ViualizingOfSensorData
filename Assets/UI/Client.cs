using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class Client : ScriptableObject {
	private static int serverPort;
	private string serverIP;
	private Socket clientSocket;

	/*
	 * The Java Agent receives a SensorID from this client nd returns the appropriate values with a comma between them
	 * General: requestMessageFromClient,currentValue,sensorid
	 * Example: Sensorid1,24,4756323835986
	*/
	public Client()
	{
		serverIP = "127.0.0.1";
		serverPort = 8080;
		Debug.Log("Server IP: " + serverIP + " und Server Port: " + serverPort);
	}

	public void initalize()
	{
		IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
		clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		if (clientSocket != null)
			Debug.Log("clientsocket created");
		clientSocket.Connect(serverAddress);
		if (clientSocket.Connected)
			Debug.Log("client connected");
	}

	public void communicate()
	{
		string messageToSend = "Sensorid1#";
		string messageReceived = "";
		int i = 0;
		while (!messageToSend.Contains("quit"))
		{
			//sending
			Debug.Log("start sending");
			if(i == 1)
				messageToSend = "quit";
			byte[] toSendBytes = System.Text.Encoding.ASCII.GetBytes(messageToSend);
			clientSocket.Send(toSendBytes);
			if (!messageToSend.Contains("quit"))
			{
				//receiving
				Debug.Log("start receiving");
				byte[] rcvBytes = new byte[128];
				clientSocket.Receive(rcvBytes);
				messageReceived = System.Text.Encoding.ASCII.GetString(rcvBytes);
				//sensorid is not in the array
				String[] values = messageReceived.Split(new Char[]{','});
				GameObject.Find("Text KR").GetComponent<TextMesh>().text += values[1];
				Debug.Log("message receivec: " + messageReceived.Substring(2));
			}
			i++;
		}
		clientSocket.Close();
	}
}