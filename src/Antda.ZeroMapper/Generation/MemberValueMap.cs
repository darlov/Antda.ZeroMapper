namespace Antda.ZeroMapper.Generation;

public class MemberValueMap(IValueProvider from, IValueSetter to)
{
  public IValueProvider From { get; } = from;

  public IValueSetter To { get; } = to;
}