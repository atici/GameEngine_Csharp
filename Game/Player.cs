using System.Numerics; 
using SDL3;
using Engine;

namespace FirstGame;
public class Player : GameObject
{
	const float MOVE_SPEED = 500f;
	Rect rect = new(30,50);

	internal override void _Init() {
		name = "Player";
		Circle circle = new Circle(50);
		circle.color = Color.Green;
		circle.fill = false;
		AddComponent(circle);

		rect = AddComponent(new Rect(30, 50));
		rect.color = Color.Blue;
		rect.fill = true;
		// rect.enabled = false;
	}

	internal override void _Update(float delta) {
		_HandleInput(delta);
	}

	void _HandleInput(float delta) {
		if (!Input.KeyboardEvent.Down) return;

		switch (Input.KeyboardEvent.Key)
		{
			case SDL.Keycode.Right:
				transform.Position += new Vector2(1,0) * delta * MOVE_SPEED;
				break;
			case SDL.Keycode.Left:
				transform.Position += new Vector2(-1,0) * delta * MOVE_SPEED;
				break;
			case SDL.Keycode.Up:
				transform.Position += new Vector2(0,-1) * delta * MOVE_SPEED;
				break;
			case SDL.Keycode.Down:
				transform.Position += new Vector2(0,1) * delta * MOVE_SPEED;
				break;
			case SDL.Keycode.H:
				if(rect != null && GetComponent(rect) != null) 
					RemoveComponent(rect);
				break;
			case SDL.Keycode.Y:
				if (rect != null) {
					rect.AddToGameobject(this);
					AddComponent(rect);
					Console.WriteLine($"Rect dimensions: {rect.width} : {rect.height}");
				}
				break;
		}
	}
}