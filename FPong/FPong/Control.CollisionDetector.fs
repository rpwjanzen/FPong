#light

namespace Pong.Control

open Microsoft.Xna.Framework
open Pong.Model

open FSat.Collision
open FSat.Shapes

type Player =
  | LeftPlayer
  | RightPlayer

[<Sealed>]
type CollisionDetector(game: Game, ballBounds: BallBounds, leftPaddleBounds: PaddleBounds, rightPaddleBounds: PaddleBounds, min: Vector2, max:Vector2) as this = class
  inherit GameComponent(game)
  
  let leftPaddleBounds = leftPaddleBounds
  let rightPaddleBounds = rightPaddleBounds
  let ballBounds = ballBounds
  
  let (min: Vector2) = min
  let (max: Vector2) = max
  
  let triggerBallHitTop, ballHitTop = Event.create()
  let triggerBallHitBottom, ballHitBottom = Event.create()
  
  let triggerBallHitLeftPaddleSide, ballHitLeftPaddleSide = Event.create()
  let triggerBallHitRightPaddleSide, ballHitRightPaddleSide = Event.create()
  
  let triggerPlayerScored, playerScored = Event.create()
  
  
  let checkTopOfScreen (c: ICircle) =
    let cnt = c.Center
    let y = cnt.Y - c.Radius
    if y < min.Y
    then
      let v = new Vector2(0.0f, min.Y - y)
      triggerBallHitTop v
    else
      ()
  
  let checkBottomOfScreen (c: ICircle) =
    let cnt = c.Center
    let y = cnt.Y + c.Radius
    if y > max.Y
    then
      let v = new Vector2(0.0f, max.Y - y)
      triggerBallHitBottom v
    else
      ()
  
  let checkLeftPaddleCollision (c: ICircle) =
    let lb = leftPaddleBounds.Bounds
    let leftCollision = calculateCircleBoxCollision c lb
    match leftCollision with
    | Some (v) -> triggerBallHitLeftPaddleSide v
    | _ -> ()
  
  let checkRightPaddleCollision (c: ICircle) =
    let rb = rightPaddleBounds.Bounds
    let rightCollision = calculateCircleBoxCollision c rb
    match rightCollision with
    | Some (v) -> triggerBallHitRightPaddleSide v
    | _ -> ()
  
  let checkRightPlayerScored (c: ICircle) minX =
    let cnt = c.Center
    let ballLeftmostPoint = cnt.X - c.Radius;
    if ballLeftmostPoint < minX
    then
      triggerPlayerScored RightPlayer
    else
      ()

  let checkLeftPlayerScored (c: ICircle) maxX =
    let cnt = c.Center
    let ballRightmostPoint = cnt.X + c.Radius;
    if ballRightmostPoint > maxX
    then
      triggerPlayerScored LeftPlayer
    else
      ()
  
  member this.BallHitTop = ballHitTop
  member this.BallHitBottom = ballHitBottom
  
  member this.BallHitLeftPaddle = ballHitLeftPaddleSide
  member this.BallHitRightPaddle = ballHitRightPaddleSide
  
  member this.PlayerScored = playerScored
        
  override this.Update(gameTime) =
    let c = ballBounds.Bounds
    checkTopOfScreen c
    checkBottomOfScreen c
    
    checkLeftPaddleCollision c
    checkRightPaddleCollision c
    
    checkLeftPlayerScored c max.X
    checkRightPlayerScored c min.X
    
    base.Update(gameTime)
  
  end