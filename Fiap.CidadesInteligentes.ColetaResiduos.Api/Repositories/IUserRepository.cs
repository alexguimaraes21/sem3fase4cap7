using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
        void ActivateDeactivateUser(UserModel user);
        UserModel? FindUserByEmail(string email);
    }
}
