using EBook.Models;
using EBook.Services.Books;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EBook.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查看全部图书信息
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetAll()
        {
            var result = await _bookService.GetAllAsync();

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 获取用户所借的所有书籍
        /// </summary>
        /// <param name="userId"></param>
        /// 
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetbyAll(int userId)
        {
            var result = await _bookService.GetAllAsync(userId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>s
        /// 查询书籍信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetbyId(GetByIdBindingModel model)
        {
            var result = await _bookService.GetbyIdAsync(model.ID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除图书信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(DeleteBookBindingModel model)
        {
            var result = await _bookService.DeleteAsync(model.ID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改图书信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Update(UpdateBookBindingModel model)
        {
            var result = await _bookService.UpdateAsync(model.ID,model.Name,model.Author,model.Price);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加图书信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Add(AddBookBindingModel model)
        {
            var result = await _bookService.AddAsync(model.Name,model.Author,model.Price);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 还书
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>S
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Return(int userId, int bookId)
        {
            var result = await _bookService.ReturnAsync(userId, bookId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 借书
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <param name="returnDate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Borrow(int userId, int bookId, DateTime returnDate)
        {
            var result = await _bookService.BorrowAsync(userId, bookId, returnDate);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddUser(string name, string phone, string address)
        {
            var result = await _bookService.AddUserAsync(name, phone, address);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}