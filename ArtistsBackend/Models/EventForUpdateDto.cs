using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistsBackend.Models
{
    public class EventForUpdateDto
    {
        public string DressCode { get; set; }
        [Required]
        public string Nickname { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public AddressDto Address { get; set; }
    }
}
