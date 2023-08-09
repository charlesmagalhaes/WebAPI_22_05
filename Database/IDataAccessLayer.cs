using Database.Models;
using System.Collections.Generic;
using System.Net.Http;

namespace Database
{
    public interface IDataAccessLayer<T>
    {
        T BuscarPorId(int id);
        int Salvar(T objeto); 
        List<T> BuscarLista();
        HttpResponseMessage Excluir(int id);
        void Atualizar(T objeto);
    }

}
