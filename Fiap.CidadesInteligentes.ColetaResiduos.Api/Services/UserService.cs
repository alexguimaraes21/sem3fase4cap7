using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void ActivateDeactivateUser(UserModel model)
        {
            _userRepository.ActivateDeactivateUser(model);
        }

        public void Add(UserModel model)
        {
            _userRepository.Add(model);
        }

        public void Delete(long id)
        {
            _userRepository.Delete(u => u.Id == id);
        }

        public IEnumerable<UserModel> FindAll(int page = 1, int pageSize = 10)
        {
            return _userRepository.FindAll(page, pageSize);
        }

        public UserModel? FindByEmail(string email)
        {
            return _userRepository.FindUserByEmail(email);
        }

        public UserModel? FindById(long id)
        {
            return _userRepository.FindUserById(id);
        }

        public void Update(UserModel model)
        {
            _userRepository.Update(model);
        }
    }
}
