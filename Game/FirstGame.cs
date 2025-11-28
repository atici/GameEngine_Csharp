using System.ComponentModel;
using System.Numerics;
using Engine;

namespace FirstGame;
public class FirstGame : Engine.Game
{
	public override void Init()
	{
		float gridCellSize = Physics.Units.METRE ;
		Grid<GridItem> engineGrid = new(
			(int)MathF.Ceiling(Constants.WINDOW_SIZE_WIDTH / gridCellSize)
			,(int)MathF.Ceiling(Constants.WINDOW_SIZE_HEIGHT / gridCellSize)
			,gridCellSize
			,(g,x,y) => new GridItem(ref g, x, y));
		engineGrid.Color = new(60,60,60);
		Registrar.RegisterDrawable(engineGrid);

		Player player = new Player();
		player.transform = new Transform(new Vector2(30, 100), new Vector2(30, 60));
		player.transform.is_static = true;

		Circle circle = new Circle();
		circle.transform.is_static = true;
		circle.transform.Position = new Vector2(1000, 400);
		circle.transform.Size.X = 300;
	}
}

/****** Run the Engine and feet the game class. ******/
internal static class Program
{
	public static void Main() {
		Console.WriteLine("Starting");
		Engine.Engine.Start(new FirstGame());
	}
}