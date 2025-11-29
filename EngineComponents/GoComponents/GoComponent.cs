namespace Engine;
public abstract class GoComponent
{
	public bool enabled = true;
	public GameObject ?gameObject;
	public Transform transform => gameObject == null ? default! : gameObject.transform;

	public GoComponent(GameObject gameObject)
	{
		System.Diagnostics.Debug.Assert(gameObject != null);
		this.gameObject = gameObject;
	}

	public virtual void Destroy() {
		gameObject?.components.Remove(this);
	}
}