using Database;
using Database.Models;
using Negocios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class ProductService
    {
        private DataAccessLayer dataAccessLayer = new DataAccessLayer();


        public void SalvarProduto(dynamic produto)
        {

            ProdutoModel novoProduto = new ProdutoModel
            {
                Nome = produto.Nome,
                Descricao = produto.Descricao
            };
            dataAccessLayer.SalvarProduto(novoProduto);
        }

        public List<ProdutoResponse> GetListProduct()
        {
            List<ProdutoModel> listaModel = dataAccessLayer.GetListProduct();
            List<ProdutoResponse> listaResponse = new List<ProdutoResponse>();

            foreach (ProdutoModel produtoModel in listaModel)
            {
                ProdutoResponse produtoResponse = ConvertToResponse(produtoModel);
                listaResponse.Add(produtoResponse);
            }

            return listaResponse;
        }

        public void ExcluirProduto (int id)
        {
            dataAccessLayer.ExcluirProduto(id);
        }

        private ProdutoResponse ConvertToResponse(ProdutoModel produtoModel)
        {
            // Lógica de conversão de ProdutoModel para ProdutoResponse
            ProdutoResponse produtoResponse = new ProdutoResponse();

            // Realize a conversão dos atributos relevantes
            produtoResponse.Id = produtoModel.Id;
            produtoResponse.Nome = produtoModel.Nome;
            produtoResponse.Descricao = produtoModel.Descricao;

            return produtoResponse;
        }

    }
}
