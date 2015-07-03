using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soundy.Data.Model;

namespace Soundy.Data.Context
{
    public class SoundyDb : DbContext
    {
        public SoundyDb()
            : base()
        {

        }

        public SoundyDb(string connectionString)
            : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Playlist>()
                .HasMany(x => x.Songs)
                .WithMany(x => x.Playlists)
                .Map(x => x.ToTable("PlaylistsSongs")
                    .MapLeftKey("PlaylistId")
                    .MapRightKey("SongId"));
        }


        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
