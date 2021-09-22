using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Entities
{
    [Table("Crew")]
    public class Crew
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        [MaxLength(2084)]
        public string ProfilePath { get; set; }

        public ICollection<MovieCrew> MovieCrews { get; set; }


    }
}
