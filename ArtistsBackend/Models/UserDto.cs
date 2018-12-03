using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtistsBackend.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        public string EmailAddress { get; set; }

        public string Username { get; set; }
    }
}
