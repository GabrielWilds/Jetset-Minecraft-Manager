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
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for NewProfileWindow.xaml
    /// </summary>
    public partial class NewProfileWindow : Window
    {

        public NewProfileWindow(string _title, string _prompt, string _buttonTitle, string _action, Core.MCProfile _profile)
        {
            this.DataContext = new NewProfileWindowViewModel(this, _action, _profile);
            InitializeComponent();
            Title = _title;
            Prompt.Text = _prompt;
            Button.Content = _buttonTitle;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            ((NewProfileWindowViewModel)this.DataContext).ButtonAction();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
