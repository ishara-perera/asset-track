namespace DefaultNamespace;

public class IAssetsRepository
{
    Task<List<Asset>> GetAllAsync();
    Task<<Asset>> GetAssetByIdAsync(int id);
    Task<<Asset>> CreateAsync(Asset asset);
    Task<<Asset>> UpdateAsset(int id, Asset asset);
    Task DeleteAsset(int id);
}