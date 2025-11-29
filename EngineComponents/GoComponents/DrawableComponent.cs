namespace Engine;
public abstract class DrawableComponent : GoComponent, IDrawable
{
	public Color color = Color.White;

	public DrawableComponent(GameObject gameObject) : base(gameObject) {
		Registrar.RegisterDrawable(this);
	}

	public override void Destroy() {
		Registrar.DeRegisterDrawable(this);
		base.Destroy();
	}

	public abstract bool Draw(nint canvas);
}