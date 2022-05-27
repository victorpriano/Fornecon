using Fornecon.WebAPI.Models;

namespace Fornecon.WebAPI.Infra.Repositories.Interfaces
{
    public interface IContratoRepository
    {
        Task<IEnumerable<Contrato>> GetContratos();
        Task<IEnumerable<Contrato>> GetContratoById(int id);
        Task<int> AddContrato(Contrato contrato);
        Task<int> UpdateContrato(int id, Contrato contrato);
        Task<int> DeleteContrato(int id);
    }
}
