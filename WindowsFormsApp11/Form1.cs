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
        int puan = 15;
        Bitmap[,] btnBitmap; // Tekrar butona göre boyutlandırılmış bitmap matrisi.
        Bitmap[,] bmps;
        Button selectedButton = null;
        const int puzzleButtonCount = 16;
        Button[] buttons = new Button[puzzleButtonCount];


        public Form1()
        {
            InitializeComponent();
            List<int> skor = readFile();
            if (skor.Count==0)
            {
                label6.Text = "";
            }
            else
            {
                label6.Text = "" + skor.Max();
            }
            for (int i = 0; i < puzzleButtonCount;)
            {
                buttons[i] = this.Controls.Find("button" + ++i, true).FirstOrDefault() as Button;
            }
            btnVisibility(false, buttons);
        }


        private void button18_Click(object sender, EventArgs e)
        {
            Image img;
            puan = 15;
            img = OpenThePicture();
            if (img == null)    // Eğer null değer gelirse boş döner
                return;

            divideThePicture(img);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                puan = 15;
                label4.Text = "";
                btnVisibility(false, buttons);
                shuffleButtons(buttons);
                // Karıştırma gerçekleştikten sonra eşleşen kare olup olmadığını kontrol eder.
                int count = checkThePieces(buttons);
                if (count < 1)
                {
                    MessageBox.Show("Tekrar Karıştırınız.");
                    return;
                }
                else if (count == 16)
                {
                    puan = 100;
                    MessageBox.Show("TEBRİKLER KAZANDINIZ !! \nPuanınız : " + puan);
                    writeFile(""+puan);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Resim Girilmedi. İşlem Gerçekleştirilemiyor !!");
            }
        }

        private int checkThePieces(Button[] buttons)
        {
            int equal = 0;
            for (int i = 0; i < buttons.Length; i++)
            {
                string str = "button" + (i + 1);
                if (compareImages(findBitmap(str), (Bitmap)buttons[i].Image))
                {
                    equal++;
                }
            }
            label3.Text = "" + equal;
            return equal;
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
            puan += checkThePieces(buttons) * 5;
        }
        // Butonların görünürlük değerini ayarlar
        private void btnVisibility(bool visibility, Button[] buttons)
        {
            foreach (Button button in buttons)
                button.Visible = visibility;
        }
        // Tüm butonların click özelliği
        private void myClick(Object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int sayac = 0;
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
                string selectBtnName  = selectedButton.Name;
                string btnName = btn.Name;
                //TODO: Buraya puanlama eklenecek
                bool kontrol1 = compareImages((Bitmap)btn.Image, findBitmap(btnName));
                bool kontrol2 = compareImages((Bitmap)selectedButton.Image, findBitmap(selectBtnName));

                if (kontrol1 && kontrol2)
                {
                    puan += 10;
                }
                else if(kontrol1 || kontrol2)
                {
                    puan += 5;
                }
                else
                {
                    puan -= 3;
                }
               
                label4.Text = ""+puan;
                if (checkThePieces(buttons) == 16)
                {
                    MessageBox.Show("TEBRİKLER KAZANDINIZ !! \nPuanınız : "+puan);
                    writeFile("" + puan);
                }
                //checkThePieces(buttons);
                selectedButton = null;
            }
        }

        private Bitmap findBitmap(string str)
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
                return true;
            }
            else if (notequal == 10000)
            {
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

        private void writeFile(string str)
        {
            string dosya_yolu = @"C:\Users\Furkan\Desktop\enyüksekskor.txt";
            StreamWriter Yaz = new StreamWriter(dosya_yolu);
            Yaz.WriteLine(str);
            Yaz.Close();
        }

        private List<int> readFile()
        {
            List<int> skorlist = new List<int>();
            string line;
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"C:\Users\Furkan\Desktop\enyüksekskor.txt");
            while ((line = file.ReadLine()) != null)
            {
                skorlist.Add(Int32.Parse(line));
            }
            skorlist.Sort();
            //Console.WriteLine(skorlist.Max());
            file.Close();
            return skorlist;
        }
    }
}
