
public class InitGameCommand : Command {
    public override void Execute(Game.GameManager game)
    {
        game.Init();
    }
}
