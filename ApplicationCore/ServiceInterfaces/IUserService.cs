using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUserAsync(UserRegisterRequestModel userRegisterResponseModel);

        Task<UserLoginResponseModel> LoginAsync(string email, string password);

        Task<UserResponseModel> GetUserByIdAsync(int id);

        Task<List<UserResponseModel>> GetAllUsersAsync();

        Task<List<ReviewResponseModel>> GetUserReviewsAsync(int id);

        Task<List<FavoriteResponseModel>> GetUserFavoritesAsync(int id);

        Task<List<PurchaseResponseModel>> GetUserPurchasesAsync(int id);

        Task<PurchaseResponseModel>  AddPurchaseAsync(PurchaseRequestModel purchaseModel);

        Task<ReviewResponseModel> AddReviewAsync(ReviewRequestModel reviewModel);

        Task<FavoriteResponseModel> AddFavoriteAsync(FavoriteRequestModel favoriteModel);

        Task<ReviewResponseModel> UpdateReviewAsync(ReviewRequestModel reviewModel);

        Task<bool> RemoveFavoriteAsync(FavoriteRequestModel favoriteModel);

        Task<bool> RemoveReviewAsync(int userId, int movieId);

    }
}
