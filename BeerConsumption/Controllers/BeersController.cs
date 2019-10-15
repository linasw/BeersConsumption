using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BeerConsumption.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BeerConsumption.Controllers
{
    public class BeersController : Controller
    {
        private static readonly HttpClient _client;

        static BeersController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44315/api/");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(@"application/json"));
        }

        // GET: Beers
        public async Task<ActionResult> Index()
        {
            try
            {
                var jsonAsString = await _client.GetStringAsync("Beers");
                var beers = JsonConvert.DeserializeObject<List<Beer>>(jsonAsString);
                return View(beers);
            }
            catch (Exception ex)
            {
                return View(new List<Beer>());
            }
        }

        // GET: Beers/Create
        public async Task<ActionResult> Create()
        {
            var beer = new BeerCreation();
            return View(beer);
        }

        // POST: Beers/Create
        [HttpPost]
        public async Task<ActionResult> Create(BeerCreation beer)
        {
            try
            {
                var json = JsonConvert.SerializeObject(beer);
                var content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await _client.PostAsync("beers", content);
                 //
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(beer);
            }
            catch
            {
                return View(beer);
            }
        }

        // GET: Beers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var jsonAsString = await _client.GetStringAsync($"beers/{id}");
                var beer = JsonConvert.DeserializeObject<BeerEdit>(jsonAsString);
                return View(beer);
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }

        // POST: Beers/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, BeerEdit beer)
        {
            try
            {
                var json = JsonConvert.SerializeObject(beer);
                var content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await _client.PutAsync($"beers/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                return View(beer);
            }
            catch
            {
                return View(beer);
            }
        }

        // GET: Beers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _client.DeleteAsync($"beers/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}