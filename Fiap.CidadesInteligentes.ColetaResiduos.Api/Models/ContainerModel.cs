namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Models
{
    public class ContainerModel
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public double Capacity { get; set; }
        public int CurrentLevel { get; set; }
    }
}
