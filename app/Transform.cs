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

      public void Rotate(float _rotationX, float _rotationY, float _rotationZ) {
         this.rotation += new Vector4(_rotationX, _rotationY, _rotationZ, 0.0f);
      }
      public void Translate(float _movementX, float _movementY, float _movementZ) {
         this.position += new Vector4(_movementX, _movementY, _movementZ, 0.0f);
      }
      public void Scale(float _scalerX, float _scalerY, float _scalerZ) {
         this.scale += new Vector4(_scalerX, _scalerY, _scalerZ, 0.0f);
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