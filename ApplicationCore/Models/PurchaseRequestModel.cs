using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class PurchaseRequestModel
    {
        public int UserId { get; set; }
        public int? PurchaseNumber { get; set; }
        public decimal? TotalPrice { get; set; }
        public int MovieId { get; set; }
    }
}
