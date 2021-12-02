using ApiProjet.Interfaces;
using ApiProjet.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiProjet.Services
{
    public class TodoServiceApiProjet : ITodoServiceApiProjet
    {
        private const string BaseUrl = "https://localhost:44309/api/";
        private readonly HttpClient _client;


        public TodoServiceApiProjet(HttpClient client)
        {
            _client = client;
        }
        //lister les commentaires
        public async Task<List<Comment>> GetAllCommentAsync(string accessToken)
        {
            using (var req = new HttpClient())
            {
                req.BaseAddress = new Uri($"{BaseUrl}{"comment"}");
                req.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var httpResponse = await req.GetAsync(req.BaseAddress);
                if (!httpResponse.IsSuccessStatusCode)
                { 
                    throw new Exception("Cannot retrieve tasks " + httpResponse.ReasonPhrase);
                }
                var content = await httpResponse.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<List<Comment>>(content);
                return task;
            }
        }
        //commentaire by id pour la partial view"commentaires" des photos
        public async Task<List<Comment>> GetCommentByIdAsync(string id, string accessToken)
        {
            using (var request = new HttpClient())
            {
                request.BaseAddress = new Uri($"{BaseUrl}{"comment/"}{id}");
                request.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var httpResponse = await request.GetAsync(request.BaseAddress);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve tasks " + httpResponse.ReasonPhrase);
                }
                var content = await httpResponse.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<List<Comment>>(content);
                return task; 
            }
        }
        // voir le commentaire à éditer
        public async Task<Comment> GetaCommentByIdAsync(int id,string accessToken)
        {
            using (var req = new HttpClient())
            {
                req.BaseAddress = new Uri($"{BaseUrl}{"commentById/"}{id}");
                req.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var httpResponse = await req.GetAsync(req.BaseAddress);

                if (!httpResponse.IsSuccessStatusCode)
                {
                
                    throw new Exception("Cannot retrieve tasks " + httpResponse.ReasonPhrase);
                }
                var content = await httpResponse.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<Comment>(content);
                return task;
            }
        }
        //post un commentaire
        public async Task<Comment> PostCommentAsync(Comment task,string accessToken)
        {
            // creer un nouvel id qui n'est pas dans la liste des id
            int cpt = -1;
            var check = getAllId(accessToken);
            foreach (var item in await check.ConfigureAwait(false))
            {           
                if (item > cpt) cpt = item;
            }
            task.Id = cpt+1;
            task.PostId = task.Id;
            using (var req = new HttpClient())
            {
                req.BaseAddress=new Uri($"{BaseUrl}{"comment/post"}");
                req.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var content = JsonConvert.SerializeObject(task);
                var httpResponse = await req.PostAsync(req.BaseAddress, new StringContent(content, Encoding.Default, "application/json"));
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot add a comment " + httpResponse.ReasonPhrase);
                }

                var createdTask = JsonConvert.DeserializeObject<Comment>(await httpResponse.Content.ReadAsStringAsync());
                return createdTask;
            }
        }
        //updater le commentaire
        public async Task<Comment> UpdateACommentAsync(Comment comment,string accessToken)
        {
            using (var req = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(comment);
                req.BaseAddress = new Uri($"{BaseUrl}{"comment/"}{"update/"}{comment.Id}");
                req.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var httpResponse = await req.PutAsync(req.BaseAddress, new StringContent(content, Encoding.Default, "application/json"));

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot update the comment " + httpResponse.ReasonPhrase);
                }

                var createdTask = JsonConvert.DeserializeObject<Comment>(await httpResponse.Content.ReadAsStringAsync());
                return createdTask;
            }
        }
        //supprimer un commentaire
        public async Task DeleteACommentAsync(int id,string accessToken) 
        {
            using (var req = new HttpClient())
            {
                req.BaseAddress = new Uri($"{BaseUrl}{"comment/delete/"}{id}");
                req.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var httpResponse = await req.DeleteAsync(req.BaseAddress);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("cannot retrieve task " + httpResponse.ReasonPhrase);
                }
            }
        }
        //get all sur les id
        private async Task<List<int>> getAllId(string accessToken) 
        {
            using (var req = new HttpClient())
            {                
                req.BaseAddress =new Uri($"{BaseUrl}{"iDcomment"}");
                req.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var httpResponse = await req.GetAsync(req.BaseAddress);
                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception("Cannot retrieve task " + httpResponse.ReasonPhrase);
                }
                var content = await httpResponse.Content.ReadAsStringAsync();
                var task = JsonConvert.DeserializeObject<List<int>>(content);
                return task;
            }
        }


    }
}
