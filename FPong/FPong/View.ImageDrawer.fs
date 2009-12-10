#light

namespace Pong.View

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Pong.Model

type ImageDrawer(game: Game, imageName: string, position: Vector2, origin: Vector2) as this = class
  inherit DrawableGameComponent(game)
  
  let mutable sb : SpriteBatch = null
  let mutable texture: Texture2D = null
  
  let origin = origin
  let mutable position = position
  let imageName = imageName
  
  member this.Position
    with get () = position
    and set p = position <- p
  
  override this.LoadContent() =
    sb <- new SpriteBatch(this.GraphicsDevice)
    texture <- this.Game.Content.Load<Texture2D>(imageName)
    
  override this.Draw(gameTime) =
    // this rectangle shouldn't have to exist
    let r = new System.Nullable<Rectangle>()
    sb.Begin()
    sb.Draw(texture, position, r, Color.White, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f)
    sb.End()
      
    base.Draw(gameTime)
  
  end