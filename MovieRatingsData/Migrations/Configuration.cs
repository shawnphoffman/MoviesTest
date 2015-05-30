namespace MovieRatingsData.Migrations
{
    using MovieRatingsData.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MovieRatingsData.Models.MovieRatingsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "MovieRatingsData.Models.MovieRatingsContext";
        }

        protected override void Seed(MovieRatingsData.Models.MovieRatingsContext context)
        {
            var users = new List<User>
            {
                new User{ Username = "shawnhoffman" },
                new User{ Username = "test2" },
                new User{ Username = "test3" },
                new User{ Username = "test4" },
            };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var ratings = new List<MovieRating>
            {
                new MovieRating{ MovieName = "Test Movie", RottenRating = 1, RottenSynopsis = "Rotten synopsis", SubmissionDate = DateTime.Now, User = users[0], UserRating = 2, UserSummary = "User summary" },
                new MovieRating{ MovieName = "Test Movie", RottenRating = 1, RottenSynopsis = "Rotten synopsis", SubmissionDate = DateTime.Now, User = users[0], UserRating = 2, UserSummary = "User summary" },
                new MovieRating{ MovieName = "Test Movie", RottenRating = 1, RottenSynopsis = "Rotten synopsis", SubmissionDate = DateTime.Now, User = users[0], UserRating = 2, UserSummary = "User summary" },
                new MovieRating{ MovieName = "Test Movie", RottenRating = 1, RottenSynopsis = "Rotten synopsis", SubmissionDate = DateTime.Now, User = users[0], UserRating = 2, UserSummary = "User summary" }
            };

            ratings.ForEach(s => context.MovieRatings.Add(s));
            context.SaveChanges();
        }
    }
}
