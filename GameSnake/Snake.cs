// Note: might not work on later iterations of the engine.
using System.Numerics;
using Engine;
using Microsoft.VisualBasic;
using SDL3;

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
	const int SNAKE_START_SIZE = 4;
	EngineOptions engineOptions;
	Grid<Section> grid; 
	Queue<Section> snake;

	Section head;

	public MainLoop(EngineOptions options) {
		engineOptions = options;	

		snake = new Queue<Section>();
		grid = CreateGrid();
		CreateAndSetSnake(SNAKE_START_SIZE, out head);
	}

	protected override void Update(float delta) {
		_HandleInput(delta);
	}

	void CreateAndSetSnake(int startingSections, out Section _head) {
		Vector2 nextPos = new Vector2(
			(int)(HEIGHT * 0.5f - MathF.Ceiling(startingSections*0.5f)),
			(int)(WIDTH * 0.5f));
		_head = grid.GetValue(nextPos);
		snake.Enqueue(_head);
		for (int i = 1; i < startingSections; i++) {
			nextPos.X++;
			MoveHead(nextPos);
		}
	}
	Grid<Section> CreateGrid() {
		Vector2 gridOffset = new Vector2(WIDTH * CELL_SIZE * 0.5f, HEIGHT * CELL_SIZE * 0.5f);
		Vector2 gridOriginPosition = new Vector2(engineOptions.window_width, engineOptions.window_height) * 0.5f - gridOffset;
		return new Grid<Section>(WIDTH, HEIGHT, CELL_SIZE, gridOriginPosition, (g, x, y) => new Section(x, y, g));
	}

	void MoveSnake(Vector2 pos) {
		if (head == null) return;
		if (pos.X >= grid.width 
			|| pos.Y >= grid.height
			|| pos.X < 0 
			|| pos.Y < 0) {
			//  Dead Snake
			return;
		}
		Section nextSection = grid.GetValue(pos);

		switch (nextSection.state) {
			case Section.State.Empty:
				MoveHead(nextSection);
				snake.Dequeue().state = Section.State.Empty;
				break;
			case Section.State.Body:
				// GameOver
				break;
			case Section.State.Food:
				MoveHead(nextSection);
				break;
		}
	}

	void MoveHead(Vector2 next) => MoveHead(grid.GetValue(next));
	void MoveHead(Section next) {
		if (head == null) return;
		head.state = Section.State.Body;
		snake.Enqueue(next);
		next.state = Section.State.Head;
		head = next;
	}

	float deltaTime = 0f;
	void _HandleInput(float delta) {
		deltaTime += delta;
		if (!Input.KeyboardEvent.Down) return;
		if( deltaTime < 0.25f) {
			return;
		}
		deltaTime = 0f;

		switch (Input.KeyboardEvent.Key)
		{
			case SDL.Keycode.Right:
				MoveSnake(head.gridPos + Physics.Vector2_Right);
				break;
			case SDL.Keycode.Left:
				MoveSnake(head.gridPos + Physics.Vector2_Left);
				break;
			case SDL.Keycode.Up:
				MoveSnake(head.gridPos + Physics.Vector2_Up);
				break;
			case SDL.Keycode.Down:
				MoveSnake(head.gridPos + Physics.Vector2_Down);
				break;
			case SDL.Keycode.R:
			 	// Reset Logic
				break;
		}
	}
}