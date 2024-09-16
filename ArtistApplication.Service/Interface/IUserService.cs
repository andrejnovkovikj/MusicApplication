using ArtistApplication.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Service.Interface
{
    public interface IUserService
    {
        string GetUsernameById(string userId);
        IEnumerable<ArtistApplicationUser> GetUsersByIds(IEnumerable<string> userIds);
    }
}
