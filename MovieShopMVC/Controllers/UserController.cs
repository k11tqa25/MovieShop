using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ICurrentUser _currentUser;
        private readonly IPurchaseService _purchaseService;

        public UserController(ICurrentUser currentUser, IPurchaseService purchaseService)
        {
            _currentUser = currentUser;
            _purchaseService = purchaseService;
        }

        public async Task<IActionResult> ConfirmPurchase(int id)
        {
            if(!_currentUser.IsAuthenticated)  return View();

            var purchaseModel = await _purchaseService.MakePurchaseAsync(_currentUser.UserId, id);
            return View(purchaseModel);
        }
    }
}
