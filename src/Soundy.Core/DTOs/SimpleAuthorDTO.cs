using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundy.Core.DTOs
{
    public class SimpleAuthorDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfilePhotoUrl { get; set; }
    }
}
