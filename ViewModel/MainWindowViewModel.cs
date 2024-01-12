using prog3_kursach.MVVM;
using System.Windows;

namespace prog3_kursach.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private static string GetPagePath(string pageName)
        {
            return $"View/{pageName}Page.xaml";
        }

        private Window window;

        private string framePath = GetPagePath("Main");
        public string FramePath
        {
            get { return framePath; }
            set 
            { 
                framePath = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel(Window window)
        {
            this.window = window;
        }

        public RelayCommand CloseWindowCommand => new RelayCommand(execute => CloseWindow());
        public RelayCommand MaximizeWindowCommand => new RelayCommand(execute => MaximizeWindow());
        public RelayCommand MinimizeWindowCommand => new RelayCommand(execute => MinimizeWindow());
        public RelayCommand OpenPageCommand => new RelayCommand(pageName => OpenPage(pageName));
        public RelayCommand DragMoveWindowCommand => new RelayCommand(execute => DragMoveWindow());

        private void CloseWindow()
        {
            Application.Current.Shutdown();
        }
        private void MaximizeWindow()
        {
            if (window.WindowState == WindowState.Maximized)
                window.WindowState = WindowState.Normal;
            else window.WindowState = WindowState.Maximized;
        }
        private void MinimizeWindow()
        {
            window.WindowState = WindowState.Minimized;
        }
        private void OpenPage(object pageName)
        {
            if (pageName != null)
                FramePath = GetPagePath(pageName as string);
        }
        private void DragMoveWindow()
        {
            window.DragMove();
        }
    }
}
