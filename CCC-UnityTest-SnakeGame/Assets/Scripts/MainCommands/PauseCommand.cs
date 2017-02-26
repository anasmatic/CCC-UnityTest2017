
public class PauseCommand : Command {
    public override void Execute(Game game)
    {
        game.Pause();
    }
}
