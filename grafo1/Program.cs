using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafo1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RedVial miMapa = new RedVial();

            // Añadimos los nodos
            miMapa.AgregarCiudad("Lima");
            miMapa.AgregarCiudad("Ica");
            miMapa.AgregarCiudad("Huancayo");

            // Añadimos las aristas con sus pesos (kilómetros)
            miMapa.RegistrarCarretera("Lima", "Ica", 300);
            miMapa.RegistrarCarretera("Lima", "Huancayo", 310);

            // Hacer consultas
            Console.WriteLine("\n--- Consultas de rutas ---");
            miMapa.ConsultarDistancia("Lima", "Ica");       // Mostrará 300 km
            miMapa.ConsultarDistancia("Ica", "Huancayo");   // Mostrará que no hay carretera directa
        }
    }
    
}
