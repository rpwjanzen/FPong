#light

namespace Pong.View

open Microsoft.Xna.Framework

[<Sealed>]
type Foreground(game: Game) = class
  inherit ImageDrawer(game, "PlayfieldFrame", Vector2.Zero, Vector2.Zero)
  end