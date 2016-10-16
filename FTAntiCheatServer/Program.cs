using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace FTAntiCheatLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServerChannel channel = new HttpServerChannel(13101);
            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Core),
                "FTAntiCheat.soap", WellKnownObjectMode.Singleton);

            while (true)
            {
                if (Console.ReadLine() == "status")
                {
                    
                    
                }
            }
        }
    }
}
