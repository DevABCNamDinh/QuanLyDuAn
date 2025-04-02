using QuanLyDuAn.Models;

namespace QuanLyDuAn.Service
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book GetBookById(Guid id);

    }
}
