using System.Numerics;
using Engine;

namespace Snake;
public class Section : GameObject{

	public enum State{Empty = 0, Body = 1, Head = 3, Food = 2}
	private State _state = 0;
	public State state {
		get => _state;
		set {
			switch (value) {
				case State.Empty:
					rect.color = Color.Black;
				break;
				case State.Body:
					rect.color = Color.White;
				break;
				case State.Head:
					rect.color = Color.Green;
				break;
				case State.Food:
					rect.color = Color.Red;
				break;
			}
			_state = value;
		}
	}

	private Rect rect;

	public Section(int x, int y, Grid<Section> g) {
		float size = g.cellSize;
		transform.position = new Vector2(x * size + g.originPosition.X, y * size + g.originPosition.Y);
		rect = new Rect(size - 2f, size - 2f);
		AddComponent(rect);
		state = State.Empty;
	}

	protected override void Init() {
	}
}