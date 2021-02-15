using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationImageApi.Models
{
    public class ImageMetadata
    {
        public string Description { get; set; }
        public string Copyright { get; set; }
        public string Artist { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
