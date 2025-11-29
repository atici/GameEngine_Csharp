using SDL3;

namespace Engine;
public class Renderer
{
	private nint canvas = 0;
	private HashSet<IDrawable> drawables = new();

	public Color BGcolor = new Color(20,20,20);

	public bool Render()
	{
		Clear();
		foreach(IDrawable d in drawables)
		{
			if (!d.Draw(canvas))
				return false;
		}

		SDL.RenderPresent(canvas);
		return true;
	}

	bool Clear()
	{
		SDL_e.SetRenderDrawColor(canvas, BGcolor);
		return SDL.RenderClear(canvas);
	}

	public void SetCanvas(nint canvas) => this.canvas = canvas;

	void _Register(ref IDrawable drawable) {
		if (drawables.Contains(drawable)) return;
		drawables.Add(drawable);
	}
	public Renderer(nint canvas) {
		Registrar.RegisterDrawableListener += (s, d) => _Register(ref d);
		this.canvas = canvas;
	}
	~Renderer() {
		Registrar.RegisterDrawableListener -= (s, d) => _Register(ref d);
	}
}