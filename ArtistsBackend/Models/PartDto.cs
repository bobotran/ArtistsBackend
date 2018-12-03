using ArtistsBackend.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArtistsBackend.Models
{
    public class PartDto
    {
        public int Id { get; set; }
        public string PartName { get; set; }

        public UserDto User { get; set; }
    }
}
