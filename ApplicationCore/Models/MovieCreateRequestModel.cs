using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Models
{
    public class MovieCreateRequestModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(2084)]
        public string Overview { get; set; }
        [MaxLength(2084)]
        public string Tagline { get; set; }

        [RegularExpression(@"^(\d{1,18})(.\d{1}?$")]
        public decimal? Revenue { get; set; }
        public decimal? Budget { get; set; }
        public string ImdbUrl { get; set; }
        public string TmdbUrl { get; set; }
        public string PosterUrl { get; set; }
        public string BackdropUrl { get; set; }
        public string OriginalLanguage { get; set; }
        public DateTime ReleaseTime { get; set; }
        public int RunTime{ get; set; }
        public decimal? Price{ get; set; }
        public List<GenreModel> Genres{ get; set; }
    }
}
