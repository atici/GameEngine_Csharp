// Note: might not work on later iterations of the engine.
using System.Numerics;
using Engine;

namespace Snake;

public class Snake : Game {
	GameObject ?main;

	public override void Init() {
		_Engine.physics.enabled = false; // We dont want any physics going on in this game.

		main = new MainLoop(_Engine.options);
		
	}

	public override void StartEngine() {
		_Engine.options.window_name = "Snake v1.0";
		base.StartEngine();
	}
}

public class MainLoop : GameObject {
	const int HEIGHT = 16;
	const int WIDTH = 16;
	const float CELL_SIZE = 30f;
	Vector2 gridOffset = new Vector2(WIDTH*CELL_SIZE*0.5f, HEIGHT*CELL_SIZE*0.5f);
	EngineOptions engineOptions;
	Grid<Section> ?grid;
	Queue<Vector2> ?snake;

	public MainLoop(EngineOptions options) {
		engineOptions = options;	
	}

	protected override void Start() {
		Vector2 gridOriginPosition = new Vector2(engineOptions.window_width, engineOptions.window_height) * 0.5f - gridOffset;
		grid = new Grid<Section>(WIDTH, HEIGHT, CELL_SIZE, gridOriginPosition, (g, x, y) => new Section(x, y, g));
	}

	protected override void Update(float delta) {
	}
}