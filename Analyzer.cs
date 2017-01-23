using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading;

namespace PromiseNotToEnqueueRepro
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class Analyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(new DiagnosticDescriptor("TEST0001", "Test", "Test", "Test", DiagnosticSeverity.Warning, isEnabledByDefault: true));

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterCompilationAction(OnCompilation);

            // Set a breakpoint in Microsoft.CodeAnalysis.CSharp.Symbols.SourceMemberFieldSymbol.FixedSized at the call to DeclaringCompilation.SymbolDeclaredEvent before continuing
            // When that breakpoint is hit, freeze the thread and disable the breakpoint.
            Debugger.Break();
        }

        private static void OnCompilation(CompilationAnalysisContext context)
        {
            // Need to ensure that the thread we are racing with finished its work before we run
            Thread.Sleep(500);

            context.Compilation.GetDiagnostics(context.CancellationToken);

            // Unfreeze the thread that was frozen above
            Debugger.Break();
        }
    }
}
