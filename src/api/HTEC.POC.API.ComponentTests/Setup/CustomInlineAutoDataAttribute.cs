using AutoFixture.Xunit2;

namespace HTEC.POC.API.ComponentTests;

public class CustomInlineAutoDataAttribute : InlineAutoDataAttribute
{
    public CustomInlineAutoDataAttribute(params object[] values)
        : base(new CustomAutoDataAttribute(), values)
    {
    }
}
