#light

namespace Pong.Model

open Microsoft.Xna.Framework

[<Sealed>]
type Ball(game: Game, center: Vector2, velocity: Vector2, radius: float32) as this = class
  inherit GameComponent(game)
  
  let mutable center = center
  let radius = radius
  let mutable velocity = velocity

  member this.Position
    with get () = center
    and set v = center <- v
    
  member this.Radius
    with get () = radius

  member this.Velocity
    with get () = velocity
    and set v = velocity <- v

  member this.Translate v =
    center <- center + v

  member this.MoveTo x y =
    center <- new Vector2(x, y)

  override this.Update(gameTime) =
    this.Translate(velocity)
    base.Update(gameTime)
    
  end