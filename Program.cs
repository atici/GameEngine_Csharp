using System.Numerics;
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

        if (!SDL.CreateWindowAndRenderer("Game Engine v0.1", Constants.WINDOW_SIZE_WIDTH, Constants.WINDOW_SIZE_HEIGHT, 0, out var window, out var renderer))
        {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            return;
        }
/********************** Start Game *****************************/
		MainGameLoop mainGameLoop = new();
		Physics physics = new();
		Player player = new Player();
		player.transform = new Transform(new Vector2(30, 100), new Vector2(30, 60));

		// Grid 
		int gridsize = 50 ;
		Grid<GridItem<GameObject>> grid = new(20,12, gridsize, (g,x,y) => {
			GameObject go = new GameObject();
			go.transform = new Transform(new Vector2(x*gridsize,y*gridsize), new Vector2(gridsize,gridsize));
			return new GridItem<GameObject>(go, ref g, x, y);
			});

        SDL.SetRenderDrawColor(renderer, 255, 0, 0, 0);

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

			SDL.SetRenderDrawColor(renderer, 255, 0, 0, 255);
            SDL.RenderClear(renderer);

			player.Draw(renderer);
			SDL.SetRenderDrawColor(renderer, 0, 0, 0, 255);
			
			grid.Draw(renderer);
			// foreach(var item in grid)
			// {
			// 	// go.Draw(renderer);
			// 	SDL.RenderRect(renderer, item.Value.transform.FRect);
			// }
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