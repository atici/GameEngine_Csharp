namespace Engine;
public abstract class Component
{
	public bool enabled = true;
	public GameObject gameObject {get; private set;}
	public Transform transform => gameObject.transform;

	public Component(GameObject gameObject) {
		this.gameObject = gameObject;
		gameObject.AddComponent(this);
		Init();
	}

	private bool _destroyed = false;

	public virtual void Init(){}
	public virtual void OnDestroy(){}

	public void Destroy() {
		if(_destroyed) return;
		_destroyed = true;

		enabled = false;
		gameObject.RemoveComponent(this);
		OnDestroy(); // This needs to go first.
	}
}