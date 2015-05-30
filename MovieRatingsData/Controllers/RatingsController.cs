using MovieRatingsData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using MovieRatingsData.Models.DTOs;
using System.Web;

namespace MovieRatingsData.Controllers
{
    /// <summary>
    /// Basic REST implementation. In a TRUE system, I would consider adding useful metadata similar to what is described
    /// in the following article => http://www.strathweb.com/2012/06/extending-your-asp-net-web-api-responses-with-useful-metadata/
    /// </summary>
    public class RatingsController : ApiController
    {
        MovieRatingsContext db = new MovieRatingsContext();

        // GET api/ratings
        public IEnumerable<object> Get()
        {
            var ratings = db.MovieRatings.Select(x => new {
                x.ID, 
                x.MovieName,
                x.RottenRating,
                x.RottenSynopsis,
                x.User.Username,
                x.UserSummary,
                x.SubmissionDate
            });
            return ratings;
        }

        // GET api/ratings/5
        public object Get(int id)
        {
            var rating = db.MovieRatings.Where(y => y.ID == id).Select(x => new
            {
                x.ID,
                x.MovieName,
                x.RottenRating,
                x.RottenSynopsis,
                x.User.Username,
                x.UserSummary,
                x.SubmissionDate
            }).FirstOrDefault();
            if (rating == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No movie rating with ID = {0}", id)),
                    ReasonPhrase = "Rating ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
            return rating;
        }

        // POST api/ratings
        public HttpResponseMessage Post([FromBody]MovieRatingDTO rating)
        {
            if (rating.UseRottenRatingOverride.HasValue)
            {
                return PersistMovieRating(new MovieRating()
                {
                    MovieName = rating.MovieName,
                    RottenRating = rating.RottenRating.Value,
                    RottenSynopsis = rating.RottenSynopsis,
                    User = db.Users.FirstOrDefault(x => x.Username == rating.Username), // <= Odd implementation. Assuming the UI isn't connected to "Users" even though it normally would be.
                    UserRating = rating.UseRottenRatingOverride.Value ? rating.RottenRating.Value : rating.UserRating,
                    UserSummary = rating.UserSummary
                });
            }
            else {

                if (string.IsNullOrWhiteSpace(rating.MovieName))
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        ReasonPhrase = "Invalid movie name."
                    };
                }

                AddRottenDetails(ref rating);

                /* Modify one of the rating systems to conform to the other. RT uses scores but doesn't provide it in their API. They
                   provide access to their "tomatometer" percentages though. Research will need to be done to see if they are comparable. */

                //if (rating.UserRating < rating.RottenRating.Value - 0.3 || ratin.UserRating > rottenRating.Value + 0.3)
                
                //Return the rating object back to the user in the response with the new Rotten Tomatoes data.
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = "Would user like to replace their rating with the available Rotten Tomatoes rating?"
                };
            }

            return new HttpResponseMessage(HttpStatusCode.OK);

        }
            
        

        // PUT api/ratings/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/ratings/5
        public void Delete(int id)
        {
        }

        private HttpResponseMessage PersistMovieRating(MovieRating rating)
        {
            try
            {
                rating.SubmissionDate = DateTime.Now;
                db.MovieRatings.Add(rating);
                db.SaveChanges();

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    ReasonPhrase = ex.Message ?? "Something went wrong."
                };
            }
            
        }

        private void AddRottenDetails(ref MovieRatingDTO rating)
        {

            var url = string.Format("http://api.rottentomatoes.com/api/public/v1.0/movies.json?apikey=9txsnh3qkb5ufnphhqv5tv5z&q={0}&page_limit=1", HttpUtility.UrlEncode(rating.MovieName));

            using (var client = new WebClient())
            {
                var json = client.DownloadString(url);
                
            }
        }

    }
}
