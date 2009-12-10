#light

namespace Pong.Control

open Pong.Model
open Pong.View
open Pong.Sound

open Microsoft.Xna.Framework
open System

type PaddleHit =
  | None
  | Left
  | Right
  
type ScoredPlayer =
  | LeftScored
  | RightScored
  
[<Sealed>]  
type CollisionHandler(game: Game, collisionDetector: CollisionDetector, ball: Ball, scoreDrawer: ScoreDrawer, hitWallPlayer: SoundPlayer, hitPaddlePlayer: SoundPlayer, pointScoredPlayer: SoundPlayer, width: float32, height: float32) as this = class
  inherit GameComponent(game)
  let random = new Random(0)
  let mutable lastPaddleCollidedWith = Left  
  
  let increaseBallVelocity (ball: Ball) =
    if ball.Velocity.Length() < 19.0f
    then
      ball.Velocity <- ball.Velocity * 1.1f
    else
      ()
  
  let resetBall (ball: Ball) (s: Player) =
    let randomPosition () =
      new Vector2(width /2.0f,
        (float32 (random.Next(int (height / 2.0f)))) + (height / 4.0f))
    ball.Position <- randomPosition ()
    let randomDirection =
      if s = LeftPlayer
      then new Vector2(float32 (random.NextDouble() * -1.0), float32 (random.NextDouble() - 0.5))
      else new Vector2(float32 (random.NextDouble()), float32 (random.NextDouble() - 0.5))
      
    randomDirection.Normalize()
    ball.Velocity <- randomDirection * 10.0f
    lastPaddleCollidedWith <- None
  
  let calculateExtra (ballVelocity: Vector2) (v: Vector2) =
    let distanceBallShouldTravel = ballVelocity.Length()
    let distanceBallTravelled = distanceBallShouldTravel - v.Length()
    ballVelocity * (1.0f - (distanceBallTravelled / distanceBallShouldTravel))
  
  do
    let handleTopOrBottomCollision =
      (fun v ->
        hitWallPlayer.Play()
        ball.Translate v
        let extra = calculateExtra ball.Velocity v
        ball.Translate (new Vector2(extra.X, -extra.Y))
        ball.Velocity <- new Vector2(ball.Velocity.X, -ball.Velocity.Y))
    
    let handlePaddleCollision hitPaddle (ball: Ball) (v: Vector2) =
      match hitPaddle with
      | None -> ()
      | _ ->
        hitPaddlePlayer.Play()
        ball.Translate(v)
        let extra = calculateExtra ball.Velocity v
        ball.Translate (new Vector2(-extra.X, extra.Y))
        
        if not (lastPaddleCollidedWith = hitPaddle)
        then
          ball.Velocity <- new Vector2(ball.Velocity.X * -1.0f, ball.Velocity.Y)
          increaseBallVelocity ball
          lastPaddleCollidedWith <- hitPaddle
        else
          ()
    
    let handlePlayerScored scoredPlayer =
      pointScoredPlayer.Play ()
      resetBall ball scoredPlayer
      match scoredPlayer with
        | RightPlayer -> scoreDrawer.RightPlayerScored ()
        | LeftPlayer -> scoreDrawer.LeftPlayerScored ()
      
    Event.listen handleTopOrBottomCollision collisionDetector.BallHitTop
    Event.listen handleTopOrBottomCollision collisionDetector.BallHitBottom
    
    Event.listen (fun v -> handlePaddleCollision Left ball v) collisionDetector.BallHitLeftPaddle
    Event.listen (fun v -> handlePaddleCollision Right ball v) collisionDetector.BallHitRightPaddle
    
    Event.listen (fun player -> handlePlayerScored player) collisionDetector.PlayerScored
  end