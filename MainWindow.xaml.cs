using System;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using Application = System.Windows.Forms.Application;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Fisherman
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0X0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0X0004;

        [DllImport("User32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInf);

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            
        }
        

        private async void StartFisherman(object sender, RoutedEventArgs e)
        {
            await Cos1();
        }

        private bool SerchPixel(string serch1)
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //Bitmap bitmap = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            Color desirePixelColor = ColorTranslator.FromHtml(serch1);

            for (int x = 0; x < SystemInformation.VirtualScreen.Width; x++)
            {
                for (int y = 0; y < SystemInformation.VirtualScreen.Height; y++)
                {
                    Color currentPixelColor = bitmap.GetPixel(x, y);
                    if (desirePixelColor == currentPixelColor)
                    {
                        MessageBox.Show(string.Format("Found Pixel at {0}, {1}", x, y));
                        return true;
                    }
                }
            }            
            MessageBox.Show("no Pixel found");
            return false;
        }

        //public async Task<bool> Run(cos)
        //{
        //    do
        //    {
        //        cos.Run();
        //        await Task.Delay(300);
        //    } while (true);
        //}

        public async Task Cos1()
        {
            var inputHexColorCode = "#0CFFC4";
            Text = inputHexColorCode + DateTime.Now.ToString();
            logViewer.Focus();
            logViewer.CaretIndex = logViewer.Text.Length;
            await Task.Run(() => logViewer.ScrollToEnd());
            //var inputHexColorCode = "#ed7d20";
            //SerchPixel(inputHexColorCode); 
        }
        public void cos() {

            var inputHexColorCode = "#0CFFC4";
            Text = inputHexColorCode + DateTime.Now.ToString();
            logViewer.Focus();
            logViewer.CaretIndex = logViewer.Text.Length;
            logViewer.ScrollToEnd();
            //var inputHexColorCode = "#ed7d20";
            //SerchPixel(inputHexColorCode); 

        }

        private string _text;
        public string Text
            {
                get { return _text; }
                set
                {
                    //if (_text != value)
                    //{
                        _text = _text + value + "\n";
                        OnPropertyChanged();
                    //}
                }
                
            }


        public event PropertyChangedEventHandler PropertyChanged;      
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
