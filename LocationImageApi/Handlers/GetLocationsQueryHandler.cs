using LocationImageApi.Queries;
using LocationImageApi.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocationImageApi.Handlers
{
    public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, IEnumerable<string>>
    {
        private readonly ILocationImageService locationImageService; 
        
        public GetLocationsQueryHandler(ILocationImageService locationImageService)
        {
            this.locationImageService = locationImageService;
        }

        public async Task<IEnumerable<string>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            return await locationImageService.GetLocations();
        }
    }
}
