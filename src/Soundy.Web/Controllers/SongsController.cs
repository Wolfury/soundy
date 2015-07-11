using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Ninject;
using Soundy.Core.DTOs;
using Soundy.Core.Mappers;
using Soundy.Core.Repositories;
using Soundy.Data.Model;
using WebGrease.Css.Extensions;
using Soundy.Core.Common;
using Newtonsoft.Json;

namespace Soundy.Web.Controllers
{
    [RoutePrefix("api/songs")]
    public class SongsController : ApiController
    {
        [Inject]
        public SongsController(IRepository<Song> songRepository)
        {
            SongRepository = songRepository;
        }

        public IRepository<Song> SongRepository { get; set; }

        #region CRUD

        public async Task<IEnumerable<Song>> Get()
        {
            return (await SongRepository.GetAsync(null, null, "Author")).OrderByDescending(song => song.DateReleased);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await SongRepository.GetAsync(id);
            if (result != null)
            {
                return Ok(SongMapper.Map(result));
            }
            return NotFound();
        }


        public async Task<IHttpActionResult> Create(CreateSongDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SongRepository.Insert(SongMapper.Map(model));
            await SongRepository.SaveAsync();
            return Ok();
        }


        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, [FromBody]SongDTO model)
        {
            SongRepository.Update(SongMapper.Map(model));
            await SongRepository.SaveAsync();
            return Ok();
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            SongRepository.Delete(id);
            await SongRepository.SaveAsync();
            return Ok();
        }
        #endregion

        [HttpGet]
        public Task<ICollection<SongDTO>> Shuffle()
        {
            return Task.Run(async () => SongMapper.Map(FisherYates.Shuffle<Song>(((await SongRepository.GetAsync()).ToArray<Song>()))));
        }

        [HttpGet]
        public async Task<IHttpActionResult> Search([FromUri]string searchTerm)
        {
            var viewModel = await SongRepository.GetAsync(x => x.Title.Contains(searchTerm) ||
                                                               x.Author.FullName.Contains(searchTerm) ||
                                                               x.Playlists.Any(p => p.Title.Contains(searchTerm)));
            return Ok(viewModel);
        }
    }
}
