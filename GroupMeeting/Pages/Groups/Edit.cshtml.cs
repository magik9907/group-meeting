using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroupMeeting.Data;
using GroupMeeting.Models;
using System.ComponentModel.DataAnnotations;
using GroupMeeting.Areas.GroupCategories.Models;

namespace GroupMeeting
{
    public class EditModel : PageModel
    {
        public class AddCategory
        {
            [Required]
            public int GroupId { get; set; }
            [Required]
            public string CategoryName { get; set; }
        }

        private readonly GroupMeeting.Data.GroupMeetingContext _context;
        public EditModel(GroupMeeting.Data.GroupMeetingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Group Group { get; set; }
        [BindProperty]
        public AddCategory AddGroupCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Groups
                .Include(a => a.GroupCategories)
                .ThenInclude(e => e.Category)
                .Include(a => a.Owner).FirstOrDefaultAsync(m => m.ID == id)
                ;

            if (Group == null)
            {
                return NotFound();
            }

            AddGroupCategory = new AddCategory() { GroupId = Group.ID };
            ViewData["OwnerID"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(Group.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.ID == id);
        }

        public async Task<IActionResult> OnPostAddCategoryAsync()
        {
            if (AddGroupCategory.GroupId <= 0 || AddGroupCategory.CategoryName == "")
            {
                await OnGetAsync(AddGroupCategory.GroupId);
                return Page();
            }
            var categoryName = AddGroupCategory.CategoryName.ToLower();
            var category = _context.Categories.FirstOrDefault(e => e.Name.ToLower() == categoryName);
            if (category == null)
            {
                await OnGetAsync(AddGroupCategory.GroupId);
                return Page();
            }

            var signCategoryToGroup = new GroupCategory() { Category = category, GroupId = AddGroupCategory.GroupId };
            _context.GroupCategories.Add(signCategoryToGroup);
            _context.SaveChanges();
            return Redirect("./Edit?id=" + signCategoryToGroup.GroupId);
        }

        public async Task<IActionResult> OnPostDeleteCategoryAsync()
        {
            var groupId = int.Parse(Request.Form["x.GroupId"]);
            var categoryId = int.Parse(Request.Form["x.CategoryId"]);
            /* var toDeleteGroupCateogry = (from c in _context.GroupCategories
                                         where c.CategoryId == categoryId
                                         && c.GroupId == groupId
                                         select c).FirstOrDefault();*/
            var ToDelete = new GroupCategory() { GroupId = groupId, CategoryId = categoryId };
            /*_context.GroupCategories.Where(e => e.GroupId == groupId || e.CategoryId == categoryId).ToList()[0];*/
            _context.GroupCategories.Remove(ToDelete);
            _context.SaveChanges();
            return Redirect("./Edit?id=" + groupId);
        }
    }
}
