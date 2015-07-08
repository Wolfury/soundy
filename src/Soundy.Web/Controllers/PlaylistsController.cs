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
    public class PlaylistsController : ApiController
    {
        [Inject]
        public PlaylistsController(IRepository<Playlist> playlistRepository)
        {
            PlaylistRepository = playlistRepository;
        }

        public IRepository<Playlist> PlaylistRepository { get; set; }

        #region CRUD

        public Task<ICollection<PlaylistDTO>> Get()
        {
            return Task.Run(async () => PlaylistMapper.Map(await PlaylistRepository.GetAsync()));
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await PlaylistRepository.GetAsync(id);
            if (result != null)
            {
                return Ok(PlaylistMapper.Map(result));
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Create([FromBody]PlaylistDTO model)
        {
            PlaylistRepository.Insert(PlaylistMapper.Map(model));
            await PlaylistRepository.SaveAsync();
            return Ok();
        }

        public async Task<IHttpActionResult> Update(int id, [FromBody]PlaylistDTO model)
        {
            PlaylistRepository.Update(PlaylistMapper.Map(model));
            await PlaylistRepository.SaveAsync();
            return Ok();
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            PlaylistRepository.Delete(id);
            await PlaylistRepository.SaveAsync();
            return Ok();
        }

        #endregion

        #region Additional

        [HttpGet]
        public async Task<IHttpActionResult> AddSongToPlaylist(int playlistId, [FromBody] SongDTO song)
        {
            Playlist playlist = await PlaylistRepository.GetAsync(playlistId);
            if (playlist != null)
            {
                playlist.Songs.Add(SongMapper.Map(song));
                await PlaylistRepository.SaveAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IHttpActionResult> RemoveSongFromPlaylist(int playlistId, [FromBody] SongDTO song)
        {
            Playlist playlist = await PlaylistRepository.GetAsync(playlistId);
            if (playlist != null)
            {
                playlist.Songs.Remove(SongMapper.Map(song));
                await PlaylistRepository.SaveAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        public Task<ICollection<SongDTO>> Shuffle([FromUri]int id, [FromBody] PlaylistDTO playlist)
        {
            return Task.Run(() => SongMapper.Map(FisherYates.Shuffle<Song>(((PlaylistMapper.Map(playlist).Songs).ToArray<Song>()))));
        }

        #endregion
    }
}
