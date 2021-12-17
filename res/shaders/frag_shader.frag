#version 330

in vec4 vertColor;
in vec2 texCoord;
out vec4 FragColor;

uniform sampler2D texture1;
uniform sampler2D texture2;
uniform sampler2D texture3;
uniform sampler2D texture4;

void main()
{
   vec4 tempFragColor = mix(texture(texture1, texCoord), texture(texture2, texCoord), 0.5);
   FragColor = mix(tempFragColor, vec4(vertColor.xyz, 1.0), 0.5);
   //FragColor = vertColor;
}