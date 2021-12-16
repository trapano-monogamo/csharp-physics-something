using System;
using System.IO;
using System.Collections.Generic;
using renderable;

namespace objectloader
{
   public class RawObjectData {
      public List<float> vertices;
      public List<uint> indices;
      public RawObjectData(List<float> v, List<uint> i)
         {vertices = v; indices = i;}
   }

   public class ObjectLoader
   {
      public static RawObjectData LoadObjFile(string path)
      {
         StreamReader sr = File.OpenText(path);

         List<float> vertices = new List<float>();
         List<uint> indices = new List<uint>();

         string line;
         while ((line = sr.ReadLine()) != null) {
            Console.WriteLine(line);
            string[] tokens = line.Split(' ');

            // load vertices
            if (tokens[0] == "v") {
               vertices.Add(float.Parse(tokens[1]));
               vertices.Add(float.Parse(tokens[2]));
               vertices.Add(float.Parse(tokens[3]));
            }
            // load indices
            if (tokens[0] == "f") {
               foreach (var t in tokens) {
                  var f = t.Split('/');
                  indices.Add(uint.Parse(f[0]));
               }
            }
         }

         return new RawObjectData(vertices,indices);
      }
   }
}