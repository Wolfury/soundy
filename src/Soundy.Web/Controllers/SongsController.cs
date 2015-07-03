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

        public Task<ICollection<SongDTO>> Get()
        {
            return Task.Run(async () => SongMapper.Map(await SongRepository.GetAsync()));
        }

        public async Task<SongDTO> Get(int id)
        {
            return SongMapper.Map(await SongRepository.GetAsync(id));
        }

        public async Task<IHttpActionResult> Post([FromBody]SongDTO model)
        {
            SongRepository.Insert(SongMapper.Map(model));
            await SongRepository.SaveAsync();
            return Ok();
        }

        public async Task<IHttpActionResult> Put(int id, [FromBody]SongDTO model)
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
    }
}
