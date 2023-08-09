using Negocios.Interfaces;
using Negocios.Models;
using Negocios.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAPI_22_05.Controllers
{
    public class PrecoController : ApiController
    {
        private readonly IPrecoService _precoService;

        public PrecoController()
        {
            _precoService = new PrecoService();
        }

        //GET api/values
        public IEnumerable<PrecoResponse> Get()
        {

            return _precoService.GetLista();
        }

        //GET api/values
        public PrecoResponse Get(int id)
        {
            return _precoService.Get(id);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _precoService.Excluir(id);
        }

    }
}