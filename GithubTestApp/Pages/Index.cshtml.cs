using GithubTestApp.Models;
using GithubTestApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GithubTestApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly TodoContext _context;

        public IndexModel(TodoContext context)
        {
            _context = context;
        }

        public List<ToDo> TodoList { get; set; }

        public async Task OnGetAsync()
        {
            TodoList = await _context.ToDos.ToListAsync();
        }
    }

    public class BackgorundService
    {
        public static void Test()
        {
            TodoContext context = new TodoContext();

            context.Add(new ToDo { Title = DateTime.Now.ToLongTimeString(), IsCompleted = true });
        }
    }
}
