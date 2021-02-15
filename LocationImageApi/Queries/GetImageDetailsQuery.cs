using LocationImageApi.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LocationImageApi.Queries
{
    public class GetImageDetailsQuery: IRequest<string>
    {
        public Uri Uri { get; set; }
    }
}
