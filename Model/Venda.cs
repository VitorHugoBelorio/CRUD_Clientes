using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Clientes.Model
{
    public class Venda
    {
        public int Id { get; set; }
        public string Produtos { get; set; }
        public Cliente Cliente_id { get; set; }
    }
}