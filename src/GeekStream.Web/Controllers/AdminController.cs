using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GeekStream.Core.Entities;
using GeekStream.Core.Services;
using GeekStream.Core.ViewModels;
using GeekStream.Core.ViewModels.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GeekStream.Web.Controllers
{
    // [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CategoryService _categoryService;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            CategoryService categoryService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                var result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index), "Home");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;

            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            role.Name = model.RoleName;
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ListRoles));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return View(nameof(ListRoles));
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return View(nameof(ListRoles));
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new {Id = roleId});
                }
            }

            return RedirectToAction("EditRole", new {Id = roleId});
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/");
                    var image = new FilePath
                    {
                        FileName = Guid.NewGuid() + file.FileName,
                        FileType = FileType.Photo,
                    };
                    await using (Stream fileStream = new FileStream(path + image.FileName, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    category.Image = image;

                    await _categoryService.SaveCategoryAsync(category);
                }

            }

            return View(category);
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _categoryService.GetCategoryById(id);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category, IFormFile file)
        {

            if (file != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/");
                await using Stream fileStream = new FileStream(path + category.Image.FileName, FileMode.Create);
                await file.CopyToAsync(fileStream);
            }

            var newCategory = new Category
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageId = category.ImageId
            };
            await _categoryService.UpdateCategory(newCategory);


            return RedirectToAction(nameof(ListCategories), "Admin");
        }

        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            return View();
        }

        [HttpPost]
        [ActionName("DeleteCategory")]
        public async Task<IActionResult> DeleteCategoryConfirmed(int id)
        {
            await _categoryService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ListCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }
    }
}
