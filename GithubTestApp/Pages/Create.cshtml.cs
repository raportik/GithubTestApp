using GithubTestApp.Models;
using GithubTestApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GithubTestApp.Pages
{
    public class CreateModel : PageModel
    {
        private readonly TodoContext _context;

        public CreateModel(TodoContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public ToDo Todo { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ToDos.Add(Todo);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
