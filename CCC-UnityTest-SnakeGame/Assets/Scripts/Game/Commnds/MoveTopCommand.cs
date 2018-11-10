public class MoveTopCommand : Command
{
    public override void Execute(Game.Player.SnakeHead snakeHead)
    {
        snakeHead.MovementHandler(Constants.TOP);
    }
}