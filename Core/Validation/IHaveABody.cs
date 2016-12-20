namespace Core.Validation
{
    public interface IHaveABody<TBody>
    {
        TBody Body { get;set; }
    }
}