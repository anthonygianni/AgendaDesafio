using AgendaDesafioAPI.Models;

namespace AgendaDesafio2.Services
{
    public interface IAgendaService
    {
        
            IEnumerable<Agenda> GetAll();
            Agenda GetById(int id);
            void Create(Agenda model);
            void Update(int id, Agenda model);
            void Delete(int id);
        

    }
}
