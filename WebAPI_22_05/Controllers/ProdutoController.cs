﻿
using Negocios;
using Negocios.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI_22_05.Controllers
{
    public class ProdutoController : ApiController
    {
        private ProductService _produtctService = new ProductService();
        

        //GET api/values
       public ProdutoResponse Get(int id)
       {
           return _produtctService.GetProduto(id);
        }

        // GET api/values/5
        public List<ProdutoResponse> Get()
        {
            return _produtctService.GetListProduct();
        }

        // POST api/values
        public int Post([FromBody] ProdutoRequest produto)
        {
     
           return _produtctService.SalvarProduto(produto);
        }

        // PUT api/values/5
        public void Put([FromBody] ProdutoRequest produto)
        {
            _produtctService.AtualizarProduto(produto);
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
           return _produtctService.ExcluirProduto(id);
        }
    }
}
