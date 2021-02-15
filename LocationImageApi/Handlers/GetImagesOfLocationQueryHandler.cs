using LocationImageApi.Queries;
using LocationImageApi.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocationImageApi.Handlers
{
    public class GetImagesOfLocationQueryHandler : IRequestHandler<GetImagesOfLocationQuery, IEnumerable<Uri>>
    {
        private readonly ILocationImageService locationImageService;

        public GetImagesOfLocationQueryHandler(ILocationImageService locationImageService)
        {
            this.locationImageService = locationImageService;
        }

        public Task<IEnumerable<Uri>> Handle(GetImagesOfLocationQuery request, CancellationToken cancellationToken)
        {
            return locationImageService.GetImagesAsync(request.Location);
        }
    }
}
