using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafo2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RedLogistica logistica = new RedLogistica();

            logistica.AgregarCentro("Centro-A");
            logistica.AgregarCentro("Centro-B");
            logistica.AgregarCentro("Centro-C");

            // Rutas dirigidas (un solo sentido)
            logistica.AgregarRuta("Centro-A", "Centro-B", 50);  // A -> B cuesta 50
            logistica.AgregarRuta("Centro-B", "Centro-C", 30);  // B -> C cuesta 30
            logistica.AgregarRuta("Centro-C", "Centro-A", 100); // C -> A cuesta 100 (retorno caro)

            // 1. Verificar Grados
            logistica.MostrarGradosNodo("Centro-A");
            // Salida: 1 (va a B), Entrada: 1 (viene de C)

            // 2. Calcular ruta con escala (A -> B -> C)
            Console.WriteLine("\n--- Calculando Ruta Logística ---");
            logistica.CalcularCostoConEscala("Centro-A", "Centro-B", "Centro-C");
            // Debería dar: 50 + 30 = $80
        }
    }
}
