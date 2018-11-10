internal class MoveDownCommand : Command
{
    public override void Execute(Game.Player.SnakeHead snakeHead)
    {
        snakeHead.MovementHandler(Constants.DOWN);
    }
}