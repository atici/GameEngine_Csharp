using System.Numerics; 
using SDL3;
using Engine;

namespace FirstGame;
public class Player : GameObject
{
	const float MOVE_SPEED = 500f;
	Rect? rect;

	protected override void OnDestroy() {
		Console.WriteLine($"[{SDL.GetTicks()}]{name} has been destroyed. Last known location: {transform.position.X}:{transform.position.Y}")	;
	}

	protected override void Init() {
		name = "Player";
		Circle circle = new Circle(this, 50);
		circle.color = Color.Green;
		circle.fill = true;

		rect = new Rect(this, 30, 50);
		rect.color = Color.Blue;
		rect.fill = true;
		// rect.enabled = false;
	}

	protected override void Update(float delta) {
		_HandleInput(delta);
	}

	void _HandleInput(float delta) {
		if (!Input.KeyboardEvent.Down) return;

		switch (Input.KeyboardEvent.Key)
		{
			case SDL.Keycode.Right:
				transform.position += new Vector2(1,0) * delta * MOVE_SPEED;
				break;
			case SDL.Keycode.Left:
				transform.position += new Vector2(-1,0) * delta * MOVE_SPEED;
				break;
			case SDL.Keycode.Up:
				transform.position += new Vector2(0,-1) * delta * MOVE_SPEED;
				break;
			case SDL.Keycode.Down:
				transform.position += new Vector2(0,1) * delta * MOVE_SPEED;
				break;
			case SDL.Keycode.H:
				if(rect != null && GetComponent<Rect>() != null) 
					RemoveComponent(rect);
				break;
			case SDL.Keycode.Y:
				if (rect != null) 
					AddComponent(rect);
				break;
			case SDL.Keycode.P:
				Destroy();	
				break;
			case SDL.Keycode.R:
				RemoveComponent(rect);
				rect = new(this, 50, 50);
				rect.color = new(Engine.Random.Range(0, 255), Engine.Random.Range(0, 255), Engine.Random.Range(0, 255));
				AddComponent(rect);
				break;
		}
	}
}