using ApiProjet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiProjet.Interfaces
{
    public interface ITodoServiceApiProjet
    {
        Task<List<Comment>> GetAllCommentAsync(string accessToken);
        Task<List<Comment>> GetCommentByIdAsync(string id,string accessToken);
        Task<Comment> GetaCommentByIdAsync(int id,string accessToken);
        Task<Comment> PostCommentAsync(Comment comment,string accessToken);
        Task<Comment> UpdateACommentAsync(Comment comment,string accessToken);
        Task DeleteACommentAsync(int id, string accessToken);


    }
}
