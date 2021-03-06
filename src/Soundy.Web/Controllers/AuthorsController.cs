﻿using System;
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
    public class AuthorsController : ApiController
    {
        [Inject]
        public AuthorsController(IRepository<Author> authorRepository)
        {
            AuthorRepository = authorRepository;
        }

        public IRepository<Author> AuthorRepository { get; set; }


        public Task<ICollection<AuthorDTO>> Get()
        {
            return Task.Run(async () => AuthorMapper.Map(await AuthorRepository.GetAsync()));
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await AuthorRepository.GetAsync(id);
            if (result != null)
            {
                return Ok(AuthorMapper.Map(result));
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Create([FromBody]AuthorDTO model)
        {
            AuthorRepository.Insert(AuthorMapper.Map(model));
            await AuthorRepository.SaveAsync();
            return Ok();
        }

        public async Task<IHttpActionResult> Update(int id, [FromBody]AuthorDTO model)
        {
            AuthorRepository.Update(AuthorMapper.Map(model));
            await AuthorRepository.SaveAsync();
            return Ok();
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            AuthorRepository.Delete(id);
            await AuthorRepository.SaveAsync();
            return Ok();
        }
    }
}
