#light

namespace Pong.Sound

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Audio

[<Sealed>]
type SoundPlayer(game: Game, soundName: string) as this = class
  inherit GameComponent(game)

  let mutable se : SoundEffect = null

  override this.Initialize() =
    se <- this.Game.Content.Load<SoundEffect>(soundName)
    
    base.Initialize()
    
  member this.Play() =
    se.Play() |> (fun _ -> ())
    
  end