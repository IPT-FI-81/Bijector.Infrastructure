using System;
using System.Linq;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Consul;

namespace Bijector.Infrastructure.Discovery
{
    public static class ConsulExtensions
    {
        public static void AddConsul(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConsulOptions>(configuration.GetSection("ConsulOptions"));
            services.AddTransient<IConsulClient, ConsulClient>(
                (provider) => new ConsulClient(consulConfig =>
                {
                    var address = configuration.GetSection("ConsulOptions").GetValue<string>("ConsulAdress");
                    consulConfig.Address = new Uri(address);                                            
                },
                (http) => {},
                (httpHandler) => {
                    var login = configuration.GetSection("ConsulOptions").GetValue<string>("ConsulUsername");
                    var pass = configuration.GetSection("ConsulOptions").GetValue<string>("ConsulPassword");
                    
                    if(!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(pass))
                        httpHandler.Credentials = new NetworkCredential(login, pass);
                }));
        }

        public static void AddConsulDiscover(this IServiceCollection services)
        {
            services.AddTransient<IServiceDiscover, ConsulDiscover>();
        }

        public static void UseConsul(this IApplicationBuilder builder, IHostApplicationLifetime lifetime)
        {
            var client = builder.ApplicationServices.GetService<IConsulClient>();
            var config = builder.ApplicationServices.GetService<IOptions<ConsulOptions>>().Value;

            var features = builder.Properties["server.Features"] as FeatureCollection;
            var addresses = features.Get<IServerAddressesFeature>();
            var address = addresses.Addresses.First();

            var uri = new Uri(address); 
            var id = $"{config.ServiceName}-{uri.Port}-{Guid.NewGuid()}";
            var tags = config.Tags.Append(config.ServiceName).ToArray();

            var check = new AgentServiceCheck()
            {                
                HTTP = $"{uri.AbsoluteUri}health/status",
                Interval = new TimeSpan(0,0,10),
                Status = HealthStatus.Passing,
                Timeout = new TimeSpan(0,0,30),
                TLSSkipVerify = true,
                DeregisterCriticalServiceAfter = new TimeSpan(0,1,0)
            };
            
            var registration = new AgentServiceRegistration()
            {
                ID = id,
                Name = config.ServiceName,
                
                Address = $"{uri.Host}",
                Port = uri.Port,
                Tags = tags/*,
                Check = check*/
            };            
            
            client.Agent.ServiceDeregister(id);
            client.Agent.ServiceRegister(registration);

            lifetime.ApplicationStopping.Register(()=>
            {
                client.Agent.ServiceDeregister(id);
            });
        }
    }
}