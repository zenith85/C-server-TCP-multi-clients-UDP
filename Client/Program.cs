/*
 * Created by SharpDevelop.
 * User: TheSacredHaven
 * Date: 1/2/2019
 * Time: 12:10 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
	class Program
	{
		public static int isStarting=1; 
		public static int  port=14000;
		public static int  UDPSport;
		public static int  UDPRport;
		public static void PhaseOne()
		{
			string ClientIPAddress=GetLocalIPAddress();
			string serverIpAddress="192.168.219.152";
			Socket ClientSocket=new Socket(AddressFamily
			                                 .InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			IPEndPoint ep=new IPEndPoint(IPAddress.Parse(serverIpAddress), port);
			ClientSocket.Connect(ep);
			Console.WriteLine("Client is connected");
			while(true)
			{
				if (isStarting==1)
				{
					string messageFromClient=null;
					messageFromClient=Regestration(ClientIPAddress);
					isStarting=0;
					ClientSocket.Send(System.Text.Encoding.ASCII.GetBytes(messageFromClient),0,messageFromClient.Length,SocketFlags.None);
					//return;
				} else 
				{
					byte[] MsgFromServer = new byte[1024];
					int size=ClientSocket.Receive(MsgFromServer);
					string MsgFromServerString=System.Text.Encoding.ASCII.GetString(MsgFromServer,0,size);
					Console.WriteLine("Server "+MsgFromServerString); //Debug ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~``
					if (System.Text.Encoding.ASCII.GetString(MsgFromServer,0,size).Contains("port:"))
					{
						UDPSport=Int32.Parse(System.Text.Encoding.ASCII.GetString(MsgFromServer,0,size).Substring(5,4));
						UDPRport=Int32.Parse(System.Text.Encoding.ASCII.GetString(MsgFromServer,0,size).Substring(9,4));
						Console.WriteLine("our chosen port is : "+UDPSport+","+UDPRport);//Debug ~~~~~~~~~~~~~~~~~~~~~
						object UDPSportO=UDPSport;
						UDPSR Threading = new UDPSR();
						Thread receivethread = new Thread(Threading.UDPreceive);
						receivethread.Start(UDPSportO);
						
						return;
					}
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
		public static string Regestration(string ip)
		{
			string regmsg="Name:"+",msgtype:Instentiate"+":"+ip;
				return regmsg;
		}
	
		public static void Main(string[] args)
		{
			PhaseOne();
			//Threading for parallel computation
			while(true){
				Console.WriteLine("1. Send UDP msg");
				string input=Console.ReadLine();
				if (input=="1"){
					Console.WriteLine("you picked 1");
					UDPSR.UDPsend(UDPRport);					
				}
			}
		}
	}
}
