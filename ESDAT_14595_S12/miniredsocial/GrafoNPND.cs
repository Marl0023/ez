using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miniredsocial {

    internal class GrafoNPND {
        private int[,] red;         
        private Persona[] lista;    
        private int cantPersonas;
        private const int MAX = 100;

        public GrafoNPND() {
            lista        = new Persona[MAX];
            red          = new int[MAX, MAX];
            cantPersonas = 0;
        }


        public bool AgregarPersona(Persona p) {
            if (cantPersonas >= MAX) {
                Console.WriteLine("  Red llena.");
                return false;
            }
            for (int i = 0; i < cantPersonas; i++) {
                if (lista[i].Nombre.ToLower() == p.Nombre.ToLower()) {
                    Console.WriteLine($"  '{p.Nombre}' ya esta en la red.");
                    return false;
                }
            }
            lista[cantPersonas] = p;
            cantPersonas++;
            Console.WriteLine($"  '{p.Nombre}' agregado/a. Total en red: {cantPersonas}");
            return true;
        }


        private int BuscarIndice(string nombre) {
            for (int i = 0; i < cantPersonas; i++) {
                if (lista[i].Nombre.ToLower() == nombre.ToLower())
                    return i;
            }
            return -1;
        }


        public bool ConectarPersonas(string nombre1, string nombre2) {
            int i = BuscarIndice(nombre1);
            int j = BuscarIndice(nombre2);
            if (i == -1) { Console.WriteLine($"  No se encontro '{nombre1}' en la red."); return false; }
            if (j == -1) { Console.WriteLine($"  No se encontro '{nombre2}' en la red."); return false; }
            if (i == j)  { Console.WriteLine("  Una persona no puede conectarse consigo misma."); return false; }
            red[i, j] = 1;
            red[j, i] = 1;
            Console.WriteLine($"  '{nombre1}' y '{nombre2}' ahora son amigos.");
            return true;
        }


        public bool SonAmigos(string nombre1, string nombre2)
        {
            int inicio = BuscarIndice(nombre1);
            int fin = BuscarIndice(nombre2);

            if (inicio == -1)
            {
                Console.WriteLine($"  No se encontro '{nombre1}' en la red.");
                return false;
            }

            if (fin == -1)
            {
                Console.WriteLine($"  No se encontro '{nombre2}' en la red.");
                return false;
            }

            if (inicio == fin)
                return true;

            if (red[inicio, fin] == 1)
                return true;

            for (int k = 0; k < cantPersonas; k++)
            {
                if (red[inicio, k] == 1 && red[k, fin] == 1)
                {
                    return true;
                }
            }
            return false;
        }

        public void MostrarPersonas() {
            if (cantPersonas == 0) {
                Console.WriteLine("  La red esta vacia.");
                return;
            }
            Console.WriteLine($"\n  Personas en la red ({cantPersonas}):");
            for (int i = 0; i < cantPersonas; i++) {
                Console.WriteLine($"  [{i}] {lista[i].Nombre} | {lista[i].Genero} | {lista[i].Edad} años");
            }
        }
    }
}
