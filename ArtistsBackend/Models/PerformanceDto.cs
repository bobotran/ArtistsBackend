using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistsBackend.Models
{
    public class PerformanceDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Host { get; set; }
        public List<PartDto> Parts { get; set; } = new List<PartDto>();
        public string Composer { get; set; }
        public Double Minutes { get; set; }
        public string Description { get; set; }


    }
}
