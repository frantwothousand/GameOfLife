using AutoMapper;
using GameOfLifeServer.Entities;
using GameOfLifeServer.Helpers;
using GameOfLifeServer.Hubs;
using GameOfLifeServer.Models.Users;

namespace GameOfLifeServer.Services;

public interface ISessionService
{
    IEnumerable<Session> GetSessions();
    Session GetByName(string name);
    void Create(Session model);
}

public class SessionService : ISessionService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public SessionService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /*public void AddGroup(string group)
    {
        var item = _context.Sessions.Add(new Session { Name = group });
        _context.SaveChanges();
    }*/

    public IEnumerable<Session> GetSessions()
    {
        return _context.MatchGroup;
    }

    public Session GetByName(string name)
    {
        return getSession(name);
    }

    public void Create(Session model)
    {
        // validamos.
        if (_context.MatchGroup.Any(x => x.Name == model.Name))
            throw new ApplicationException($"El grupo {model.Name} ya existe.");

        var item = _mapper.Map<Session>(model);

        _context.MatchGroup.Add(item);
        _context.SaveChanges();
    }

    public void AddUser(string name, string username)
    {
        // we add user to group.
        if (!_context.MatchGroup.Any(x => x.Name == name))
            throw new ApplicationException($"El grupo {name} no existe.");

        if (!_context.MatchGroup.First(x => x.Name == name).Users.Any(x => x.Username == username))
        {
            _context.MatchGroup.First(x => x.Name == name).Users.Add(new User { Username = username });
            _context.SaveChanges();
        }
        else return;
        
    }

    public bool SessionExists(string name)
    {
        return _context.MatchGroup.FirstOrDefault(x => x.Name == name) != null;
    }

    public void CreateNewMessage(MessageType message)
    {
        if(SessionExists(message.Group))
        {
            var item = _mapper.Map<MessageEntity>(message);

            _context.Messages.Add(item);
            _context.SaveChanges();
        }
    }

    // helper methods
    private Session getSession(string name)
    {
        var session = _context.MatchGroup.Find(name);
        if (session == null) throw new KeyNotFoundException("Sesion no encontrada.");
        return session;
    }
}