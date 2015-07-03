using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soundy.Data.Model;

namespace Soundy.Core.DTOs
{
    public class SongDTO
    {
        public SongDTO()
        {
            Playlists = new Collection<PlaylistDTO>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public string FileUrl { get; set; }
        public AuthorDTO Author { get; set; }
        public DateTime DateReleased { get; set; }

        public ICollection<PlaylistDTO> Playlists { get; set; }
    }
}
