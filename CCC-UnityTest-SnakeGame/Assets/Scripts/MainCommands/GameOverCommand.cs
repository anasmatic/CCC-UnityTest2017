
public class CameOverCommand : Command {
    public override void Execute(Game.GamePlayManager game)
    {
        game.GameOver();
    }
}
