namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.ResponseModels
{
    public class UserResponseModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
    }
}
