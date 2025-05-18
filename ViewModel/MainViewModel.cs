using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Variant1.Models;
using Variant1.Views;

namespace Variant1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly User _currentUser;

        public ObservableCollection<Book> Books { get; set; }

        public Book? SelectedBook { get; set; }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand IssueCommand { get; }
        public ICommand ReturnCommand { get; }

        public MainViewModel(User currentUser)
        {
            _currentUser = currentUser;

            using var db = new Variant1Context();
            Books = new ObservableCollection<Book>(db.Books.ToList());

            AddCommand = new RelayCommand(_ => AddBook());
            EditCommand = new RelayCommand(_ => EditBook(), _ => SelectedBook != null);
            DeleteCommand = new RelayCommand(_ => DeleteBook(), _ => SelectedBook != null);
            IssueCommand = new RelayCommand(_ => IssueBook(), _ => SelectedBook != null && SelectedBook.Status == 0);
            ReturnCommand = new RelayCommand(_ => ReturnBook(), _ => SelectedBook != null && SelectedBook.Status == 1);
        }

        private void AddBook()
        {
            var window = new BookEditView();
            if (window.ShowDialog() == true)
                RefreshBooks();
        }

        private void EditBook()
        {
            if (SelectedBook == null) return;
            var window = new BookEditView(SelectedBook);
            if (window.ShowDialog() == true)
                RefreshBooks();
        }


        private void DeleteBook()
        {
            if (SelectedBook == null) return;

            using var db = new Variant1Context();
            var book = db.Books.Find(SelectedBook.Id);
            if (book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
                Books.Remove(SelectedBook);
            }
        }

        private void IssueBook()
        {
            if (SelectedBook == null) return;

            using var db = new Variant1Context();
            var book = db.Books.Find(SelectedBook.Id);
            if (book != null)
            {
                book.UserId = _currentUser.Id;
                book.Status = 1;
                db.SaveChanges();
                RefreshBooks();
            }
        }

        private void ReturnBook()
        {
            if (SelectedBook == null) return;

            using var db = new Variant1Context();
            var book = db.Books.Find(SelectedBook.Id);
            if (book != null)
            {
                book.UserId = null;
                book.Status = 0;
                db.SaveChanges();
                RefreshBooks();
            }
        }

        private void RefreshBooks()
        {
            using var db = new Variant1Context();
            Books.Clear();
            foreach (var b in db.Books.ToList())
                Books.Add(b);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
