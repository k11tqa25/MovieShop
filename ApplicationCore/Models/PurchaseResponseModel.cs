using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class PurchaseResponseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string UserName { get; set; } 
        public string MovieTitle{ get; set; }
        public decimal Price{ get; set; }
        public DateTime PurchaseDate{get; set; }

    }
}
