using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BitMapper
{
    public partial class frmBitMapper : Form
    {
        public frmBitMapper()
        {
            InitializeComponent();
        }
        
        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            // Open the file dialog and select a file
            string path;
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                // User has selected a valid file.
                path = file.FileName;

                // Open the selected file.
                Image image = Image.FromFile(path);
                Bitmap myBitmap = new Bitmap(path);

                // Adjust the picturebox to match the selected file.
                pictureBox1.Image = image;
                pictureBox1.Height = image.Height;
                pictureBox1.Width = image.Width;
                txtHeight.Text = image.Height.ToString();
                txtWidth.Text = image.Width.ToString();

                richTextBox1.Text = "{" + '\n';

                int x = 0;
                int xMax = image.Width - 1;
                int yMax = image.Height - 1;
                int bitValue = 1;
                int bitTotal = 0;
                string Target = "Color [A=255, R=0, G=0, B=0]";
                string bitV = "";
                Color pixelColor = myBitmap.GetPixel(0, 0);

                // process each of the rows.
                for (int y = 0; y <= yMax; y++)
                {
                    // Check the row number and add line feed if 
                    if (IsDivisble(y, 8) == true)
                    {
                        richTextBox1.Text = richTextBox1.Text + '\n';
                    }

                    // Process each pixel on the row.
                    for (int byteVal = 0; byteVal < (xMax + 1) / 8; byteVal++)
                    {
                        for (int count = 0; count <= 7; count++)
                        {
                            pixelColor = myBitmap.GetPixel((byteVal * 8) + count, y);
                            bitV = pixelColor.ToString();
                            if (Target == bitV)
                            {
                                bitTotal = bitTotal + bitValue;
                            }
                            bitValue *= 2;
                        }
                        richTextBox1.Text = richTextBox1.Text + "0x" + bitTotal.ToString("X") + ',';
                        bitValue = 1;
                        bitTotal = 0;
                    }
                    richTextBox1.Text = richTextBox1.Text + '\n';
                    bitTotal = 0;
                }
            }


            
        }

        public bool IsDivisble(int x, int n)
        {
            return (x % n) == 0;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitMapperAboutBox b = new BitMapperAboutBox();
            b.Show();
        }
        
    }
}
