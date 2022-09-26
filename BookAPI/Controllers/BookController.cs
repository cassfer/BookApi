using BookAPI.Model;
using BookAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await this._bookRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book?>> GetBooks(int id)
        {
            return await this._bookRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
        {
            var newBook =  await _bookRepository.Create(book);
            return CreatedAtAction(nameof(GetBooks), new { id = newBook.Id }, newBook);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var bookToDelete = await _bookRepository.Get(id);

            if(bookToDelete == null)
            {
                return NotFound();
            }
            await _bookRepository.Delete(bookToDelete.Id);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> PutBook(int id, [FromBody] Book book)
        {
            try
            {
                if (id == book.Id)
                {
                    await _bookRepository.Update(book);
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception _)
            {
                return NotFound();
            }


        }
    }
}
