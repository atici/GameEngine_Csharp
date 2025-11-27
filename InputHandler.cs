using SDL3;

public class InputHandler
{
	RenderObj obj;
	public InputHandler(RenderObj obj)
	{
		this.obj = obj;
	}

	public void Handle(SDL.Event e)
	{
		if (!e.Key.Down) return;

		switch (e.Key.Key)
		{
			case SDL.Keycode.Right:
				obj.Move(new Vector2Int(1, 0));
				break;
			case SDL.Keycode.Left:
				obj.Move(new Vector2Int(-1, 0));
				break;
			case SDL.Keycode.Up:
				obj.Move(new Vector2Int(0, -1));
				break;
			case SDL.Keycode.Down:
				obj.Move(new Vector2Int(0, 1));
				break;
		}
	}
}