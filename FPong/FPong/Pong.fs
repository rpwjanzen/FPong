#light

namespace Game
module Pong

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Audio
open Microsoft.Xna.Framework.Content
open Microsoft.Xna.Framework.Input
open Microsoft.Xna.Framework.Graphics

open Pong.Model
open Pong.View
open Pong.Control
open Pong.Sound

[<Sealed>]
type Pong() as this = class
  inherit Game()
  
  let mutable graphics : GraphicsDeviceManager = null
  
  do
    let width = 1280
    let height = 720
            
    base.Content.RootDirectory <- "Content"    
    graphics <- new GraphicsDeviceManager(this)
    graphics.PreferredBackBufferWidth <- width
    graphics.PreferredBackBufferHeight <- height
    
    let background = new Background(this)
    let foreground = new Foreground(this)
    let leftGoalLine = new GoalLine(this, 150.0f);
    let rightGoalLine = new GoalLine(this, float32 width - 150.0f);
    
    let paddleShadow = 5;
    let paddleHeight = 86;
    let paddleMaxY = height - (paddleHeight / 2) + paddleShadow;
    let paddleMinY = paddleHeight / 2;
      
    let leftPaddle = new Paddle(this, new Vector2(100.0f, (float32 height) / 2.0f), 29.0f, float32 paddleHeight)
    let leftPaddleMover = new GamePadMover(this, leftPaddle, PlayerIndex.One, float32 5, float32 paddleMinY, float32 paddleMaxY);
    let leftPaddleView = new PaddleView(this, leftPaddle, "GreenPaddle")
    let leftPaddleBounds = new PaddleBounds(this, leftPaddle);

    let rightPaddle = new Paddle(this, new Vector2(float32 width - 100.0f, float32 height / 2.0f), 29.0f, float32 paddleHeight);
    let rightPaddleMover = new PaddleMover(this, rightPaddle, Keys.K, Keys.M, 5.0f, float32 paddleMinY, float32 paddleMaxY);
    let rightPaddleView = new PaddleView(this, rightPaddle, "RedPaddle");
    let rightPaddleBounds = new PaddleBounds(this, rightPaddle);    

    let ball = new Ball(this, new Vector2(float32 width / 2.0f, float32 height / 2.0f), new Vector2(5.0f, 5.0f), 17.0f)
    let ballView = new BallView(this, ball);
    let ballBounds = new BallBounds(this, ball);
    
    let scoreDrawer = new ScoreDrawer(this)
    
    let hitWallPlayer = new SoundPlayer(this, "HitWall")
    let hitPaddlePlayer = new SoundPlayer(this, "HitPaddle")
    let pointScoredPlayer = new SoundPlayer(this, "PlayerScored")
    
    let collisionDetector = new CollisionDetector(this, ballBounds, leftPaddleBounds, rightPaddleBounds, new Vector2(100.0f, 0.0f), new Vector2(float32 width - 100.0f, float32 height))
    let collisionHandler = new CollisionHandler(this, collisionDetector, ball, scoreDrawer, hitWallPlayer, hitPaddlePlayer, pointScoredPlayer, float32 width, float32 height)
    
    this.Components.Add(background)
    this.Components.Add(leftGoalLine)
    this.Components.Add(rightGoalLine)
    
    this.Components.Add(leftPaddle)
    this.Components.Add(leftPaddleMover)
    this.Components.Add(leftPaddleView)
    this.Components.Add(leftPaddleBounds)
  
    this.Components.Add(rightPaddle)
    this.Components.Add(rightPaddleMover)
    this.Components.Add(rightPaddleView)
    this.Components.Add(rightPaddleBounds)
  
    this.Components.Add(ball)
    this.Components.Add(ballView)
    this.Components.Add(ballBounds)
    
    this.Components.Add(collisionDetector)
    this.Components.Add(collisionHandler)
    
    this.Components.Add(foreground)
    this.Components.Add(scoreDrawer)
    
    this.Components.Add(hitWallPlayer)
    this.Components.Add(hitPaddlePlayer)
    this.Components.Add(pointScoredPlayer)
    
  override this.Draw(gameTime) =
    let gd = graphics.GraphicsDevice
    gd.Clear(Color.CornflowerBlue)
    
    base.Draw(gameTime)
    
  end
