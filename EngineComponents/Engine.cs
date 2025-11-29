using SDL3;

namespace Engine;

public class Engine
{
	public MainGameLoop mainGameLoop {get; protected set;} = new MainGameLoop();
	public Physics physics {get; protected set;} = new Physics();
	public Renderer renderer {get; protected set;} = new Renderer(0);
	
    [STAThread]
    public void Start(Game game) {
        if (!SDL.Init(SDL.InitFlags.Video)) {
            SDL.LogError(SDL.LogCategory.System, $"SDL could not initialize: {SDL.GetError()}");
            return;
        }

        if (!SDL.CreateWindowAndRenderer("Game Engine v0.1", Constants.WINDOW_SIZE_WIDTH, Constants.WINDOW_SIZE_HEIGHT, 0, out var window, out var canvas)) {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            return;
        }
		renderer.SetCanvas(canvas);

/********************** Start Game *****************************/
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