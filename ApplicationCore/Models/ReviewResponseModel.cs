using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class ReviewResponseModel
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string MovieTitle{ get; set; }
        public string UserName{ get; set; }
        public string Review{ get; set; }
        public decimal Rating { get; set; }
    }
}
