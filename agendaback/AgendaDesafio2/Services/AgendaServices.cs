using AgendaDesafioAPI.Data;
using AgendaDesafioAPI.Models;
using AutoMapper;


namespace AgendaDesafio2.Services
{
    
    public class AgendaServices : IAgendaService
    {
        private DataContext _context;
        private readonly IMapper _mapper;
        public AgendaServices(DataContext context,IMapper mapper
            )
        {
            _context = context;
            _mapper = mapper;

        }

        public IEnumerable<Agenda> GetAll()
        {
            return _context.Agendas;
        }

        public Agenda GetById(int id)
        {
            return getAgenda(id);
        }


        public void Create(Agenda model)
        {
            // validate
            if (_context.Agendas.Any(x => x.Email == model.Email))
                throw new Exception("User with the email '" + model.Email + "' already exists");

            // map model to new user object
            var user = _mapper.Map<Agenda>(model);

            
            // save user
            _context.Agendas.Add(user);
            _context.SaveChanges();
        }

        public void Update(int id, Agenda model)
        {
            var user = getAgenda(id);

            // validate
            if (model.Email != user.Email && _context.Agendas.Any(x => x.Email == model.Email))
                throw new Exception("User with the email '" + model.Email + "' already exists");

            

            // copy model to user and save
            _mapper.Map(model, user);
            /*user.Nome = model.Nome;
            user.Email = model.Email;
            user.Telefone = model.Telefone;*/
            _context.Agendas.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = getAgenda(id);
            _context.Agendas.Remove(user);
            _context.SaveChanges();
        }
        private Agenda getAgenda(int id)
        {
            var agenda = _context.Agendas.Find(id);
            if (agenda == null) throw new KeyNotFoundException("User not found");
            return agenda;
        }

    }
}
