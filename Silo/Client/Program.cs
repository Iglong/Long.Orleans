using GrainInterfaces;
using Orleans;
using Orleans.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Model;

namespace Client
{
    class Program
    {
        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await ConnectClient())
                {
                    await DoClientWork(client);
                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nException while trying to run client: {e.Message}");
                Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;
            client = new ClientBuilder()
                .UseStaticClustering(new System.Net.IPEndPoint[] {
                    new System.Net.IPEndPoint(IPAddress.Parse("192.168.3.17"),30000)
                })
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                })
                .ConfigureLogging(logger => logger.AddConsole())
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connected to silo host \n");
            return client;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            // example of calling grains from the initialized client
            var friend = client.GetGrain<IChat>(1110);//           
            ChatCallback chatCallback = new ChatCallback();

            var observer = await client.CreateObjectReference<IChatCallback>(chatCallback);
            await friend.Subscribe(observer);
            await friend.SayHello("Good morning, HelloGrain!");
            Console.WriteLine("你说：\n\n{0}\n\n", "Good morning, HelloGrain!");
            var gather = client.GetGrain<IGather>("dfg");
            var responseGather = await gather.HydrologyData(new Model.RequestModel<object> { Origin = new PassportModel() });
            Console.WriteLine($"{JsonConvert.SerializeObject(responseGather)}");
        }
    }
}
