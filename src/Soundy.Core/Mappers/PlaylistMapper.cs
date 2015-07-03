using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soundy.Core.DTOs;
using Soundy.Data.Model;

namespace Soundy.Core.Mappers
{
    public static class PlaylistMapper
    {
        public static PlaylistDTO Map(Playlist input)
        {
            return new PlaylistDTO
            {
                Id = input.Id,
                Title = input.Title,
                DateCreated = input.DateCreated,
                Songs = SongMapper.Map(input.Songs)
            };
        }
        public static ICollection<PlaylistDTO> Map(IEnumerable<Playlist> input)
        {
            return input.Select(x => new PlaylistDTO()
            {
                Id = x.Id,
                Title = x.Title,
                DateCreated = x.DateCreated,
                Songs = SongMapper.Map(x.Songs)
            }).ToList<PlaylistDTO>();
        }
        public static Playlist Map(PlaylistDTO input)
        {
            return new Playlist
            {
                Title = input.Title,
                DateCreated = DateTime.Now
            };
        }
        public static ICollection<Playlist> Map(IEnumerable<PlaylistDTO> input)
        {
            return input.Select(x => new Playlist()
            {
                Title = x.Title,
                DateCreated = DateTime.Now
            }).ToList<Playlist>();
        }
    }
}
