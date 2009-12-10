#light

namespace Pong.View

open Microsoft.Xna.Framework

[<Sealed>]
type GoalLine(game: Game, x: float32) = class
  inherit ImageDrawer(game, "CheckerGoalLine", new Vector2(x, 0.0f), new Vector2(58.0f / 2.0f, 0.0f))
end