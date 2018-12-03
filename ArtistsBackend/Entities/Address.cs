using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistsBackend.Entities
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AddressLine { get; set; }

        public string Building { get; set; }

        public string City { get; set; }

        public int FloorLevel { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}
