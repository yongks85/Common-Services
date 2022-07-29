namespace Bootstrap.Abstraction;

/// <summary>
/// Services that will be resolved and start after registration
/// </summary>
public interface IOnStartService<TOption> where TOption:IOption {

    void Initialize(TOption options);

}