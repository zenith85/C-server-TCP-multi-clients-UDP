using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace server
{
	/// <summary>
	/// Description of ConnectionManager.
	/// </summary>
	class ConnectionManager
	{
		public static List<string> ASendingPortsTable= new List<string>();
		public static List<string> AReceivingPortsTable= new List<string>();
		public static List<string> ClientsTable = new List<string>();
		public void ServerInitiate()
		{
			PortsManager.PreparePortsSending(ASendingPortsTable); //ask the ports manager to generate available udp ports
			PortsManager.PreparePortsReceiving(AReceivingPortsTable); //ak the ports manager to generate available udp ports
			int port =14000; //server port
			string IpAddress=GetLocalIPAddress();
			Socket ServerListener=new Socket(AddressFamily
			                                 .InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint ep=new IPEndPoint(IPAddress.Parse(IpAddress), port);
			try {
				ServerListener.Bind(ep);
				ServerListener.Listen(100);
				Console.WriteLine("Server is listening..");
				Socket ClientSocket = default(Socket);
				Console.WriteLine(ClientSocket);
				ConnectionManager p = new ConnectionManager();
				
				while(true)
				{
					ClientSocket=ServerListener.Accept();
					Thread UserThread= new Thread(new ThreadStart(()=>p.User(ClientSocket)));
					UserThread.Start();
				}
			}catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}
		public void User(Socket client)
		{
			while(true){
				try {
				byte[] msgs=new byte[1024];
				int size=client.Receive(msgs);
				string messagercvd=System.Text.Encoding.ASCII.GetString(msgs,0,size);
				Console.WriteLine(messagercvd);//debug line ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
				
				if (messagercvd.Contains("Name:"))
				{
					string CIP=messagercvd.Substring(26);
					string CSport=PortsManager.PickaPort(ASendingPortsTable);
					string CRport=PortsManager.PickaPort(AReceivingPortsTable);
					
					string Regestration=CIP+CSport+CRport;
					object RegestrationO=Regestration;
					Program.UDPlisteningDynamicThreading(Regestration);
					
					UpdateClientsTable(ClientsTable,CIP,CSport,CRport);
					string portmsg="port:"+CSport+CRport;
					Console.WriteLine("port:"+CSport+","+CRport);//debug line ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
					client.Send(System.Text.Encoding.ASCII.GetBytes(portmsg),0,portmsg.Length,SocketFlags.None);
					//sending the udp port number to the client to connect to udp broadcasting
					return;
				}
				}catch(Exception e){
					Console.WriteLine(e);
				}
				
			}
		}
		public static string GetLocalIPAddress()
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}
			throw new Exception("No network adapters with an IPv4 address in the system!");
		}
		public static IList<string> UpdateClientsTable(List<string> ClientsTable,string CIP, string CSport, string CRport)
		{
			ClientsTable.Add(CIP+":"+CSport+":"+CRport);
			//debug ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			foreach (string o in ClientsTable)
			{
				Console.WriteLine(o);
			}
			//debug ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
			return ClientsTable;
		}
		
		public static void Broadcaster()
		{
			foreach (string o in ClientsTable)
			{
				string ip=o.Substring(0,15);
				string Sport=o.Substring(16,4);
				string Rport=o.Substring(21,4);
				UDPSR.UDPsend(ip,Sport,Rport);
				
			}
		}
		public static void UDPBroadCastCmsg(string Cmsg)
		{
			foreach (string o in ClientsTable)
			{
				string ip=o.Substring(0,15);
				string Sport=o.Substring(16,4);
				string Rport=o.Substring(21,4);
				UDPSR.UDPsendCmsg(ip,Sport,Rport,Cmsg);
				
			}
		}
	}
	
}

