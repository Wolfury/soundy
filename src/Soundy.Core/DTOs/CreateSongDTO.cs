using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundy.Core.DTOs
{
    public class CreateSongDTO
    {
        public string Title { get; set; }
        public string CoverUrl { get; set; }
        public string FileUrl { get; set; }
        public int AuthorId { get; set; }
    }
}
