using Proyecto1.Ejecutor.Analizador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proyecto1
{
     

    public partial class Form1 : Form
    {

        Gramatica_Ejecutar ejecutar = new Gramatica_Ejecutar();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Abrir();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Abrir()
        {
            var contenido = string.Empty;
            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = "c:\\";
            file.Filter = "txt files(*.txt)|*.txt|All files (*.*)|*.*";
            file.RestoreDirectory = true;
            if (file.ShowDialog() == DialogResult.OK)
            {
                var filest = file.OpenFile();
                StreamReader reader = new StreamReader(filest);
                contenido = reader.ReadToEnd();
            }
            richTextBox1.AppendText(contenido);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
