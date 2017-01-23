namespace PromiseNotToEnqueueRepro
{
    public static class Program
    {
        static int Main(string[] args)
        {
            string[] csharpArgs = new[]
            {
                "/target:library",
                $"/analyzer:{typeof(Analyzer).Assembly.Location}",
                "test.cs"
            };

            int result = 0;
            do
            {
                result = Microsoft.CodeAnalysis.CSharp.CommandLine.Program.Main(new string[0], csharpArgs);
            } while (result == 0);
            return result;
        }
    }
}
