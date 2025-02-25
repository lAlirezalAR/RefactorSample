namespace Utilities.Framework.Config
{
    public class JaegerConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsEnabled { get; set; }
        public double SamplingRate { get; set; }
    }
}
