using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LocationImageApi.Queries
{
    public class GetImagesOfLocationQuery: IRequest<IEnumerable<Uri>>
    {
        public string Location { get; set; }
    }
}
