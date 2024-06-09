using System.ComponentModel.DataAnnotations;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.ViewModels
{
    public class ContainerViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Campo Location é obrigatório")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Campo Location deve ter no mínimo {2} e no máximo {1} caracteres")]
        [Display(Name = "Location")]

        public string Location { get; set; }
        
        [Required(ErrorMessage = "Campo Capacity é obrigatório")]
        [Display(Name = "Capacity")]
        public double Capacity { get; set; }

        [Required(ErrorMessage = "Campo CurrentLevel é obrigatório")]
        [Display(Name = "CurrentLevel")]
        public int CurrentLevel { get; set; }
    }
}
