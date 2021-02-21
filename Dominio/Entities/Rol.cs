using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Elipgo.SuperZapatos.Dominio.Entities
{
    [Table("Roles")]
    public class Rol
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("name"), Required, MaxLength(100)]
        public string Name { get; set; }
        [Column("description"), Required, MaxLength(400)]
        public string Description { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }

    }
}
