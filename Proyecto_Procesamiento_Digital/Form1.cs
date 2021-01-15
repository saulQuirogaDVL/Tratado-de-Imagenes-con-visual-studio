using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Procesamiento_Digital
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap ImagenOriginal;
        Bitmap ImagenResultado;
        int AnchoImagen, AltoImagen,cont=0;
        Pen color;

        private void pixeleadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cont = 16, contPro = cont * cont, cont1 = cont, cont2 = cont;
            int rojo = 0, verde = 0, azul = 0;
            Color miColor = new Color();
            Color nuevoColor = new Color();

            for (int x = 0; x < AnchoImagen; x += cont)
            {
                cont2 = cont;
                for (int y = 0; y < AltoImagen; y += cont)
                {
                    for (int i = x; i < cont1; i++)
                    {
                        for (int j = y; j < cont2; j++)
                        {
                            miColor = ImagenOriginal.GetPixel(i, j);
                            rojo += miColor.R;
                            verde += miColor.G;
                            azul += miColor.B;

                        }
                    }
                    rojo = Convert.ToInt32(rojo / contPro);
                    verde = Convert.ToInt32(verde / contPro);
                    azul = Convert.ToInt32(azul / contPro);
                    for (int i = x; i < cont1; i++)
                    {
                        for (int j = y; j < cont2; j++)
                        {
                            nuevoColor = Color.FromArgb(255, rojo, verde, azul);
                            ImagenResultado.SetPixel(i, j, nuevoColor);
                        }
                    }
                    if ((cont2 + cont) < AltoImagen)
                    {
                        cont2 += cont;
                    }
                    else
                    {
                        cont2 = AltoImagen;
                    }
                }
                if ((cont1 + cont) < AnchoImagen)
                {
                    cont1 += cont;
                }
                else
                {
                    cont1 = AnchoImagen;
                }
            }
            pctLienzo.Image = ImagenResultado;
            pctLienzo.Refresh();
        }


        int px0, px1, px2;
        int py0, py1, py2;

        private void trackBar1_DragOver(object sender, DragEventArgs e)
        {
            
        }

        int trackVal = 5;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            button1.Text = "Nivel: " + trackBar1.Value;
            trackVal = int.Parse(trackBar1.Value.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int xrdt1 = (ImagenOriginal.Width * px1) / pctLienzo.Width;
                int yrdt1 = (ImagenOriginal.Height * py1) / pctLienzo.Height;
                int xrdt2 = (ImagenOriginal.Width * px2) / pctLienzo.Width;
                int yrdt2 = (ImagenOriginal.Height * py2) / pctLienzo.Height;
                int cont = 0;
                Color miColor = new Color();
                for (int i = xrdt1; i < xrdt2; i++)
                {
                    for (int j = yrdt1; j < yrdt2; j++)
                    {
                        if (cont == trackVal || j == yrdt1)
                        {
                            miColor = ImagenOriginal.GetPixel(i, j);
                            cont = 0;
                        }
                        ImagenResultado.SetPixel(i, j, miColor);
                        cont++;
                    }
                }
                pctLienzo.Image = ImagenResultado;
                pctLienzo.Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Imagen No Instanciada");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ImagenOriginal = (Bitmap)(Bitmap.FromFile(opfAbrir.FileName));
                AnchoImagen = ImagenOriginal.Width;
                AltoImagen = ImagenOriginal.Height;
                pctLienzo.Image = ImagenOriginal;
                ImagenResultado = ImagenOriginal;
                pctLienzo.Refresh();
                checkBox1.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Imagen No Instanciada");
            }
        }

        private void pixelearZonaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
        }



        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                MessageBox.Show("encendido");
                cont = 1;
                pictureBox2.Visible = true;
            }
            else
            {
                MessageBox.Show("Apagado");
                cont = 0;
                pictureBox2.Visible = false;
                pictureBox2.Image = pictureBox1.Image;
                pictureBox2.Location=new Point (1028, 302);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            color = new Pen(Brushes.Black,5);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            color = new Pen(Brushes.White,5);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            color = new Pen(Brushes.LightGreen,5);
        }

        private void marcaNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int xrdt1 = (ImagenOriginal.Width * px1) / pctLienzo.Width;
            int yrdt1 = (ImagenOriginal.Height * py1) / pctLienzo.Height;
            int xrdt2 = (ImagenOriginal.Width * px2) / pctLienzo.Width;
            int yrdt2 = (ImagenOriginal.Height * py2) / pctLienzo.Height;
            int cont = 0;
            Color miColor = new Color();
            for (int i = xrdt1; i < xrdt2; i++)
            {
                for (int j = yrdt1; j < yrdt2; j++)
                {
                    if (cont == 50 || j == yrdt1)
                    {                 
                       miColor=ImagenOriginal.GetPixel(i, j);
                        cont = 0;
                    }
                    ImagenResultado.SetPixel(i, j, miColor);
                    cont++;
                }
            }
            pctLienzo.Image = ImagenResultado;
            pctLienzo.Refresh();
        }

        int pnum = 0;
        Color micol;
        private void pctLienzo_MouseMove(object sender, MouseEventArgs e)
        {
            px0 = e.X;
            py0 = e.Y;
        }

        private void pctLienzo_Click(object sender, EventArgs e)
        {

            try { 
            if (cont == 0)
            {
                if (pnum == 0)
                {
                    px1 = px0;
                    py1 = py0;
                    pnum = 1;
                    Color micolor = new Color();

                }
                else
                {
                    pctLienzo.Image = ImagenOriginal;
                    pctLienzo.Refresh();
                    px2 = px0;
                    py2 = py0;
                    pnum = 0;
                    Graphics g;
                    g = pctLienzo.CreateGraphics();
                    g.DrawRectangle(color, px1, py1, px2 - px1, py2 - py1);
                }
            }
            else
            {
                int xrdt = (ImagenOriginal.Width * px0) / pctLienzo.Width;
                int yrdt = (ImagenOriginal.Height * py0) / pctLienzo.Height;
                Bitmap imagenR = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                pictureBox2.Left = px0 + 10;
                pictureBox2.Top = py0 + 35;
                Color micolor = new Color();
                int contX = 4, contY = 4;

                for (int i = 0; i < pictureBox2.Width; i++)
                {
                    for (int j = 0; j < pictureBox2.Height; j++)
                    {
                        //if (contY + j % 4 == 4||j==0)
                        //{
                        micol = ImagenOriginal.GetPixel(xrdt + i, yrdt + j);
                        //}

                        imagenR.SetPixel(i, j, micol);

                    }
                }
                pictureBox2.Image = imagenR;
                pictureBox2.Refresh();
            }
            }
            catch(Exception ex)
            {

            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (opfAbrir.ShowDialog() == DialogResult.OK)
            {
                ImagenOriginal = (Bitmap)(Bitmap.FromFile(opfAbrir.FileName));
                AnchoImagen = ImagenOriginal.Width;
                AltoImagen = ImagenOriginal.Height;
                pctLienzo.Image = ImagenOriginal;
                ImagenResultado = ImagenOriginal;
                checkBox1.Checked = false;
            }
        }
    }
}
