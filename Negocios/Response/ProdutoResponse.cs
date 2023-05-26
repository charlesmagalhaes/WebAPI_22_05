
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negocios.Models
{
    public class ProdutoResponse
    {

            public int Id { get; set; }
            public string Nome { get; set; }
            public string Descricao { get; set; }

        public static explicit operator ProdutoResponse(ProdutoModel source)
        {
            return new ProdutoResponse()
            {
                Id = source.Id,
                Nome = source.Nome,
                Descricao = source.Descricao
            
            };
        }

    }
}