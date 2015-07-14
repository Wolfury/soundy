using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Ninject;
using Soundy.Core.DTOs;
using Soundy.Core.Mappers;
using Soundy.Core.Repositories;
using Soundy.Data.Model;
using Soundy.Core.Common;

namespace Soundy.Web.Controllers
{
    [RoutePrefix("api/playlists")]
    public class PlaylistsController : ApiController
    {
        [Inject]
        public PlaylistsController(IRepository<Playlist> playlistRepository, IRepository<Song> songsRepository)
        {
            PlaylistRepository = playlistRepository;
            SongsRepository = songsRepository;
        }

        public IRepository<Playlist> PlaylistRepository { get; set; }
        public IRepository<Song> SongsRepository { get; set; }

        #region CRUD

        public async Task<IEnumerable<Playlist>> Get()
        {
            return (await PlaylistRepository.GetAsync(null, null, "Songs")).OrderByDescending(playlist => playlist.DateCreated);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = (await PlaylistRepository.GetAsync(x => x.Id == id, null, "Songs")).FirstOrDefault();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody]PlaylistDTO model)
        {
            PlaylistRepository.Insert(PlaylistMapper.Map(model));
            await PlaylistRepository.SaveAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, [FromBody]PlaylistDTO model)
        {
            PlaylistRepository.Update(PlaylistMapper.Map(model));
            await PlaylistRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            PlaylistRepository.Delete(id);
            await PlaylistRepository.SaveAsync();
            return Ok();
        }

        #endregion

        #region Additional


        [Route("AddSongToPlaylist/{playlistId}")]
        [HttpPost]
        public async Task<IHttpActionResult> AddSongToPlaylist([FromUri]int playlistId, [FromBody] SongDTO songDto)
        {
            Playlist playlist = (await PlaylistRepository.GetAsync(x => x.Id == playlistId, null, "Songs")).FirstOrDefault();
            if (playlist != null)
            {
                Song song = await SongsRepository.GetAsync(songDto.Id);
                if (song != null)
                {
                    if (playlist.Songs.Any(x => x.Id == song.Id))
                    {
                        return Ok();
                    }
                    playlist.Songs.Add(song);
                };
                await PlaylistRepository.SaveAsync();
                return Ok();
            }
            return NotFound();
        }


        [Route("RemoveSongFromPlaylist/{playlistId}")]
        [HttpPost]
        public async Task<IHttpActionResult> RemoveSongFromPlaylist([FromUri]int playlistId, [FromBody] SongDTO songDto)
        {
            Playlist playlist = (await PlaylistRepository.GetAsync(x => x.Id == playlistId, null, "Songs")).FirstOrDefault();
            if (playlist != null)
            {
                Song song = await SongsRepository.GetAsync(songDto.Id);
                if (song != null)
                {
                    if (playlist.Songs.Any(x => x.Id == song.Id))
                    {
                        return Ok();
                    }
                    playlist.Songs.Remove(song);
                };
                await PlaylistRepository.SaveAsync();
                return Ok();
            }
            return NotFound();
        }
     
        [HttpGet]
        public Task<IEnumerable<Song>> Shuffle([FromUri]int id)
        {
            return Task.Run(async () => FisherYates.Shuffle<Song>(((await PlaylistRepository.GetAsync(x => x.Id == id, null, "Songs")).FirstOrDefault()).Songs.ToArray<Song>()));
        }

        #endregion
    }
}
