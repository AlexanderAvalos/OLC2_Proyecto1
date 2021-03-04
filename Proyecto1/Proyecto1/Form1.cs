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
        int caracter;

        List<string> salida = new List<string>();
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
            ejecutar();
        }

        private void ejecutar()
        {

            Sintactico_ejecutar sintactico = new Sintactico_ejecutar();
            string entrada = richTextBox1.Text.Trim();
            sintactico.Analizar(entrada, new Gramatica_Ejecutar());
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
            timer1.Interval = 10;
            timer1.Start();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string entrada = richTextBox1.Text.Trim();
            richTextBox2.AppendText(entrada);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            caracter = 0;
            int altura = richTextBox1.GetPositionFromCharIndex(0).Y;
            int cont = 0;
            if (richTextBox1.Lines.Length > 0)
            {
                for (int i = 0; i < (richTextBox1.Lines.Length - 1); i++)
                {
                    cont = i + 1;
                    string conv = cont.ToString();
                    e.Graphics.DrawString(conv, richTextBox1.Font, Brushes.Blue, pictureBox1.Width - (e.Graphics.MeasureString((conv), richTextBox1.Font).Width + 10), altura);
                    caracter += richTextBox1.Lines[i].Length + 1;
                    altura = richTextBox1.GetPositionFromCharIndex(caracter).Y;

                }
            }
            else
            {
                e.Graphics.DrawString("1", richTextBox1.Font, Brushes.Blue, pictureBox1.Width - (e.Graphics.MeasureString("1", richTextBox1.Font).Width + 10), altura);
            }
        }
    }
}
