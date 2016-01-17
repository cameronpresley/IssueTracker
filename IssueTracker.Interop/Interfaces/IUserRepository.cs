using IssueTracker.Core;

namespace IssueTracker.Interop.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(Models.User user);
        void DeleteUser(Models.User user);
    }
}
