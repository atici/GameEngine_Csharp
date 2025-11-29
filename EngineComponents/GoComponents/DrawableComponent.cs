using System.Runtime.CompilerServices;

namespace Engine;
public abstract class DrawableComponent : GoComponent, IDrawable
{
	public Color color = Color.White;

	internal override void AddToGameobject(GameObject gameObject){
		Registrar.RegisterDrawable(this);
		base.AddToGameobject(gameObject);
	}

	public override void Destroy() {
		Registrar.DeRegisterDrawable(this);
		base.Destroy();
	}

	public abstract bool Draw(nint canvas);
}