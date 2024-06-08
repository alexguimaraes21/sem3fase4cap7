using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public interface IUserService : IGenericService<UserModel>
    {
        UserModel FindByEmail(string email);
        void ActivateDeactivateUser(UserModel model);
    }
}
