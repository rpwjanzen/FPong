#light

namespace Pong.View

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

[<Sealed>]
type ScoreDrawer(game: Game) as this = class
  inherit DrawableGameComponent(game)
  
  let mutable sb : SpriteBatch = null
  let mutable sf : SpriteFont = null
  
  let mutable leftScore = 0
  let mutable rightScore = 0
    
  override this.LoadContent() =
    sb <- new SpriteBatch(this.GraphicsDevice)
    sf <- this.Game.Content.Load<SpriteFont>("ScoreFont")
  
    base.LoadContent()
  
  member this.LeftPlayerScored () =
    leftScore <- leftScore + 1
    
  member this.RightPlayerScored () =
    rightScore <- rightScore + 1
  
  override this.Draw(gameTime) =
    sb.Begin()
    sb.DrawString(sf, leftScore.ToString(), new Vector2(300.0f,0.0f), Color.White)
    sb.DrawString(sf, rightScore.ToString(), new Vector2(1280.0f - 400.0f,0.0f), Color.White)
    sb.End()
    
    base.Draw(gameTime)
    
  end