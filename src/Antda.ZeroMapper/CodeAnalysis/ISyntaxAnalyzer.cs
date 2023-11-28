using Antda.ZeroMapper.CodeAnalysis.Models;

namespace Antda.ZeroMapper.CodeAnalysis;

public interface ISyntaxAnalyzer
{
  MappingClassDeclaration? BuildGenerationUnit(MappingClassSymbol symbol, CancellationToken cancellationToken);
}