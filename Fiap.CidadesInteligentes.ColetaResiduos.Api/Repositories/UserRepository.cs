using Fiap.CidadesInteligentes.ColetaResiduos.Api.Context;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public class UserRepository : GenericRepository<UserModel>, IGenericRepository<UserModel>
    {
        public UserRepository(DatabaseContext context) : base(context) { }

        public void ActivateDeactivateUser(UserModel user)
        {
            if (user != null)
            {
                if (user.IsActive)
                {
                    user.IsActive = false;
                }
                else
                {
                    user.IsActive = true;
                }
                Update(user);
            }
        }

        public UserModel? FindUserByEmail(string email)
        {
            return FindOneBy(u => u.Email.Equals(email));
        }

        public UserModel? FindUserById(long userId)
        {
            return FindOneBy(u => u.Id == userId);
        }
    }
}
