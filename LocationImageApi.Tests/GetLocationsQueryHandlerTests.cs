using LocationImageApi.Handlers;
using LocationImageApi.Queries;
using LocationImageApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LocationImageApi.Tests
{
    public class GetLocationsQueryHandlerTests
    {
        Mock<ILocationImageService> locationImageServiceMock;
        public GetLocationsQueryHandlerTests()
        {
            locationImageServiceMock = new Mock<ILocationImageService>();
        }

        [Fact]
        public async Task GetLocations_EmptyLocationImages_ReturnsEmptyListAsync()
        {
            locationImageServiceMock
                .Setup(x => x.GetLocations())
                .Returns(Task.FromResult<IEnumerable<string>>(new List<string>()));
            
            var getLocationsQuery = new GetLocationsQuery { };
            var sut = new GetLocationsQueryHandler(locationImageServiceMock.Object);

            var result = await sut.Handle(getLocationsQuery, default);

            Assert.Equal(await Task.FromResult<IEnumerable<string>>(new List<string>()), result);
        }
    }
}
