
public class PlayGameCommand : Command {
    public override void Execute(Game.GamePlayManager game)
    {
        game.StartGame();
    }
}
