using JournalClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JournalClient.Controllers
{
    public class BrandController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BrandController(IHttpClientFactory httpClientFactory) 
        { 
            _httpClientFactory = httpClientFactory;
        }

        // GET: BrandController
        public async Task<ActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7146/api/brand");


            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    List<Brand> brands = JsonConvert.DeserializeObject<List<Brand>>(content);

                    if (brands != null) {

                        return View(brands);

                    }
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "API request failed with status code: " + response.StatusCode);
                    return View();
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                return View();
            }

            return View();
           
        }

        // GET: BrandController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Brand brand)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonContent = System.Text.Json.JsonSerializer.Serialize(brand);

            HttpContent content = new StringContent(
                jsonContent,
                System.Text.Encoding.UTF8,
                "application/json"
                );

            var response = await client.PostAsync("https://localhost:7146/api/brand", content);

            try
            {

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "API request failed with status code: " + response.StatusCode);
                    return View(brand);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                return View(brand);
            }
        }

        // GET: BrandController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BrandController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
