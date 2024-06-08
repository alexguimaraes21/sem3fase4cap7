namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Models
{
    public class TruckModel
    {
        public long Id { get; set; }
        public string LicensePlate { get; set; }
        public double Capacity { get; set; }
        public bool Available { get; set; }
    }
}
