using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class FavoriteResponseModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string  UserName { get; set; }
        public string  MovieTitle { get; set; }
    }
}
