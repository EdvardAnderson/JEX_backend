namespace JEX_backend.API;

public interface ISaveable
{
    Task<bool> SaveChangesAsync();
}
