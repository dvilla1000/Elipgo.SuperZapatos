using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Elipgo.SuperZapatos.Dominio.Entities
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("name"), Required, MaxLength(100)]
        public string Name { get; set; }
        [Column("apellido_paterno"), Required, MaxLength(100)]
        public string ApellidoPaterno { get; set; }
        [Column("apellido_materno"), Required, MaxLength(100)]
        public string ApellidoMaterno { get; set; }
        [Column("email"), Required, MaxLength(100)]
        public string Email { get; set; }
        [Column("password"), Required, MaxLength(40)]
        public string Password { get; set; }
        public ICollection<Rol> Roles { get; set; }
    }
}
