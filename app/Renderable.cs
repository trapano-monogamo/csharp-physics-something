using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using shader;
using texture;
using transform;
using lightsource;

namespace renderable {
   public class Renderable : Transform
   {
      public string name;

      // mesh
      private int vertexBufferObject;
      private int elementBufferObject;
      public int vertexArrayObject;

      public float[] _vertexData;
      public float[] _normalData;
      public float[] _textureData;
      public uint[] _indexData;

      // material
      //Material material;

      // other
      public Shader shaderProgram;
      public List<Texture> textures;

      public Renderable()
         : base(new Vector4(.0f), new Vector4(.0f), new Vector4(.0f))
      {}

      public Renderable(float[] vertexRawData, uint[] indexData, Shader _shaderProgram, Texture[] _textures, string name = "unknown")
         : base(new Vector4(.0f), new Vector4(1.0f), new Vector4(.0f))
      {
         // set up object properties
         this.name = name;
         shaderProgram = _shaderProgram;
         textures = new List<Texture>(_textures);
         _vertexData = vertexRawData;
         _indexData = indexData;

         // building VAO
         vertexArrayObject = GL.GenVertexArray();
         GL.BindVertexArray(vertexArrayObject);

         // building VBO
         vertexBufferObject = GL.GenBuffer();
         GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
         GL.BufferData(BufferTarget.ArrayBuffer, vertexRawData.Length * sizeof(float), vertexRawData, BufferUsageHint.StaticDraw);

         // building EBO
         elementBufferObject = GL.GenBuffer();
         GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
         GL.BufferData(BufferTarget.ElementArrayBuffer, indexData.Length * sizeof(uint), indexData, BufferUsageHint.StaticDraw);

         // setting up buffer attributes (vertices and texture coordinates)
         shaderProgram.UseProgram();

         // aPosition
         int posLocation = shaderProgram.GetAttribLocation("aPosition");
         GL.VertexAttribPointer(posLocation, 3, VertexAttribPointerType.Float, false, 12 * sizeof(float), 0 * sizeof(float));
         GL.EnableVertexAttribArray(posLocation);
         // aNormal
         int norLocation = shaderProgram.GetAttribLocation("aNormal");
         GL.VertexAttribPointer(norLocation, 3, VertexAttribPointerType.Float, false, 12 * sizeof(float), 3 * sizeof(float));
         GL.EnableVertexAttribArray(norLocation);
         // aColor
         int colLocation = shaderProgram.GetAttribLocation("aColor");
         GL.VertexAttribPointer(colLocation, 4, VertexAttribPointerType.Float, false, 12 * sizeof(float), 6 * sizeof(float));
         GL.EnableVertexAttribArray(colLocation);
         // aTexCoords
         int texCoordLocation = shaderProgram.GetAttribLocation("aTexCoord");
         GL.EnableVertexAttribArray(texCoordLocation);
         GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 12 * sizeof(float), 10 * sizeof(float));

         UseAllTextures();

         base.recalculateTransform();

         // unbind used objects and buffers
         GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
         GL.BindVertexArray(0);
      }

      public void UseAllTextures() {
         // using each texture and setting up each texture unit
         int tex_counter = 1;
         int unit_counter = 33984; // TextureUnit enum starts from 33984 and goes up by 1 each unit
         foreach (var t in this.textures) {
            // use the right texture unit
            t.UseTexture((TextureUnit)(unit_counter));
            // set the right sampler uniform
            shaderProgram.SetInt("texture" + tex_counter.ToString(), tex_counter - 1);
            tex_counter += 1;
            unit_counter += 1;
         }
      }

      public void Delete()
      {
         // unbind and delete objects and buffers
         GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
         GL.DeleteBuffer(vertexBufferObject);
         // for some reason i can't delete the VAO the same way
         // GL.BindVertexArray(0);
         // GL.DeleteVertexArray(vertexArrayObject);
      }
   }
}