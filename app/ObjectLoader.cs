using System;
using System.IO;
using System.Collections.Generic;
using renderable;

namespace objectloader
{
   public class RawObjectData {
      public string name;
      public List<float> vertices;
      public List<float> texcoords;
      public List<float> normals;
      public List<uint> vindices;
      public List<uint> tindices;
      public List<uint> nindices;
      public string material;

      public void Print()
      {
         foreach (var v in vertices){
            Console.Write(v + " ");
         }
         foreach (var t in texcoords){
            Console.Write(t + " ");
         }
         foreach (var n in normals){
            Console.Write(n + " ");
         }
         Console.Write("\n");
         for(int i=0; i<vindices.Count; i++){
            Console.Write(vindices[i] + "/" + tindices[i] + "/" + nindices[i] + " ");
         }
         Console.Write("\n");
      }
   }

   public class ObjectLoader
   {
      public static RawObjectData LoadObjFile(string path)
      {
         StreamReader sr = File.OpenText(path);

         List<float> vertices = new List<float>();
         List<float> texcoords = new List<float>();
         List<float> normals = new List<float>();
         List<uint> vindices = new List<uint>();
         List<uint> tindices = new List<uint>();
         List<uint> nindices = new List<uint>();
         string material = "";

         string line;
         while ((line = sr.ReadLine()) != null) {
            //Console.WriteLine(line);
            string[] tokens = line.Split(' ');

            switch (tokens[0]) {
               // vertex position
               case "v":
                  vertices.Add(float.Parse(tokens[1]));
                  vertices.Add(float.Parse(tokens[2]));
                  vertices.Add(float.Parse(tokens[3]));
                  break;

               // texture coordinates
               case "vt":
                  texcoords.Add(float.Parse(tokens[1]));
                  texcoords.Add(float.Parse(tokens[2]));
                  break;

               // vertex normal
               case "vn":
                  normals.Add(float.Parse(tokens[1]));
                  normals.Add(float.Parse(tokens[2]));
                  normals.Add(float.Parse(tokens[3]));
                  break;

               // face indices (v/vt/vn)
               case "f":
                  for (int i=1; i<tokens.Length; i++) {
                     var f = tokens[i].Split('/');
                     vindices.Add(uint.Parse(f[0]));
                     tindices.Add(uint.Parse(f[1]));
                     nindices.Add(uint.Parse(f[2]));
                  }
                  break;
               
               case "usemtl":
                  material = tokens[1];
                  break;

               default: break;
            }
         }

         return new RawObjectData{
            vertices = vertices,
            texcoords = texcoords,
            normals = normals,
            vindices = vindices,
            tindices = tindices,
            nindices = nindices,
            material = material,
         };
      }
      
      public static void LoadMtlFile(string path) {}
   }
}