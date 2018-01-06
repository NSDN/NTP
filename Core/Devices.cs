using System;
using System.Windows;
using dotNSGDX.Entity;
using dotNSGDX.Utility;

namespace NTP.Core
{
    public class Devices
    {
        public class Rail : Device
        {
            protected Shape shape;

            public Rail() : base()
            {
                info.id = 1;
                info.dev = info.key = "test";
                info.input = "in";
                info.output = "out";

                shape = new Shape();
                shape.AddColor(1, 0, 0).AddVertex(1, 1)
                    .AddColor(1, 1, 0).AddVertex(1, -1)
                    .AddColor(0, 1, 0).AddVertex(-1, -1)
                    .AddColor(0, 0, 1).AddVertex(-1, 1);
            }

            public override Result OnRender(IRenderer renderer)
            {
                renderer.Draw(shape, info.x, info.y, info.r);
                return Result.DONE;
            }

            public override Result OnUpdate(int t)
            {
                return Result.DONE;
            }
        }

        public class Rail1 : Rail
        {
            public GetScale scale;

            public delegate float GetScale();

            public Rail1(FrameworkElement element, GetScale scale) : base()
            {
                shape = new Shape();
                shape.AddColor(1, 0, 0).AddVertex(0, 1)
                    .AddColor(0, 1, 0).AddVertex(1, -1)
                    .AddColor(0, 0, 1).AddVertex(-1, -1);

                this.scale = scale;

                element.MouseMove += (sender, e) =>
                {
                    info.x = (float)e.GetPosition(element).X - (float)(element.ActualWidth / 2);
                    info.y = -((float)e.GetPosition(element).Y - (float)(element.ActualHeight / 2));
                    info.x /= this.scale.Invoke();
                    info.y /= this.scale.Invoke();
                };
            }

            public override Result OnRender(IRenderer renderer)
            {
                renderer.Draw(shape, info.x, info.y, info.r);
                if (renderer is Renderer)
                {
                    SharpGL.OpenGL gl = ((Renderer)renderer).GL;
                    gl.DrawText(16, 16, 1, 1, 1, "Consolas", 16, "x=" + info.x + ",y=" + info.y + ",s=" + scale.Invoke());
                }
                return Result.DONE;
            }
        }
    }
}
