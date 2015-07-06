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

namespace Soundy.Web.Controllers
{
    public class SongsController : ApiController
    {
        [Inject]
        public SongsController(IRepository<Song> songRepository)
        {
            SongRepository = songRepository;
        }

        public IRepository<Song> SongRepository { get; set; }

        #region CRUD

        public Task<ICollection<SongDTO>> Get()
        {
            return Task.Run(async () => SongMapper.Map(await SongRepository.GetAsync()));
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

        public async Task<IHttpActionResult> Create([FromBody]SongDTO model)
        {
            SongRepository.Insert(SongMapper.Map(model));
            await SongRepository.SaveAsync();
            return Ok();
        }

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
        public async Task<IHttpActionResult> Search([FromUri]string searchTerm)
        {
            var viewModel = await SongRepository.GetAsync(x => x.Title.Contains(searchTerm) ||
                                                               x.Author.FullName.Contains(searchTerm) ||
                                                               x.Playlists.Any(p => p.Title.Contains(searchTerm)));
            return Ok(viewModel);
        }
    }
}
