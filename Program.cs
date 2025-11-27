using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using SDL3;

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

        if (!SDL.CreateWindowAndRenderer("SDL3 Create Window", 800, 600, 0, out var window, out var renderer))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            return;
        }
/********************** Start Game *****************************/
		MainGameLoop mainGameLoop = new();
		Player player = new Player();
		player.transform = new Transform(new Vector2(30, 100), new Vector2(30, 60));

		// Grid 
		Grid<GameObject> grid = new(20,12, 50f, (g,x,y) => {
			GameObject go = new GameObject();
			go.transform = new Transform(new Vector2(x*50,y*50), new Vector2(50, 50));
			return go;
			});

        SDL.SetRenderDrawColor(renderer, 255, 0, 0, 0);

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
			mainGameLoop.UpdateLoop();

			SDL.SetRenderDrawColor(renderer, 255, 0, 0, 255);
            SDL.RenderClear(renderer);

			player.Draw(renderer);
			SDL.SetRenderDrawColor(renderer, 0, 0, 0, 255);
			foreach(GameObject go in grid)
			{
				// go.Draw(renderer);
				SDL.RenderRect(renderer, go.transform.FRect);
			}
			// SDL.SetRenderDrawColor(renderer, 255, 255, 0, 255);
			// SDL.RenderFillRect(renderer, player.transform.FRect);
			SDL.RenderLine(renderer, 0, 0, 800, 600);

            SDL.RenderPresent(renderer);

			// SDL.RenderRect(renderer, )
        }

        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);

        SDL.Quit();
    }
}