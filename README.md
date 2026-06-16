En los grupos definidos cree un proyecto en C# consola .NET Core para simular en la aplicación Uber con las siguientes consideraciones:
•	La fuente debe estar en un repositorio de GitHub
•	Usará un grafo no dirigido ponderado cuyos datos se guardarán en una lista de adyacencia formada por una lista enlazada dentro de una lista enlazada para ahorra el máximo de espacio. El uso de estructuras predefinidas en C# invalidará la respuesta.
•	Cada vértice tendrá un identificador único tipo int y será la intersección de dos calles. Para simplificar solo se podrá viajar entre intersecciones de calles
•	La ponderación de cada arista será con dos indicadores: distancia en kilómetros y tiempo en minutos
•	Permitirá crear Carros con estas propiedades: placa, color, tipo (Particular/Taxi), soles x min, vértice.
•	El aplicativo tendrá un menú con estas opciones:
o	1 - Agrega Carro: Pide sus propiedades y lo agrega a un arreglo de Carros. Si es Taxi debe indicar obligatoriamente los soles x min. La propiedad vértice se le asigna al azar de entre las existentes
o	2 – Modifica Tipo Carro: Permite cambiar tipo, tal que si elige Taxi debe pedir soles x min y la propiedad vértice se le asigna al azar de entre las existentes. Pero si elige Particular debe borrar soles x min y Vértice.
o	3 - Agrega Arista: Agrega 2 vértices, la distancia y tiempo de la arista. Debe validar que si 2 vértices ya estan conectados por una arista no se debe crear una nueva.
o	4 – Asignar Ubicaciones a Taxis: Le asignara un vértice al azar a todos los Taxis y se los guardara en su respectiva propiedad
o	5 – Busca Taxis Cercanos: Lista los Taxis que estan hasta en un segundo nivel de cercanía de un vértice indicado.
o	6 – Distancia entre 2 puntos: Pide 2 vértices Y busca si estan conectados hasta en un tercer nivel. Si es verdad muestra la distancia total y tiempo entre ellos
o	7 – Tomar Taxi: Pide 1 vértice de Origen y 1 vértice de Destino, tal que si están a más de 3 niveles de distancia rechaza la carrera. Muestra la lista de taxis cercanos hasta en 2 niveles indicando cuanto tardaran en llegar al punto de origen, cuanto costara la carrera y cuanto demorara en llegar al destino. El usuario elegirá uno y se generará un mensaje donde le pide el monto total por yape.
o	9 - Fin

Pruebe el aplicativo con 4 taxis, 1 Particular, 10 vértices y 10 aristas.

using System;

namespace UberSimulation
{
    // ==========================================
    // ESTRUCTURAS DEL GRAFO (Listas enlazadas)
    // ==========================================
    
    class AristaNodo
    {
        public int VerticeDestino;
        public double Distancia;
        public double Tiempo;
        public AristaNodo Siguiente;
    }

    class VerticeNodo
    {
        public int Id;
        public AristaNodo CabezaAristas;
        public VerticeNodo Siguiente;
    }

    class Grafo
    {
        public VerticeNodo Cabeza;

        public void AgregarArista(int origen, int destino, double distancia, double tiempo)
        {
            AgregarVertice(origen);
            AgregarVertice(destino);

            if (ExisteArista(origen, destino))
            {
                Console.WriteLine($"[!] La arista entre {origen} y {destino} ya existe.");
                return;
            }

            AgregarAristaDirigida(origen, destino, distancia, tiempo);
            AgregarAristaDirigida(destino, origen, distancia, tiempo);
        }

        private void AgregarVertice(int id)
        {
            if (ObtenerVertice(id) == null)
            {
                VerticeNodo nuevo = new VerticeNodo { Id = id, CabezaAristas = null, Siguiente = Cabeza };
                Cabeza = nuevo;
            }
        }

        public VerticeNodo ObtenerVertice(int id)
        {
            VerticeNodo actual = Cabeza;
            while (actual != null)
            {
                if (actual.Id == id) return actual;
                actual = actual.Siguiente;
            }
            return null;
        }

        private bool ExisteArista(int origen, int destino)
        {
            VerticeNodo v = ObtenerVertice(origen);
            if (v == null) return false;

            AristaNodo a = v.CabezaAristas;
            while (a != null)
            {
                if (a.VerticeDestino == destino) return true;
                a = a.Siguiente;
            }
            return false;
        }

        private void AgregarAristaDirigida(int origen, int destino, double distancia, double tiempo)
        {
            VerticeNodo v = ObtenerVertice(origen);
            AristaNodo nueva = new AristaNodo 
            { 
                VerticeDestino = destino, 
                Distancia = distancia, 
                Tiempo = tiempo, 
                Siguiente = v.CabezaAristas 
            };
            v.CabezaAristas = nueva;
        }

