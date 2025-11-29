/****** Run the Engine and feet the game class. ******/
using Engine;

internal static class Program
{
	public static void Main() {
		Game game = new Snake.Snake();
		game.StartEngine();
	}
}