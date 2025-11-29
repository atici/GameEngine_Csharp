using System.Runtime.CompilerServices;

namespace Engine;
public abstract class DrawableComponent : GoComponent, IDrawable
{
	public Color color = Color.White;

	public override void OnAdd(){
		Registrar.RegisterDrawableInvoke(this);
	}

	public override void OnRemove() {
		Registrar.DeRegisterDrawableInvoke(this);
	}

	public abstract bool Draw(nint canvas);
}