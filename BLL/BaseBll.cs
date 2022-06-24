namespace BLL;

using System.Threading.Tasks;
using BLL.Interfaces;

public abstract class BaseBll : IBll
{
    public abstract int SaveChanges();

    public abstract Task<int> SaveChangesAsync();

}