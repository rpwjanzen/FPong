#light

namespace Pong.Control

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

open Pong.Model

[<Sealed>]
type GamePadMover(game: Game, paddle: Paddle, playerIndex: PlayerIndex, delta: float32, minY: float32, maxY: float32) as this = class
  inherit GameComponent(game)
  
  let paddle = paddle
  let playerIndex = playerIndex
  let delta = delta
  let maxY = maxY
  let minY = minY
    
  override this.Update(gameTime) =
    let gs = GamePad.GetState(playerIndex)
    if gs.IsConnected
    then
      if gs.DPad.Up = ButtonState.Pressed
      then
        let y = max minY paddle.Center.Y - delta
        paddle.MoveTo(paddle.Center.X, y)
      else
        ()
      
      if gs.DPad.Down = ButtonState.Pressed
      then
        let y = min maxY paddle.Center.Y + delta
        paddle.MoveTo(paddle.Center.X, y)
      else
        ()
    else
      ()
      
    base.Update(gameTime)
    
  end
