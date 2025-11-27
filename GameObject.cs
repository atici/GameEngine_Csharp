using SDL3;

public class GameObject : IDrawable
{
	public string name = "Gameobject";
	public Transform transform = new();
	public Color color = new(0, 0, 255, 255);

	internal virtual void _Init(){}
	internal virtual void _Start(){}
	internal virtual void _Update(float delta){}

	public GameObject()
	{
		MainGameLoop.Start += (s, e) => _Start(); 
		MainGameLoop.Update += (s, d) => _Update(d); 
		_Init();
	}

	~GameObject()
	{
		MainGameLoop.Update -= (s, d) => _Update(d); 
	}

	public void Draw(nint renderer)
	{
		SDL_e.SetRenderDrawColor(renderer, color);
		SDL.RenderFillRect(renderer, transform.FRect);
	}
}
