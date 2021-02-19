using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elipgo.SuperZapatos.ApiSuperZapatos.DTO
{
    public class ResponseArticles
        : ResponseGeneral
    {
        [JsonProperty("total_elements")]
        public long TotalElements
        {
            get { return Articles.Count; }
        }

        public IList<Article> Articles { get; set; }

    }
}
