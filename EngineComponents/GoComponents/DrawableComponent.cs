namespace Engine;
public abstract class DrawableComponent : Component, IDrawable
{
	public Color color = Color.White;

	public override void Init(){
		Registrar.RegisterDrawableInvoke(this);
	}

	public override void OnDestroy() {
		Registrar.DeRegisterDrawableInvoke(this);
	}

	public abstract bool Draw(nint canvas);

	public DrawableComponent(GameObject go) : base(go){}
}