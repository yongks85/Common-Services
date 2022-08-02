namespace Bootstrap.MultiStart;

/// <summary>
/// Services that will be resolved and start after registration
/// </summary>
public interface IOnStartService
{

    void Initialize();

}
