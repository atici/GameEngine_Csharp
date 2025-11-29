using SDL3;

namespace Engine;
public class GameObject
{
	public string name = "Gameobject";
	public Transform transform = new();

	public HashSet<GoComponent> components = new();

	internal virtual void _Init(){}
	internal virtual void _Start(){}
	internal virtual void _Update(float delta){}

	public GameObject() {
		MainGameLoop.Start += (s, e) => {
			Registrar.RegisterGO(this);
			_Start();
			}; 
		MainGameLoop.Update += (s, d) => _Update(d); 
		_Init();
	}

	~GameObject() {
		MainGameLoop.Start -= (s, e) => {
			Registrar.RegisterGO(this);
			_Start();
			}; 
		MainGameLoop.Update -= (s, d) => _Update(d); 
	}

	public GoComponent? GetComponent<TComponent>() where TComponent : GoComponent {
		return components.FirstOrDefault((c) => c.GetType() is TComponent);
	}
	public GoComponent? AddComponent<TComponent>(TComponent component) where TComponent : GoComponent {
		components.Add(component);
		return component;
	}
}