using SDL3;

public class MainGameLoop
{
	public static event EventHandler<float> ?Update;
	public static event EventHandler ?Start;

	float delta;
	ulong lastTime = SDL.GetTicks();

	public void InvokeStart() => Start?.Invoke(this, EventArgs.Empty);
	public void InvokeUpdate()
	{
		ulong currentTime = SDL.GetTicks();
		delta = (currentTime - lastTime) * Constants.Units.MILISECOND;

		if (delta >= Constants.UPDATE_CLOCK)
		{
			Update?.Invoke(this, delta);
			lastTime = currentTime;
		}
	}
}