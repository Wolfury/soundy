using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundy.Data.Model
{
    public class Author
    {
        public Author()
        {
            Songs = new HashSet<Song>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }

        public string ProfilePhotoUrl { get; set; }

        public ICollection<Song> Songs { get; set; }
    }
}
