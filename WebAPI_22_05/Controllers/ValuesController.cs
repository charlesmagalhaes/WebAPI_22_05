
using Negocios;
using Negocios.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace WebAPI_22_05.Controllers
{
    public class ValuesController : ApiController
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
        public void Post([FromBody] ProdutoRequest produto)
        {
     
            _produtctService.SalvarProduto(produto);
        }

        // PUT api/values/5
        public void Put([FromBody] ProdutoRequest produto)
        {
            _produtctService.AtualizarProduto(produto);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _produtctService.ExcluirProduto(id);
        }
    }
}
