using System;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.Generic;

using MahApps.Metro.Controls;
using SharpGL;
using SharpGL.Enumerations;
using NTP.Core;

namespace NTP
{
    /// <summary>
    /// EditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditorWindow : MetroWindow
    {

        OpenGL gl;

        #region "Control"
        double x = 0, y = 0, z = 5;
        Point prev, now; bool state = false;
        #endregion

        Renderer renderer;
        Map map;

        public EditorWindow()
        {
            InitializeComponent();
        }

        private void BtnLogo_Click(object sender, RoutedEventArgs e)
        {
            ToolPanel.IsOpen = !ToolPanel.IsOpen;
        }

        private void GLControl_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            gl.LoadIdentity();
            gl.Translate(GLControl.ActualWidth / 2.0, GLControl.ActualHeight / 2.0, 0);
            gl.Translate(x, y, 0);
            gl.Scale(z, z, 1);

            map.core.OnRender();
        }

        private void GLControl_Resized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Ortho2D(0, GLControl.ActualWidth, 0, GLControl.ActualHeight);
            gl.MatrixMode(MatrixMode.Modelview);
        }

        private void GLControl_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            gl = GLControl.OpenGL;
            gl.ClearColor(0.0F, 0.0F, 0.0F, 1.0F);

            renderer = new Renderer(gl);
            map = new Map(renderer);
            map.core.Add(new Devices.Rail());
        }

        private void GLControl_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.MiddleButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                now = e.GetPosition(GLControl);
                if (state)
                {
                    x += (now.X - prev.X) * 2.0;
                    y += -(now.Y - prev.Y) * 2.0;
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
            double dz = e.Delta / 120.0;
            if (z < 5) z += dz;
            else if (z == 5)
            {
                if (dz > 0) z += dz * 5;
                else z += dz;
            }
            else z += dz * 5;
            if (z < 1) z = 1;
            if (z > 50) z = 50;
        }
    }
}
