using prog3_kursach.ViewModel;
using System.Windows.Controls;

namespace prog3_kursach.View
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();
        }
    }
}
