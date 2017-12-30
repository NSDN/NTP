using System;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.Generic;

using MahApps.Metro.Controls;
using SharpGL;
using SharpGL.Enumerations;

namespace NTP
{
    /// <summary>
    /// EditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditorWindow : MetroWindow
    {

        OpenGL gl;
        double x = 0, y = 0, z = -2.5;
        Point prev, now; bool state = false;

        public EditorWindow()
        {
            InitializeComponent();
        }

        private void GLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.LoadIdentity();
            gl.Translate(x, y, z);

            gl.Begin(BeginMode.Triangles);
            gl.Color(1.0F, 0.0F, 0.0F); gl.Vertex(0.0F, 1.0F);
            gl.Color(0.0F, 1.0F, 0.0F); gl.Vertex(-1.0F, -1.0F);
            gl.Color(0.0F, 0.0F, 1.0F); gl.Vertex(1.0F, -1.0F);
            gl.End();

            gl.DrawText(4, 4, 1.0F, 1.0F, 1.0F, "Consolas", 16.0F, String.Format("({0:F},{1:F},{2:F})", x, y, z));

            gl.Flush();

        }

        private void GLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            gl = GLControl.OpenGL;
            gl.ClearColor(0.0F, 0.0F, 0.0F, 1.0F);
        }

        private void GLControl_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.MiddleButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                now = e.GetPosition(GLControl);
                if (state)
                {
                    double v = 200.0 / Math.Abs(z);
                    x += (now.X - prev.X) / v;
                    y += -(now.Y - prev.Y) / v;
                    state = false;
                }
                else
                {
                    prev = now;
                    state = true;
                }
            }
            else state = false;
        }

        private void GLControl_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            z += Math.Pow(e.Delta / 40.0 / 10.0, 3);
            if (z > -1) z = -1;
        }
    }
}
