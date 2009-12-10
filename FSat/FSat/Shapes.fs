#light

namespace FSat

module Shapes

open VectorMath
open Microsoft.Xna.Framework

type IAxisAlignedBox =
  abstract member Center : Vector2 with get
  abstract member HalfWidthX : Vector2 with get
  abstract member HalfWidthY : Vector2 with get

type AxisAlignedBox (center: Vector2, halfWidthX: Vector2, halfWidthY: Vector2) =
  interface IAxisAlignedBox with
    member this.Center
      with get () = center
    member this.HalfWidthX
      with get () = halfWidthX
    member this.HalfWidthY
      with get () = halfWidthY

let corners (box: IAxisAlignedBox) =
  let center = box.Center
  let halfWidthX = box.HalfWidthX
  let halfWidthY = box.HalfWidthY
  
  let ur = center + halfWidthX + halfWidthY
  let lr = center + halfWidthX - halfWidthY
  let ll = center - halfWidthX + halfWidthY
  let ul = center - halfWidthX - halfWidthY
  [ur; lr; ll; ul]

type ICircle =
  abstract member Center : Vector2 with get
  abstract member Radius : float32 with get
  
type Circle (center: Vector2, radius: float32) =
  interface ICircle with
    member this.Center
      with get () = center
    member this.Radius
      with get () = radius