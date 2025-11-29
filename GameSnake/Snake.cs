// Note: might not work on later iterations of the engine.
using System.Numerics;
using Engine;
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
	const float CELL_SIZE = 50f;
	const int SNAKE_START_SIZE = 4;

	bool isDead = false;
	float gameSpeed = 0.5f; // Smaller the faster.
	Vector2 currentDirection = Physics.Vector2_Right;
	Vector2 lastDirection = Physics.Vector2_Right;

	EngineOptions engineOptions;
	Grid<Section> grid; 
	Queue<Section> snake;
	Section head;

	float deltaTime = 0f;
	protected override void Update(float delta) {
		deltaTime += delta;
		_HandleInput(delta);

		if (isDead) return;
		if (deltaTime < gameSpeed) return;
		deltaTime = 0f;
		MoveSnake(currentDirection);
		lastDirection = currentDirection;
	}

	public MainLoop(EngineOptions options) {
		engineOptions = options;	
		isDead = false;

		snake = new Queue<Section>();
		grid = CreateGrid();
		CreateAndSetSnake(SNAKE_START_SIZE, out head);
		PlaceFood();
	}

	void Die() {
		isDead = true;
		foreach(Section s in snake) {
			Color color = new Color(
				Engine.Random.Range(128, 255)
				,Engine.Random.Range(0, 192)
				,Engine.Random.Range(64, 255));
			Rect ?rect = s.GetComponent<Rect>();
			if(rect != null)
				rect.color = color;	
			s.state = Section.State.Dead;
		}
	}

	void PlaceFood() {
		List<Section> emptySections = new();
		foreach(Section s in grid)
			if(s.state == Section.State.Empty) emptySections.Add(s);
		int numEmpty= emptySections.Count;
		if(numEmpty == 0) {
			Die(); // I dont have anything else for now.  Console.WriteLine("You won!!!");
			return;	
		}
		emptySections[Engine.Random.Range(0, numEmpty)].state = Section.State.Food;
	}

	void MoveSnake(Vector2 pos) {
		if (head == null) return;
		pos += head.gridPos;
		if (pos.X >= grid.width 
			|| pos.Y >= grid.height
			|| pos.X < 0 
			|| pos.Y < 0) {
			Die();
			return;
		}
		Section nextSection = grid.GetValue(pos);

		switch (nextSection.state) {
			case Section.State.Empty:
				MoveHead(nextSection);
				snake.Dequeue().state = Section.State.Empty;
				break;
			case Section.State.Body:
				Die();
				break;
			case Section.State.Food:
				MoveHead(nextSection);
				PlaceFood();
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

	float inputDeltaTime = 0f;
	void _HandleInput(float delta) {
		inputDeltaTime += delta;
		if (!Input.KeyboardEvent.Down) return;
		if( inputDeltaTime < 0.25f) {
			return;
		}
		inputDeltaTime = 0f;

		switch (Input.KeyboardEvent.Key)
		{
			case SDL.Keycode.Right:
				if(lastDirection == Physics.Vector2_Left) return;
				currentDirection = Physics.Vector2_Right;
				break;
			case SDL.Keycode.Left:
				if(lastDirection == Physics.Vector2_Right) return;
				currentDirection = Physics.Vector2_Left;
				break;
			case SDL.Keycode.Up:
				if(lastDirection == Physics.Vector2_Down) return;
				currentDirection = Physics.Vector2_Up;
				break;
			case SDL.Keycode.Down:
				if(lastDirection == Physics.Vector2_Up) return;
				currentDirection = Physics.Vector2_Down;
				break;
			case SDL.Keycode.R:
			 	// Reset Logic
				break;
		}
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
}