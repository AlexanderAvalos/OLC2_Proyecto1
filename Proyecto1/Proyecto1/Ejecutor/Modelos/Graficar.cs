using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            ruta = "C:\\compiladores2";
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

        public void generarimagen(String texto)
        {
            grafica = new StringBuilder();
            String rdot = ruta + "\\imagen.dot";
            String rpng = ruta + "\\imagen.svg ";
            grafica.Append(texto);
            this.generarDot(rdot, rpng);
        }

        public void HTML_ts(TablaDeSimbolos ts)
        {

            String Contenido_html;
            Contenido_html = "<html><head><meta charset=\u0022utf-8\u0022></head>\n" +
            "<body>" +
            "<h1 align='center'>Tabla de Simbolos</h1></br>" +
            "<table cellpadding='10' border = '1' align='center'>" +
            "<tr>" +

            "<td><strong>Id" +
            "</strong></td>" +

            "<td><strong>Tipo Dato" +
            "</strong></td>" +

            "<td><strong>Valor" +
            "</strong></td>" +

            "</tr>";

            String Cad_tokens = "";
            String tempo_tokens = "";
            foreach (Simbolo sim in ts)
            {

               tempo_tokens += "<tr>" +

               "<td>" + sim.Id +
               "</td>" +

               "<td>" + sim.Tipo.ToString() +
               "</td>" +


               "<td>" + sim.Valor.ToString() +
               "</td>" +

               "</tr>";

            }
            Cad_tokens = Cad_tokens + tempo_tokens;
        Contenido_html = Contenido_html + Cad_tokens +
            "</table>" +
            "</body>" +
            "</html>";
            File.WriteAllText("C:\\compiladores2\\Tabla_Simbolos.html", Contenido_html);
            var p = new Process();
        p.StartInfo = new ProcessStartInfo(@"C:\compiladores2\Tabla_Simbolos.html")
        {
            UseShellExecute = true
            };
        p.Start();
        }
}
}
