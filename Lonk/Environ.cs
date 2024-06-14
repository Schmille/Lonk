namespace Lonk
{
    public static class Environ
    {
        public static string Host { get => Environment.GetEnvironmentVariable("Host") ?? string.Empty; }
        public static string DbPath { get => Environment.GetEnvironmentVariable("DbPath") ?? ".\\lonk.db"; }
    }
}
