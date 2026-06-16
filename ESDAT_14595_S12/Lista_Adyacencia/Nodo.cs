using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista_Adyacencia
{
    internal class Nodo
    {
        public int Direccion { get ; set; }
        public int Peso { get; set; }
        public Nodo Siguiente { get; set; }
        public Nodo Anterior { get; set; }

        public Nodo(int direccion, int peso) {
            Direccion = direccion;
            Peso = peso;
            Siguiente = null;
            Anterior = null;
        }
    }
}
