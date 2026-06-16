using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lista_Adyacencia
{
    internal class ListaE
    {
        private Nodo primero = null;
        private Nodo ultimo = null;

        public void insertar(int direccion, int peso){
            Nodo nuevo = new Nodo(direccion, peso); 

            if (primero == null){
                primero = nuevo;
                ultimo = nuevo;
            }else{
                Nodo ultNodo = ultimo;
                ultNodo.Siguiente = nuevo;
                nuevo.Anterior = ultNodo;
                ultimo = nuevo;
            }
        }

        public void mostrar()
        {
            Nodo actual = primero;

            while (actual != null){
                Console.WriteLine($" {actual.Direccion} / Peso: {actual.Peso}");
                
                actual = actual.Siguiente;
            }
        }

    }
}
