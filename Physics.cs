using System.Data;
using System.Numerics;

public static class PhysicsEvents
{
	public static event EventHandler<GameObject> ?RegisterTransform;
	
	public static void RegisterGO(GameObject go) => RegisterTransform?.Invoke(null, go);
}

public class Physics
{
	public static readonly Vector2 VECTOR_DOWN = new Vector2(0, 1);
	public const float GRAVITY = 998f;

	HashSet<GameObject> GOs = new();

	void _Register(GameObject go) {
		if (GOs.Contains(go)) return;
		GOs.Add(go);
	}

	void Update(float delta) {
		Console.WriteLine($"Adding {GRAVITY * (1/delta)} per second.");
		foreach ( GameObject go in GOs) {
			go.transform.Position += VECTOR_DOWN * GRAVITY * delta ; 
		}
	}

	public Physics() {
		MainGameLoop.Update += (s, d) => Update(d);
		PhysicsEvents.RegisterTransform += (s, g) => _Register(g);
	}
	~Physics() {
		MainGameLoop.Update -= (s, d) => Update(d);
		PhysicsEvents.RegisterTransform -= (s, t) => _Register(t);
	}
}