using ArtistApplication.Domain.Identity;
using ArtistApplication.Repository;
using ArtistApplication.Repository.Interface;
using ArtistApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string GetUsernameById(string userId)
        {
            var user = _userRepository.Get(userId);
            return user != null ? user.UserName : "Unknown";
        }

        public IEnumerable<ArtistApplicationUser> GetUsersByIds(IEnumerable<string> userIds)
        {
            return _userRepository.GetUsersByIds(userIds);
        }
    }
}
