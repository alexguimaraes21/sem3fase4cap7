using System.ComponentModel.DataAnnotations;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.ViewModels
{
    public class TruckViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo License Plate é obrigatório")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "O campo License Plate deve conter {1} caracteres")]
        [Display(Name = "License Plate")]
        public string LicensePlate { get; set; }

        [Required(ErrorMessage = "Campo Capacity é obrigatório")]
        [Display(Name = "Capacity")]
        public double Capacity { get; set; }

        [Required(ErrorMessage = "Campo Available é obrigatório")]
        [Display(Name = "Available")]
        public bool Available { get; set; }
    }
}
