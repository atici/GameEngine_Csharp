using SDL3;

namespace Engine;
public class Circle : GoComponent, IDrawable
{
	public float radius;
	public Color color = Color.White;
	public bool fill = true;

	public Circle(float radius, GameObject gameObject)
	{
		System.Diagnostics.Debug.Assert(gameObject != null);
		this.radius = radius;
		this.gameObject = gameObject;
		
		Registrar.RegisterDrawable(this);
	}

	public void Destroy()
	{
		gameObject?.components.Remove(this);
		Registrar.DeRegisterDrawable(this);
	}

	public bool Draw(nint canvas)
	{
		if(!enabled) return false;
		SDL_e.SetRenderDrawColor(canvas, color);
		if (fill)
			return SDL_e.RenderFillCircle(canvas, transform.Position.X, transform.Position.Y, radius);
		else
			return SDL_e.RenderCircle(canvas, transform.Position.X, transform.Position.Y, radius);
	}
}