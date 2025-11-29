using SDL3;

namespace Engine;
public class Engine
{
	public EngineOptions options = new EngineOptions(); 
	public GameClock clock {get; protected set;} = new GameClock();
	public Physics physics {get; protected set;} = new Physics();
	public Renderer renderer {get; protected set;} = new Renderer(0);
	
    [STAThread]
    public void Start(Game game) {
        if (!SDL.Init(SDL.InitFlags.Video)) {
            SDL.LogError(SDL.LogCategory.System, $"SDL could not initialize: {SDL.GetError()}");
            return;
        }

        if (!SDL.CreateWindowAndRenderer(options.window_name, options.window_width, options.window_height, 0, out var window, out var canvas)) {
            SDL.LogError(SDL.LogCategory.Application, $"Error creating window and rendering: {SDL.GetError()}");
            return;
        }
		renderer.SetCanvas(canvas);

/********************** Start Game *****************************/
		game.Init();

		clock.InvokeStart();
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
			clock.InvokeUpdate();
			renderer.Render();
        }

        SDL.DestroyRenderer(canvas);
        SDL.DestroyWindow(window);

        SDL.Quit();
    }
}

public class EngineOptions {
	public string window_name = "Game_Engine_v0.2";
	public int window_width =  Constants.WINDOW_SIZE_WIDTH;
	public int window_height =  Constants.WINDOW_SIZE_HEIGHT;
}