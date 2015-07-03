using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundy.Core.DTOs
{
    public class PlaylistDTO
    {
        public PlaylistDTO()
        {
            Songs = new Collection<SongDTO>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<SongDTO> Songs { get; set; }
    }
}
