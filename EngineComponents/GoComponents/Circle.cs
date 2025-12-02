using SDL3;

namespace Engine;
public class Circle : DrawableComponent
{
	public float radius;
	public bool fill = true;

	public Circle(float radius, GameObject go) : base(go) {
		this.radius = radius;
	}

	public override bool Draw(nint canvas) {
		if(!enabled) return false;
		SDL_e.SetRenderDrawColor(canvas, color);
		if (fill)
			return SDL_e.RenderFillCircle(canvas, transform.position.X, transform.position.Y, radius);
		else
			return SDL_e.RenderCircle(canvas, transform.position.X, transform.position.Y, radius);
	}
}