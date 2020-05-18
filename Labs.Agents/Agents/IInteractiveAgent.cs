namespace Labs.Agents
{
    public interface IInteractiveAgent<TAction, TActionResult>
    {
        Interaction<TAction, TActionResult> Interaction { get; }
    }
}
