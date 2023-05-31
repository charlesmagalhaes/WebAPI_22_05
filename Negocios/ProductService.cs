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

        public ProdutoResponse GetProduto(int id)
        {
            var resp = dataAccessLayer.GetProductById(id);
            return (ProdutoResponse)resp;
        }
        public ResultadoExclusaoProduto ExcluirProduto(int id)
        {
            ResultadoExclusaoProduto resultado = new ResultadoExclusaoProduto();

            try
            {
                // Chamada ao método de acesso aos dados
                var resp = dataAccessLayer.ExcluirProduto(id);

                resultado.Sucesso = resp.Sucesso;
                resultado.StatusRequisicao = resp.StatusRequisicao;
                resultado.Mensagem = resp.Mensagem;
                resultado.MensagemErro = resp.MensagemErro;
            }
            catch (Exception ex)
            {
                resultado.Sucesso = false;
                resultado.StatusRequisicao = false;
                resultado.MensagemErro = ex.Message;
            }

            return resultado;
        }


        public void AtualizarProduto(ProdutoRequest produto)
        {
            ProdutoModel novoProduto = new ProdutoModel
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao
            };

            dataAccessLayer.AtualizarProduto(novoProduto);
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
