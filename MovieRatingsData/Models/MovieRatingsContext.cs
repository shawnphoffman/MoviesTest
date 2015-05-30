namespace MovieRatingsData.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MovieRatingsContext : DbContext
    {
        public MovieRatingsContext()
            : base("name=MovieRatingsData.Model.MovieRatingsContext")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
