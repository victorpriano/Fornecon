using Dapper;
using Fornecon.WebAPI.Models;
using Fornecon.WebAPI.Infra.Repositories.Interfaces;
using Fornecon.WebAPI.Repository.DataContext;
using System.Data;

namespace Fornecon.WebAPI.Repository
{
    public class ContratoRepository : IContratoRepository
    {
        private readonly Context _context;
        public ContratoRepository(Context context)
        {
            _context = context;
        }
        public async Task<int> AddContrato(Contrato contrato)
        {
            var incluir = await _context.Connection.ExecuteAsync("sp_Add_Contrato",
               new
               {
                   IdFornecedor = contrato.IdFornecedor,
                   DocContrato = contrato.DocContrato,
                   DataVencimento = contrato.DataVencimento,
                   Ativo = contrato.Ativo
               }, commandType: CommandType.StoredProcedure);

            return incluir;

        }

        public async Task<int> DeleteContrato(int id)
        {
            var delete = await _context.Connection.ExecuteAsync("sp_Delete_Contrato", 
                new
                {
                    Id = id
                }, commandType: CommandType.StoredProcedure);

            return delete;
        }

        public async Task<IEnumerable<Contrato>> GetContratoById(int id)
        {
            var contrato = await _context.Connection.QueryAsync<Contrato, Fornecedor, Contrato>(
                $"SELECT * FROM Contrato C INNER JOIN Fornecedor F ON F.Id = C.Id_Fornecedor WHERE C.Id = {id}",
                map: (con, forn) =>
                {
                    con.Fornecedor = forn;
                    return con;
                }, splitOn: "Id_Fornecedor, Id");

            return contrato;
        }

        public async Task<IEnumerable<Contrato>> GetContratos()
        {
            var contratos = (await _context.Connection.QueryAsync<Contrato, Fornecedor, Contrato>(
                $"SELECT * FROM Contrato C INNER JOIN Fornecedor F ON F.Id = C.Id_Fornecedor",
                map: (con, forn) =>
                {
                    con.Fornecedor = forn;
                    return con;
                }, splitOn: "Id_Fornecedor, Id"));

            return contratos;
        }

        public Task<int> UpdateContrato(int id, Contrato contrato)
        {
            var update = _context.Connection.ExecuteAsync("sp_Update_Contrato",
                new
                {
                    Id = id,
                    DocContrato = contrato.DocContrato,
                    DataVencimento = contrato.DataVencimento,
                    Ativo = contrato.Ativo
                }, commandType: CommandType.StoredProcedure);

            return update;
        }
    }
}
