using System.ComponentModel.DataAnnotations.Schema;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }

        public UserModel()
        {
            CreatedAt = DateTime.Now;
            IsActive = true;
            Role = "User";
        }
    }
}
