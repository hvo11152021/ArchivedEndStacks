using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

//PROG1621 Lab 1A - Hy Vo

namespace GameInterface
{
    public sealed partial class MenuState : Page
    {
        public MenuState()
        {
            this.InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
