using Antda.ZeroMapper.CodeAnalysis.Models;

namespace Antda.ZeroMapper.CodeAnalysis;

public interface IMethodAnalyzer
{
  IEnumerable<MappingMethodDeclaration> GetMethods(MappingClassSymbol symbol, CancellationToken cancellationToken);
}