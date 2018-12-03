using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistsBackend.Models
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string AddressLine { get; set; }

        public string Building { get; set; }

        public string City { get; set; }

        public int FloorLevel { get; set; }
    }
}
