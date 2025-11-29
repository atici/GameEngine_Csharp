using System.Numerics;
using Engine;

namespace FirstGame;
public class FirstGame : Engine.Game
{
	const string WINDOW_NAME =  "Oi Im making ma first game mama.";
	const int WINDOW_SIZE_HEIGHT = 900;
	const int WINDOW_SIZE_WIDTH = 900;

	public override void Init()
	{
		float gridCellSize = Physics.Units.METRE ;
		Grid<GridItem> engineGrid = new(
			(int)MathF.Ceiling(WINDOW_SIZE_WIDTH / gridCellSize)
			,(int)MathF.Ceiling(WINDOW_SIZE_HEIGHT / gridCellSize)
			,gridCellSize
			,(g,x,y) => new GridItem(ref g, x, y));
		engineGrid.Color = new(60,60,60);
		Registrar.RegisterDrawable(engineGrid);

		Player player = new Player();
		player.transform = new Transform(new Vector2(30, 100));
		player.transform.is_static = true;
	}

	public override void StartEngine() {
		EngineOptions options = _Engine.options;
		options.window_name = WINDOW_NAME;
		options.window_width = WINDOW_SIZE_WIDTH;
		options.window_height = WINDOW_SIZE_HEIGHT;
		base.StartEngine();
	}
}

/****** Run the Engine and feet the game class. ******/
internal static class Program
{
	public static void Main() {
		Console.WriteLine("Starting");
		FirstGame game = new FirstGame();
		game.StartEngine();
	}
}