using System;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Fisherman
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        CancellationTokenSource _tokenSource = null;
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
            _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;
            await Cos1(token);
        }
        private void StopFisherman(object sender, RoutedEventArgs e)
        {
            //updateLog("task canceled send!");
            _tokenSource.Cancel();
        }
        private void TestClick(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show(BoundariesVal);

        }
        //private bool SerchPixel(string serch1)
        //{
        //    Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        //    //Bitmap bitmap = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
        //    Graphics graphics = Graphics.FromImage(bitmap as Image);
        //    graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
        //    Color desirePixelColor = ColorTranslator.FromHtml(serch1);

        //    for (int x = 0; x < SystemInformation.VirtualScreen.Width; x++)
        //    {
        //        for (int y = 0; y < SystemInformation.VirtualScreen.Height; y++)
        //        {
        //            Color currentPixelColor = bitmap.GetPixel(x, y);
        //            if (desirePixelColor == currentPixelColor)
        //            {
        //                //MessageBox.Show(string.Format("Found Pixel at {0}, {1}", x, y));
        //                updateLog(string.Format("Found Pixel at {0}, {1}", x, y));
        //                return true;
        //            }
        //        }
        //    }
        //    updateLog("no Pixel found");
        //    return false;
        //}
        private async Task SerchPixel(string serch1)
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //Bitmap bitmap = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
            Graphics graphics = Graphics.FromImage(bitmap as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            Color desirePixelColor = ColorTranslator.FromHtml(serch1);
            await Task.Delay(300);
            for (int x = 0; x < SystemInformation.VirtualScreen.Width; x++)
            {
                for (int y = 0; y < SystemInformation.VirtualScreen.Height; y++)
                {
                    Color currentPixelColor = bitmap.GetPixel(x, y);
                    if (desirePixelColor == currentPixelColor)
                    {
                        //MessageBox.Show(string.Format("Found Pixel at {0}, {1}", x, y));
                        updateLog(string.Format("Found Pixel at {0}, {1}", x, y));
                        
                    }
                }
            }
            updateLog("no Pixel found");
           
        }

        //public async Task<bool> Run(cos)
        //{
        //    do
        //    {
        //        cos.Run();
        //        await Task.Delay(300);
        //    } while (true);
        //}

        public async Task Cos1(CancellationToken token)
        {
            
            
                do
                {
                
                    await Task.Delay(100);
                    var inputHexColorCode = "#0EFFC5";
                    updateLog(inputHexColorCode + " " + DateTime.Now.ToString());
                   // await SerchPixel(inputHexColorCode);
                if (token.IsCancellationRequested)
                {
                    //clean up code
                    updateLog("task canceled !");
                    // token.ThrowIfCancellationRequested();
                }
            } while (!token.IsCancellationRequested);
            
            
            
                
                //var inputHexColorCode = "#ed7d20";
                //SerchPixel(inputHexColorCode); 
               // await Cos1(token);
        }
        public void updateLog(string logText) {

            Text = logText;
            //logViewer.Focus();
            logViewer.CaretIndex = logViewer.Text.Length;
            logViewer.ScrollToEnd();           

        }
        private void SetBoundaries(object sender, RoutedEventArgs e)
        {
            BoundariesView boundariesView1 = new BoundariesView();
            //boundariesView1.WindowStyle = "None";
            boundariesView1.Show();
        }       

        private string _text;
        public string Text
            {
                get { return _text; }
                set
                {
                //if (_text != value)
                //{
                //_text = _text + value + "\n";
                _text = value + "\n" + _text;

                OnPropertyChanged();
                    //}
                }
                
            }

        private string _boudariesVal;
        public string BoundariesVal
        {
            get { return _boudariesVal; }
            set
            {
                //if (_text != value)
                //{
                //_text = _text + value + "\n";
                _boudariesVal = value;
                //System.Windows.MessageBox.Show(value);
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