        public int ObtenerVerticeAleatorio(Random rnd)
        {
            int count = 0;
            VerticeNodo actual = Cabeza;
            while (actual != null) { count++; actual = actual.Siguiente; }
            if (count == 0) return -1;

            int index = rnd.Next(count);
            actual = Cabeza;
            for (int i = 0; i < index; i++) actual = actual.Siguiente;
            return actual.Id;
        }

        // Búsqueda de rutas limitando el nivel de profundidad
        public ResultadoRuta BuscarRuta(int origen, int destino, int maxNiveles)
        {
            ResultadoRuta mejor = new ResultadoRuta { Encontrado = false, TiempoTotal = double.MaxValue };
            int[] visitados = new int[100]; 
            BuscarRutaRecursivo(origen, destino, 0, maxNiveles, 0, 0, visitados, 0, mejor);
            return mejor;
        }

        private void BuscarRutaRecursivo(int actual, int destino, int nivel, int maxNiveles, double dist, double tiempo, int[] visitados, int numVisitados, ResultadoRuta mejor)
        {
            if (actual == destino)
            {
                if (tiempo < mejor.TiempoTotal)
                {
                    mejor.Encontrado = true;
                    mejor.DistanciaTotal = dist;
                    mejor.TiempoTotal = tiempo;
                    mejor.Niveles = nivel;
                }
                return; 
            }
            if (nivel >= maxNiveles) return;

            visitados[numVisitados] = actual;
            numVisitados++;

            VerticeNodo v = ObtenerVertice(actual);
            if (v != null)
            {
                AristaNodo a = v.CabezaAristas;
                while (a != null)
                {
                    bool yaVisitado = false;
                    for (int i = 0; i < numVisitados; i++)
                    {
                        if (visitados[i] == a.VerticeDestino) { yaVisitado = true; break; }
                    }

                    if (!yaVisitado)
                    {
                        BuscarRutaRecursivo(a.VerticeDestino, destino, nivel + 1, maxNiveles, dist + a.Distancia, tiempo + a.Tiempo, visitados, numVisitados, mejor);
                    }
                    a = a.Siguiente;
                }
            }
        }
    }

    class ResultadoRuta
    {
        public bool Encontrado;
        public double DistanciaTotal;
        public double TiempoTotal;
        public int Niveles;
    }

    // ==========================================
    // CLASE CARRO Y PROGRAMA PRINCIPAL
    // ==========================================
    
    class Carro
    {
        public string Placa;
        public string Color;
        public bool EsTaxi;
        public double SolesPorMin;
        public int Vertice;
    }

    class Program
    {
        static Grafo grafo = new Grafo();
        static Carro[] carros = new Carro[100];
        static int totalCarros = 0;
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            CargarDatosDePrueba();

