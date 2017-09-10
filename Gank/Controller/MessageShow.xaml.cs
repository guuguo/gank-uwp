using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Gank.Controller
{
    public sealed partial class MessageShow : UserControl
    {
        public MessageShow()
        {
            this.InitializeComponent();
        }
        public async void Show(string content, int showTime)
        {
            grid_GG.Visibility = Visibility.Visible;
           
            _In.Storyboard.Begin();
            txt_GG.Text = content;
            await Task.Delay(showTime);
            _Out.Storyboard.Begin();
           
            _Out.Storyboard.Completed += Storyboard_Completed;
           
        }

        private void Storyboard_Completed(object sender, object e)
        {
            grid_GG.Visibility = Visibility.Collapsed;
        }
    }
}
