//  Carlos Mallard

using System;
using System.IO;

namespace Generador
{
    public class Program
    {
        static void Main(string[] args)
        {
            static void instanciaObjeto()
            {
                Lenguaje a = new Lenguaje();
                {
                    while (!a.FinArchivo())
                    {
                        a.NextToken();
                    }
                }
            }

            try
            {
                instanciaObjeto();
                GC.Collect();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}