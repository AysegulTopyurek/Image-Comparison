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
        // Image img;
        int puan = 0;
        Bitmap[,] btnBitmap;
        Bitmap[,] bmps;
        Button selectedButton = null;
        const int puzzleButtonCount = 16;
        
        public Form1()
        {
            InitializeComponent();
        }


        private void button18_Click(object sender, EventArgs e)
        {
            Image img;
            img = OpenThePicture();
            if (img == null)    // Eğer null değer gelirse boş döner
                return;

            divideThePicture(img);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Button[] buttons = new Button[puzzleButtonCount];

            // buttonları ayarla
            for (int i = 0; i < puzzleButtonCount;)
            {
                buttons[i] = this.Controls.Find("button" + ++i, true).FirstOrDefault() as Button;
            }

            btnVisibility(false, buttons);
            shuffleButtons(buttons);
            //string str = "button1";
            //Bitmap bit = new Bitmap("C:\\Users\\Furkan\\Desktop\\Puzzle\\" + str + ".jpg");

            //compareImages(btnBitmap[0,0], (Bitmap)buttons[0].Image);
            // Karıştırma gerçekleştikten sonra eşleşen kare olup olmadığını kontrol eder.
            if (!checkThePieces(buttons))
            {
                MessageBox.Show("Tekrar Karıştırınız.");
                return;
            }
        }

        private bool checkThePieces(Button[] buttons)
        {
            Bitmap bitmap;
            int equal = 0;
            for (int i = 0; i < buttons.Length; i++)
            {
                string str = "button" + (i + 1);
                //bitmap = new Bitmap("C:\\Users\\Furkan\\Desktop\\Puzzle\\"+str+".jpg");
                //(Bitmap)buttons[i].Image)
                if (compareImages(buttonÇek(str), (Bitmap)buttons[i].Image))
                {
                    equal++;
                }
            }
            label3.Text = "" + equal;
            if (equal>=1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Resim okuma işlemi yaptığımız fonksiyon
        private Image OpenThePicture()
        {
            Image img;
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image Files| *.jpg; *.jpeg; *.png; *.gif; *.tif";
            if (openDialog.ShowDialog() != DialogResult.OK)
                return null;

            img = new Bitmap(openDialog.FileName);
            return img;
        }

        // Okuduğumuz resmi 16 eş parçaya ayıran fonksiyon
        private void divideThePicture(Image img)
        {
            bmps = new Bitmap[4, 4];
            btnBitmap = new Bitmap[4, 4];
            int widthThird = (int)((double)img.Width / 4.0 + 0.5);
            int heightThird = (int)((double)img.Height / 4.0 + 0.5);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int index = (i * 4 + j) + 1;
                    bmps[i, j] = new Bitmap(widthThird, heightThird);
                    Graphics g = Graphics.FromImage(bmps[i, j]);
                    g.DrawImage(img, new Rectangle(0, 0, widthThird, heightThird), new Rectangle(j * widthThird, i * heightThird, widthThird, heightThird), GraphicsUnit.Pixel);
                    // Burada butona göre boyutlandırma yapıyoruz ve buton nesnelerinin içerisine yerleştiriyoruz.
                    btnBitmap[i, j] = new Bitmap(bmps[i, j], new Size(100, 100));
                    Button button = this.Controls.Find("button" + index, true).FirstOrDefault() as Button;
                    button.Image = (Image)btnBitmap[i, j];
                    button.Visible = false;
                    g.Dispose();
                }
            }
            //saveImages(btnBitmap);
        }

        //Butonların karıştırılmasını sağlar
        private void shuffleButtons(Button[] buttons)
        {

            if (buttons.Length != puzzleButtonCount)
            {
                return;
                // throw exception
            }

            Random r = new Random();
            Button tmp = new Button();

            List<int> sayilar = new List<int>();
            // indisleri random olarak shuffle haline getir
            for (int i = 0, randomValue = 0; i < puzzleButtonCount;)
            {
                randomValue = r.Next(0, puzzleButtonCount);
                if (!sayilar.Contains(randomValue))
                {
                    sayilar.Add(randomValue);
                    i++;
                }
            }

            for (int i = 0, rnd; i < puzzleButtonCount; i++)
            {
                rnd = sayilar[i];
                tmp.Image = buttons[i].Image;
                buttons[i].Image = buttons[rnd].Image;
                buttons[rnd].Image = tmp.Image;
            }

            btnVisibility(true, buttons);
        }

        // Butonların görünürlük değerini ayarlar
        private void btnVisibility(bool visibility, Button[] buttons)
        {
            foreach (Button button in buttons)
                button.Visible = visibility;
        }

        private void myClick(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (selectedButton == null)
            {
                selectedButton = btn;
            }
            else if (btn != selectedButton)
            {
                Button swap = new Button();
                swap.Image = selectedButton.Image;
                selectedButton.Image = btn.Image;
                btn.Image = swap.Image;
                //(Bitmap)selectedButton.Image
                string furkan  = selectedButton.Name;
                string furkan1 = btn.Name;
                //buttonÇek(furkan);(Bitmap)selectedButton.Image
                if (compareImages((Bitmap)btn.Image , buttonÇek(furkan1)))
                //{
                    puan += 10;
                //}
                //else
                //{
                //    MessageBox.Show("Resimler Eşit Değildir.");
                //}
                if (compareImages((Bitmap)selectedButton.Image, buttonÇek(furkan)))
                //{
                    puan += 10;
                //}
                //else
                //{
                //    MessageBox.Show("Resimler Eşit Değildir.");
                //}
                label4.Text = ""+puan;
                selectedButton = null;
            }
        }

        private Bitmap buttonÇek(string str)
        {
            string tmp = "button";
            for (int i = 0; i <4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int index = (i * 4 + j) + 1;
                    tmp += index;
                    if (str == tmp)
                    {
                        //MessageBox.Show(tmp);
                        return btnBitmap[i,j];
                    }
                    tmp = "button";
                }

            }
            return null;
        }
        private bool compareImages(Bitmap bmp1, Bitmap bmp2)
        {
            double equal = 0;
            double notequal = 0;
            ArrayList arl = new ArrayList();
            ArrayList arl2 = new ArrayList();
            for (int a = 0; a < bmp1.Width; a++)
            {
                for (int b = 0; b < bmp1.Height; b++)
                {
                    arl.Add(bmp1.GetPixel(a, b).Name);
                    arl2.Add(bmp2.GetPixel(a, b).Name);
                }
            }

            for (int a = 0; a < arl.Count; a++)
            {
                if (arl[a].ToString() == arl2[a].ToString())
                {
                    equal++;
                }
                else
                {
                    notequal++;
                }
            }

            if (notequal == 0)
            {
                label3.Text = "%100";
                return true;
            }
            else if (notequal == 10000)
            {
                label3.Text = "%0";
                return false;
            }
            else
                return false;
            //else
            //{
            //    if (equal > notequal)
            //    {
            //        label3.Text = "%" + Convert.ToString(100 - ((notequal * 100) / equal)).Substring(0, 5);
            //    }
            //    else
            //    {
            //        label3.Text = "%" + Convert.ToString((equal * 100) / notequal).Substring(0, 6);
            //    }
            //}
        }

        // Parçalara ayrıdığımız resmi tek tek dosyaya kaydeder.
        private void saveImages(Bitmap[,] bmp)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j <4; j++)
                {
                    int index = (i * 4 + j) + 1;
                    bmp[i,j].Save(@"C:\Users\Furkan\Desktop\Puzzle\button" + index + ".jpg", ImageFormat.Jpeg);

                }
            }
        }

        private void yaz(string str)
        {
            string dosya_yolu = @"C:\Users\Furkan\Desktop\metinbelgesi.txt";
            StreamWriter Yaz = new StreamWriter(dosya_yolu);
            Yaz.WriteLine(str);
           
            Yaz.Close();
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

