using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocios.Models
{
    public class PrecoRequst

    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public decimal _Preco { get; set; }
        public DateTime Data { get; set; }


    }

}