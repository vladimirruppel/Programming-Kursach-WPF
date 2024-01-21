using prog3_kursach.ViewModel;
using System.Windows.Controls;

namespace prog3_kursach.View
{
    public partial class EditPage : Page
    {
        public EditPage()
        {
            InitializeComponent();
            DataContext = new EditPageViewModel(); 
        }
    }
}
