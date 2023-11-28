using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Antda.ZeroMapper.Extensions;

/// Copy from https://github.com/dotnet/roslyn/blob/v4.2.0/src/Workspaces/SharedUtilitiesAndExtensions/Compiler/Core/Extensions/CompilationExtensions.cs
internal static class CompilationExtensions
{
  /// <summary>
  /// Gets a type by its metadata name to use for code analysis within a <see cref="Compilation"/>. This method
  /// attempts to find the "best" symbol to use for code analysis, which is the symbol matching the first of the
  /// following rules.
  ///
  /// <list type="number">
  ///   <item><description>
  ///     If only one type with the given name is found within the compilation and its referenced assemblies, that
  ///     type is returned regardless of accessibility.
  ///   </description></item>
  ///   <item><description>
  ///     If the current <paramref name="compilation"/> defines the symbol, that symbol is returned.
  ///   </description></item>
  ///   <item><description>
  ///     If exactly one referenced assembly defines the symbol in a manner that makes it visible to the current
  ///     <paramref name="compilation"/>, that symbol is returned.
  ///   </description></item>
  ///   <item><description>
  ///     Otherwise, this method returns <see langword="null"/>.
  ///   </description></item>
  /// </list>
  /// </summary>
  /// <param name="compilation">The <see cref="Compilation"/> to consider for analysis.</param>
  /// <param name="fullyQualifiedMetadataName">The fully-qualified metadata type name to find.</param>
  /// <returns>The symbol to use for code analysis; otherwise, <see langword="null"/>.</returns>
  public static INamedTypeSymbol? GetBestTypeByMetadataName(this Compilation compilation, string fullyQualifiedMetadataName)
  {
    INamedTypeSymbol? type = null;

    foreach (var currentType in compilation.GetTypesByMetadataName(fullyQualifiedMetadataName))
    {
      if (ReferenceEquals(currentType.ContainingAssembly, compilation.Assembly))
      {
        Debug.Assert(type is null);
        return currentType;
      }

      switch (currentType.GetResultantVisibility())
      {
        case SymbolVisibility.Public:
        case SymbolVisibility.Internal when currentType.ContainingAssembly.GivesAccessTo(compilation.Assembly):
          break;

        default:
          continue;
      }

      if (type != null)
      {
        // Multiple visible types with the same metadata name are present
        return null;
      }

      type = currentType;
    }

    return type;
  }
}