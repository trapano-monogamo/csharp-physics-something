using System;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace shader
{
   public class Shader : IDisposable
   {
      int handle;

      public Shader(string vertexPath, string fragmentPath)
      {
         // shader handles
         int vertexShader;
         int fragmentShader;

         // reading shader source files
         string vertexShaderSource;
         using (StreamReader sr = new StreamReader(vertexPath, Encoding.UTF8))
         {
            vertexShaderSource = sr.ReadToEnd();
         }

         string fragmentShaderSource;
         using (StreamReader sr = new StreamReader(fragmentPath, Encoding.UTF8))
         {
            fragmentShaderSource = sr.ReadToEnd();
         }

         // creating shaders
         vertexShader = GL.CreateShader(ShaderType.VertexShader);
         GL.ShaderSource(vertexShader, vertexShaderSource);
         fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
         GL.ShaderSource(fragmentShader, fragmentShaderSource);

         // compiling shaders
         GL.CompileShader(vertexShader);
         string infoLogVert = GL.GetShaderInfoLog(vertexShader);
         if (infoLogVert != System.String.Empty)
            Console.WriteLine(infoLogVert);

         GL.CompileShader(fragmentShader);
         string infoLogFrag = GL.GetShaderInfoLog(fragmentShader);
         if (infoLogFrag != System.String.Empty)
            Console.WriteLine(infoLogFrag);

         // creating program
         this.handle = GL.CreateProgram();
         GL.AttachShader(this.handle, vertexShader);
         GL.AttachShader(this.handle, fragmentShader);
         GL.LinkProgram(this.handle);
      }

      public int GetUniformLocation(string name) {
         GL.UseProgram(handle);
         int location = GL.GetUniformLocation(handle, name);
         return location;
      }

      public int GetAttribLocation(string attribName)
      {
         return GL.GetAttribLocation(handle, attribName);
      }

      public void SetMatrix4(string name, Matrix4 mat, bool transpose = true) {
         this.UseProgram();
         int location = GetUniformLocation(name);
         GL.UniformMatrix4(location, transpose, ref mat);
      }

      public void SetInt(string name, int value)
      {
         this.UseProgram();
         int location = GL.GetUniformLocation(handle, name);
         GL.Uniform1(location, value);
      }

      public void SetVector3(string name, Vector3 vec)
      {
         this.UseProgram();
         int location = GL.GetUniformLocation(handle, name);
         GL.Uniform3(location, vec.X, vec.Y, vec.Z);
      }

      public void UseProgram()
      {
         GL.UseProgram(this.handle);
      }

      private bool disposedValue = false;

      protected virtual void Dispose(bool disposing)
      {
         if (!disposedValue) {
            GL.DeleteProgram(handle);
            disposedValue = true;
         }
      }

      ~Shader()
      {
         GL.DeleteProgram(this.handle);
      }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }
   }
}