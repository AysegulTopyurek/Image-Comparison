using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace WindowsFormsApp11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        Image img;

        private void button18_Click(object sender, EventArgs e)
        {





            OpenFileDialog openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                img = new Bitmap(openDialog.FileName);



            }



            int widthThird = (int)((double)img.Width / 4.0 + 0.5);
            int heightThird = (int)((double)img.Height / 4.0 + 0.5);
            //var img = Image.FromFile("media\\a.png");
            Bitmap[,] bmps = new Bitmap[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var index = i * 4 + j;

                    bmps[i, j] = new Bitmap(widthThird, heightThird);
                    Graphics g = Graphics.FromImage(bmps[i, j]);
                    g.DrawImage(img, new Rectangle(0, 0, widthThird, heightThird), new Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
                    g.Dispose();

                }
            }
            button1.Image = bmps[0, 0];
            button2.Image = bmps[0, 1];
            button3.Image = bmps[0, 2];
            button4.Image = bmps[0, 3];
            button5.Image = bmps[1, 0];
            button6.Image = bmps[1, 1];
            button7.Image = bmps[1, 2];
            button8.Image = bmps[1, 3];
            button9.Image = bmps[2, 0];
            button10.Image = bmps[2, 1];
            button11.Image = bmps[2, 2];
            button12.Image = bmps[2, 3];
            button13.Image = bmps[3, 0];
            button14.Image = bmps[3, 1];
            button15.Image = bmps[3, 2];
            button16.Image = bmps[3, 3];







        }
        Random r;
        List<Button> buttons;
        private void Whack_A_Mole_Load(object sender, EventArgs e)
        {
            r = new Random();

            buttons = new List<Button>
            {
             button1,
            button2,
            button3,
            button4,
            button5,
            button6,
            button7,
            button8,
            button9,
            button10,
            button11,
            button12,
            button13,
            button14,
            button15,
            button16,
        };
            foreach (Button button in buttons)
                button.Visible = false;
        }

        private void button17_Click(object sender, EventArgs e)
        {

            var randomValue = r.Next(0, 16);
            Button btn = this.Controls.Find("button" + randomValue, true).FirstOrDefault() as Button;
            btn.Visible = true;

        }
    }
}

            /*
            //Random rand = new Random();
            int rows = 5;//No of Rows as per Desire
            int columns = 6;//No of columns as per Desire
            var imgarray = new Image[rows, columns];//Create Image Array of Size Rows X Colums
            var img = image1;//Get Image from anywhere, From File Or Using Dialogbox used previously
            int height = img.Height;
            int width = img.Width;//Get image Height & Width of Input Image
            int one_img_h = height / rows;
            int one_img_w = width / columns;//You need Rows x Columns, So get 1/rows Height, 1/columns width of original Image
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    imgarray[i, j] = new Bitmap(one_img_w, one_img_h);//generating new bitmap
                    var graphics = Graphics.FromImage(imgarray[i, j]);
                    graphics.DrawImage(img, new Rectangle(0, 0, one_img_w, one_img_h), new Rectangle(j * one_img_w, i * one_img_h, one_img_w, one_img_h), GraphicsUnit.Pixel);//Generating Splitted Pieces of Image
                    graphics.Dispose();
                }
            }
            //Image Is spitted You can use it by getting image from **imgarray[Rows, Columns]**
            //Or You can Save it by using Following Code


            var destinationFolderName = "";//Define a saving path
            FolderBrowserDialog FolderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = FolderBrowserDialog1.ShowDialog();//Get folder Path Where splitted Image Saved
            if (result == DialogResult.OK)
            {
                destinationFolderName = FolderBrowserDialog1.SelectedPath;
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        imgarray[i, j].Save(@"" + destinationFolderName + "/Image_" + i + "_" + j + ".jpg");//Save every image in Array [row][column] on local Path
                    }
                }
            }
        } 
       /* Bitmap bmp1;
        ArrayList arr = new ArrayList();
        ArrayList arr2 = new ArrayList();

       
            */
            /*  openFileDialog1.ShowDialog();

              var img = Image.FromFile(openFileDialog1.FileName);
              var imgarray = new Image[16];
              int widthThird = (int)((double)img.Width / 4.0 + 0.5);
              int heightThird = (int)((double)img.Height / 4.0 + 0.5);

              for (int i = 0; i < 4; i++)
              {
                  for (int j = 0; j < 4; j++)
                  {
                      var index = i * 4 + j;
                      imgarray[index] = new Bitmap(widthThird, heightThird);
                      var graphics = Graphics.FromImage(imgarray[index]);
                      graphics.DrawImage(img, new Rectangle(0, 0, widthThird, heightThird), new Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
                      graphics.Dispose();
                  }
                  button1.Image = imgarray[0];
                  button2.Image = imgarray[1];
                  button3.Image = imgarray[2];
                  button4.Image = imgarray[3];
                  button5.Image = imgarray[4];
                  button6.Image = imgarray[5];
                  button7.Image = imgarray[6];
                  button8.Image = imgarray[7];
                  button9.Image = imgarray[8];
                  button10.Image = imgarray[9];
                  button11.Image = imgarray[10];
                  button12.Image = imgarray[11];
                  button13.Image = imgarray[12];
                  button14.Image = imgarray[13];
                  button15.Image = imgarray[14];
                  button16.Image = imgarray[15];

              }*/// a.png has 312X312 width and height
                 /*int widthThird = (int)((double)img.Width / 4.0 + 0.5);
                 int heightThird = (int)((double)img.Height / 4.0 + 0.5);
                 Bitmap[,] bmps = new Bitmap[4, 4];
                 for (int i = 0; i < 4; i++)
                     for (int j = 0; j < 4; j++)
                     {
                         bmps[i, j] = new Bitmap(widthThird, heightThird);
                         Graphics g = Graphics.FromImage(bmps[i, j]);
                         g.DrawImage(img, new Rectangle(0, 0, widthThird, heightThird), new Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
                         g.Dispose();
                     }
                 button1.Image = bmps[0, 0];
                 button2.Image = bmps[0, 1];
                 button3.Image = bmps[0, 2];
                 button4.Image = bmps[0, 3];
                 button5.Image = bmps[1, 0];
                 button6.Image = bmps[1, 1];
                 button7.Image = bmps[1, 2];
                 button8.Image = bmps[1, 3];
                 button9.Image = bmps[2, 0];
                 button10.Image = bmps[2, 1];
                 button11.Image = bmps[2, 2];
                 button12.Image = bmps[2, 3];
                 button13.Image = bmps[3, 0];
                 button14.Image = bmps[3, 1];
                 button15.Image = bmps[3, 2];
                 button16.Image = bmps[3, 3];
             }*/

            /*
            arr.Clear();
            openFileDialog1.ShowDialog();
            bmp1 = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName);
            int kare = 160;
            for (int i = 0; i < bmp1.Width; i++)
            {
                for (int k = 0; k < bmp1.Height; k++)
                {
                    arr.Add(bmp1.GetPixel(i, k).Name);
                }
                if (bmp1.Height % kare != 0 || bmp1.Width % kare != 0)
                {
                    string gecerliOlculer = OrtakKatlar(bmp1.Height, bmp1.Width);

                    return;
                }
                int dikeyParca = bmp1.Height / kare;
                int yatayParca = bmp1.Width / kare;
                Rectangle cropAlani = new Rectangle(0, 0, kare, kare);

                Directory.CreateDirectory(@"C:\Users\user\Desktop\PROJELER");

                for (int d = 0; d < dikeyParca; d++)
                {
                    for (int y = 0; y < yatayParca; y++)
                    {
                        cropAlani.Y = d * kare;
                        cropAlani.X = y * kare;
                        Image parcaResim = Crop(bmp1, cropAlani);
                        parcaResim.Save(@"C:\Users\user\Desktop\PROJELER\" + d + "x" + y + ".jpg", ImageFormat.Jpeg);
                    }
                }


            }
        }


            private static Image Crop(Image image, Rectangle rect)
            {
                Bitmap resim = new Bitmap(image);
                Bitmap parcaResim = resim.Clone(rect, resim.PixelFormat);

                return (Image)(parcaResim);
            }

        

            private static string OrtakKatlar(int sayi1, int sayi2)
            {
                int kucukSayi = Math.Min(sayi1, sayi2);
                string ortak = "";

                for (int i = 2; i <= kucukSayi; i++)
                {
                    if (sayi1 % i == 0 && sayi2 % i == 0)
                        ortak += i.ToString() + ",";
                }

                ortak = ortak.Remove(ortak.Length - 1);

                return ortak;
            }*/

