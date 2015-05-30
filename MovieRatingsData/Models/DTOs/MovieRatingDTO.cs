using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieRatingsData.Models.DTOs
{
    public class MovieRatingDTO
    {
        public string MovieName { get; set; }
        public int UserRating { get; set; }
        public string UserSummary { get; set; }
        public int? RottenRating { get; set; }
        public string RottenSynopsis { get; set; }
        public string Username { get; set; }
        public bool? UseRottenRatingOverride { get; set; }
    }
}