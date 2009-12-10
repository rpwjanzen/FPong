#light
namespace FSat

module VectorMath

open Microsoft.Xna.Framework

let toLength (v: Vector2) newLength =
  v * (newLength / v.Length())

let project v (axis: Vector2) =
  let l = axis.LengthSquared()
  let dp = Vector2.Dot(v, axis)
  let projX = (dp / l) * axis.X
  let projY = (dp / l) * axis.Y
  new Vector2(projX, projY)
