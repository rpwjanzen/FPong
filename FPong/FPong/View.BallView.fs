#light

namespace Pong.View

open Pong.Model
open Microsoft.Xna.Framework

[<Sealed>]
type BallView(game: Game, ball: Ball) as this = class
  inherit ImageDrawer(game, "Ball", ball.Position, new Vector2(ball.Radius / 2.0f, ball.Radius / 2.0f))
    
  let Ball ball = ball
    
  override this.Update(gameTime) =
    this.Position <- ball.Position
      
    base.Update(gameTime)
end
