namespace DefaultNamespace;

public class AssetsRepository : IAssetsRepository
{
    private readonly AppDbContext _context;

    public AssetsRepository(AppDbContext context)
    {
        this._context = context;
    }
}