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
		GameClock.Start += (s, e) => {
			Registrar.RegisterGO(this);
			_Start();
			}; 
		GameClock.Update += (s, d) => _Update(d); 
		_Init();
	}

	~GameObject() {
		GameClock.Start -= (s, e) => {
			Registrar.RegisterGO(this);
			_Start();
			}; 
		GameClock.Update -= (s, d) => _Update(d); 
	}

	public TComponent? GetComponent<TComponent>(TComponent type) where TComponent : GoComponent {
		return components.FirstOrDefault((c) => c.GetType() == type.GetType()) as TComponent;
	}
	public TComponent AddComponent<TComponent>(TComponent component) where TComponent : GoComponent {
		// Debug.Assert(component != null);
		components.Add(component);
		component.internal_AddToGO(this);
		return component;
	}
	public bool RemoveComponent<TComponent>(TComponent component) where TComponent : GoComponent {
		bool result = components.Remove(component);
		if(!result) return false;
		component.internal_Remove();
		return result;
	}
}