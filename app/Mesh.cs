// 

namespace mesh
{
   public class Mesh {
      public int vertexBufferObject;
      public int elementBufferObject;
      public int vertexArrayObject;

      public float[] _vertexData;
      public float[] _normalData;
      public float[] _textureData;
      public uint[] _indexData;

      public Mesh(float[] vertexRawData, uint[] indexData)
      {
         // divide vertexRawData in
         //    1. positions
         //    2. colors
         //    3. texture coords
         //    4. normals
         // make a function to rebuild them
         // (necessary for the loading process of .obj and .mtl files)
      }
   }
}