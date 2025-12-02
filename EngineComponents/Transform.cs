using System.Numerics;

namespace Engine;
public class Transform : Component
{
	public Vector2 position;
	public float rotation;
	public bool is_static = false;

	public Transform? parent { get; protected set; } = null;
	private List<Transform> children { get; } = new();

	public Transform(GameObject gameObject) : base(gameObject)
	{
		position = Vector2.Zero;
		rotation = 0f;
	}

	public override void OnDestroy() {
		gameObject.Destroy();
	}

#region Children
	public Transform? AddChildren(Transform transform) {
		if (children.Contains(transform)) return null;
		children.Add(transform);
		return transform;	
	}
	public Transform? GetChild(int index) {
		if (index >= children.Count) return null;
		return children[index];
	}
	public List<Transform> GetAllChildren() {
		return children.ToList();
	} 
#endregion
}