
public class PauseCommand : Command {
    public override void Execute(Game.GameManager game)
    {
        game.Pause();
    }
}
