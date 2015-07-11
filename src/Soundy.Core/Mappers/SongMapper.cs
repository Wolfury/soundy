using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soundy.Core.DTOs;
using Soundy.Data.Model;

namespace Soundy.Core.Mappers
{
    public static class SongMapper
    {
        public static SongDTO Map(Song input)
        {
            return new SongDTO
            {
                Id = input.Id,
                Title = input.Title,
                CoverUrl = input.CoverUrl,
                FileUrl = input.FileUrl,
                DateReleased = input.DateReleased,
                Playlists = PlaylistMapper.Map(input.Playlists),
                Author = AuthorMapper.Map(input.Author)
            };
        }
        public static ICollection<SongDTO> Map(IEnumerable<Song> input)
        {
            return input.Select(x => new SongDTO()
            {
                Id = x.Id,
                Title = x.Title,
                CoverUrl = x.CoverUrl,
                FileUrl = x.FileUrl,
                DateReleased = x.DateReleased,
                Playlists = PlaylistMapper.Map(x.Playlists)
            }).ToList<SongDTO>();
        }

        public static Song Map(SongDTO input)
        {
            return new Song
            {
                Title = input.Title,
                CoverUrl = input.CoverUrl,
                FileUrl = input.FileUrl,
                DateReleased = DateTime.Now,
                Author = AuthorMapper.Map(input.Author)
            };
        }

        public static Song Map(CreateSongDTO input)
        {
            return new Song
            {
                Title = input.Title,
                CoverUrl = input.CoverUrl,
                FileUrl = input.FileUrl,
                DateReleased = DateTime.Now,
                AuthorId = input.AuthorId
            };
        }
        public static ICollection<Song> Map(IEnumerable<SongDTO> input)
        {
            return input.Select(x => new Song()
            {
                Id = x.Id,
                Title = x.Title,
                CoverUrl = x.CoverUrl,
                FileUrl = x.FileUrl,
                DateReleased = DateTime.Now
            }).ToList<Song>();
        }
    }
}
