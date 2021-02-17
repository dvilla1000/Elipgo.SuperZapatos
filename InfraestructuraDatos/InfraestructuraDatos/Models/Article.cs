using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Elipgo.SuperZapatos.InfraestructuraDatos.Models
{
    [Table("Articles")]
    public class Article
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("name"), Required, MaxLength(400)]
        public string Name { get; set; }
        [Column("description"), MaxLength(1000)]
        public string Description { get; set; }

        [Column("price"), Required]
        public double Price { get; set; }

        [Column("total_in_shelf")]
        public double TotalInShelf { get; set; }

        [Column("total_in_vault")]
        public double TotalInVault { get; set; }   

        [ForeignKey("storeId")]
        public long StoreId { get; set; }
        public Store Store { get; set; }


    }
}
