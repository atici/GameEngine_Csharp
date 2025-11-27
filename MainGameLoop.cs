using SDL3;

public class MainGameLoop
{
	public static event EventHandler<float> ?UpdateEvent;

	float delta;
	ulong lastTime = SDL.GetTicks();
	public void UpdateLoop()
	{
		ulong currentTime = SDL.GetTicks();
		delta = (currentTime - lastTime) * Constants.TO_SECONDS;

		if (delta >= Constants.UPDATE_CLOCK)
		{
			UpdateEvent?.Invoke(this, delta);
			lastTime = currentTime;
		}
	}
}