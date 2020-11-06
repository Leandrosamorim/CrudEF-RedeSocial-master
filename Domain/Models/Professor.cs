using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models.Models
{
    public class Professor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "O nome deve possuir entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "O sobrenome deve possuir entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Sobrenome { get; set; }
        [Required]
        public DateTime Birth { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Foto")]
        public string ImageUri { get; set; }
    }
}