            int opcion = 0;
            do
            {
                Console.WriteLine("\n--- MENÚ UBER SIMULATION ---");
                Console.WriteLine("1 - Agrega Carro");
                Console.WriteLine("2 - Modifica Tipo Carro");
                Console.WriteLine("3 - Agrega Arista");
                Console.WriteLine("4 - Asignar Ubicaciones a Taxis");
                Console.WriteLine("5 - Busca Taxis Cercanos");
                Console.WriteLine("6 - Distancia entre 2 puntos");
                Console.WriteLine("7 - Tomar Taxi");
                Console.WriteLine("9 - Fin");
                Console.Write("Seleccione opción: ");
                
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1: OpAgregaCarro(); break;
                        case 2: OpModificaTipoCarro(); break;
                        case 3: OpAgregaArista(); break;
                        case 4: OpAsignarUbicaciones(); break;
                        case 5: OpBuscaTaxisCercanos(); break;
                        case 6: OpDistanciaPuntos(); break;
                        case 7: OpTomarTaxi(); break;
                        case 9: Console.WriteLine("Saliendo..."); break;
                        default: Console.WriteLine("Opción no válida."); break;
                    }
                }
            } while (opcion != 9);
        }

        static void OpAgregaCarro()
        {
            Carro c = new Carro();
            Console.Write("Placa: "); c.Placa = Console.ReadLine();
            Console.Write("Color: "); c.Color = Console.ReadLine();
            Console.Write("Tipo (1=Taxi, 2=Particular): ");
            c.EsTaxi = Console.ReadLine() == "1";

            if (c.EsTaxi)
            {
                Console.Write("Soles x Minuto: ");
                c.SolesPorMin = double.Parse(Console.ReadLine());
                c.Vertice = grafo.ObtenerVerticeAleatorio(rnd);
                Console.WriteLine($"Asignado aleatoriamente al vértice: {c.Vertice}");
            }
            else
            {
                c.SolesPorMin = 0;
                c.Vertice = -1; // No aplicable para particulares
            }

            carros[totalCarros++] = c;
            Console.WriteLine("Carro agregado correctamente.");
        }

        static void OpModificaTipoCarro()
        {
            Console.Write("Ingrese la placa del carro a modificar: ");
            string placa = Console.ReadLine();
            Carro c = null;

            for (int i = 0; i < totalCarros; i++)
            {
                if (carros[i].Placa.Equals(placa, StringComparison.OrdinalIgnoreCase))
                {
                    c = carros[i]; break;
                }
            }

            if (c == null) { Console.WriteLine("Carro no encontrado."); return; }

            Console.WriteLine($"Tipo actual: {(c.EsTaxi ? "Taxi" : "Particular")}");
            Console.Write("Nuevo Tipo (1=Taxi, 2=Particular): ");
            bool nuevoEsTaxi = Console.ReadLine() == "1";

            if (nuevoEsTaxi && !c.EsTaxi)
            {
                c.EsTaxi = true;
                Console.Write("Ingrese Soles x Minuto: ");
                c.SolesPorMin = double.Parse(Console.ReadLine());
                c.Vertice = grafo.ObtenerVerticeAleatorio(rnd);
                Console.WriteLine($"Vértice asignado al azar: {c.Vertice}");
            }
            else if (!nuevoEsTaxi && c.EsTaxi)
            {
                c.EsTaxi = false;
                c.SolesPorMin = 0;
                c.Vertice = -1;
                Console.WriteLine("Propiedades de Taxi (soles/min y vértice) borradas.");
            }
            else
            {
                Console.WriteLine("El carro ya era de ese tipo.");
            }
        }

        static void OpAgregaArista()
        {
            Console.Write("Vértice Origen: "); int v1 = int.Parse(Console.ReadLine());
            Console.Write("Vértice Destino: "); int v2 = int.Parse(Console.ReadLine());
            Console.Write("Distancia (km): "); double dist = double.Parse(Console.ReadLine());
            Console.Write("Tiempo (min): "); double tiempo = double.Parse(Console.ReadLine());

            grafo.AgregarArista(v1, v2, dist, tiempo);
            Console.WriteLine("Operación completada (si no existía, se ha creado).");
        }

        static void OpAsignarUbicaciones()
        {
            int asig = 0;
            for (int i = 0; i < totalCarros; i++)
            {
                if (carros[i].EsTaxi)
                {
                    carros[i].Vertice = grafo.ObtenerVerticeAleatorio(rnd);
                    asig++;
                }
            }
            Console.WriteLine($"Se reasignaron ubicaciones a {asig} taxis.");
        }

        static void OpBuscaTaxisCercanos()
        {
            Console.Write("Ingrese su vértice actual: ");
            int vActual = int.Parse(Console.ReadLine());

            Console.WriteLine("Taxis a máximo 2 niveles de distancia:");
            bool encontro = false;
            for (int i = 0; i < totalCarros; i++)
            {
                if (carros[i].EsTaxi && carros[i].Vertice != -1)
                {
                    ResultadoRuta r = grafo.BuscarRuta(carros[i].Vertice, vActual, 2);
                    if (r.Encontrado)
                    {
                        Console.WriteLine($"- Placa: {carros[i].Placa} | Ubicación: Vértice {carros[i].Vertice} | Distancia: {r.DistanciaTotal}km | Tiempo a ti: {r.TiempoTotal}min");
                        encontro = true;
                    }
                }
            }
            if (!encontro) Console.WriteLine("No se encontraron taxis cercanos.");
        }

        static void OpDistanciaPuntos()
        {
            Console.Write("Vértice 1: "); int v1 = int.Parse(Console.ReadLine());
            Console.Write("Vértice 2: "); int v2 = int.Parse(Console.ReadLine());

            ResultadoRuta r = grafo.BuscarRuta(v1, v2, 3);
            if (r.Encontrado)
            {
                Console.WriteLine($"Conectados a {r.Niveles} niveles.");
                Console.WriteLine($"Distancia total: {r.DistanciaTotal} km");
                Console.WriteLine($"Tiempo total: {r.TiempoTotal} min");
            }
            else
            {
                Console.WriteLine("Los vértices no están conectados dentro de los 3 niveles de distancia.");
            }
        }

        static void OpTomarTaxi()
        {
            Console.Write("Vértice Origen: "); int vOrigen = int.Parse(Console.ReadLine());
            Console.Write("Vértice Destino: "); int vDestino = int.Parse(Console.ReadLine());

            ResultadoRuta rutaViaje = grafo.BuscarRuta(vOrigen, vDestino, 3);
            if (!rutaViaje.Encontrado)
            {
                Console.WriteLine("[Rechazado] El destino está a más de 3 niveles de distancia o inalcanzable.");
                return;
            }

            Console.WriteLine("\n--- Taxis Cercanos (máximo 2 niveles) ---");
            int encontrados = 0;
            Carro[] taxisOpción = new Carro[100];
            double[] tiemposLlegada = new double[100];

            for (int i = 0; i < totalCarros; i++)
            {
                if (carros[i].EsTaxi && carros[i].Vertice != -1)
                {
                    ResultadoRuta rTaxi = grafo.BuscarRuta(carros[i].Vertice, vOrigen, 2);
                    if (rTaxi.Encontrado)
                    {
                        double costoCarrera = rutaViaje.TiempoTotal * carros[i].SolesPorMin;
                        Console.WriteLine($"[{encontrados}] Placa: {carros[i].Placa}");
                        Console.WriteLine($"    Tiempo en llegar a ti: {rTaxi.TiempoTotal} min");
                        Console.WriteLine($"    Tiempo de carrera: {rutaViaje.TiempoTotal} min");
                        Console.WriteLine($"    Costo estimado de carrera: S/. {costoCarrera:F2}");
                        
                        taxisOpción[encontrados] = carros[i];
                        tiemposLlegada[encontrados] = rTaxi.TiempoTotal;
                        encontrados++;
                    }
                }
            }

            if (encontrados == 0)
            {
                Console.WriteLine("No hay taxis disponibles cerca para aceptar tu viaje.");
                return;
            }

            Console.Write("\nSeleccione el índice [#] del taxi que desea: ");
            if (int.TryParse(Console.ReadLine(), out int sel) && sel >= 0 && sel < encontrados)
            {
                Carro taxiElegido = taxisOpción[sel];
                double totalPagar = rutaViaje.TiempoTotal * taxiElegido.SolesPorMin;
                Console.WriteLine($"\nViaje aceptado con el Taxi {taxiElegido.Placa}.");
                Console.WriteLine($"> POR FAVOR YAPE EL MONTO DE S/. {totalPagar:F2} AL CONDUCTOR.");
                // Actualizamos la ubicación del taxi al finalizar el viaje
                taxiElegido.Vertice = vDestino; 
            }
            else
            {
                Console.WriteLine("Selección inválida. Viaje cancelado.");
            }
        }

        // ==========================================
        // DATOS DE PRUEBA EXIGIDOS
        // ==========================================
        static void CargarDatosDePrueba()
        {
            Console.WriteLine("Cargando 10 vértices, 10 aristas, 4 taxis y 1 particular...");

            // 10 Vértices implícitos al crear 10 aristas (conectados de forma secuencial y circular)
            grafo.AgregarArista(1, 2, 1.5, 3.0);
            grafo.AgregarArista(2, 3, 2.0, 4.0);
            grafo.AgregarArista(3, 4, 0.8, 2.0);
            grafo.AgregarArista(4, 5, 3.5, 7.0);
            grafo.AgregarArista(5, 6, 1.2, 2.5);
            grafo.AgregarArista(6, 7, 2.2, 5.0);
            grafo.AgregarArista(7, 8, 1.0, 2.0);
            grafo.AgregarArista(8, 9, 0.5, 1.0);
            grafo.AgregarArista(9, 10, 4.0, 8.0);
            grafo.AgregarArista(10, 1, 1.8, 3.5); // Arista 10, cierra el bucle

            // 4 Taxis
            AgregarCarroMock("TX-111", "Rojo", true, 0.50);
            AgregarCarroMock("TX-222", "Blanco", true, 0.60);
            AgregarCarroMock("TX-333", "Negro", true, 0.45);
            AgregarCarroMock("TX-444", "Amarillo", true, 0.70);

            // 1 Particular
            AgregarCarroMock("PR-999", "Azul", false, 0);

            Console.WriteLine("Datos cargados correctamente.\n");
        }

        static void AgregarCarroMock(string placa, string color, bool esTaxi, double costoMin)
        {
            Carro c = new Carro { Placa = placa, Color = color, EsTaxi = esTaxi };
            if (esTaxi)
            {
                c.SolesPorMin = costoMin;
                c.Vertice = grafo.ObtenerVerticeAleatorio(rnd);
            }
            else
            {
                c.SolesPorMin = 0;
                c.Vertice = -1;
            }
            carros[totalCarros++] = c;
        }
    }
}

