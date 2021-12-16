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
   FragColor = mix(texture(texture1, texCoord), mix(texture(texture2, texCoord), vec4(vertColor.xyz, 1.0), 0.5), 0.5);
}