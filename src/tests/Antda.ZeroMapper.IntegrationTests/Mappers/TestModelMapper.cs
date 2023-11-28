using Antda.ZeroMapper.Abstractions;
using Antda.ZeroMapper.IntegrationTests.Models;

namespace Antda.ZeroMapper.IntegrationTests.Mappers;

public class TestModelMapper2
{
  [Mapper]
  public partial class TestModelMapper
  {
    public string MapToSS { get; set; }

    public partial TestModel MapToModel(TestModelDb from, [MapTo(nameof(TestModel.StringValue))] string newVal);

  }
}
