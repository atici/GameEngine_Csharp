namespace Engine;
public abstract class GoComponent
{
	public bool enabled = true;
	public GameObject ?gameObject {get; protected set;}
	public Transform transform => gameObject == null ? default! : gameObject.transform;

	internal virtual void AddToGameobject(GameObject gameObject) {
		this.gameObject = gameObject;
	} 

	public virtual void Destroy() {
		gameObject = null;
	}
}