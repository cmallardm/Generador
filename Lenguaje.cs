// Carlos Mallard

// Requerimiento 1.- Construir un método para escribir en el archivo lenguaje.cs identando el código-
//                   "{" incrementa un tabulador, "}" decrementa un tabulador. *

// Requerimiento 2.- Declarar un atributo primeraProduccion de tipo string y actualizarlo con la primera
//                   producción de la gramática. 

// Requerimiento 3.- La primera producción es pública, y el resto privadas.

// Requerimiento 4.- El constructor Lexico() parametrizado debe validar que
//                   la extensión del archivo a compilar sea .gen, si no existe debe lanzar una excepción.

// Requerimiento 5.- Resolver la ambiguedad de ST, y SNT
//                   Recorrer linea por linea el archivo gram opara para exraer el nombre de cada producción

// Requerimiento 6.- Agregar el paréntesis izquierdo y derecho escapados en la matriz de transiciones.

// Requerimiento 6.- Implementar el OR y la cerradura epsilon

using System;
using System.Collections.Generic;

namespace Generador
{
    public class Lenguaje : Sintaxis
    {
        List<string> listaSNT;
        public Lenguaje()
        {
            listaSNT = new List<string>();
        }
        public Lenguaje(string nombre) : base(nombre)
        {
            listaSNT = new List<string>();
        }

        public string calculadorTab(string cadenas)
        {
            string cadenaMod = cadenas;

            string tabulacion = "";

            int contadorTab = 0;

            if (cadenas.Contains("{"))
            {
                contadorTab++;
            }
            if (cadenas.Contains("}"))
            {
                contadorTab--;
            }
            
            for (int i = 0; i < contadorTab; i++)
            {
                tabulacion += "\t";
            }
            
            return tabulacion + cadenaMod;
        }

        public void Gramatica()
        {
            cabecera();
            Programa("Programa");
            cabeceraLenguaje();
            listadeProducciones();
            lenguaje.WriteLine("}");
            lenguaje.WriteLine("}");
        }
        private bool esSNT(string contenido)
        {
            return listaSNT.Contains(contenido);
        }
        private void agregarSNT(string contenido)
        {
            // Requerimiento 5
            listaSNT.Add(contenido);
        }

        private void Programa(string produccionPrincipal)
        {

            programa.WriteLine(calculadorTab("using System;"));
            programa.WriteLine(calculadorTab("using System.IO;"));
            programa.WriteLine(calculadorTab("using System.Collections.Generic;"));
            programa.WriteLine(calculadorTab("namespace "));
            programa.WriteLine(calculadorTab("{"));
            programa.WriteLine(calculadorTab("class " + produccionPrincipal));
            programa.WriteLine(calculadorTab("{"));
            programa.WriteLine(calculadorTab("static void Main(string[] args)"));
            programa.WriteLine(calculadorTab("{"));
            programa.WriteLine(calculadorTab("try"));
            programa.WriteLine(calculadorTab("{"));
            programa.WriteLine(calculadorTab("instanciaObjeto();"));
            programa.WriteLine(calculadorTab("GC.Collect();"));
            programa.WriteLine(calculadorTab("}"));
            programa.WriteLine(calculadorTab("catch (Exception e)"));
            programa.WriteLine(calculadorTab("{"));
            programa.WriteLine(calculadorTab("Console.WriteLine(e.Message);"));
            programa.WriteLine(calculadorTab("}"));
            programa.WriteLine(calculadorTab("}"));
            programa.WriteLine(calculadorTab("static void instanciaObjeto()"));
            programa.WriteLine(calculadorTab("{"));
            programa.WriteLine(calculadorTab((produccionPrincipal + " a = new " + produccionPrincipal + "();")));
            programa.WriteLine(calculadorTab("{"));
            programa.WriteLine(calculadorTab("while (!a.FinArchivo())"));
            programa.WriteLine(calculadorTab("{"));
            programa.WriteLine(calculadorTab("a.NextToken();"));
            programa.WriteLine(calculadorTab("}"));
            programa.WriteLine(calculadorTab("}"));
            programa.WriteLine(calculadorTab("}"));
            programa.WriteLine(calculadorTab("}"));
            programa.WriteLine(calculadorTab("}"));
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

        }

        private void listadeProducciones()
        {
            lenguaje.WriteLine("private void " + getContenido() + "()");
            lenguaje.WriteLine("{");
            match(Tipos.SNT);
            match(Tipos.Produce);
            simbolos();
            match(Tipos.FinProduccion);
            lenguaje.WriteLine("}");
            if (!FinArchivo())
            {
                listadeProducciones();
            }
        }
        private void simbolos()
        {
            if (esTipo(getContenido()))
            {
                lenguaje.WriteLine("\t\t\tmatch(Tipos." + getContenido() + ");");
                match(Tipos.SNT);
            }
            else if (getClasificacion() == Tipos.ST)
            {
                lenguaje.WriteLine("\t\t\t" + getContenido() + "();");
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
