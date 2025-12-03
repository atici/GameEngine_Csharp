namespace Engine;
public abstract class Component
{
	public bool enabled = true;
	public GameObject gameObject {get; private set;}
	public Transform transform => gameObject.transform;

	public Component(GameObject gameObject, bool addToComponents = true) {
		this.gameObject = gameObject;
		if (addToComponents)
			gameObject.AddComponent(this);
		Init();
	}

	private bool _destroyed = false;

	protected virtual void Init(){}
	protected virtual void OnDestroy(){}

	public void Destroy() {
		if(_destroyed) return;
		_destroyed = true;
		enabled = false;
		gameObject.RemoveComponent(this);
		OnDestroy(); // This needs to go first.
	}

	internal void internal_Destroy() {
		if(_destroyed) return;
		_destroyed = true;
		enabled = false;
		OnDestroy(); // This needs to go first.
	}
}