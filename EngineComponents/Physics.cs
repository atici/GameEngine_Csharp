using System.Numerics;

namespace Engine;
public class Physics
{
	public static class Units
	{
		public const int METRE = 100;
		public const float MILISECOND = 0.001f;
		public const float GRAVITY = 9.81f * METRE / 1; // metre/second == 100/1
	}
	public static readonly Vector2 Vector2_Down 	= new Vector2(0, 1);
	public static readonly Vector2 Vector2_Up 		= new Vector2(0, -1);
	public static readonly Vector2 Vector2_Right 	= new Vector2(1, 0);
	public static readonly Vector2 Vector2_Left		= new Vector2(-1, 0);
	public bool enabled = true;

// Subject to big changes over here.
	HashSet<GameObject> GameObjects = new();

	void Update(float delta) {
		if(!enabled) return;

		foreach ( GameObject go in GameObjects) {
			if (go.transform.is_static || !go.enabled) continue;
			go.transform.position += Vector2_Up * Units.GRAVITY * delta ; 
		}
	}

	public Physics() {
		GameClock.Update += Update;
		Registrar.RegisterPhysics += _Register;
		Registrar.DeRegisterPhysics += _DeRegister;
	}
	~Physics() {
		GameClock.Update -= Update;
		Registrar.RegisterPhysics -= _Register;
		Registrar.DeRegisterPhysics -= _DeRegister;
	}
	void _Register(GameObject go) {
		GameObjects.Add(go);
	}
	void _DeRegister(GameObject go) {
		GameObjects.Remove(go);
	}
}