#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aNormal;
layout(location = 2) in vec4 aColor;
layout(location = 3) in vec2 aTexCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec4 vertColor;
out vec2 texCoord;

void main(void)
{
   texCoord = aTexCoord;
   vertColor = aColor;
   gl_Position = vec4(aPosition, 1.0) * model * view * projection;
}