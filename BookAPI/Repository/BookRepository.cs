using BookAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        public readonly BookContext _context;
        public BookRepository(BookContext context)
        {
            this._context = context;
        }
        async Task<IEnumerable<Book>> IBookRepository.Get()
        {
            return await this._context.Books.ToListAsync();
        }
        async Task<Book?> IBookRepository.Get(int id)
        {
            return await this._context.Books.FindAsync(id);
        }
        async Task<Book> IBookRepository.Create(Book book)
        {
            this._context.Books.Add(book);
            await this._context.SaveChangesAsync();
            return book;
        }
        async Task IBookRepository.Delete(int id)
        {
            var bookToDelete = await this._context.Books.FindAsync(id);
            this._context.Books.Remove(bookToDelete);
            await this._context.SaveChangesAsync();
        }
        async Task IBookRepository.Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }
    }
}
