
using Negocios;
using Negocios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_22_05.Models;

namespace WebAPI_22_05.Controllers
{
    public class ValuesController : ApiController
    {
        private ProductService _produtctService = new ProductService();
        

        // GET api/values
       // public IEnumerable<string> Get()
      //  {
       //     return new string[] { "value1", "value2" };
      //  }

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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _produtctService.ExcluirProduto(id);
        }
    }
}
