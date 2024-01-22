using prog3_kursach.ViewModel;
using System.Windows.Controls;

namespace prog3_kursach.View
{
    public partial class MyAlbumsPage : Page
    {
        public MyAlbumsPage()
        {
            InitializeComponent();
            DataContext = new MyAlbumsPageViewModel();
        }
    }
}
