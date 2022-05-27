using Dapper;
using Fornecon.WebAPI.Models;
using Fornecon.WebAPI.Infra.Repositories.Interfaces;
using Fornecon.WebAPI.Repository.DataContext;
using System.Data;

namespace Fornecon.WebAPI.Repository
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly Context _context;
        public FornecedorRepository(Context context)
        {
            _context = context;
        }

        public async Task<int> AddFornecedor(Fornecedor fornecedor)
        {
            var incluir = await _context.Connection.ExecuteAsync("sp_Add_Fornecedor",
                
            new
            {
                Nome = fornecedor.Nome,
                NomeFantasia = fornecedor.NomeFantasia,
                CNPJ = fornecedor.CNPJ,
                Ativo = fornecedor.Ativo
            }, commandType: CommandType.StoredProcedure);

            return incluir;
        }

        public async Task<int> DeleteFornecedor(int id)
        {
            var delete = await _context.Connection.ExecuteAsync("sp_Delete_Fornecedor",
                new
                {
                    Id = id
                }, commandType: CommandType.StoredProcedure);
            
            return delete;
        }

        public async Task<IEnumerable<Fornecedor>> GetFornecedorById(int id)
        {
            var fornecedor = await _context.Connection.QueryAsync<Fornecedor>($"SELECT * FROM Fornecedor WHERE Id = {id}");

            return fornecedor;
        }

        public async Task<IEnumerable<Fornecedor>> GetFornecedores()
        {
            var fornecedores = await _context.Connection.QueryAsync<Fornecedor>($"SELECT * FROM Fornecedor");

            return fornecedores;
        }

        public async Task<int> UpdateFornecedor(int id, Fornecedor fornecedor)
        {
            var update = await _context.Connection.ExecuteAsync("sp_Update_Fornecedor",
                new
                {
                    Id = fornecedor.Id,
                    Nome = fornecedor.Nome,
                    NomeFantasia = fornecedor.NomeFantasia,
                    CNPJ = fornecedor.CNPJ,
                    Ativo = fornecedor.Ativo

                }, commandType: CommandType.StoredProcedure);

            return update;
        }
    }
}
