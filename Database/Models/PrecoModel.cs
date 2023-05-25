using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class PrecoModel
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public decimal _Preco { get; set; }
        public DateTime Data { get; set; }
    }
}
