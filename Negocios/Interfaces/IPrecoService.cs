using Negocios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Interfaces
{
    public interface IPrecoService
    {
        List<PrecoResponse> GetLista();
        PrecoResponse Get(int id);
        void Salvar(PrecoResponse preco);
        void Excluir(int id);
        void Atualizar(PrecoResponse preco);
    }
}
