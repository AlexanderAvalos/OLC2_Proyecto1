using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1.Ejecutor.Modelos
{
    class Graficar
    {
        String ruta;
        StringBuilder grafica;
        public Graficar()
        {
            ruta = "C:/compiladores 2";
        }
        private void generarDot(String rdot, String rpng)
        {
            System.IO.File.WriteAllText(rdot, grafica.ToString());
            String comandoDot = "dot.exe -Tsvg " + rdot + " -o " + rpng + " ";
            var comando = String.Format(comandoDot);
            var procesoStart = new System.Diagnostics.ProcessStartInfo("cmd", "/C" + comando);
            var proceso = new System.Diagnostics.Process();
            proceso.StartInfo = procesoStart;
            proceso.Start();
            proceso.WaitForExit();

        }

        public void graficar(String texto)
        {
            grafica = new StringBuilder();
            String rdot = ruta + "\\imagen.dot";
            String rpng = ruta + "\\imagen.svg ";
            grafica.Append(texto);
            this.generarDot(rdot, rpng);
        }
    }
}
