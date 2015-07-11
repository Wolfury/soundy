using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Soundy.Data.Context;
using Soundy.Data.Model;

namespace Soundy.Web.Controllers
{
    public class SongsEfController : ApiController
    {
        private SoundyDb db = new SoundyDb();

        // GET: api/SongsEf
        public IQueryable<Song> GetSongs()
        {
            return db.Songs;
        }

        // GET: api/SongsEf/5
        [ResponseType(typeof(Song))]
        public async Task<IHttpActionResult> GetSong(int id)
        {
            Song song = await db.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        // PUT: api/SongsEf/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSong(int id, Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != song.Id)
            {
                return BadRequest();
            }

            db.Entry(song).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/SongsEf
        [ResponseType(typeof(Song))]
        public async Task<IHttpActionResult> PostSong(Song song)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Songs.Add(song);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = song.Id }, song);
        }

        // DELETE: api/SongsEf/5
        [ResponseType(typeof(Song))]
        public async Task<IHttpActionResult> DeleteSong(int id)
        {
            Song song = await db.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            db.Songs.Remove(song);
            await db.SaveChangesAsync();

            return Ok(song);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SongExists(int id)
        {
            return db.Songs.Count(e => e.Id == id) > 0;
        }
    }
}