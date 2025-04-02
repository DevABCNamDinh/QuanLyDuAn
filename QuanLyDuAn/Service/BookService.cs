using QuanLyDuAn.Models;
using QuanLyDuAn.Repository;

namespace QuanLyDuAn.Service
{
    public class BookService : IBookService
    {
        private readonly  IGenericRepository<Book> _BookRepository;
        public BookService(IGenericRepository<Book> BookRepository)
        {
            _BookRepository = BookRepository;
        }
        public List<Book> GetAllBooks()
        {
            return _BookRepository.GetAll();
        }

        public Book GetBookById(Guid id)
        {
            return _BookRepository.GetById(id);
        }
    }
}
