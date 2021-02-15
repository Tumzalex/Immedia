using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationImageApi.Queries
{
    public class GetLocationsQuery: IRequest<IEnumerable<string>>
    {

    }
}
