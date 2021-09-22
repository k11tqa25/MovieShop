using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IPurchaseService
    {
        Task<PurchaseResponseModel> MakePurchaseAsync(int userId, int movieId);

        Task<List<PurchaseResponseModel>> GetAllPurchasesAsync();
    }
}
