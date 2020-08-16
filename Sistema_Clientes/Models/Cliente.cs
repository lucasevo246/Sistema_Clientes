using Sistema_Clientes.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Sistema_Clientes.Models
{
    public class Cliente
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Campo Nome obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Campo Tipo obrigatório")]
        [Range(1, 2, ErrorMessage = "Campo Tipo obrigatório")]
        public Tipo Tipo { get; set; }

        [Display(Name = "Documento")]
        [Required(ErrorMessage = "Campo Documento obrigatório")]
        public string Documento { get; set; }

       
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Campo telefone obrigatório")]
        public string Tel { get; set; }

        public bool Ativo { get; set; }
    }

    public class ClienteDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
    }
}