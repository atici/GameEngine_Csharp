using SDL3;

namespace Engine;
public interface IDrawable
{
	public SDL.FRect GetFRect();
	public bool Draw(nint canvas);
}