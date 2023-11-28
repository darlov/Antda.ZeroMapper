using Antda.ZeroMapper.CodeAnalysis;
using Antda.ZeroMapper.Extensions;
using Antda.ZeroMapper.Memory;
using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper;

public  class CompilationContext : ICompilationContext
{
  private readonly Compilation _compilation;

  public CompilationContext(Compilation compilation)
  {
    _compilation = compilation;
    SyntaxAnalyzer = new SyntaxAnalyzer(this, new MethodAnalyzer(this));
    MemoryCache = new MemoryCache();
    SymbolHelper = new SymbolHelper(this, MemoryCache);

  }

  public INamedTypeSymbol? GetTypeByMetadataName(string fullyQualifiedMetadataName) 
    => _compilation.GetBestTypeByMetadataName(fullyQualifiedMetadataName);

  public ISyntaxAnalyzer SyntaxAnalyzer { get; }

  public ISymbolHelper SymbolHelper { get; }
  public IMemoryCache MemoryCache { get; }
}
