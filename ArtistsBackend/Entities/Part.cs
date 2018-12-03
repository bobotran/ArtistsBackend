using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistsBackend.Entities
{
    public class Part
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PartName { get; set; }

        public int UserId { get; set; }


        [ForeignKey("PerformanceId")]
        public Performance Performance { get; set; }
        public int PerformanceId { get; set; }
    }
}
