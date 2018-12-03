using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistsBackend.Entities
{
    public class Performance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Host { get; set; }
        public List<Part> Parts { get; set; } = new List<Part>();
        public string Composer { get; set; }
        public Double Minutes { get; set; }
        public string Description { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}
