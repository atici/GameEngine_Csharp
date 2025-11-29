using SDL3;

namespace Engine;
public class Rect : DrawableComponent
{
	public float height;
	public float width;
	public bool fill = true;

	public SDL.FRect FRect => new SDL.FRect{
		X= transform.Position.X - width*0.5f, 
		Y=transform.Position.Y - height*0.5f,
		W= width,
		H=height
	};

	public Rect(float width, float height, GameObject gameObject) : base(gameObject) {
		this.width = width;
		this.height = height;
	}

	public override bool Draw(nint canvas) {
		if(!enabled) return true;
		SDL_e.SetRenderDrawColor(canvas, color);
		if (fill)
			return SDL.RenderFillRect(canvas, FRect);
		else
			return SDL.RenderRect(canvas, FRect);
	}
}