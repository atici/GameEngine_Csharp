namespace Engine;
public abstract class GoComponent
{
	public bool enabled = true;
	public GameObject ?gameObject {get; private set;}
	public Transform transform => gameObject == null ? default! : gameObject.transform;

	public virtual void OnAdd(){}
	public virtual void OnRemove(){}

	internal void internal_AddToGO(GameObject gameObject) {
		this.gameObject = gameObject;
		if(this.gameObject != null) OnAdd();
	} 

	internal void internal_Remove() {
		if(gameObject == null) return;
		OnRemove(); // This needs to go first.
		gameObject = null;
	}
}