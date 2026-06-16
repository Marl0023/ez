using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafo1 {
    internal class Grafo {
        private int[,] matriz;
        private int cantidadND;

        public Grafo(int cantidadND) {
            this.cantidadND = cantidadND;
            matriz = new int[cantidadND, cantidadND];

        }
        public void Agregar(int origen, int destino) {
            matriz[origen, destino] = 1;
            matriz[destino, origen] = 1;
        }
        public void mostrar() {
            for (int i = 0; i < cantidadND; i++) {
                for (int c = 0; c < cantidadND; c++) {
                    Console.Write(matriz[i, c] + "");
                }
                Console.WriteLine();
            }
        }
    }
}
