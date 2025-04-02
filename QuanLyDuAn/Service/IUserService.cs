using QuanLyDuAn.Models;

namespace QuanLyDuAn.Service
{
    public interface IUserService
    {
        User Register(User user);
        User Login(string username, string password);
    }
}
