using System;
using Xbehave;

namespace BddShop.Tests.Features.Home.Showroom
{
    public class ShowroomFeatureTests
    {
        [Scenario(DisplayName = "PresentShowroomViewModel")]
        public void PresentShowroomSection()
        {
            $"Given current tenant is carsales"
                .x(() => throw new NotImplementedException());
            $"And I have an instance of request bus"
                .x(() => throw new NotImplementedException());
            $"When I send request for showroom view model"
                .x(() => throw new NotImplementedException());
            $"Then I should receive a showroom view model"
                .x(() => throw new NotImplementedException());
            $"And view model should match as expected"
                .x(() => throw new NotImplementedException());
        }
    }
}
