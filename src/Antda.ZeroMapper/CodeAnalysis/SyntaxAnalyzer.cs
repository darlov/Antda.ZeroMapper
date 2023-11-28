using Antda.ZeroMapper.CodeAnalysis.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Antda.ZeroMapper.CodeAnalysis;

public class SyntaxAnalyzer : ISyntaxAnalyzer
{
  private readonly ICompilationContext _context;
  private readonly IMethodAnalyzer _methodAnalyzer;

  public SyntaxAnalyzer(ICompilationContext context, IMethodAnalyzer methodAnalyzer)
  {
    _context = context;
    _methodAnalyzer = methodAnalyzer;
  }

  public MappingClassDeclaration? BuildGenerationUnit(MappingClassSymbol symbol, CancellationToken cancellationToken)
  {
    if (IsClassValid(symbol))
    {
      var methods = _methodAnalyzer.GetMethods(symbol, cancellationToken).ToList();

      if (methods.Any())
      {
        return new MappingClassDeclaration(symbol, methods);
      }
    }

    return null;
  }

  private bool IsClassValid(MappingClassSymbol symbol)
  {
    if (symbol.TargetNode.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)))
    {
      return true;
    }
    
    // TODO: Error: Class is not partial

    return false;
  }
}