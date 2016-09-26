using SportsStore.Domain.Abstract;
using System.Web.Mvc;
using System.Linq;
using SportsStore.Domain.Entities;


namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }


        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int porductID)
        {
            Product deleteProduct = repository.DeleteProduct(porductID);
            if(deleteProduct != null)
            {
                TempData["messge"] = string.Format("{0} was deleted", deleteProduct.Name);
            }
            return RedirectToAction("Index");

        }
    }
}