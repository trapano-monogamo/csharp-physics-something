using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using renderable;
using lightsource;

namespace camera {
   public class Camera
   {
      // transform
      public Vector3 position;
      public Vector3 up;
      public Vector3 dir;
      public Vector3 right;

      // view control
		// TODO: make fovy a property, so that you can clamp it whenever you set it
      public float fovy;
      public float _yaw;
      public float _pitch;
      public float yaw {
         get {return _yaw;}
         set{_yaw = value;}
      }
      public float pitch {
         get { return _pitch; }
         set { _pitch = Math.Clamp(value, -89.0f, 89.0f); }
      }

      // matrices
      public Matrix4 projection;
      public Matrix4 view;

      public Camera(float _fovy) {
         view = Matrix4.Identity;
         position = Vector3.Zero;
         right = Vector3.UnitX;
         up = Vector3.UnitY;
         dir = -Vector3.UnitZ;
         fovy = _fovy;

         SetPerspective(fovy, 1.0f, .1f, 10.0f);
      }

      public void Move(Vector3 movement) {
         this.position += movement;
         this.UpdateView();
      }

      public void ZoomIn(float zoom)
      {
         this.fovy /= zoom;
         this.UpdateProjection();
      }
      public void ZoomOut(float zoom)
      {
         this.fovy *= zoom;
         this.UpdateProjection();
      }

      public void UpdateDirection() {
         this.dir.X = (float)Math.Cos(MathHelper.DegreesToRadians(_pitch)) * (float)Math.Cos(MathHelper.DegreesToRadians(_yaw));
         this.dir.Y = (float)Math.Sin(MathHelper.DegreesToRadians(_pitch));
         this.dir.Z = (float)Math.Cos(MathHelper.DegreesToRadians(_pitch)) * (float)Math.Sin(MathHelper.DegreesToRadians(_yaw));
         this.dir = Vector3.Normalize(this.dir);
         this.right = Vector3.Normalize(Vector3.Cross(this.dir, Vector3.UnitY));
         this.up = Vector3.Normalize(Vector3.Cross(this.right, this.dir));
         this.UpdateView();
      }

      public void UpdateView() {
         this.view = Matrix4.LookAt(position, position + dir, up);
      }

      public void UpdateProjection()
      {
         SetPerspective(fovy, 1.0f, .1f, 10.0f);
      }

      public void SetPerspective(float fovy, float aspect, float near, float far) {
         this.projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fovy), aspect, near, far);
      }

      public void SetOrthographic(float w, float h, float n, float f) {
         this.projection = Matrix4.CreateOrthographic(w,h,n,f);
      }

      public void Render(Renderable obj, Vector3 light_direction, Vector3 light_color, bool wireframeMode) {
         // wireframe mode
         if (wireframeMode) {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
         } else {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
         }


         // bind VAO, textures, shader
         GL.BindVertexArray(obj.vertexArrayObject);
         // binding textures
         if (obj.textures.Count == 0) {
            //GL.Disable(EnableCap.Texture2D);
            //GL.BindTexture(TextureTarget.Texture2D, 0);
            obj.shaderProgram.SetInt("isTextured", 0);
         } else {
            //GL.Enable(EnableCap.Texture2D);
            obj.shaderProgram.SetInt("isTextured", 1);
            obj.UseAllTextures();
         }
         obj.shaderProgram.SetMatrix4("projection", projection);
         obj.shaderProgram.SetMatrix4("view", view);
         obj.shaderProgram.SetMatrix4("model", obj.recalculateTransform());
         obj.shaderProgram.SetVector3("light_direction", light_direction);
         obj.shaderProgram.SetVector3("light_color", light_color);
         obj.shaderProgram.UseProgram();
         
         // draw call
         GL.DrawElements(PrimitiveType.Triangles, obj._indexData.Length, DrawElementsType.UnsignedInt, 0);
      }

      public void RenderLight(LightSource obj, bool wireframeMode) {
         // wireframe mode
         if (wireframeMode) {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
         } else {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
         }

         // bind VAO, textures, shader
         GL.BindVertexArray(obj.vertexArrayObject);
         // binding textures
         if (obj.textures.Count == 0) {
            //GL.Disable(EnableCap.Texture2D);
            //GL.BindTexture(TextureTarget.Texture2D, 0);
            obj.shaderProgram.SetInt("isTextured", 0);
         } else {
            //GL.Enable(EnableCap.Texture2D);
            obj.shaderProgram.SetInt("isTextured", 1);
            obj.UseAllTextures();
         }
         obj.shaderProgram.SetMatrix4("projection", projection);
         obj.shaderProgram.SetMatrix4("view", view);
         obj.shaderProgram.SetMatrix4("model", obj.recalculateTransform());

         obj.shaderProgram.UseProgram();
         
         // draw call
         GL.DrawElements(PrimitiveType.Triangles, obj._indexData.Length, DrawElementsType.UnsignedInt, 0);
      }
   }
}
