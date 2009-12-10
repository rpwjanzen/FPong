#light

namespace FSat

module Collision

open Microsoft.Xna.Framework
open Overlap
open VectorMath
open Shapes
open System

let pointsOnAxis (circle:ICircle) axis =
  let p0 = circle.Center + toLength axis (circle.Radius)
  let p1 = circle.Center - toLength axis (circle.Radius)
  (p0, p1)

let fOption f oa ob =
  match oa, ob with
  | None, _ -> None
  | _, None -> None
  | Some(a), Some(b) -> Some (f a b)

let calculateCircleBoxCollision (circle:ICircle) box =
  let createAxis (v0: Vector2) (v1: Vector2) = v0 - v1
  let boxCorners = corners box
  let axes = [box.HalfWidthX; box.HalfWidthY;]
             @ List.map (createAxis (circle.Center)) boxCorners
  let circlePoints = Seq.map (pointsOnAxis circle) axes
  let collisionVectors = Seq.map2 (fun (p0, p1) axis -> minOverlap [p0; p1] boxCorners axis) circlePoints axes
  Seq.fold (fun a b -> fOption minLength a b) (Seq.hd collisionVectors) collisionVectors

let calculateCircleCircleCollision (a:ICircle) (b:ICircle) =
  let vDiff = (a.Center - b.Center)
  let distance = vDiff.Length()
  let radiusSum = a.Radius + b.Radius
  distance - radiusSum < 0.0f  