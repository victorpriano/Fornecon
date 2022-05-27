using Fornecon.WebAPI.Models;

namespace Fornecon.WebAPI.Infra.Repositories.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<IEnumerable<Fornecedor>> GetFornecedores();
        Task<IEnumerable<Fornecedor>> GetFornecedorById(int id);
        Task<int> AddFornecedor(Fornecedor fornecedor);
        Task<int> UpdateFornecedor(int id, Fornecedor fornecedor);
        Task<int> DeleteFornecedor(int id);
    }
}
