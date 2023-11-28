namespace Antda.ZeroMapper.CodeAnalysis;

public class GenerationUnit
{
    public MappingClassSymbol Symbol { get; }
    public CompilationContext Context { get; }
    public CancellationToken CancellationToken { get; }

    public GenerationUnit(MappingClassSymbol symbol, CompilationContext context, CancellationToken cancellationToken)
    {
        Symbol = symbol;
        Context = context;
        CancellationToken = cancellationToken;
    }
}