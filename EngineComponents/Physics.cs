using System.Numerics;

namespace Engine;
public static class Registrar
{
	public static event EventHandler<GameObject> ?RegisterGOListener;
	public static void RegisterGO(GameObject go) => RegisterGOListener?.Invoke(null, go);

	public static event EventHandler<IDrawable> ?RegisterDrawableListener;
	public static void RegisterDrawable(IDrawable d) => RegisterDrawableListener?.Invoke(null, d);
	public static event EventHandler<IDrawable> ?DeRegisterDrawableListener;
	public static void DeRegisterDrawable(IDrawable d) => DeRegisterDrawableListener?.Invoke(null, d);
}

public class Physics
{
	public static class Units
	{
		public const int METRE = 100;
		public const float MILISECOND = 0.001f;
		public const float GRAVITY = 9.81f * METRE / 1; // metre/second == 100/1
	}
	public static readonly Vector2 VECTOR_DOWN = new Vector2(0, 1);

	HashSet<GameObject> GOs = new();

	void _Register(GameObject go) {
		if (GOs.Contains(go)) return;
		GOs.Add(go);
	}

	void Update(float delta) {
		foreach ( GameObject go in GOs) {
			if (go.transform.is_static) continue;
			go.transform.Position += VECTOR_DOWN * Units.GRAVITY * delta ; 
		}
	}

	public Physics() {
		GameClock.Update += (s, d) => Update(d);
		Registrar.RegisterGOListener += (s, g) => _Register(g);
	}
	~Physics() {
		GameClock.Update -= (s, d) => Update(d);
		Registrar.RegisterGOListener -= (s, t) => _Register(t);
	}
}