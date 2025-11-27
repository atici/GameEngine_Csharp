using SDL3;

public class GameObject : IDrawable
{
	public string name = "Gameobject";
	public Transform transform = new();
	public Color color = new();

	internal virtual void _Update(float delta){}
	internal virtual void _Init(){}

	public GameObject()
	{
		MainGameLoop.UpdateEvent += (s, d) => _Update(d); 
		_Init();
	}

	~GameObject()
	{
		MainGameLoop.UpdateEvent -= (s, d) => _Update(d); 
	}

	public void Draw(nint renderer)
	{
		SDL_e.SetRenderDrawColor(renderer, color);
		SDL.RenderFillRect(renderer, transform.FRect);
	}
}
