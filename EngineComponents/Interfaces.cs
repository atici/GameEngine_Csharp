using SDL3;

namespace Engine;
public interface IDrawable
{
	public bool Draw(nint canvas);
}