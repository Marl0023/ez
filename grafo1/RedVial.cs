using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafo1
{
    internal class RedVial
    {
        private int[,] rutas;          // Matriz de adyacencia para los kilómetros
        private string[] ciudades;     // Lista de ciudades (nodos)
        private int cantCiudades;
        private const int MAX = 10;

        public RedVial()
        {
            ciudades = new string[MAX];
            rutas = new int[MAX, MAX];
            cantCiudades = 0;

            // Inicializamos la matriz con -1 (significa "no hay carretera")
            for (int i = 0; i < MAX; i++)
            {
                for (int j = 0; j < MAX; j++)
                {
                    rutas[i, j] = -1;
                }
            }
        }

        // 1. Agregar una ciudad (Nodo)
        public void AgregarCiudad(string nombre)
        {
            if (cantCiudades < MAX)
            {
                ciudades[cantCiudades] = nombre;
                cantCiudades++;
            }
        }

        // Método auxiliar para buscar el índice de la ciudad
        private int BuscarIndice(string nombre)
        {
            for (int i = 0; i < cantCiudades; i++)
            {
                if (ciudades[i].ToLower() == nombre.ToLower()) return i;
            }
            return -1;
        }

        // 2. Registrar una carretera con sus Kilómetros (Arista Ponderada)
        public void RegistrarCarretera(string origen, string destino, int kilometros)
        {
            int i = BuscarIndice(origen);
            int j = BuscarIndice(destino);

            if (i != -1 && j != -1)
            {
                rutas[i, j] = kilometros;
                rutas[j, i] = kilometros; // Es bidireccional (No dirigido)
                Console.WriteLine($"Carretera registrada: {origen} ↔ {destino} ({kilometros} km)");
            }
        }

        // 3. Consultar distancia directa
        public void ConsultarDistancia(string origen, string destino)
        {
            int i = BuscarIndice(origen);
            int j = BuscarIndice(destino);

            if (i == -1 || j == -1)
            {
                Console.WriteLine("Una o ambas ciudades no existen.");
                return;
            }

            int distancia = rutas[i, j];
            if (distancia == -1)
            {
                Console.WriteLine($"No hay una carretera directa entre {origen} y {destino}.");
            }
            else
            {
                Console.WriteLine($"La distancia directa entre {origen} y {destino} es de {distancia} km.");
            }
        }
    }

}
