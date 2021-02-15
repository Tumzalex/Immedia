using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationImageWeb.Models
{
    public class LocationImagesViewModel
    {
        public string LocationName { get; set; }
        public IEnumerable<string> ImageLinks { get; set; }
    }
}
