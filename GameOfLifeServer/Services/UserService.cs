using AutoMapper;
using GameOfLifeServer.Entities;
using GameOfLifeServer.Helpers;
using GameOfLifeServer.Models.Users;

namespace GameOfLifeServer.Services;

public interface IUserService
{
    IEnumerable<User> GetAll();
    User GetById(int id);
    void Create(UserRequest model);
    //void Delete(int id);
}

public class UserService : IUserService
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public UserService(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users;
    }

    public User GetById(int id)
    {
        return getUser(id);
    }

    public void Create(UserRequest model)
    {
        // validamos.
        if (_context.Users.Any(x => x.Username == model.PlayerName))
            return;

        //
        var user = _mapper.Map<User>(model);

        _context.Users.Add(user);
        _context.SaveChanges();
    }
    
    // helper methods
    private User getUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) throw new KeyNotFoundException("Usuario no encontrado.");
        return user;
    }
}
