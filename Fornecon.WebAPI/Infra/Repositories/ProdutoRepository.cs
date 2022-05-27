using Dapper;
using Fornecon.WebAPI.Models;
using Fornecon.WebAPI.Infra.Repositories.Interfaces;
using Fornecon.WebAPI.Repository.DataContext;
using System.Data;

namespace Fornecon.WebAPI.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly Context _context;
        public ProdutoRepository(Context context)
        {
            _context = context;
        }

        public async Task<int> AddProduto(Produto produto)
        {
            var incluir = await _context.Connection.ExecuteAsync("sp_Add_Produto",
                new
                {
                    IdFornecedor = produto.IdFornecedor,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    DataInclusao = produto.DataInclusao,
                    Quantidade = produto.Quantidade,
                    Ativo = produto.Ativo,
                    Preco = produto.Preco
                }, commandType: CommandType.StoredProcedure);

            return incluir;
        }

        public async Task<int> DeleteProduto(int id)
        {
            var delete = await _context.Connection.ExecuteAsync("sp_Delete_Produto",
                new
                {
                    Id = id
                }, commandType: CommandType.StoredProcedure);

            return delete;
        }

        public async Task<IEnumerable<Produto>> GetProdutoById(int id)
        {
            var produto = await _context.Connection.QueryAsync<Produto, Fornecedor, Produto>(
                $"SELECT * FROM Produto P INNER JOIN Fornecedor F ON F.Id = P.Id_Fornecedor WHERE P.Id = {id}",
                map: (prod, forn) =>
                {
                    prod.Fornecedor = forn;
                    return prod;
                }, splitOn: "Id_Fornecedor, Id");

            return produto;
        }

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = await _context.Connection.QueryAsync<Produto, Fornecedor, Produto>(
                $"SELECT * FROM Produto P INNER JOIN Fornecedor F ON F.Id = P.Id_Fornecedor",
                map: (prod, forn) =>
                {
                    prod.Fornecedor = forn;
                    return prod;
                }, splitOn: "Id_Fornecedor, Id");

            return produtos;
        }

        public async Task<int> UpdateProduto(int id, Produto produto)
        {
            var update = await _context.Connection.ExecuteAsync("sp_Update_Produto",
                new
                {
                    Id = id,
                    Nome = produto.Nome,
                    Descricao = produto.Descricao,
                    DataInclusao = produto.DataInclusao,
                    Quantidade = produto.Quantidade,
                    Ativo = produto.Ativo,
                    Preco = produto.Preco
                }, commandType: CommandType.StoredProcedure);

            return update;
        }
    }
}
