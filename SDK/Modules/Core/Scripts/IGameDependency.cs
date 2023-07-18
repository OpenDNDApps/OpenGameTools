namespace OGT
{
    public interface IGameDependency
    {
        public static bool IsReady { get; private set; }
    } 
}
