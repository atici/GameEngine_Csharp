using System.Diagnostics;
using System.Numerics;

namespace Engine;
public class Transform : Component
{
	// protected Vector2 parentOffset;
	private Vector2 _position;
	public Vector2 position {
		get => _position;
		set {
			foreach(Transform c in children)
				c.position = c.localPosition + value;
			_position = value; 
		}
	}
	public Vector2 localPosition {
		get {
			if(parent == null) return _position;
			return _position - parent.position;
		} 
		set {
			if(parent == null) {
				_position = value;
				return;
			}
			_position = value + parent.position;
		}
	} 
	public float rotation;
	public float localRotation;

	public bool is_static = false;

	private Transform? _parent;
	public Transform? parent { 
		get => _parent; 
		protected set {
			if (value != null) {
				Debug.Assert(value != this, "Parent can not be itself.");
				Debug.Assert(!children.Contains(value), "Child of a Transform cannot be its parent. Use RemoveChild() first.");
			}
			_parent = value;
		} 
	}
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
	public Transform AddChild(Transform child) {
		if (children.Contains(child)) return child;
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