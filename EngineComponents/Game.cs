namespace Engine;
public abstract class Game
{
	public Engine _Engine {get;} = new Engine();
	public abstract void Init();
	public virtual void Start() {
		_Engine.Start(this);
	}
}