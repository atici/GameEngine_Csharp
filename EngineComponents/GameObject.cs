namespace Engine;
public class GameObject
{
	public string name = "Gameobject";
	public Transform transform = new();
	public HashSet<GoComponent> components = new();

	private bool _enabled = true;
	public bool enabled {
		get => _enabled;
		set {
			if (isDestroyed) return;
			foreach(GoComponent comp in components)
				comp.enabled = value;
			_enabled = value;
		}
	} 

	private bool isDestroyed = false;

	protected virtual void Init(){}
	protected virtual void Start(){}
	protected virtual void Update(float delta){}
	protected virtual void OnDestroy(){}

	private void _Start() {
		if (!enabled) return;
		// Registrar.RegisterPhysicsInvoke(this);
		Start();
	}

	private void _Update(float delta) {
		if (!enabled) return;
		Update(delta);
	}
	private void _Init() {
		if (!enabled) return;
		Init();
	}

	public GameObject(bool enabled = true) {
		this.enabled = enabled;
		GameClock.Start += _Start;
		GameClock.Update += _Update; 
		_Init();
	}

	public void Destroy() {
		if(isDestroyed) return;
		isDestroyed = true;
		enabled = false;
		GameClock.Start -= _Start;
		GameClock.Update -= _Update; 
		OnDestroy();
		internal_RemoveAllComponents();
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

	private void internal_RemoveAllComponents() {
		// Copying to a new array because RemoveComponent modifies the components HasMap.
		GoComponent[] componentsCopy = new GoComponent[components.Count];
		components.CopyTo(componentsCopy);	
		foreach(GoComponent comp in componentsCopy)
			RemoveComponent(comp);
	}
}