// Carlos Mallard

// Requerimiento 1.- Construir un método para escribir en el archivo lenguaje.cs identando el código-
//                   "{" incrementa un tabulador, "}" decrementa un tabulador. *

// Requerimiento 2.- Declarar un atributo primeraProduccion de tipo string y actualizarlo con la primera
//                   producción de la gramática. *

// Requerimiento 3.- La primera producción es pública, y el resto privadas. * 

// Requerimiento 4.- El constructor Lexico() parametrizado debe validar que
//                   la extensión del archivo a compilar sea .gram, si no existe debe lanzar una excepción. *

// Requerimiento 5.- Resolver la ambiguedad de ST, y SNT
//                   Recorrer linea por linea el archivo gram opara para exraer el nombre de cada producción

// Requerimiento 6.- Agregar el paréntesis izquierdo y derecho escapados en la matriz de transiciones. * 

// Requerimiento 7.- Implementar la cerradura epsilon

using System;
using System.Collections.Generic;

namespace Generador
{
    public class Lenguaje : Sintaxis
    {
        List<string> listaSNT;
        string primeraProduccion;
        public Lenguaje()
        {
            listaSNT = new List<string>();
            primeraProduccion = "";
        }
        public Lenguaje(string nombre) : base(nombre)
        {
            listaSNT = new List<string>();
            primeraProduccion = "";
        }

        string tabulacion = "";
        public string calculadorTab(string cadenas)
        {

            if (cadenas.Contains("{"))
            {
                tabulacion = tabulacion + "\t";
                return tabulacion.Substring(0, tabulacion.Length - 1) + cadenas;
            }
            else if (cadenas.Contains("}"))
            {
                tabulacion = tabulacion.Substring(0, tabulacion.Length - 1);
                return tabulacion + cadenas;
            }
            else
            {
                return tabulacion + cadenas;
            }
        }

        string tabulacion2 = "";
        public string calculadorTab2(string cadenas)
        {

            if (cadenas.Contains("{"))
            {
                tabulacion2 = tabulacion2 + "\t";
                return tabulacion2.Substring(0, tabulacion2.Length - 1) + cadenas;
            }
            else if (cadenas.Contains("}"))
            {
                tabulacion2 = tabulacion2.Substring(0, tabulacion2.Length - 1);
                return tabulacion2 + cadenas;
            }
            else
            {
                return tabulacion2 + cadenas;
            }
        }

        // hacer metodo que imprima la lista.SNT

        /*
        public void imprimirSNT()
        {
            foreach (string snt in listaSNT)
            {
                Console.WriteLine(snt);
            }
        }
        */ // taba debuggeando

        public void Gramatica()
        {
            agregarSNT();
            
            cabecera();

            primeraProduccion = getContenido();

            cabeceraLenguaje();
            listadeProducciones();
            
            Programa(primeraProduccion);

            lenguaje.WriteLine(calculadorTab("}"));
            lenguaje.WriteLine(calculadorTab("}"));
        }
        private bool esSNT(string contenido)
        {
            return listaSNT.Contains(contenido);
        }

        // Recorrer linea por linea para extrar los SNT
        private void agregarSNT()
        {
            int actualLinea = linea;

            int actualContador = contador;

            string variable = getContenido();

            while (!FinArchivo())
            {
                NextToken();
                if (getClasificacion() == Tipos.FinProduccion)
                {
                    NextToken();
                    listaSNT.Add(getContenido());
                }
            }

            linea = actualLinea;

            contador = actualContador - variable.Length;

            archivo.DiscardBufferedData();
            archivo.BaseStream.Seek(contador, SeekOrigin.Begin);

            NextToken();

        }

