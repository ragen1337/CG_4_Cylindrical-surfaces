using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_4_Cylindrical_surfaces
{
    public partial class Form1 : Form
    {
        int x = 100;
        int z = 100;
        double[,,] pointsArray;

        public Form1()
        {
            InitializeComponent();
        }

        private void EllipticalCylinderCoord(out double[,,] pointsArray, int x, int z, double a, double b)
        {
            pointsArray = new double[2 * x, 3, 2 * z];
            for (int i = -x; i < x; i++)
            {
                for (int j = -z; j < z; j++)
                {
                    pointsArray[i + x, 0, j + z] = i;
                    pointsArray[i + x, 1, j + z] = (b/a) * Math.Sqrt( a*a - i*i );
                    if( a * a - i * i == 0)
                    {
                        pointsArray[i + x, 1, j + z] = 0.000001;
                    }
                    pointsArray[i + x, 2, j + z] = j;
                }
            }
        }

        private void HyperbolicCylinderCoord(out double[,,] pointsArray, int x, int z, double a, double b)
        {
            pointsArray = new double[2 * x, 3, 2 * z];
            for (int i = -x; i < x; i++)
            {
                for (int j = -z; j < z; j++)
                {
                    pointsArray[i + x, 0, j + z] = i;
                    pointsArray[i + x, 1, j + z] = (b / a) * Math.Sqrt( i * i - a*a );
                    pointsArray[i + x, 2, j + z] = j;
                }
            }
        }

        private void ParabolicCylinderCoord(out double[,,] pointsArray, int x, int z, double p)
        {
            pointsArray = new double[2 * x, 3, 2 * z];
            for (int i = -x; i < x; i++)
            {
                for (int j = -z; j < z; j++)
                {
                    pointsArray[i + x, 0, j + z] = i;
                    pointsArray[i + x, 1, j + z] = Math.Sqrt(2 * p * i);
                    pointsArray[i + x, 2, j + z] = j;
                }
            }
        }

        private void DrawCylinderSurface(double[,,] pointsArray, int x, int z, Graphics g)
        {
            double x0, y0;
            double picture = 50;
            PointF point1;
            PointF point2;
            Pen blackPen_1 = new Pen(Color.Black, 2);

            for (int i = -x; i < x; i++)
            {
                for (int j = -z; j < z; j++)
                {
                    x0 = ((pointsArray[i + x, 0, j + z] * picture) / pointsArray[i + x, 2, j + z]) + pictureBox1.Width / 2;
                    y0 = pictureBox1.Height / 2 - (pointsArray[i + x, 1, j + z] * picture) / pointsArray[i + x, 2, j + z];

                    if (double.IsNaN(y0))
                    {
                        continue;
                    }

                    point1 = new PointF((float)x0, (float)y0);
                    point2 = new PointF((float)x0 + 1, (float)y0 + 1);

                    g.DrawLine(blackPen_1, point1, point2);

                    y0 = pictureBox1.Height / 2 - (-pointsArray[i + x, 1, j + z] * picture) / pointsArray[i + x, 2, j + z];

                    point1 = new PointF((float)x0, (float)y0);
                    point2 = new PointF((float)x0 + 1, (float)y0 + 1);

                    g.DrawLine(blackPen_1, point1, point2);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Brush brush = new SolidBrush(Color.WhiteSmoke);
            g.FillRectangle(brush, 0, 0, pictureBox1.Width, pictureBox1.Height);

            double a;
            double b;
            bool a_res = Double.TryParse(textBox3.Text, out a);
            bool b_res = Double.TryParse(textBox2.Text, out b);

            if (a < 0 || !a_res)
                a = 100;

            if (b < 0 || !b_res)
                b = 100;

            EllipticalCylinderCoord(out pointsArray, x, z, a, b);

            DrawCylinderSurface(pointsArray, x, z, g);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Brush brush = new SolidBrush(Color.WhiteSmoke);
            g.FillRectangle(brush, 0, 0, pictureBox1.Width , pictureBox1.Height);

            double a;
            double b;
            bool a_res = Double.TryParse(textBox1.Text, out a);
            bool b_res = Double.TryParse(textBox4.Text, out b);

            if (a < 0 || !a_res)
                a = 100;

            if (b < 0 || !b_res)
                b = 100;

            HyperbolicCylinderCoord(out pointsArray, x, z, a, b);

            DrawCylinderSurface(pointsArray, x, z, g);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Brush brush = new SolidBrush(Color.WhiteSmoke);
            g.FillRectangle(brush, 0, 0, pictureBox1.Width, pictureBox1.Height);

            double p;
            bool p_res = Double.TryParse(textBox5.Text, out p);

            if (p < 0 || !p_res)
                p = 100;

            ParabolicCylinderCoord(out pointsArray, x, z, p);
         
            DrawCylinderSurface(pointsArray, x, z, g);

            g.FillRectangle(brush, pictureBox1.Width/2, 0, pictureBox1.Width/2, pictureBox1.Height);
        }
    }
}
