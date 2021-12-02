using NasaImage.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NasaImage.Interface;

namespace NasaImage.Service
{
    public class TodoServiceDate : ITodoServiceDate
    {
        private const string BaseUrl = "https://epic.gsfc.nasa.gov/api/natural/";
        private readonly HttpClient _client;
        private const string key = "?api_key=AXLKGW0zCxZEXQctZ4WtPpQRRA7xNbkRvyA2i0JC";
        private const string _date = "date/";

        public TodoServiceDate(HttpClient client) 
        {
            _client = client;
        }

        public async Task<List<TodoDate>> GetAllAsync() 
        {
            var httpResponse = await _client.GetAsync($"{BaseUrl}{"all"+key}");
            if (!httpResponse.IsSuccessStatusCode) 
            {
                throw new Exception("Cannot retrieve tasks");
            }
            var content = await httpResponse.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<List<TodoDate>>(content);
            return task;
        }
        public  List<TodoDateId> GetAllForGrid() 
        {
            int nbPage = 0;
            var list = new List<Model.TodoDateId>();
            List<Model.TodoDate> list1 =  GetAllAsync().Result;
            foreach (var item in list1)
            {
                nbPage++;
                list.Add(new TodoDateId { id = nbPage, date = item.date });
            }
            return list;
        }
        public async Task<List<TodoResult>> GetTodoResultAsync(string date) 
        {
            var httpResponse = await _client.GetAsync($"{BaseUrl}{_date}{date+key}");
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }
            var content = await httpResponse.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<List<TodoResult>>(content);
            return task;
        }
        public async Task<TodoResult> GetTodoImageResult(string identifier) 
        {
            string x = identifier;
            string y = x.Substring(0,4);
            string m = x.Substring(4, 2);
            string d = x.Substring(6,2);
            string date=y+"-"+m+"-"+d;
            var httpResponse = await _client.GetAsync($"{BaseUrl}{_date}{date + key}");
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }
            var content = await httpResponse.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<List<TodoResult>>(content);
            var tsk = task.Find(x => x.identifier == identifier);
            tsk.todoResultlist = task;
            return tsk;
        }
        public async Task<List<TodoDate>> GetPageAsync(string date)
        {
            var httpResponse = await _client.GetAsync($"{BaseUrl}{"all" + key}");
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }
            var content = await httpResponse.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<List<TodoDate>>(content);
            return task;
        }
    }
}
