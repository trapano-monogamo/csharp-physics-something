#version 330

in vec4 vertColor;
in vec2 texCoord;
out vec4 FragColor;

uniform vec3 light_direction;
uniform vec3 light_color;
uniform int isTextured;

uniform sampler2D texture1;
uniform sampler2D texture2;
uniform sampler2D texture3;
uniform sampler2D texture4;

void main()
{
   vec4 base_frag_color;
   if (isTextured == 1) {
      vec4 tempFragColor = mix(texture(texture1, texCoord), texture(texture2, texCoord), 0.5);
      base_frag_color = mix(tempFragColor, vec4(vertColor.xyz, 1.0), 0.5);
   } else {
      base_frag_color = vertColor;
   }

   FragColor = base_frag_color * vec4(light_color, 1.0);
}