using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Illisian.Network;
using Illisian.Network.TransportStream;

namespace Illisian.Niva
{
	public class Server
	{

		public int ListenPort = 56777;

		SimpleServer _server;

		public void Initialise()
		{
			_server = new SimpleServer(ListenPort);
			_server.OnDataReceivedEvent += new SimpleServer.OnDataReceivedDelegate(_server_OnDataReceivedEvent);
			_server.OnClientConnectedEvent += new SimpleServer.OnClientConnectedDelegate(_server_OnClientConnectedEvent);
			_server.OnClientDisconnectEvent += new SimpleServer.OnClientDisconnectDelegate(_server_OnClientDisconnectEvent);

			_server.Start();
		}

		void _server_OnClientDisconnectEvent(Guid clientId)
		{
			
		}

		void _server_OnClientConnectedEvent(Guid clientId)
		{
			
		}

		void _server_OnDataReceivedEvent(Guid clientId, byte[] data)
		{
			
		}



	}
}
