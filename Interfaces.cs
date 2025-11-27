using SDL3;

public interface IDrawable
{
	public SDL.FRect GetFRect();
	public void Draw(nint renderer);
}