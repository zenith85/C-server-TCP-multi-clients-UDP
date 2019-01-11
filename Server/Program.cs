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

namespace server
{
	class Program
	{
		public static void Main(string[] args)
		{

			ConnectionManager ThreadingC= new ConnectionManager();
			Thread manager = new Thread(new ThreadStart(ThreadingC.ServerInitiate));
			manager.Start();
			
			while(true){
				Console.WriteLine("1. Send UDP msg");
				Console.WriteLine("Please input a value!");
				string input=Console.ReadLine();
				if (input=="1"){
					Console.WriteLine("you picked 1");
					ConnectionManager.Broadcaster();
				}
				else if (input =="2"){
					Console.WriteLine("you picked 2");
				}
			}			
		}
		public static void UDPlisteningDynamicThreading(object Registration)
		{
			Console.WriteLine(Registration);
			UDPSR Threading=new UDPSR();
			Thread receivingthread=new Thread(Threading.UDPreceive);
			receivingthread.Start(Registration);
			
		}
	}
}
