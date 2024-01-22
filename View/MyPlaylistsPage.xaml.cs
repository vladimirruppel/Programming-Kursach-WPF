using prog3_kursach.ViewModel;
using System.Windows.Controls;

namespace prog3_kursach.View
{
    public partial class MyPlaylistsPage : Page
    {
        public MyPlaylistsPage()
        {
            InitializeComponent();
            DataContext = new MyPlaylistsPageViewModel();
        }
    }
}
