using NasaImage.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NasaImage.Interface
{
    public interface ITodoServiceDate
    {
        Task<List<TodoDate>> GetAllAsync();
        Task<List<TodoResult>> GetTodoResultAsync(string date);
        Task<TodoResult> GetTodoImageResult(string identifier);
        Task<List<TodoDate>> GetPageAsync(string date);

        List<TodoDateId> GetAllForGrid();
        
    }
}
