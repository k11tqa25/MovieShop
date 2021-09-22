using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Trailer
    {
        public int Id { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }

        // Navigation Properties
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        //
    }
}
