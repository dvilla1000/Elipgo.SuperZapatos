using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Elipgo.SuperZapatos.Dominio.Entities
{
    [Table("Stores")]
    public class Store
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Column("name"), Required, MaxLength(400)]
        public string Name { get; set; }
        [Column("address"), Required, MaxLength(1000)]
        public string Address { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
