//  Carlos Mallard

namespace Generador
{
    public class Token
    {
        private string Contenido = "";
        private Tipos Clasificacion;
        public enum Tipos
        {
            Produce, SNT, ST, FinProduccion, FinArchivo, PIzq, PDer, 
        }

        public void setContenido(string contenido)
        {
            this.Contenido = contenido;
        }

        public void setClasificacion(Tipos clasificacion)
        {
            this.Clasificacion = clasificacion;
        }

        public string getContenido()
        {
            return this.Contenido;
        }

        public Tipos getClasificacion()
        {
            return this.Clasificacion;
        }

    }
}