namespace CardSystem.Provider
{
    public interface IProvider<TParam, TReturn>
    {
        TReturn Provide(TParam param);
    }
}

