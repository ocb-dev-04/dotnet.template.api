namespace SharedSources.Errors
{
   public static class ExceptionsBuilder
    {
        public static string Build(int statusCode, string message) => string.Format("{0}, {1}", statusCode, message);
    }
}