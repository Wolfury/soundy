using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundy.Data.Model
{
    public class Playlist
    {
        public Playlist()
        {
            Songs = new HashSet<Song>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DateCreated { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}
