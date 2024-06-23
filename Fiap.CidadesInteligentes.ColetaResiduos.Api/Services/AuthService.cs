using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Handlers;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public class AuthService : IAuthService
    {
        public readonly IUserRepository _repository;
        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }
        public UserModel? Authenticate(string username, string password)
        {
            var user = _repository.FindUserByEmail(username);
            if (user != null)
            {
                if(user.Password == password) 
                {
                    return user;
                } else
                {
                    return null;
                }
            } else
            {
                return null;
            }
        }
    }
}
