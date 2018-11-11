
public class InitGameCommand : Command {
    public override void Execute(Game.GamePlayManager game)
    {
        game.Init();
    }
}
