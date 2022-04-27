﻿using AutoFixture.Xunit2;

namespace HTEC.Engagement.API.ComponentTests
{
    public class CustomInlineAutoDataAttribute : InlineAutoDataAttribute
    {
        public CustomInlineAutoDataAttribute(params object[] values)
            : base(new CustomAutoDataAttribute(), values)
        {
        }
    }
}
