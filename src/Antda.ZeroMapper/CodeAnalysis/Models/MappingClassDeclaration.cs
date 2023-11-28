using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.CodeAnalysis.Models;

public class MappingClassDeclaration
{
  public MappingClassDeclaration(MappingClassSymbol symbol, IReadOnlyCollection<MappingMethodDeclaration> methodDeclarations)
  {
    Symbol = symbol;
    MethodDeclarations = methodDeclarations;
  }

  public bool IsStatic => Symbol.TargetSymbol.IsStatic;

  public string Name => Symbol.TargetSymbol.Name;

  public MappingClassSymbol Symbol { get; }

  public IReadOnlyCollection<MappingMethodDeclaration> MethodDeclarations { get; }
}