using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System;
using ApplicationCore.Entities;
using System.Collections.Generic;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IMovieRepository movieRepository, IReviewRepository reviewRepository, IFavoriteRepository favoriteRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _movieRepository = movieRepository;
            _reviewRepository = reviewRepository;
            _favoriteRepository = favoriteRepository;
        }

        public async Task<List<UserResponseModel>> GetAllUsersAsync()
        {
            var users = await _userRepository.ListAllAsync();
            var response = new List<UserResponseModel>();
            foreach (var user in users)
            {
                response.Add(new UserResponseModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth
                });
            }
            return response;

        }

        public async Task<UserResponseModel> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var userResponseModel = new UserResponseModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };
            return userResponseModel;
        }

        public async Task<List<ReviewResponseModel>> GetUserReviewsAsync(int id)
        {
            var user = await _userRepository.GetUserReviewsAsync(id);
            var response = new List<ReviewResponseModel>();
            foreach (var review in user.Reviews)
            {
                response.Add(new ReviewResponseModel()
                {
                    MovieId = review.MovieId,
                    UserId = review.UserId,
                    MovieTitle = review.Movie.Title,
                    UserName = user.FirstName + " " + user.LastName,
                    Rating = review.Rating,
                    Review = review.ReviewText
                });

            }
            return response;
        }

        public async Task<List<PurchaseResponseModel>> GetUserPurchasesAsync(int id)
        {
            var user = await _userRepository.GetUserPurchasesAsync(id);
            var response = new List<PurchaseResponseModel>();
            foreach (var p in user.Purchases)
            {
                response.Add(new PurchaseResponseModel()
                {
                    MovieId = p.MovieId,
                    UserId = p.UserId,
                    MovieTitle = p.Movie.Title,
                    UserName = p.User.FirstName + " " + p.User.LastName,
                    Price = p.TotalPrice,
                    PurchaseDate = p.PurchaseDateTime
                });
            }
            return response;
        }

        public async Task<List<FavoriteResponseModel>> GetUserFavoritesAsync(int id)
        {
            var user = await _userRepository.GetUserFavoritesAsync(id);
            var response = new List<FavoriteResponseModel>();
            foreach (var favorite in user.Favorates)
            {
                response.Add(new FavoriteResponseModel()
                {
                    MovieId = favorite.MovieId,
                    UserId = favorite.UserId,
                    MovieTitle = favorite.Movie.Title,
                    UserName = favorite.User.FirstName + " " + favorite.User.LastName
                });
            }
            return response;
        }

        public async Task<FavoriteResponseModel> AddFavoriteAsync(FavoriteRequestModel favoriteModel)
        {
            var favorite = new Favorite()
            {
                MovieId = favoriteModel.MovieId,
                UserId = favoriteModel.UserId
            };

            var newFavorite  = await _favoriteRepository.AddAsync(favorite);
            var movie = await _movieRepository.GetByIdAsync(favoriteModel.MovieId);
            var user = await _userRepository.GetByIdAsync(favoriteModel.UserId);
            return new FavoriteResponseModel
            {
                UserId = newFavorite.UserId,
                MovieId = newFavorite.MovieId,
                MovieTitle = movie.Title,
                UserName = user.FirstName + " " + user.LastName,
            };
        }

        public async Task<PurchaseResponseModel> AddPurchaseAsync(PurchaseRequestModel purchaseModel)
        {
            var movie = await _movieRepository.GetByIdAsync(purchaseModel.MovieId);

            if (movie == null) return null;

           var newPurchase = await _purchaseRepository.AddAsync(new Purchase() { 
                MovieId = purchaseModel.MovieId,
                UserId = purchaseModel.UserId,
                PurchaseDateTime = DateTime.Now,
                TotalPrice = movie.Price.GetValueOrDefault()
            });

            return new PurchaseResponseModel
            {
                Id = newPurchase.Id,
                MovieId = newPurchase.MovieId,
                UserId = newPurchase.UserId,
                MovieTitle = newPurchase.Movie.Title,
                UserName = newPurchase.User.FirstName + " " + newPurchase.User.LastName,
                Price = newPurchase.Movie.Price.GetValueOrDefault(),
                PurchaseDate = newPurchase.PurchaseDateTime
            };
        }

        public async Task<ReviewResponseModel> AddReviewAsync(ReviewRequestModel reviewModel)
        {
            var review = new Review()
            {
                MovieId = reviewModel.MovieId,
                UserId = reviewModel.UserId,
                Rating = reviewModel.Rating,
                ReviewText = reviewModel.ReviewText
            };

            var newReview = await _reviewRepository.AddAsync(review);
            var movie = await _movieRepository.GetByIdAsync(review.MovieId);
            var user = await _userRepository.GetByIdAsync(review.UserId);

            return new ReviewResponseModel
            {
                MovieId = newReview.MovieId,
                UserId = newReview.UserId,
                Rating = newReview.Rating,
                Review = newReview.ReviewText,
                MovieTitle = movie.Title,
                UserName = user.FirstName + " " + user.LastName
            };
        }

        public async Task<ReviewResponseModel> UpdateReviewAsync(ReviewRequestModel reviewModel)
        {
            var review = new Review()
            {
                MovieId = reviewModel.MovieId,
                UserId = reviewModel.UserId,
                Rating = reviewModel.Rating,
                ReviewText = reviewModel.ReviewText
            };

            var newReview = await _reviewRepository.UpdateAsync(review);
            var movie = await _movieRepository.GetByIdAsync(review.MovieId);
            var user = await _userRepository.GetByIdAsync(review.UserId);

            return new ReviewResponseModel
            {
                MovieId = newReview.MovieId,
                UserId = newReview.UserId,
                Rating = newReview.Rating,
                Review = newReview.ReviewText,
                MovieTitle = movie.Title,
                UserName = user.FirstName + " " + user.LastName
            };
        }

        public async Task<bool> RemoveReviewAsync(int userId, int movieId)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(userId, movieId);
            await _reviewRepository.DeleteAsync(review);
            return true;
        }

        public async Task<UserLoginResponseModel> LoginAsync(string email, string password)
        {
            var dbUser = await _userRepository.GetUserByEmailAsync(email);
            if(dbUser != null)
            {
                var hashedPassword = HashPassword(password, dbUser.Salt);
                if(hashedPassword == dbUser.HashedPassword)
                {
                    // Correct password
                    var userLoginResponse = new UserLoginResponseModel
                    {
                        Id = dbUser.Id,
                        Email = dbUser.Email,
                        FirstName = dbUser.FirstName,
                        LastName = dbUser.LastName,
                        DateOfBirth = dbUser.DateOfBirth,
                        Password = dbUser.HashedPassword
                    };

                    return userLoginResponse;
                }
            }

            return null;
        }

        public async Task<UserRegisterResponseModel> RegisterUserAsync(UserRegisterRequestModel requestModel)
        {
            // 1. Make sure the email does not exists in the database User table
            var dbUser = await _userRepository.GetUserByEmailAsync(requestModel.Email);
            if(dbUser != null)
            {
                // Already have user with the same email
                throw new ConflictException("Email already exists.");
            }

            // 2. Create a unique salt for the user password
            var salt = CreateSalt();
            var hashedPassword = HashPassword(requestModel.Password, salt);

            // 3. Save to database
            var user = new User
            {
                Email = requestModel.Email,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                DateOfBirth = requestModel.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };
            var createdUser  =  await _userRepository.AddAsync(user);

            // 4. Map the createdUser to the UserRegisterResponseModel
            var userResponse = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                DateOfBirth = createdUser.DateOfBirth,
                Password = createdUser.HashedPassword,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email
            };

            return userResponse;
        }

        public async Task<bool> RemoveFavoriteAsync(FavoriteRequestModel favoriteModel)
        {
            var favorite = await _favoriteRepository.GetFavoriteAsync(favoriteModel.MovieId, favoriteModel.UserId);
            await _favoriteRepository.DeleteAsync(favorite);
            return true;
        }

        private string CreateSalt()
        {
            // Never write our own code. Use the standard. 
            // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-5.0

            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        private string HashPassword(string password, string salt)
        {
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            // This Pbkdf2 is US standard 
            // Other protocols are: Aarogon, BCrypt
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

    }
}