        private void Programa(string primeraProduccion)
        {
            programa.WriteLine(calculadorTab2("using System;"));
            programa.WriteLine(calculadorTab2("using System.IO;"));
            programa.WriteLine(calculadorTab2("using System.Collections.Generic;"));
            programa.WriteLine(calculadorTab2(""));
            programa.WriteLine(calculadorTab2("namespace Generico"));
            programa.WriteLine(calculadorTab2("{"));
            programa.WriteLine(calculadorTab2("public class Generico"));
            programa.WriteLine(calculadorTab2("{"));
            programa.WriteLine(calculadorTab2("static void Main(string[] args)"));
            programa.WriteLine(calculadorTab2("{"));
            programa.WriteLine(calculadorTab2("try"));
            programa.WriteLine(calculadorTab2("{"));
            programa.WriteLine(calculadorTab2("instanciaObjeto();"));
            programa.WriteLine(calculadorTab2("GC.Collect();"));
            programa.WriteLine(calculadorTab2("}"));
            programa.WriteLine(calculadorTab2("catch (Exception e)"));
            programa.WriteLine(calculadorTab2("{"));
            programa.WriteLine(calculadorTab2("Console.WriteLine(e.Message);"));
            programa.WriteLine(calculadorTab2("}"));
            programa.WriteLine(calculadorTab2("}"));
            programa.WriteLine(calculadorTab2("static void instanciaObjeto()"));
            programa.WriteLine(calculadorTab2("{"));
            programa.WriteLine(calculadorTab2("Lenguaje a = new Lenguaje();"));
            programa.WriteLine(calculadorTab2(("a." + primeraProduccion + "();")));
            programa.WriteLine(calculadorTab2("}"));
            programa.WriteLine(calculadorTab2("}"));
            programa.WriteLine(calculadorTab2("}"));

        }

        private void cabecera()
        {
            match("Gramatica");
            match(":");
            match(Tipos.ST);
            match(Tipos.FinProduccion);
        }

        private void cabeceraLenguaje()
        {
            lenguaje.WriteLine(calculadorTab("using System;"));
            lenguaje.WriteLine(calculadorTab("using System.IO;"));
            lenguaje.WriteLine(calculadorTab("using System.Collections.Generic;"));
            lenguaje.WriteLine(calculadorTab(""));
            lenguaje.WriteLine(calculadorTab("namespace Generico"));
            lenguaje.WriteLine(calculadorTab("{"));
            lenguaje.WriteLine(calculadorTab("public class Lenguaje : Sintaxis"));
            lenguaje.WriteLine(calculadorTab("{"));
            lenguaje.WriteLine(calculadorTab("string nombreProyecto;"));
            lenguaje.WriteLine(calculadorTab("public Lenguaje(string nombre) : base(nombre)"));
            lenguaje.WriteLine(calculadorTab("{"));
            lenguaje.WriteLine(calculadorTab("}"));
            lenguaje.WriteLine(calculadorTab("public Lenguaje()"));
            lenguaje.WriteLine(calculadorTab("{"));
            lenguaje.WriteLine(calculadorTab("}"));
            lenguaje.WriteLine(calculadorTab("public void Dispose()"));
            lenguaje.WriteLine(calculadorTab("{"));
            lenguaje.WriteLine(calculadorTab("cerrar();"));
            lenguaje.WriteLine(calculadorTab("}"));
        }

        int contadorProducciones = 0;
        private void listadeProducciones()
        {
            string tipoProduccion = "";

            if (contadorProducciones == 0)
            {
                tipoProduccion = "public";
                primeraProduccion = getContenido();
            }
            else
            {
                tipoProduccion = "private";
            }

            lenguaje.WriteLine(calculadorTab(tipoProduccion + " void " + getContenido() + "()"));
            lenguaje.WriteLine(calculadorTab("{"));
            match(Tipos.ST);
            match(Tipos.Produce);
            simbolos();
            match(Tipos.FinProduccion);
            lenguaje.WriteLine(calculadorTab("}"));
            if (!FinArchivo())
            {
                contadorProducciones++;
                listadeProducciones();
            }
        }

        
        private void simbolos()
        {
            Console.WriteLine(esSNT(getContenido()));
            if (esTipo(getContenido()))
            {
                lenguaje.WriteLine(calculadorTab("match(Tipos." + getContenido() + ");"));
                match(Tipos.ST);
            }
            else if (esSNT(getContenido()))
            {
                lenguaje.WriteLine(calculadorTab(getContenido() + "();"));
                match(Tipos.ST);
            }else if (getClasificacion() == Tipos.ST)
            {
                lenguaje.WriteLine(calculadorTab("match(\"" + getContenido() + "\");"));
                match(Tipos.ST);
            }
            if (getClasificacion() != Tipos.FinProduccion)
            {
                simbolos();
            }
        }


        private bool esTipo(string clasificacion)
        {
            switch (clasificacion)
            {
                case "Identificador":
                case "Numero":
                case "Caracter":
                case "Asignacioncase":
                case "Inicializacion":
                case "OperadorLogico":
                case "OperadorRelacional":
                case "OperadorTernario":
                case "OperadorTermino":
                case "OperadorFactor":
                case "IncrementoTermino":
                case "IncrementoFactor":
                case "FinSentencia":
                case "Cadena":
                case "TipoDato":
                case "Zona":
                case "Condicion":
                case "Ciclo":
                    return true;
            }
            return false;
        }
    }
}
