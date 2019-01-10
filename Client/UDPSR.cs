using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace Client
{
	/// <summary>
	/// Description of UDPSR.
	/// </summary>
	class UDPSR
	{
		public static void UDPsend(int port)
		{
			Console.WriteLine("argument passed to us with "+Program.UDPRport+" and "+Program.UDPSport);
			//UdpClient socket = new UdpClient(Program.UDPSport);
			UdpClient socket = new UdpClient(port);			
			//IPEndPoint target = new IPEndPoint(IPAddress.Parse("192.168.219.152"), Program.UDPSport);
			IPEndPoint target = new IPEndPoint(IPAddress.Parse("192.168.219.152"), port);
			byte[] message = Encoding.ASCII.GetBytes("hello there?");
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
			if (msgstring.Contains(Program.GetLocalIPAddress()))
			{
				Console.WriteLine(msgstring + " bytes from " + source);				
			} else 
			{
				Console.WriteLine("this is an echo");				
			}
			// schedule the next receive operation once reading is done:
			socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
			//socket.Close();
			
		}
			public void UDPreceive(object port)
		{
			//UdpClient socket = new UdpClient(Program.UDPRport);
			UdpClient socket = new UdpClient(Int32.Parse(port.ToString()));
			// schedule the first receive operation:
			socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
			//IPEndPoint target = new IPEndPoint(IPAddress.Parse("192.168.219.152"), Program.UDPRport);
			IPEndPoint target = new IPEndPoint(IPAddress.Parse("192.168.219.152"), Int32.Parse(port.ToString()));
			byte[] message = Encoding.ASCII.GetBytes("hello there?");
			socket.SendAsync(message,message.Length,target);
			return;
		}
	}
}
