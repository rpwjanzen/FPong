#light

namespace FSat

module Overlap

open VectorMath
open Microsoft.Xna.Framework
open Util

// utility
let fDot f a b axis =
  if f (Vector2.Dot(a, axis)) (Vector2.Dot(b, axis))
  then a
  else b  

// stable
let minDot a b axis = fDot (<=) a b axis
let maxDot a b axis = fDot (>=) a b axis

// stable
let minDots xs axis = Seq.reduce (fun acc p -> minDot acc p axis) xs
let maxDots xs axis = Seq.reduce (fun acc p -> maxDot acc p axis) xs

let greaterThan a b axis =
  (not (a = b)) && ((maxDot a b axis) = a)

let lessThan a b axis =
   (not (a = b)) && ((minDot a b axis) = a)

let minLength (a: Vector2) (b: Vector2) =
  if a.Length() <= b.Length()
  then a
  else b

let minOverlap xs ys axis =
  // map points onto axis
  let xsProj = Seq.map (fun v -> project v axis) xs
  let ysProj = Seq.map (fun v -> project v axis) ys
  
  // get dot products onto axis
  let minX = minDots xsProj axis
  let maxX = maxDots xsProj axis
  
  let minY = minDots ysProj axis
  let maxY = maxDots ysProj axis
  
  // A:         |---...
  // B: ...--|  
  if greaterThan minX maxY axis
  // A: ...----|
  // B:             |----...
  // then by SAT, they do not intersect
     || lessThan maxX minY axis
  then
    None
  else
    let moveRight = maxY - minX
    let moveLeft = maxX - minY
    Some(minLength moveRight moveLeft)
  