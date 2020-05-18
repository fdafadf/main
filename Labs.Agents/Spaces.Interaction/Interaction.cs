namespace Labs.Agents
{
    public class Interaction<TAction, TActionResult>
    {
        public ISpaceField From { get; internal set; }
        public ISpaceField To { get; internal set; }
        public TAction Action;
        public TActionResult ActionResult { get; internal set; }
    }
}
