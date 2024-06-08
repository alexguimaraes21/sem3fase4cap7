using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public interface IAuthService
    {
        UserModel Authenticate(string username, string password);
    }
}
