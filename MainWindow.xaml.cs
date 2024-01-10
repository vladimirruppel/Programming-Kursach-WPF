﻿using prog3_kursach.ViewModel;
using System.Windows;

namespace prog3_kursach
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel viewModel = new MainWindowViewModel(this);
            DataContext = viewModel;
        }
    }
}
