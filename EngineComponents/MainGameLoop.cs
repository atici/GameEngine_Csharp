using SDL3;

namespace Engine;
public class GameClock
{
	public static event Action<float> ?Update;
	public static event Action ?Start;

	private float _update_clock = 1/Constants.TARGET_FPS;
	private int _target_fps = Constants.TARGET_FPS; 
	public int target_fps { 
		get => _target_fps; 
		set {
			_target_fps = value;
			_update_clock = 1/value;
		}
	}

	float delta;
	ulong lastTime = SDL.GetTicks();

	public void InvokeStart() => Start?.Invoke();
	public void InvokeUpdate()
	{
		ulong currentTime = SDL.GetTicks();
		delta = (currentTime - lastTime) * 0.001f; // Turn to seconds.

		if (delta >= _update_clock)
		{
			Update?.Invoke(delta);
			lastTime = currentTime;
		}
	}
}