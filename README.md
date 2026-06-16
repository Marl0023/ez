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
