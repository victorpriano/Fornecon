using Fornecon.WebAPI.Models;

namespace Fornecon.WebAPI.Infra.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetProdutos();
        Task<IEnumerable<Produto>> GetProdutoById(int id);
        Task<int> AddProduto(Produto produto);
        Task<int> UpdateProduto(int id, Produto produto);
        Task<int> DeleteProduto(int id);
    }
}
