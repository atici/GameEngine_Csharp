using System.Runtime.CompilerServices;

namespace Engine;
public abstract class DrawableComponent : GoComponent, IDrawable
{
	public Color color = Color.White;

	public override void OnAdd(){
		Registrar.RegisterDrawable(this);
	}

	public override void OnRemove() {
		Registrar.DeRegisterDrawable(this);
	}

	public abstract bool Draw(nint canvas);
}