using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;
        public PurchaseService(IPurchaseRepository purchaseRepository, IMovieRepository movieRepository, IUserRepository userRepository)
        {
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
            _userRepository = userRepository;
        }

        public async Task<List<PurchaseResponseModel>> GetAllPurchasesAsync()
        {
            var purchases = await _purchaseRepository.ListAllAsync();
            var response = new List<PurchaseResponseModel>();
            foreach (var p in purchases)
            {
                response.Add(new PurchaseResponseModel()
                {
                    Id = p.Id,
                    MovieId = p.MovieId,
                    UserId = p.UserId,
                    MovieTitle = p.Movie.Title,
                    UserName = p.User.FirstName + " " + p.User.LastName,
                    PurchaseDate = p.PurchaseDateTime,
                    Price = p.TotalPrice
                });
            }

            return response;
        }

        public async Task<PurchaseResponseModel> MakePurchaseAsync(int userId, int movieId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var movie = await _movieRepository.GetByIdAsync(movieId);
            var purchase = new Purchase
            {
                MovieId = movieId,
                UserId = userId,
                PurchaseDateTime = DateTime.Now,
                TotalPrice = movie.Price.GetValueOrDefault()
            };

            var newPurchase = await _purchaseRepository.AddAsync(purchase);
            return new PurchaseResponseModel
            {
                Id = newPurchase.Id,
                MovieId = newPurchase.MovieId,
                UserId = newPurchase.UserId,
                MovieTitle = movie.Title,
                UserName = user.FirstName + " " + user.LastName,
                Price = movie.Price.GetValueOrDefault(),                
                PurchaseDate = newPurchase.PurchaseDateTime
            };
        }
    }
}
