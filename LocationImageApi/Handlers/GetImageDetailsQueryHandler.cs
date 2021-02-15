using LocationImageApi.Models;
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
    public class GetImageDetailsQueryHandler : IRequestHandler<GetImageDetailsQuery, string>
    {
        private readonly ILocationImageService locationImageService;

        public GetImageDetailsQueryHandler(ILocationImageService locationImageService)
        {
            this.locationImageService = locationImageService;
        }

        public Task<string> Handle(GetImageDetailsQuery request, CancellationToken cancellationToken)
        {
            return locationImageService.GetImageMetadataAsync(request.Uri);
        }
    }
}
