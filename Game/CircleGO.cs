using System.Drawing;
using Engine;
using SDL3;

public class Circle : GameObject
{
	public override bool Draw(nint canvas)
	{
		SDL_e.SetRenderDrawColor(canvas, color);
		SDL_e.RenderFillCircle(canvas, transform.Position.X, transform.Position.Y, transform.Size.X);
		return true;
	}
}