#light

namespace Pong.Model

open Microsoft.Xna.Framework

[<Sealed>]
type Paddle(game: Game, center: Vector2, width: float32, height: float32) as this = class
  inherit GameComponent(game)
  
  let mutable center = center
  let width = width
  let height = height

  member this.Center
    with get () = center

  member this.Width
    with get () = width
  
  member this.Height
    with get () = height
  
  member this.Corners
    with get () =
      let halfWidth = width / 2.0f
      let halfHeight = height / 2.0f
      [
        center + new Vector2(halfWidth, halfHeight)
        center + new Vector2(halfWidth, -halfHeight)
        center + new Vector2(-halfWidth, -halfHeight)
        center + new Vector2(-halfWidth, halfHeight)
      ]
  
  member this.MoveTo(x,y) =
    center <- new Vector2(x, y)
  
  member this.Translate(v) =
    center <- center + v

  end