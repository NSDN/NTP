using System;

using SharpGL;
using SharpGL.Enumerations;

using dotNSGDX.Entity;
using dotNSGDX.Utility;

namespace NTP.Core
{
    public class Renderer : IRenderer
    {
        public OpenGL GL { get; protected set; }

        public Renderer(OpenGL gl)
        {
            GL = gl;
        }

        public void Begin()
        {
            GL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        }

        public void Draw(Shape shape, float x, float y, float rotate)
        {
            GL.PushMatrix();
            GL.Rotate(0, 0, rotate);
            GL.Translate(x, y, 0);
            GL.Begin(BeginMode.Polygon);
            foreach (Shape.Instruction i in shape.instBuf)
            {
                if (i.IsColor()) GL.Color(((Shape.Color4)i).r, ((Shape.Color4)i).g, ((Shape.Color4)i).b, ((Shape.Color4)i).a);
                else GL.Vertex(((Shape.Vec3)i).x, ((Shape.Vec3)i).y, ((Shape.Vec3)i).z);
            }
            GL.End();
            GL.PopMatrix();
        }

        public void End()
        {
            GL.Flush();
        }
    }
}
