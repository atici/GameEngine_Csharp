using System.Numerics;
using SDL3;
using Engine;

namespace Create_Window;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        if (!SDL.Init(SDL.InitFlags.Video))
        {
            SDL.LogError(SDL.LogCategory.System, $"SDL could not initialize: {SDL.GetError()}");
            return;
        }

        if (!SDL.CreateWindowAndRenderer("Game Engine v0.1", Constants.WINDOW_SIZE_WIDTH, Constants.WINDOW_SIZE_HEIGHT, 0, out var window, out var canvas))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            return;
        }
/********************** Start Game *****************************/
		MainGameLoop mainGameLoop = new();
	
		Physics physics = new();
		Renderer renderer = new Renderer(canvas);

		// Grid 
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

		mainGameLoop.InvokeStart();
        var loop = true;
        while (loop)
        {
            while (SDL.PollEvent(out var e))
            {
				Input._UpdateEvent(e);
                if ((SDL.EventType)e.Type == SDL.EventType.Quit || Input.GetKeyDown(SDL.Keycode.Q))
                {
                    loop = false;
                }
            }
			mainGameLoop.InvokeUpdate();
			renderer.Render();
        }

        SDL.DestroyRenderer(canvas);
        SDL.DestroyWindow(window);

        SDL.Quit();
    }
}