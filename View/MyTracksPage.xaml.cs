using prog3_kursach.ViewModel;
using System.Windows.Controls;

namespace prog3_kursach.View
{
    public partial class MyTracksPage : Page
    {
        public MyTracksPage()
        {
            InitializeComponent();
            DataContext = new MyTracksPageViewModel();
        }
    }
}
