using Proyecto1.Ejecutor.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
      
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static Dictionary<string, TablaDeSimbolos> heap = new Dictionary<string, TablaDeSimbolos>();
        public static RichTextBox consola = new RichTextBox();

        public static Tipo getTipo(string name)
        {
            Tipo tipo = Tipo.OBJECT;
            switch (name)
            {
                case "INTEGER":
                    tipo = Tipo.ENTERO;
                    break;
                case "double":
                    tipo = Tipo.DECIMAL;
                    break;
                case "string":
                    tipo = Tipo.CADENA;
                    break;
                case "boolean":
                    tipo = Tipo.BOOLEAN;
                    break;
                
            }
            return tipo;
        }
    }
}
