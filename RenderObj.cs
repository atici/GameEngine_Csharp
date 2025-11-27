using SDL3;

public class RenderObj
{
	public const int MOVE_SPEED = 3;
	public const int GRAVITY = 1;
	public SDL.FRect rect;

	public bool is_static = false;

	public RenderObj(SDL.FRect _rect)
	{
		rect = _rect;
	}

	public void Move(Vector2Int offset)
	{
		Move(offset.X, offset.Y);
	}

	public void Move(int x, int y)
	{
		rect.X += x;
		rect.Y += y;
	}

	public void CalculateGravity()
	{
		if ( is_static) return;
		Move(0, GRAVITY);
	}
}