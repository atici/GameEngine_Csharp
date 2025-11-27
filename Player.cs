using System.Numerics;
using SDL3;

public class Player : GameObject
{
	const float MOVE_SPEED = 30f;

	internal override void _Init()
	{
		color = new(200, 30, 100);
	}

	internal override void _Update(float delta)
	{
		// Console.WriteLine(delta);
		_HandleInput(delta);
	}

	void _HandleInput(float delta)
	{
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
		}
	}
}