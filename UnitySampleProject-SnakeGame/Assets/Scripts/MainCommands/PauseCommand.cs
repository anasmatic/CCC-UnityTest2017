
public class PauseCommand : Command {
    public override void Execute(Game.GamePlayManager game)
    {
        game.Pause();
    }
}
