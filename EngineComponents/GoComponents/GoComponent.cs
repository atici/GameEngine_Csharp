
namespace Engine;
public abstract class GoComponent
{
	public bool enabled = true;
	public GameObject ?gameObject;
	public Transform transform => gameObject == null ? default! : gameObject.transform;
}