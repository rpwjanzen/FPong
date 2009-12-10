#light

namespace Pong.Model

open Microsoft.Xna.Framework
open FSat.Shapes

[<Sealed>]
type PaddleBounds(game: Game, paddle: Paddle) as this = class
  inherit GameComponent(game)
  
  // this being here is silly
  let calculateBounds (p:Paddle) =
    new AxisAlignedBox(
      p.Center,
      new Vector2(p.Width / 2.0f, 0.0f),
      new Vector2(0.0f, p.Height / 2.0f))

  let mutable bounds = calculateBounds paddle
  let paddle = paddle

  member this.Bounds
    with get () = bounds

  override this.Update(gameTime) =
    bounds <- calculateBounds paddle
    base.Update(gameTime)
      
  end