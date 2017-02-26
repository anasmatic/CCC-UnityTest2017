
public class PlayGameCommand : Command {
    public override void Execute(Game game)
    {
        game.StartGame();
    }
}
