using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了"空白页"项模板

namespace AppTheme
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private  void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AppSettings));
           
          
        }
        public static string EncryptString(string str)
        {
            MD5 md5 = MD5.Create();

            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            byte[] byteNew = md5.ComputeHash(byteOld);
           
            StringBuilder sb = new StringBuilder();

            foreach (byte b in byteNew)
            {
                
                sb.Append(b.ToString("x2"));
            }
           
            return sb.ToString();
        }
        public string CreateRandCdkeys(int x)
        {
            string[] codeSerial = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            Random rand = new Random();
            int temp = -1;
            string cdKey = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(x + i * temp * unchecked((int)DateTime.Now.Ticks));
                }
                int randIndex = rand.Next(0, 35);
                temp = randIndex;
                cdKey += codeSerial[randIndex];
            }
            return cdKey;
        }

    }


    public delegate Task<int> PrintCaller([CallerMemberName] string Caller = null);
    public class MyClass
    {
        public async Task<string> InitiateAPICallAsync(PrintCaller apiCall)
        {
            var response = await apiCall();
            return "Test";
        }

        public async void MyFunc()
        {
            var helper = new APIHelper();
            var str1 = await InitiateAPICallAsync(new PrintCaller(helper.GetData));
            var str2 = await helper.GetData();
        }
       


    }
    public class APIHelper
{
    public void Test(string p)
    {

    }
    public async Task<int> GetData([CallerMemberName] string Caller = null)
    {

        if (Caller == "InitiateAPICallAsync")
        {
            // do some thing
        }
        else
        {
            //Show Warning
            var dialog = new MessageDialog("Waring!!! Please don't call it directly");
            await dialog.ShowAsync();
        }

        return 0;
    }
}



public class Response1 : BaseResponse { }

public class Response2 : BaseResponse { }

public class BaseResponse { }
}


