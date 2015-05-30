using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MovieRatingsData.Models.RottenTomatoes
{
    [DataContract]
    public class RottenMoviesResponse
    {
        [DataMember(Name="movies")]
        public RottenMovie[] movies { get; set; }
    }

    [DataContract]
    public class RottenMovie
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember(Name = "ratings.audience_score")]
        public int rating { get; set; }

        [DataMember]
        public string synopsis { get; set; }
    }
}