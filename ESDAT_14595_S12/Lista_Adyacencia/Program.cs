using System;
using Lista_Adyacencia;

namespace Lista_Adyacencia_PND
{
    public class Program
    {
        public static void Main(string[] args)
        {

            GrafoPND a = new GrafoPND(4);

            a.Agregar(0,2,2);
            a.Agregar(2,3,3);
            a.Agregar(0, 3, 1);
            a.Agregar(1,3, 4);
            a.mostrar();
        }
    }
}