using System.Numerics;

namespace Engine;
public class Transform : Component
{
	public Vector2 position;
	public float rotation;
	public bool is_static = false;

	public Transform? parent { get; protected set; } = null;
	private List<Transform> children { get; } = new();

	public Transform(GameObject gameObject) : base(gameObject, addToComponents:false) {
		position = Vector2.Zero;
		rotation = 0f;
	}

	protected override void OnDestroy() {
		foreach( Transform c in children)
			c.Destroy();
		gameObject.Destroy();
	}

#region Children
	/// <summary>
	/// Add transform as a child to this transform. Automatically removes from old parent if it exists.
	/// </summary>
	/// <param name="child"></param>
	/// <returns>Transform thats been added as child.</returns>
	public Transform? AddChildren(Transform child) {
		if (children.Contains(child)) return null;
		if (child.parent != null) {
			child.parent.RemoveChild(child);
		}
		child.parent = this;
		children.Add(child);
		return child;
	}
	/// <summary>
	/// Removes child if it is the child of this transform. 
	/// </summary>
	/// <param name="child"></param>
	/// <returns>Returns true if child successfully removed. Otherwise returns false.</returns>
	public bool RemoveChild(Transform child) {
		if (!children.Remove(child)) return false;
		child.parent = null;
		return true;
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