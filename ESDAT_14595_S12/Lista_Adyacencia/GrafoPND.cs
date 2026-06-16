using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista_Adyacencia
{
    internal class GrafoPND
    {
        private ListaE[] Lista_A;
        private int vertices;

        public GrafoPND(int cantidadPND){
            Lista_A = new ListaE[cantidadPND];
            vertices = cantidadPND;

            for (int i = 0; i < cantidadPND; i++){
                Lista_A[i] = new ListaE();
            }
        }

        public void Agregar(int origen, int destino, int peso){
            if (origen >= vertices || destino >= vertices) {
                Console.WriteLine("Origen o destino fuera de la cantidad permitida");
                return;
            }
            Lista_A[origen].insertar(destino, peso);
            Lista_A[destino].insertar(origen, peso);
        }
        public void mostrar(){
            int i = 0;
            foreach(ListaE elemento in Lista_A){
                Console.WriteLine($"Lista {i}:");
                elemento.mostrar();
                i++;
            }
        }
    }
}
