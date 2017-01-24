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

            return Microsoft.CodeAnalysis.CSharp.CommandLine.Program.Main(new string[0], csharpArgs);
        }
    }
}
