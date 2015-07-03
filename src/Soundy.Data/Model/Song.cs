using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundy.Data.Model
{
    public class Song
    {
        public Song()
        {
            Playlists = new HashSet<Playlist>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string CoverUrl { get; set; }
        public string FileUrl { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
        public int AuthorId { get; set; }

        public DateTime DateReleased { get; set; }

        public ICollection<Playlist> Playlists { get; set; } 

    }
}
