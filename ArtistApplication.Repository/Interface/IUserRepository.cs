using ArtistApplication.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistApplication.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<ArtistApplicationUser> GetAll();
        ArtistApplicationUser Get(string id);
        void Insert(ArtistApplicationUser entity);
        void Update(ArtistApplicationUser entity);
        void Delete(ArtistApplicationUser entity);
    }
}
