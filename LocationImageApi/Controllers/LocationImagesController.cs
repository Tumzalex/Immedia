using LocationImageApi.Models;
using LocationImageApi.Queries;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LocationImageApi.Controllers
{
    [Route("api/[controller]")]
    public class LocationImagesController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMediator mediator;

        public LocationImagesController(IWebHostEnvironment webHostEnvironment, IMediator mediator)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.mediator = mediator;
        }

        [HttpPost("locations")]
        public async Task<IEnumerable<string>> Get([FromBody] GetLocationsQuery getLocationsQuery)
        {
            var results = await mediator.Send(getLocationsQuery);

            return results;
        }

        [HttpPost("images")]
        public async Task<IEnumerable<Uri>> Get([FromBody] GetImagesOfLocationQuery getImagesOfLocationQuery)
        {
            return await mediator.Send(getImagesOfLocationQuery);
        }

        [HttpPost("image/details")]
        public async Task<string> Get([FromBody] GetImageDetailsQuery getImageDetailsQuery)
        {
            return await mediator.Send(getImageDetailsQuery);
        }
    }
}
