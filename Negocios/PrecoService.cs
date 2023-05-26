using Database;
using Database.Models;
using Negocios.Interfaces;
using Negocios.Models;
using System;
using System.Collections.Generic;

namespace Negocios.Response
{
    public class PrecoService : IPrecoService

    {
        private readonly  DataAccessLayerPreco _dataAccessLayerPreco = new DataAccessLayerPreco();

        public PrecoService()
        {
           
        }

        public void Atualizar(PrecoResponse preco)
        {
            throw new System.NotImplementedException();
        }

        public void Excluir(int id)
        {
            _dataAccessLayerPreco.ExcluirPreco(id);
        }

        public PrecoResponse Get(int id)
        {
            var resp = _dataAccessLayerPreco.GetPrecotById(id);

            if (resp == null)
            {
                return new PrecoResponse();
            }

            return (PrecoResponse)resp;
        }



        public List<PrecoResponse> GetLista()
        {
            List<PrecoModel> listaModel = _dataAccessLayerPreco.GetListPreco();
            List<PrecoResponse> listaResponse = new List<PrecoResponse>();

            foreach (PrecoModel preco in listaModel)
            {
                PrecoResponse resp = (PrecoResponse)preco;
                listaResponse.Add(resp);
            }
            return listaResponse;
        }

        public void Salvar(PrecoResponse preco)
        {
            throw new System.NotImplementedException();
        }

        // Métodos da classe PrecoService

    }
}

