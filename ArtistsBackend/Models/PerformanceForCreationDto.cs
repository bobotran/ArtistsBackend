using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistsBackend.Models
{
    public class PerformanceForCreationDto
    {
        [Required]
        public string Title { get; set; }
        public string Host { get; set; }
        public List<PartDto> Parts { get; set; } = new List<PartDto>();
        [Required]
        public string Composer { get; set; }
        public Double Minutes { get; set; }
        public string Description { get; set; }
    }
}
