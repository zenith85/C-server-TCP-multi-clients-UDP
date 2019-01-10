using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace server
{
	/// <summary>
	/// Description of UDPSR.
	/// </summary>
	class UDPSR
	{
		public static void UDPsend(string ip, string Sport, string Rport)
		{
			UdpClient socket = new UdpClient(Int32.Parse(Sport)); // `new UdpClient()` to auto-pick port
			IPEndPoint target = new IPEndPoint(IPAddress.Parse(ip), Int32.Parse(Sport));
			byte[] message = Encoding.ASCII.GetBytes("hello there?");
			socket.SendAsync(message,message.Length,target);
			socket.Close();
		}
		public static void UDPsendCmsg(string ip, string Sport, string Rport, string Cmsg)
		{
			UdpClient socket = new UdpClient(Int32.Parse(Sport)); // `new UdpClient()` to auto-pick port
			IPEndPoint target = new IPEndPoint(IPAddress.Parse(ip), Int32.Parse(Sport));
			byte[] message = Encoding.ASCII.GetBytes(Cmsg);
			socket.SendAsync(message,message.Length,target);
			socket.Close();
		}
	
		
		static void OnUdpData(IAsyncResult result) {
			// this is what had been passed into BeginReceive as the second parameter:
			UdpClient socket = result.AsyncState as UdpClient;
			// points towards whoever had sent the message:
			IPEndPoint source = new IPEndPoint(0, 0);
			// get the actual message and fill out the source:
			byte[] message = socket.EndReceive(result, ref source);
			string msgstring = Encoding.ASCII.GetString(message);
			// do what you'd like with `message` here:
			Console.WriteLine(msgstring + " bytes from " + source);
			ConnectionManager.UDPBroadCastCmsg(msgstring);
			// schedule the next receive operation once reading is done:
			socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
			//socket.Close();
			
		}
			//public void UDPreceive(object CIPO,object CSportO, object CRportO)
			public void UDPreceive(object Registration)
		{
			Console.WriteLine("this is UDPreceive regestration :"+Registration);
			Console.WriteLine(Registration.ToString().Substring(0,15));
			Console.WriteLine(Registration.ToString().Substring(15,4));
			Console.WriteLine(Registration.ToString().Substring(19,4));
			string IP=Registration.ToString().Substring(0,15);
			string PortS=Registration.ToString().Substring(15,4);
			string PortR=Registration.ToString().Substring(19,4);
			
			UdpClient socket = new UdpClient(Int32.Parse(PortR));
			// schedule the first receive operation:
			socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
			IPEndPoint target = new IPEndPoint(IPAddress.Parse(IP), Int32.Parse(PortR));
			byte[] message = Encoding.ASCII.GetBytes("hello there?");
			socket.SendAsync(message,message.Length,target);
			return;
		}
	}
}
