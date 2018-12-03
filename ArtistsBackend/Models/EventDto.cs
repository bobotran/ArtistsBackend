using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ArtistsBackend.Models
{
    public class EventDto
    {
        public int Id { get; set; }
        public string DressCode { get; set; }
        public string Nickname { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public AddressDto Address { get; set; }

        public List<PerformanceDto> Performances { get; set; } = new List<PerformanceDto>();
    }
}