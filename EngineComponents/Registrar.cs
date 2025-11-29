namespace Engine;
public static class Registrar
{
	public static event Action<GameObject> ?RegisterPhysics;
	public static void RegisterPhysicsInvoke(GameObject go) => RegisterPhysics?.Invoke(go);
	public static event Action<GameObject> ?DeRegisterPhysics;
	public static void DeRegisterPhysicsInvoke(GameObject go) => RegisterPhysics?.Invoke(go);

	public static event Action<IDrawable> ?RegisterDrawable;
	public static void RegisterDrawableInvoke(IDrawable d) => RegisterDrawable?.Invoke(d);
	public static event Action<IDrawable> ?DeRegisterDrawable;
	public static void DeRegisterDrawableInvoke(IDrawable d) => DeRegisterDrawable?.Invoke(d);
}