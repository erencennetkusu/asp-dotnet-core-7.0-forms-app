using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormsApp.Controllers;

public class HomeController : Controller
{

    public IActionResult Index(string searchString, string category)
    {
        var products = Repostory.Products;

        if (!string.IsNullOrEmpty(searchString))
        {

            ViewBag.searchString = searchString;

            products = products.Where(x => x.Name.ToLower().Contains(searchString)).ToList();
        }


        if (!string.IsNullOrEmpty(category) && category != "0")
        {


            products = products.Where(x => x.CategoryId == int.Parse(category)).ToList();
        }

        ViewBag.Categorys = new SelectList(Repostory.Category, "CategoryId", "CategoryName", category);

        return View(products);
    }

    public IActionResult Create()
    {
        ViewBag.Categorys = new SelectList(Repostory.Category, "CategoryId", "CategoryName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {
        ViewBag.Categorys = new SelectList(Repostory.Category, "CategoryId", "CategoryName");


        if (imageFile != null)
        {

            var extension = Path.GetExtension(imageFile.FileName); //dosyanın uzantısını alır.
            var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
            if (ModelState.IsValid)
            {



                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                model.Image = randomFileName;
                model.ProductId = Repostory.Products.Count + 1;
                Repostory.productCreate(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {

                ModelState.AddModelError("", "User is locked.");
                return View(model);

            }
        }
        else
        {
            ModelState.AddModelError("", "User is locked.");
            return View(model);
        }



    }

    public IActionResult Edit(int? id)
    {


        if (id == null)
        {
            return NotFound();
        }
        var entity = Repostory.Products.FirstOrDefault(x => x.ProductId == id);

        ViewBag.Categorys = new SelectList(Repostory.Category, "CategoryId", "CategoryName", entity?.CategoryId);
        return View(entity);
    }



    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product Model, IFormFile? imageFile)
    {


        if (id == Model.ProductId)
        {

            if (ModelState.IsValid)
            {

                if (imageFile != null)
                {
                    var extension = Path.GetExtension(imageFile.FileName); //dosyanın uzantısını alır.
                    var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    Model.Image = randomFileName;
                }
                Repostory.updateProduct(Model);
                return RedirectToAction(nameof(Index));
            }
        }
        var entity = Repostory.Products.FirstOrDefault(x => x.ProductId == id);
        ViewBag.Categorys = new SelectList(Repostory.Category, "CategoryId", "CategoryName", entity?.CategoryId);
        return View(entity);

    }


    public IActionResult Delete(int id)
    {

        if (id != null)
        {
            var entity = Repostory.Products.FirstOrDefault(x => x.ProductId == id);
            Repostory.deleteProduct(entity);
            return RedirectToAction(nameof(Index));
        }
        else
        {
            var entity = Repostory.Products.FirstOrDefault(x => x.ProductId == id);
            ViewBag.Categorys = new SelectList(Repostory.Category, "CategoryId", "CategoryName", entity?.CategoryId);
            return View(entity);
        }
    }





}
