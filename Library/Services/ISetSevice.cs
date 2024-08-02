using Library.Models;
using Library.ViewModels;

namespace Library.Services
{
    public interface ISetSevice
    {
        Task<List<SetModel>> GetSetsByShelfId(long libId);

        Task<SetModel> CreateNewSet(SetVM setVM);
    }
}
