using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafo2
{
    internal class RedLogistica
    {
        private int[,] matrizCosto; // Matriz de adyacencia
        private string[] centros;    // Nombres de los centros de distribución
        private int cantCentros;
        private const int MAX = 10;
        private const int INFINITO = -1; // Usamos -1 para indicar que no hay conexión

        public RedLogistica()
        {
            centros = new string[MAX];
            matrizCosto = new int[MAX, MAX];
            cantCentros = 0;

            for (int i = 0; i < MAX; i++)
                for (int j = 0; j < MAX; j++)
                    matrizCosto[i, j] = INFINITO;
        }

        public void AgregarCentro(string nombre)
        {
            if (cantCentros < MAX)
            {
                centros[cantCentros] = nombre;
                cantCentros++;
            }
        }

        private int BuscarIndice(string nombre)
        {
            for (int i = 0; i < cantCentros; i++)
            {
                if (centros[i].ToLower() == nombre.ToLower()) return i;
            }
            return -1;
        }

        // Registrar Ruta Dirigida (Solo de origen a destino)
        public void AgregarRuta(string origen, string destino, int costo)
        {
            int i = BuscarIndice(origen);
            int j = BuscarIndice(destino);

            if (i != -1 && j != -1)
            {
                matrizCosto[i, j] = costo; // Ojo: NO se duplica al revés porque es DIRIGIDO
                Console.WriteLine($"Ruta habilitada: {origen} → {destino} (Costo: ${costo})");
            }
        }

        // 🔥 PREGUNTA TÍPICA DE EXAMEN: Calcular Grados (Entrada y Salida)
        public void MostrarGradosNodo(string nombre)
        {
            int idx = BuscarIndice(nombre);
            if (idx == -1) return;

            int gradoSalida = 0;
            int gradoEntrada = 0;

            for (int i = 0; i < cantCentros; i++)
            {
                // Si sale del nodo hacia otro...
                if (matrizCosto[idx, i] != INFINITO) gradoSalida++;

                // Si viene de otro nodo hacia este...
                if (matrizCosto[i, idx] != INFINITO) gradoEntrada++;
            }

            Console.WriteLine($"\nCentro [{nombre}]:");
            Console.WriteLine($"  -> Grado de Salida (Rutas que ofrece): {gradoSalida}");
            Console.WriteLine($"  -> Grado de Entrada (Rutas que recibe): {gradoEntrada}");
        }

        // 🚀 LÓGICA COMPLEJA: Calcular costo de una ruta con un intermediario
        public void CalcularCostoConEscala(string origen, string via, string destino)
        {
            int o = BuscarIndice(origen);
            int v = BuscarIndice(via);
            int d = BuscarIndice(destino);

            if (o == -1 || v == -1 || d == -1)
            {
                Console.WriteLine("Uno de los centros no existe.");
                return;
            }

            int tramo1 = matrizCosto[o, v]; // Origen -> Vía
            int tramo2 = matrizCosto[v, d]; // Vía -> Destino

            if (tramo1 == INFINITO || tramo2 == INFINITO)
            {
                Console.WriteLine($"Inviable: No se puede conectar {origen} con {destino} pasando por {via}.");
            }
            else
            {
                int costoTotal = tramo1 + tramo2;
                Console.WriteLine($"Ruta con escala encontrada: {origen} → {via} → {destino} | Costo Total: ${costoTotal}");
            }
        }
    }
}
