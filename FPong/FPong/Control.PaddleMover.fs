#light

namespace Pong.Control

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

open Pong.Model

[<Sealed>]
type PaddleMover(game: Game, paddle: Paddle, up: Keys, down: Keys, delta: float32, minY: float32, maxY: float32) as this = class
  inherit GameComponent(game)
  
  let paddle = paddle
  let up = up
  let down = down
  let delta = delta
  let maxY = maxY
  let minY = minY
    
  override this.Update(gameTime) =
    let ks = Keyboard.GetState()
    if ks.IsKeyDown(up)
    then
      let y = max minY paddle.Center.Y - delta
      paddle.MoveTo(paddle.Center.X, y)
    else
      ()
      
    if ks.IsKeyDown(down)
    then
      let y = min maxY paddle.Center.Y + delta
      paddle.MoveTo(paddle.Center.X, y)
    else
      ()
      
    base.Update(gameTime)
    
  end