
public class InitGameCommand : Command {
    public override void Execute(Game game)
    {
        game.Init();
    }
}
