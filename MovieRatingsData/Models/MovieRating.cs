using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MovieRatingsData.Models
{
    public class MovieRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string MovieName { get; set; }
        public int UserRating { get; set; }
        public string UserSummary { get; set; }
        public int RottenRating { get; set; }
        public string RottenSynopsis { get; set; }
        public DateTime SubmissionDate { get; set; }

        public virtual User User { get; set; }
    }
}