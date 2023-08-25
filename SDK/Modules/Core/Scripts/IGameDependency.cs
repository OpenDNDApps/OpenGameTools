namespace OGT
{
    public class GameDependency : BaseBehaviour, IGameDependency { }
    
    public interface IGameDependency
    {
        public static bool IsReady { get; private set; }
    } 
}
