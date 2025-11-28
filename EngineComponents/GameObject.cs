using SDL3;

namespace Engine;
public class GameObject : IDrawable
{
	public string name = "Gameobject";
	public Transform transform = new();
	public Color color = new(255, 255, 255, 255);
	public bool Visible = true;

	internal virtual void _Init(){}
	internal virtual void _Start(){}
	internal virtual void _Update(float delta){}

	public GameObject()
	{
		MainGameLoop.Start += (s, e) => {
			Registrar.RegisterDrawable(this);
			Registrar.RegisterGO(this);
			_Start();
			}; 
		MainGameLoop.Update += (s, d) => _Update(d); 
		_Init();
	}

	~GameObject()
	{
		MainGameLoop.Start -= (s, e) => {
			Registrar.RegisterDrawable(this);
			Registrar.RegisterGO(this);
			_Start();
			}; 
		MainGameLoop.Update -= (s, d) => _Update(d); 
	}

	public virtual bool Draw(nint canvas)
	{
		if(!Visible) return true;
		SDL_e.SetRenderDrawColor(canvas, color);
		return 
			// SDL.RenderRect(canvas, transform.FRect) 
			// && 
			SDL.RenderFillRect(canvas, GetFRect());
	}

	public SDL.FRect GetFRect() => transform.FRect;
}