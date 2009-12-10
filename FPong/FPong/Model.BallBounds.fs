#light

namespace Pong.Model

open FSat.Shapes
open Microsoft.Xna.Framework

[<Sealed>]
type BallBounds(game: Game, ball: Ball) as this = class
  inherit GameComponent(game)
  
  let calculateBounds (b:Ball) =
    new Circle (b.Position, b.Radius)    
  
  let mutable bounds = calculateBounds ball
  let ball = ball
  
  member this.Bounds
    with get () = bounds
  
  override this.Update(gameTime) =
    bounds <- calculateBounds ball
    base.Update(gameTime)
  
  end