using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Elipgo.SuperZapatos.AppFormSuperZapatos.Models
{
    public class ArticlesModel
    {
        public bool Success { get; set; }
        public long TotalElements { get; set; }
        public IList<Article> Articles { get; set; }
    }

    public class Article
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        [DisplayName("Total In Shelf")]
        public double TotalInShelf { get; set; }
        [DisplayName("Total In Vault")]
        public double TotalInVault { get; set; }

        public long StoreId { get; set; }
    }
}
