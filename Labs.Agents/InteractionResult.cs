namespace Labs.Agents
{
    public enum InteractionResult
    {
        // Na przykłąd jeśli agent jest Destroyed to ignorujemy jego akcje.
        Ignored,
        Success,
        Collision
    }
}
