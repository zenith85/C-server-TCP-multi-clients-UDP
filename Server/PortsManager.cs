using System;
using System.Collections.Generic;
using System.Linq;

namespace server
{
	/// <summary>
	/// Description of PortsManager.
	/// </summary>
	public class PortsManager
	{
		public static void PreparePortsSending(List<string> AvailableSPorts)
		{
			for (int i = 5390; i<5490; i++)
			{
				AvailableSPorts.Add(i.ToString());
			}
			return;
		}
		public static void PreparePortsReceiving(List<string> AvailableRPorts)
		{
			for (int i = 5491; i<5591; i++)
			{
				AvailableRPorts.Add(i.ToString());
			}
			return;
		}
		public static string PickaPort(List<string> AvailablePorts)
		{
			string PickaPort=AvailablePorts.ElementAt(new Random().Next(1,99));
			AvailablePorts.Remove(PickaPort);
			return PickaPort;
		}
		public static void AddPort(List<string> AvailablePorts, string PortAddIn)
		{
			AvailablePorts.Add(PortAddIn);
			return;
		}
		public static void RemovePorts(List<string> AvailablePorts, string PortPullout)
		{
			AvailablePorts.Remove(PortPullout);
			return;
		}
	}
}
