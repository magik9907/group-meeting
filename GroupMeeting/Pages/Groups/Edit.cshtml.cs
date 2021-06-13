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
using Microsoft.AspNetCore.Identity;
using GroupMeeting.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;


namespace GroupMeeting
{
    [Authorize]
    public class EditModel : PageModel
    {
        public class AddCategory
        {
            [Required]
            public int GroupId { get; set; }
            [Required]
            public string CategoryName { get; set; }
        }

        private readonly GroupMeetingContext _context;

        private readonly UserManager<User> _userManager;
        public EditModel(GroupMeetingContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.ID == id);
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

            if (!GroupExists(Group.ID))
            {
                return NotFound();
            }
      //      Group.City = _context.Cities.FirstOrDefault(c => c.ID == Group.CityID);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null || user.Id != Group.OwnerID)
                return Redirect("./Details?id=" + id);

            AddGroupCategory = new AddCategory() { GroupId = Group.ID };
            ViewData["OwnerID"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.Values.Where(x=>x.ValidationState.ToString() =="Valid").ToArray().Length < 4)
            {
                await OnGetAsync(Group.ID);
                return Page();
            }
 //           var groupCity = await _context.GroupCity.FirstAsync(gc => gc.CityID == Group.CityID && gc.GroupID == Group.ID);
            var city = _context.Cities.FirstOrDefault(a => a.Name.ToLower() == Group.City.Name.ToLower());
            if (city == null)
            {
                city = Group.City;
                _context.Cities.Add(city);
            }
            Group.City = city;
    //       Group.CityID = city.ID;
      //      _context.GroupCity.Remove(groupCity);
      //      groupCity = new GroupCity
       //     {
        //        CityID = Group.CityID,
        //        GroupID = Group.ID
         //   };
         //   _context.GroupCity.Add(groupCity);
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
            if (_context.GroupCategories.Any(e => e.CategoryId == signCategoryToGroup.Category.Id && e.GroupId == signCategoryToGroup.GroupId) == false)
            {
                _context.GroupCategories.Add(signCategoryToGroup);
                _context.SaveChanges();
            }
            return Redirect("./Edit?id=" + signCategoryToGroup.GroupId);
        }

        public async Task<IActionResult> OnPostDeleteCategoryAsync()
        {
            var groupId = int.Parse(Request.Form["x.GroupId"]);
            var categoryId = int.Parse(Request.Form["x.CategoryId"]);
            var ToDelete = new GroupCategory() { GroupId = groupId, CategoryId = categoryId };
            _context.GroupCategories.Remove(ToDelete);
            _context.SaveChanges();
            return Redirect("./Edit?id=" + groupId);
        }
    }
}
