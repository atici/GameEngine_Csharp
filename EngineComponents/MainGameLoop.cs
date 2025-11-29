using System.Runtime;
using System.Security.Principal;
using SDL3;

namespace Engine;
public class GameClock
{
	public static event EventHandler<float> ?Update;
	public static event EventHandler ?Start;

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

	public void InvokeStart() => Start?.Invoke(this, EventArgs.Empty);
	public void InvokeUpdate()
	{
		ulong currentTime = SDL.GetTicks();
		delta = (currentTime - lastTime) * 0.001f; // Turn to seconds.

		if (delta >= _update_clock)
		{
			Update?.Invoke(this, delta);
			lastTime = currentTime;
		}
	}
}