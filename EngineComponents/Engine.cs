using System.Numerics;
using SDL3;
using Engine;

namespace Engine;

public static class Engine
{
    [STAThread]
    public static void Start(Game game)
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

		game.Init();

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