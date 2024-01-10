using prog3_kursach.MVVM;
using System.Windows;

namespace prog3_kursach.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public Window window { get; set; }

        public MainWindowViewModel(Window window)
        {
            this.window = window;
        }

        public RelayCommand CloseWindowCommand => new RelayCommand(execute => CloseWindow());
        public RelayCommand MaximizeWindowCommand => new RelayCommand(execute => MaximizeWindow());
        public RelayCommand MinimizeWindowCommand => new RelayCommand(execute => MinimizeWindow());

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
    }
}
