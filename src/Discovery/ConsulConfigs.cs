namespace Bijector.Infrastructure.Discovery
{
    public class ConsulOptions
    {
        public string ServiceName { get; set; }

        public string[] Tags { get; set; }

        public string ServiceAddress { get; set; }

        public string ConsulAdress { get; set; }

        public string ConsulUsername { get; set; }

        public string ConsulPassword { get; set; }
    }
}