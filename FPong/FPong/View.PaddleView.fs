#light

namespace Pong.View

open Pong.Model
open Microsoft.Xna.Framework

[<Sealed>]
type PaddleView(game: Game, p: Paddle, imageName: string) as this = class
  inherit ImageDrawer(game, imageName, p.Center, new Vector2(p.Width / 2.0f, p.Height / 2.0f))
  
  let paddle = p
  
  override this.Update(gameTime) =
    this.Position <- p.Center
    
    base.Update(gameTime)
  end
