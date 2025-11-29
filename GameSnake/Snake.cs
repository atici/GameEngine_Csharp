// Note: might not work on later iterations of the engine.
using Engine;

namespace Snake;

public class Snake : Game {
	public override void Init() {
		_Engine.physics.enabled = false;

	}

	public override void StartEngine() {
		_Engine.options.window_name = "Snake v1.0";
		base.StartEngine();
	}
}