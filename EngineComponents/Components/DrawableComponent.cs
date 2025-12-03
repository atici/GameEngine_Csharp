namespace Engine;
public abstract class DrawableComponent : Component, IDrawable
{
	public Color color = Color.White;

	protected override void Init(){
		Registrar.RegisterDrawableInvoke(this);
	}

	protected override void OnDestroy() {
		Registrar.DeRegisterDrawableInvoke(this);
	}

	public abstract bool Draw(nint canvas);

	public DrawableComponent(GameObject go) : base(go){}
}