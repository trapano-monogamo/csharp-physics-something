using OpenTK.Mathematics;

namespace transform {
   public class Transform {
      public Vector4 position {set;get;}
      public Vector4 rotation {set;get;}
      public Vector4 scale    {set;get;}

      public Matrix4 transformMatrix;

      public Transform(Vector4 s_position, Vector4 s_scale, Vector4 s_rotation) {
         position = s_position;
         scale = s_scale;
         rotation = s_rotation;
         transformMatrix = Matrix4.Identity;
      }

      public void Rotate(Vector3 _rotation) {
         this.rotation += new Vector4(_rotation.X, _rotation.Y, _rotation.Z, 0.0f);
      }
      public void Translate(Vector3 _movement) {
         this.position += new Vector4(_movement.X, _movement.Y, _movement.Z, 0.0f);
      }
      public void Scale(Vector3 _scaler) {
         this.scale *= new Vector4(_scaler.X, _scaler.Y, _scaler.Z, 0.0f);
      }

      public Matrix4 recalculateTransform() {
         // combining xyz rotations
         Matrix4 tempRotationX = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation[0]));
         Matrix4 tempRotationY = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation[1]));
         Matrix4 tempRotationZ = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation[2]));
         Matrix4 tempRotation = tempRotationX * tempRotationY * tempRotationZ;
         // scaling
         Matrix4 tempScale = Matrix4.CreateScale(scale[0], scale[1], scale[2]);
         // translation
         Matrix4 tempTranslation = Matrix4.CreateTranslation(position[0], position[1], position[2]);

         // putting it all together
         transformMatrix = tempRotation * tempScale * tempTranslation;
         return transformMatrix;
      }
   }
}