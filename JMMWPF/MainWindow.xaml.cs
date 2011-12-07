using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)this.DataContext).LoadProfile();
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)this.DataContext).CopyProfile();
        }

        private void Rename(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)this.DataContext).RenameProfile();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)this.DataContext).NewProfile();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            ((MainWindowViewModel)this.DataContext).DeleteProfile();
        }
    }
}
