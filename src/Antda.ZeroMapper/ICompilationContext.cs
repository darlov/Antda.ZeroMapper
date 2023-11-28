using Antda.ZeroMapper.CodeAnalysis;
using Antda.ZeroMapper.Memory;
using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper;

public interface ICompilationContext
{
  INamedTypeSymbol? GetTypeByMetadataName(string fullyQualifiedMetadataName);

  ISyntaxAnalyzer SyntaxAnalyzer { get; }
  
  ISymbolHelper SymbolHelper { get; }

  IMemoryCache MemoryCache { get; }
}