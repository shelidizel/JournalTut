using Azure;
using JournalAPI.DTO;
using JournalClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JournalClient.Controllers
{
    public class JournalController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public JournalController(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: JournalController
        public async Task<ActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7146/api/journal");


            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    List<Journal> journals = JsonConvert.DeserializeObject<List<Journal>>(content);

                    if (journals != null)
                    {

                        return View(journals);

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

        // GET: JournalController/Details/5
        public async Task<ActionResult> Details(int id)
        {

            ViewBag.ProductList = null;
            ViewBag.SupplierList = null;
            ViewBag.CurrencyList = null;
            ViewBag.AccountList = null;

            var productsActionResult = await GetProducts();


            if (productsActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okResult)
            {

                if (okResult.Value is List<Product> products)
                {
                    if (products.Any())
                    {
                        ViewBag.ProductList = BuildSelectList(
                            products,
                            p => p.Code,
                            p => p.Name
                            );
                    }
                }
            }

            var suppliersActionResult = await GetSuppliers();
            if (suppliersActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okRes)
            {
                if (okRes.Value is List<Supplier> suppliers)
                {
                    if (suppliers.Any())
                    {
                        ViewBag.SupplierList = BuildSelectList(
                            suppliers,
                            p => p.SupplierID,
                            p => p.SupplierName
                            );
                    }
                }
            }

            var currenciesActionResult = await GetCurrencies();
            if (currenciesActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okRe)
            {
                if (okRe.Value is List<Currency> currencies)
                {
                    if (currencies.Any())
                    {
                        ViewBag.CurrencyList = BuildSelectList(
                            currencies,
                            p => p.Id,
                            p => p.Name
                            );
                    }
                }
            }

            var accountsActionResult = await GetAccounts();
            if (accountsActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult accResult)
            {
                if (accResult.Value is List<Account> accounts)
                {
                    if (accounts.Any())
                    {
                        ViewBag.AccountList = BuildSelectList(
                            accounts,
                            p => p.AccountID,
                            p => p.AccountName
                            );
                    }
                }
            }


            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7146/api/journal/"+id.ToString());


            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    Journal journal = JsonConvert.DeserializeObject<Journal>(content);

                    if (journal != null)
                    {

                        return View(journal);

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
            ModelState.AddModelError(string.Empty, "An error occurred: " );
            return View();
        }

        // GET: JournalController/Create
        public async Task<ActionResult> Create()
        {

            Journal item = new Journal();
            item.JournalBSs.Add(new JournalBS() { Id = 1 });
            item.JournalPLs.Add(new JournalPL() { JournalPID = 1 });

            ViewBag.ProductList = null;
            ViewBag.SupplierList = null;
            ViewBag.CurrencyList = null;
            ViewBag.AccountList = null;

            var productsActionResult = await GetProducts();
            

            if (productsActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okResult)
            {
               
                if (okResult.Value is List<Product> products)
                {
                    if (products.Any())
                    {
                        ViewBag.ProductList = BuildSelectList(
                            products,
                            p => p.Code,   
                            p => p.Name   
                            );
                    }
                }
            }

            var suppliersActionResult = await GetSuppliers();
            if (suppliersActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okRes)
            {
                if (okRes.Value is List<Supplier> suppliers)
                {
                    if (suppliers.Any())
                    {
                        ViewBag.SupplierList = BuildSelectList(
                            suppliers,
                            p => p.SupplierID,
                            p => p.SupplierName
                            );
                    }
                }
            }

            var currenciesActionResult = await GetCurrencies();
            if (currenciesActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okRe)
            {
                if (okRe.Value is List<Currency> currencies)
                {
                    if (currencies.Any())
                    {
                        ViewBag.CurrencyList = BuildSelectList(
                            currencies,
                            p => p.Id,
                            p => p.Name
                            );
                    }
                }
            }

            var accountsActionResult = await GetAccounts();
            if (accountsActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult accResult)
            {
                if (accResult.Value is List<Account> accounts)
                {
                    if (accounts.Any())
                    {
                        ViewBag.AccountList = BuildSelectList(
                            accounts,
                            p => p.AccountID,
                            p => p.AccountName
                            );
                    }
                }
            }


            return View(item);
        }

        // POST: JournalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Journal journal)
        {

            var client = _httpClientFactory.CreateClient();

            List<JournalPLDto> journalPLDtos = new List<JournalPLDto>();    

            foreach (var item in journal.JournalPLs)
            {
                JournalPLDto journalPLDto = new JournalPLDto { 
                    AccountId = item.AccountId,
                    Description = item.Description,
                    UnitID = item.UnitID,
                    StartDate = item.StartDate,
                    Isstart = item.Isstart,
                    Amount = item.Amount,
                };
                journalPLDtos.Add(journalPLDto);
            }

            List<JournalBSDto> journalBSDtos = new List<JournalBSDto>();

            foreach (var item in journal.JournalBSs)
            {
                JournalBSDto journalBSDto = new JournalBSDto
                {
                    ProductCode = item.ProductCode,
                    Quantity = item.Quantity,
                    Fob = item.Fob,
                    PrcInBaseCurr = item.PrcInBaseCurr,
                };
                journalBSDtos.Add(journalBSDto);
            }

            JournalDto journalDto = new JournalDto
            {
                JournalNumber = journal.JournalNumber,
                JournalDate = journal.JournalDate,
                SupplierID = journal.SupplierID,
                BaseCurrencyId = journal.BaseCurrencyId,
                PoCurrencyId = journal.PoCurrencyId,
                ExchangeRate = journal.ExchangeRate,
                DiscountPercentage = journal.DiscountPercentage,
                QuotationNumber = journal.QuotationNumber,
                QuotationDate = journal.QuotationDate,
                PaymentTerms = journal.PaymentTerms,
                Remarks = journal.Remarks,
                JournalBSs = journalBSDtos,
                JournalPLs = journalPLDtos,

            };

            var jsonContent = System.Text.Json.JsonSerializer.Serialize(journalDto);

            HttpContent content = new StringContent( 
                jsonContent, 
                System.Text.Encoding.UTF8, 
                "application/json" // Crucial: sets the Content-Type header
             );

            var response = await client.PostAsync("https://localhost:7146/api/journal", content);

            string errorContent = await response.Content.ReadAsStringAsync();

            Debug.WriteLine($"API Call Failed: Status Code {(int)response.StatusCode}");
            Debug.WriteLine($"Error Details: {errorContent}");

            try
            {
               
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    
                    ModelState.AddModelError(string.Empty, "API request failed with status code: " + response.StatusCode);
                    return View(journal); 
                }
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                return View(journal);
            }
        }

        // GET: JournalController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7146/api/journal/" + id.ToString());

            Journal? item = null;


            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    Journal journal = JsonConvert.DeserializeObject<Journal>(content);

                    if (journal != null)
                    {

                        item = journal;

                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }

            ViewBag.ProductList = null;
            ViewBag.SupplierList = null;
            ViewBag.CurrencyList = null;
            ViewBag.AccountList = null;

            var productsActionResult = await GetProducts();


            if (productsActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okResult)
            {

                if (okResult.Value is List<Product> products)
                {
                    if (products.Any())
                    {
                        ViewBag.ProductList = BuildSelectList(
                            products,
                            p => p.Code,
                            p => p.Name
                            );
                    }
                }
            }

            var suppliersActionResult = await GetSuppliers();
            if (suppliersActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okRes)
            {
                if (okRes.Value is List<Supplier> suppliers)
                {
                    if (suppliers.Any())
                    {
                        ViewBag.SupplierList = BuildSelectList(
                            suppliers,
                            p => p.SupplierID,
                            p => p.SupplierName
                            );
                    }
                }
            }

            var currenciesActionResult = await GetCurrencies();
            if (currenciesActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okRe)
            {
                if (okRe.Value is List<Currency> currencies)
                {
                    if (currencies.Any())
                    {
                        ViewBag.CurrencyList = BuildSelectList(
                            currencies,
                            p => p.Id,
                            p => p.Name
                            );
                    }
                }
            }

            var accountsActionResult = await GetAccounts();
            if (accountsActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult accResult)
            {
                if (accResult.Value is List<Account> accounts)
                {
                    if (accounts.Any())
                    {
                        ViewBag.AccountList = BuildSelectList(
                            accounts,
                            p => p.AccountID,
                            p => p.AccountName
                            );
                    }
                }
            }


            return View(item);
        }

        // POST: JournalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Journal journal)
        {

            var client = _httpClientFactory.CreateClient();

            List<JournalPLDto> journalPLDtos = new List<JournalPLDto>();

            foreach (var item in journal.JournalPLs)
            {
                JournalPLDto journalPLDto = new JournalPLDto
                {
                    AccountId = item.AccountId,
                    Description = item.Description,
                    UnitID = item.UnitID,
                    StartDate = item.StartDate,
                    Isstart = item.Isstart,
                    Amount = item.Amount,
                };
                journalPLDtos.Add(journalPLDto);
            }

            List<JournalBSDto> journalBSDtos = new List<JournalBSDto>();

            foreach (var item in journal.JournalBSs)
            {
                JournalBSDto journalBSDto = new JournalBSDto
                {
                    ProductCode = item.ProductCode,
                    Quantity = item.Quantity,
                    Fob = item.Fob,
                    PrcInBaseCurr = item.PrcInBaseCurr,
                };
                journalBSDtos.Add(journalBSDto);
            }

            JournalDto journalDto = new JournalDto
            {
                JournalNumber = journal.JournalNumber,
                JournalDate = journal.JournalDate,
                SupplierID = journal.SupplierID,
                BaseCurrencyId = journal.BaseCurrencyId,
                PoCurrencyId = journal.PoCurrencyId,
                ExchangeRate = journal.ExchangeRate,
                DiscountPercentage = journal.DiscountPercentage,
                QuotationNumber = journal.QuotationNumber,
                QuotationDate = journal.QuotationDate,
                PaymentTerms = journal.PaymentTerms,
                Remarks = journal.Remarks,
                JournalBSs = journalBSDtos,
                JournalPLs = journalPLDtos,

            };

            var jsonContent = System.Text.Json.JsonSerializer.Serialize(journalDto);

            HttpContent content = new StringContent(
                jsonContent,
                System.Text.Encoding.UTF8,
                "application/json" // Crucial: sets the Content-Type header
             );

            var response = await client.PutAsync("https://localhost:7146/api/journal/" + id.ToString(), content);

            string errorContent = await response.Content.ReadAsStringAsync();

            

            try
            {

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    ModelState.AddModelError(string.Empty, "API request failed with status code: " + response.StatusCode);
                    return View(journal);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, "An error occurred: " + ex.Message);
                return View(journal);
            }
        }

        // GET: JournalController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync("https://localhost:7146/api/journal/" + id.ToString());

            Journal? item = null;


            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    Journal journal = JsonConvert.DeserializeObject<Journal>(content);

                    if (journal != null)
                    {

                        item = journal;

                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }

            ViewBag.ProductList = null;
            ViewBag.SupplierList = null;
            ViewBag.CurrencyList = null;
            ViewBag.AccountList = null;

            var productsActionResult = await GetProducts();


            if (productsActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okResult)
            {

                if (okResult.Value is List<Product> products)
                {
                    if (products.Any())
                    {
                        ViewBag.ProductList = BuildSelectList(
                            products,
                            p => p.Code,
                            p => p.Name
                            );
                    }
                }
            }

            var suppliersActionResult = await GetSuppliers();
            if (suppliersActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okRes)
            {
                if (okRes.Value is List<Supplier> suppliers)
                {
                    if (suppliers.Any())
                    {
                        ViewBag.SupplierList = BuildSelectList(
                            suppliers,
                            p => p.SupplierID,
                            p => p.SupplierName
                            );
                    }
                }
            }

            var currenciesActionResult = await GetCurrencies();
            if (currenciesActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult okRe)
            {
                if (okRe.Value is List<Currency> currencies)
                {
                    if (currencies.Any())
                    {
                        ViewBag.CurrencyList = BuildSelectList(
                            currencies,
                            p => p.Id,
                            p => p.Name
                            );
                    }
                }
            }

            var accountsActionResult = await GetAccounts();
            if (accountsActionResult.Result is Microsoft.AspNetCore.Mvc.OkObjectResult accResult)
            {
                if (accResult.Value is List<Account> accounts)
                {
                    if (accounts.Any())
                    {
                        ViewBag.AccountList = BuildSelectList(
                            accounts,
                            p => p.AccountID,
                            p => p.AccountName
                            );
                    }
                }
            }


            return View(item);
        }

        // POST: JournalController/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int JournalID)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                Debug.WriteLine("==============");
                Debug.WriteLine(JournalID);

                var response = await client.DeleteAsync("https://localhost:7146/api/journal/" + JournalID.ToString());

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("===========================");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return RedirectToAction("Delete", new { id = JournalID});
            }

            return RedirectToAction("Delete", new { id = JournalID });
        }

        private async Task<ActionResult<List<Product>>> GetProducts()
        {
           
            var client = _httpClientFactory.CreateClient(); 

            try
            {
                var response = await client.GetAsync("https://localhost:7146/api/product");

                
               


                if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();

                        List<Product> products = JsonConvert.DeserializeObject<List<Product>>(content);

                        if (products != null)
                        {

                        return Ok(products);

                        }
                    }
                    else
                    {

                        return NotFound("No products were found or deserialization failed.");
                    }
              


             
            }
         
            catch (HttpRequestException ex)
            {
                return StatusCode(503, "Could not connect to the product service.");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, "Error processing product data.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred."); 
            }

            return StatusCode(500, "An unexpected error occurred.");
        }

        private async Task<ActionResult<List<Supplier>>> GetSuppliers()
        {

            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync("https://localhost:7146/api/supplier");


                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(content);

                    if (suppliers != null)
                    {

                        return Ok(suppliers);

                    }
                }
                else
                {
                    return NotFound("No suppliers were found or deserialization failed.");
                }


            }

            catch (HttpRequestException ex)
            {
                return StatusCode(503, "Could not connect to the supplier service.");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, "Error processing supplier data.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

            return StatusCode(500, "An unexpected error occurred.");
        }

        private async Task<ActionResult<List<Currency>>> GetCurrencies()
        {


            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync("https://localhost:7146/api/currency");


                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    List<Currency> currencies = JsonConvert.DeserializeObject<List<Currency>>(content);

                    if (currencies != null)
                    {

                        return Ok(currencies);

                    }
                }
                else
                {

                    return NotFound("No currencies were found or deserialization failed.");
                }




            }

            catch (HttpRequestException ex)
            {
                return StatusCode(503, "Could not connect to the currency service.");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, "Error processing currency data.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected currency occurred.");
            }

            return StatusCode(500, "An unexpected error occurred.");
        }

        private async Task<ActionResult<List<Currency>>> GetAccounts()
        {


            var client = _httpClientFactory.CreateClient();

            try
            {
                var response = await client.GetAsync("https://localhost:7146/api/account");


                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(content);

                    if (accounts != null)
                    {

                        return Ok(accounts);

                    }
                }
                else
                {

                    return NotFound("No accounts were found or deserialization failed.");
                }




            }



            catch (HttpRequestException ex)
            {
                return StatusCode(503, "Could not connect to the account service.");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, "Error processing account data.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected account occurred.");
            }

            return StatusCode(500, "An unexpected error occurred.");
        }

        private static List<SelectListItem> BuildSelectList<T>(List<T> list,  Func<T, object> valueSelector, Func<T, string> textSelector)     
        {
            if (list == null)
            {
                return new List<SelectListItem>();
            }

            
            var listSelectList = list.Select(item => new SelectListItem()
            {
                
                Value = valueSelector(item)?.ToString() ?? "",

               
                Text = textSelector(item) ?? ""
            }).ToList();

            
            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "------- Select ------"
            };

            listSelectList.Insert(0, defItem);

            return listSelectList;
        }


        }
    }
