using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soundy.Data.Model;

namespace Soundy.Core.DTOs
{
    public class AuthorDTO
    {
        public AuthorDTO()
        {
            Songs = new Collection<SongDTO>();
        }
        public int Id { get; set; }

        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public ICollection<SongDTO> Songs { get; set; }
    }
}
