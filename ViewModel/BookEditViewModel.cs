using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Variant1.Models;

namespace Variant1.ViewModels
{
    public class BookEditViewModel : INotifyPropertyChanged
    {
        private readonly Book? _originalBook;

        public string Article { get; set; } = "";
        public string Title { get; set; } = "";
        public string? Genre { get; set; }
        public string? Description { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public int Status { get; set; } = 0;

        public BookEditViewModel(Book? book = null)
        {
            if (book != null)
            {
                _originalBook = book;
                Article = book.Article;
                Title = book.Title;
                Genre = book.Genre;
                Description = book.Description;
                ReleaseDate = book.ReleaseDate;
                Status = book.Status;
            }
        }

        public bool Save()
        {
            using var db = new Variant1Context();

            if (string.IsNullOrWhiteSpace(Article) || string.IsNullOrWhiteSpace(Title))
                return false;

            if (_originalBook == null)
            {
                var newBook = new Book
                {
                    Article = Article,
                    Title = Title,
                    Genre = Genre,
                    Description = Description,
                    ReleaseDate = ReleaseDate,
                    Status = Status
                };
                db.Books.Add(newBook);
            }
            else
            {
                var book = db.Books.Find(_originalBook.Id);
                if (book == null) return false;

                book.Article = Article;
                book.Title = Title;
                book.Genre = Genre;
                book.Description = Description;
                book.ReleaseDate = ReleaseDate;
                book.Status = Status;
            }

            db.SaveChanges();
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
