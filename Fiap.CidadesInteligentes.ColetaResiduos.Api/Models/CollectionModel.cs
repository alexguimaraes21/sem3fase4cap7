namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Models
{
    public class CollectionModel
    {
        public long Id { get; set; }
        public DateTime DateTime { get; set; }
        public long ContainerId { get; set; }
        public ContainerModel Container { get; set; }
        public long RouteId { get; set; }
        public RouteModel Route { get; set; }
    }
}
