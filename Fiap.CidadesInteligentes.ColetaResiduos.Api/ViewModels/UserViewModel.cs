﻿using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Password é obrigatório")]
        [Display(Name = "Password")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "O campo Password deve ter no mínimo {2} e no máximo {1} caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo Role é obrigatório")]
        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}
