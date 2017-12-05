using System;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace remoteServer
{
	public class Server : MarshalByRefObject, RemotingInterface.RemoteInterfaceString
	{
		static void Main()
		{
			// Create a TCP channel to transfer data
			TcpChannel canal = new TcpChannel(12345);

			// register channel
			ChannelServices.RegisterChannel(canal);

			// Start server listning in a Singleton object
			RemotingConfiguration.RegisterWellKnownServiceType(
				typeof(Server), "Server",  WellKnownObjectMode.Singleton);

			// keep console alive
			Console.WriteLine("Server started");
            Console.ReadLine();	
		}

		// Remove server timeout
		public override object  InitializeLifetimeService()
		{
			return null;
		}
		

		#region members of RemoteInterfaceString

		public string Hello()
		{
			// TODO : implement
			return "The string was received in the server" ;
		}

		#endregion
	}
}
