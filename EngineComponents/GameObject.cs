namespace Engine;
public class GameObject
{
	public string name = "Gameobject";
	public Transform transform { get; }

	private HashSet<Component> components { get; } = new();
	private bool _destroyed = false;
	private bool _enabled = true;
	public bool enabled {
		get => _enabled;
		set {
			if (_destroyed) return;
			foreach(Component comp in components)
				comp.enabled = value;
			_enabled = value;
		}
	}

	protected virtual void Init(){}
	protected virtual void Start(){}
	protected virtual void Update(float delta){}
	protected virtual void OnDestroy(){}

	private void _Start() {
		if (!enabled) return;
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
		transform = new Transform(this);
		GameClock.Start += _Start;
		GameClock.Update += _Update; 
		this.enabled = enabled;
		_Init();
	}

	public void Destroy() {
		if(_destroyed) return;
		_destroyed = true;

		enabled = false;
		GameClock.Start -= _Start;
		GameClock.Update -= _Update; 
		OnDestroy();
		_RemoveAllComponents();
	}

#region Components
	public TComponent? GetComponent<TComponent>() where TComponent : Component {
		return components.FirstOrDefault((c) => c is TComponent) as TComponent;
	}
	public TComponent? AddComponent<TComponent>(TComponent component) where TComponent : Component {
		if (component.gameObject != this) return null;
		components.Add(component);
		return component;
	}
	public bool RemoveComponent<TComponent>(TComponent? component) where TComponent : Component {
		if(component == null) return false;
		bool result = components.Remove(component);
		if(!result) return false;
		component.Destroy();
		return result;
	}
	private void _RemoveAllComponents() {
		// Copying to a new array because RemoveComponent modifies the components HashSet.
		Component[] componentsCopy = new Component[components.Count];
		components.CopyTo(componentsCopy);	
		foreach(Component comp in componentsCopy)
			RemoveComponent(comp);
	}
#endregion
#region Children
	public Transform? AddChildren(Transform transform) => transform.AddChildren(transform);

	public TGameObject? AddChildren<TGameObject>(TGameObject go) where TGameObject : GameObject => AddChildren(go.transform)?.gameObject as TGameObject;

	public bool RemoveChild(Transform child) => transform.RemoveChild(child);

	public GameObject? GetChild(int index) => transform.GetChild(index)?.gameObject;
	public List<GameObject> GetAllChildren() {
		List<GameObject> result = new();
		foreach(Transform c in transform.GetAllChildren())
			result.Add(c.gameObject);
		return result;
	}
	#endregion
}