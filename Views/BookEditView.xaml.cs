using System.Windows;
using Variant1.Models;
using Variant1.ViewModels;

namespace Variant1.Views
{
    public partial class BookEditView : Window
    {
        public BookEditViewModel ViewModel { get; }

        public BookEditView(Book? book = null)
        {
            InitializeComponent();
            ViewModel = new BookEditViewModel(book);
            DataContext = ViewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Save())
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении книги. Проверьте данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
