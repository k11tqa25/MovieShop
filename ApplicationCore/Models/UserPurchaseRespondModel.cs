using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class UserPurchasesRespondModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<Purchases> Purchases { get; set; }
    }

    public class Purchases
    {
        public decimal Price { get; set; }
        public string MovieTitle { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
