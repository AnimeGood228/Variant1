using System.Windows;
using Variant1.Models;
using Variant1.ViewModels;

namespace Variant1.Views
{
    public partial class MainView : Window
    {
        public MainView(User user)
        {
            InitializeComponent();
            DataContext = new MainViewModel(user);
        }
    }
}
