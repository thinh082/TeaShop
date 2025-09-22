using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TeaShop.Models;
using TeaShop.Respository;

namespace TeaShop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CartRespostory _cartrepository;
    private readonly ProductRepository _productRepository;
    public HomeController(ILogger<HomeController> logger,CartRespostory cartRespostory, ProductRepository productRepository)
    {
        _logger = logger;
        _cartrepository = cartRespostory;
        _productRepository = productRepository;
    }

    public IActionResult Index()
    {
        string? email = HttpContext.Session.GetString("Email");       
        int sum = _cartrepository.SumProduct(email);
        if (sum > 0) 
        {
            HttpContext.Session.SetInt32("TotalPr", sum);
        }
        ViewBag.Top4 = _productRepository.Top4();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
