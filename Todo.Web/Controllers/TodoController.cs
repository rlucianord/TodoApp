using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Models;

namespace Todo.Web.Controllers
{
    public class TodoController : Controller
    {
        private ILogger<TodoController> _logger;
        private IConfiguration _Configure;
        private string apiBaseUrl;
        List<TodoItemDTO> TodoList;

        // Dependency Injection  
        public TodoController(ILogger<TodoController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configure = configuration;

            apiBaseUrl = _Configure.GetValue<string>("TodoItems");
        }

        public async Task<IActionResult> IndexAsync()
        {
            TodoList = new List<TodoItemDTO>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiBaseUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    TodoList = JsonConvert.DeserializeObject<List<TodoItemDTO>>(apiResponse);
                }
            }
            return View(TodoList);



        }

        [HttpGet]
        public async Task<IActionResult> EditItem(int? id)
        {
            var Todo = new TodoItemDTO();

            if (id == null)
            {
                return NotFound();
            }


            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiBaseUrl + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Todo = JsonConvert.DeserializeObject<TodoItemDTO>(apiResponse);
                }
            }
            return View(Todo);
        }

        //private static void PutsProperties(HttpClient client, string handle, Dictionary<string, string> properties)
        //{
        //    foreach (var p in properties)
        //    {
        //        var result = client.PutAsync("/api/containers/" + handle + "/properties/" + p.Key, new StringContent(p.Value)).Result;
        //        result.IsSuccessStatusCode.should_be_true();
        //    }
        //}


        [HttpPost]
        public async Task<IActionResult> EditItem(TodoItemDTO todoItemDTO)
        {
            var Todo = new TodoItemDTO();
            int id = todoItemDTO.Id;




            using (var httpClient = new HttpClient())
            {
                string Object = JsonConvert.SerializeObject(todoItemDTO);
                StringContent content =new StringContent(Object, Encoding.UTF8,"application/json");

                using (var response = httpClient.PutAsync(apiBaseUrl,content).Result)

                {

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Console.WriteLine("Success");
                    }
                }
            }
            return RedirectToAction("Index");
        }


    }
}
