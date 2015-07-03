using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soundy.Core.DTOs;
using Soundy.Data.Model;

namespace Soundy.Core.Mappers
{
    public static class AuthorMapper
    {
        public static AuthorDTO Map(Author input)
        {
            return new AuthorDTO
            {
                Id = input.Id,
                FullName = input.FullName,
                ProfilePhotoUrl = input.ProfilePhotoUrl,
                BirthDate = input.BirthDate,
                Songs = SongMapper.Map(input.Songs)
            };
        }
        public static ICollection<AuthorDTO> Map(IEnumerable<Author> input)
        {
            return input.Select(x => new AuthorDTO()
            {
                Id = x.Id,
                FullName = x.FullName,
                ProfilePhotoUrl = x.ProfilePhotoUrl,
                BirthDate = x.BirthDate,
                Songs = SongMapper.Map(x.Songs)
            }).ToList<AuthorDTO>();
        }

        public static Author Map(AuthorDTO input)
        {
            return new Author
            {
                FullName = input.FullName,
                ProfilePhotoUrl = input.ProfilePhotoUrl,
                BirthDate = input.BirthDate
            };
        }
        public static ICollection<Author> Map(IEnumerable<AuthorDTO> input)
        {
            return input.Select(x => new Author()
            {
                FullName = x.FullName,
                ProfilePhotoUrl = x.ProfilePhotoUrl,
                BirthDate = x.BirthDate
            }).ToList<Author>();
        }
    }
}
