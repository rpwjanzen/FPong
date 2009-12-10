#light

namespace Pong.View

open Microsoft.Xna.Framework

[<Sealed>]
type Background(game: Game) = class
  inherit ImageDrawer(game, "Background", Vector2.Zero, Vector2.Zero)
  end
