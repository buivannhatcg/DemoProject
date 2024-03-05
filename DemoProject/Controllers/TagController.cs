using DemoProject.Models.Tag;
using DemoProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        public IActionResult Index(TagListVM model)
        {
            model.PageSize = 3;
            var result = _tagService.GetPaginatedTag(model);
            
            return View(result);
        }
        [HttpGet]
        [Route("Tag/Create")]
        public IActionResult Create()
        {
            var tag = new TagVM();
            return View(tag);
        }
        [HttpPost]
        [Route("Tag/Create")]
        public async Task<IActionResult> Create(TagVM tag)
        {
            if (!ModelState.IsValid)
            {
                return View(tag);
            }

            tag.Name = tag.Name.Trim();
            var tagExitts = await _tagService.ExistsByNameAsync(tag.Name);
            if (tagExitts)
            {
                ModelState.AddModelError(string.Empty, "loi trung name");
                return View(tag);
            };
            await _tagService.CreateTag(tag);
            return RedirectToAction("Create");

        }
        [HttpGet]
        [Route("Tag/Edit/{id}")]
        public async Task<ActionResult> EditTag(int id)
        {
            var tag = new TagVM();
            if (id == null)
            {
                NotFound();
            }
            var tagdb = await _tagService.GetByIdAsync(id);
            if (tagdb != null)
            {
                tag.TagId = tagdb.TagId;
                tag.Name = tagdb.Name;
                tag.DisplayName = tagdb.DisplayName;
            }
            return View(tag);

        }
        [HttpPost]
        [Route("Tag/Edit/{id}")]
        public async Task<ActionResult> EditTag(int id, TagVM model)
        {
            if (!ModelState.IsValid || !model.TagId.Equals(id))
            {
                return View(model);
            }

            var tagexit = await _tagService.ExitAsync(id);
            if (!tagexit)
            {
                return View(model);
            }
            model.Name = model.Name.Trim();
            var tagByName = await _tagService.GetName(model.Name);
            if (tagByName != null && !tagByName.TagId.Equals(id))
            {
                return View(model);
            }
            await _tagService.EditTag(model);
            return View(model);
        }
        [HttpPost]
        [Route("Tag/Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if(id == null)
            {
                NotFound();
            }
            var result = _tagService.DeleteTag(id);
            return RedirectToAction("Index");
        }
    }
}
