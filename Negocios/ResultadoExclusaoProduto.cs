using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class ResultadoExclusaoProduto
    {
        public bool Sucesso { get; set; }
        public bool StatusRequisicao { get; set; }
        public string MensagemErro { get; set; }
        public string Mensagem { get; set; }
    }
}
