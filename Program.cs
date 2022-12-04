//  Carlos Mallard

using System;
using System.IO;

namespace Generador
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                instanciaObjeto();
                GC.Collect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            static void instanciaObjeto()
            {
                Lenguaje a = new Lenguaje("c2.gram");
                a.Gramatica();
                /*{
                    while (!a.FinArchivo())
                    {
                        a.NextToken();
                    }
                }*/
            }
        }
    }
}