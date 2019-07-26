using Grains;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Orleans.Storage;
using Microsoft.Extensions.Hosting;
using System.Net.Sockets;
using Orleans;
using Orleans.Runtime;
using System.Linq;

namespace Silo
{
    class Program
    {
        public static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("\n\n Press Enter to terminate...\n\n");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            var invariant = "System.Data.SqlClient"; // for Microsoft SQL Server
            var connectionString = @"Data Source=.;Initial Catalog=Orleans;User Id=sa;Password=123456;Integrated Security=True;Pooling=False;Max Pool Size=200;Asynchronous Processing=True;MultipleActiveResultSets=True";
            var siloPort = GetAvailablePort(11111, 11119);
            var gatewayPort = GetAvailablePort(30000, 30009);
            var healthCheckPort = GetAvailablePort(8880, 8889);
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                 //监听的主silo 远程连接点 为空则创建一个主silo连接点
                 .UseDevelopmentClustering(new IPEndPoint(IPAddress.Parse("192.168.3.17"), 11111))
                 .Configure<ClusterOptions>(options =>
                 {
                     options.ClusterId = "dev";
                     options.ServiceId = "OrleansBasics";
                 }).ConfigureLogging(logger => logger.AddConsole())
                 .Configure<AdoNetClusteringSiloOptions>(options => { options.ConnectionString = connectionString; options.Invariant = invariant; })
                 .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Parse("192.168.3.17"))
             .Configure<EndpointOptions>(options =>
              {
                  //这里的IP决定了是本机 还是内网 还是公网
                  options.AdvertisedIPAddress = IPAddress.Parse("192.168.3.17");
                  //监听的端口
                  options.SiloPort = 11111;
                  //监听的网关端口
                  options.GatewayPort = 30000;
                  //监听的silo 远程连接点
                  options.GatewayListeningEndpoint = new IPEndPoint(options.AdvertisedIPAddress, options.GatewayPort);
                  //监听的silo 远程端口连接点
                  options.SiloListeningEndpoint = new IPEndPoint(options.AdvertisedIPAddress, options.SiloPort);
              })
             .ConfigureServices(service =>
              {
                  service.AddHealthChecks()
                         .AddCheck<GrainHealthCheck>("GrainHealth")
                         .AddCheck<SiloHealthCheck>("SiloHealth")
                         .AddCheck<StorageHealthCheck>("StorageHealth")
                         .AddCheck<ClusterHealthCheck>("ClusterHealth");
                  service.AddSingleton<IHealthCheckPublisher, LoggingHealthCheckPublisher>()
                          .Configure<HealthCheckPublisherOptions>(options =>
                          {
                              options.Period = TimeSpan.FromSeconds(1);
                          });
                  service.Configure<ConsoleLifetimeOptions>(options =>
                          {
                              options.SuppressStatusMessages = true;
                          });
                  service.Configure<HealthCheckHostedServiceOptions>(options =>
                             {
                                 options.Port = healthCheckPort;
                                 options.PathString = "/health";
                             });
                  service.AddLogging(logger => logger.AddConsole());
              });   
            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
        public static int GetAvailablePort(int start, int end)
        {
            for (var port = start; port < end; ++port)
            {
                var listener = TcpListener.Create(port);
                listener.ExclusiveAddressUse = true;
                try
                {
                    listener.Start();
                    return port;
                }
                catch (SocketException)
                {
                    continue;
                }
                finally
                {
                    listener.Stop();
                }
            }

            throw new InvalidOperationException();
        }
    }
}
