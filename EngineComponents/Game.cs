namespace Engine;
public abstract class Game
{
	public Engine _Engine {get;} = new Engine();
	public virtual void StartEngine() => _Engine.Start(this);
	public abstract void Init();
}