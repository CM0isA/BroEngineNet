#version 460 core
layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec4 aColor;
layout(location = 2) in vec3 aNormals;


layout(location = 3) uniform mat4 model;
layout(location = 4) uniform mat4 view;
layout(location = 5) uniform mat4 projection;

out vec4 Color;

void main()
{
    gl_Position =  projection * view * model * vec4(aPosition, 1.0f);

    Color = aColor;
}