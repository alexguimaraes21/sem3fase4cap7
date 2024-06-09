namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.ResponseModels
{
    public class ContainerResponseModel
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public double Capacity { get; set; }
        public int CurrentLevel { get; set; }
    }
}
