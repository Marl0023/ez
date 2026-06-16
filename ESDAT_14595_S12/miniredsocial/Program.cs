/*
 * MiniRedSocial — Semana 11
 *
 * Menu:
 * 1-Agrega Persona (agregar a la lista)
 * 2-Conecta Personas (pide 2 nombres y conecta
 *      esas personas mediante la red[,])
 * 3-Son_amigos? (pide 2 nombres e indica si estan relacionados,
 *      es decir que si un amigo de un amigo los conoce)
 * 4-Fin
 */

using miniredsocial;

GrafoNPND red = new GrafoNPND();
int opcion;

do {
    Console.Clear();
    Console.WriteLine("\n=============================");
    Console.WriteLine("      MINI RED SOCIAL");
    Console.WriteLine("=============================");
    Console.WriteLine("  1 - Agrega Persona");
    Console.WriteLine("  2 - Conecta Personas");
    Console.WriteLine("  3 - Son amigos?");
    Console.WriteLine("  4 - Fin");
    Console.Write("Opcion: ");

    if (!int.TryParse(Console.ReadLine(), out opcion)) opcion = 0;

    switch (opcion) {

        case 1:
            Console.WriteLine("\n-- Agregar Persona --");
            Console.Write("Nombre : "); string nom = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Genero : "); string gen = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Edad   : "); int.TryParse(Console.ReadLine(), out int edad);

            red.AgregarPersona(new Persona { Nombre = nom, Genero = gen, Edad = edad });
            red.MostrarPersonas();
            break;

        case 2:
            Console.WriteLine("\n-- Conectar Personas --");
            Console.Write("Nombre 1: "); string n1 = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Nombre 2: "); string n2 = Console.ReadLine()?.Trim() ?? "";
            red.ConectarPersonas(n1, n2);
            break;

        case 3:
            Console.WriteLine("\n-- Son Amigos? --");
            Console.Write("Nombre 1: "); string p1 = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Nombre 2: "); string p2 = Console.ReadLine()?.Trim() ?? "";
            bool amigos = red.SonAmigos(p1, p2);
            Console.WriteLine(amigos
                ? $"\n  SI — '{p1}' y '{p2}' estan conectados en la red."
                : $"\n  NO — '{p1}' y '{p2}' NO estan conectados en la red.");
            break;

        case 4:
            Console.WriteLine("\n  Saliendo de la red...");
            break;

        default:
            Console.WriteLine("  Opcion invalida, intente de nuevo.");
            break;
    }
    Console.ReadKey();

} while (opcion != 4);
