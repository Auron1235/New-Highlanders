using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Map_Generator
{
    public class Camera2D
    {
        private Vector2 m_target;
        public Vector2 Target
        {
            get { return m_target; }
            set
            {
                m_target = value - new Vector2(ViewPort.Width / 2.0f, ViewPort.Height / 2.0f);
                ViewPort.X = (int)m_target.X;
                ViewPort.Y = (int)m_target.Y;
            }
        }
        private Viewport ViewPort;

        //Gary's addition for drawing code.
        private Rectangle m_viewPortRect;
        public Rectangle ViewPortRect
        {
            get { return m_viewPortRect; }
            set { m_viewPortRect = value; }
        }

        public Camera2D(int x, int y, int width, int height)
        {
            ViewPort = new Viewport(x, y, width, height);
            Target = Vector2.Zero;

            //Gary's addition
            m_viewPortRect = new Rectangle(x, y, width, height);
        }

        public Matrix GetViewPortMatrix
        {
            get { return Matrix.CreateTranslation(new Vector3(-Target, 0f)); }  
        }

        //EVERYTHING BELOW THIS LINE IS OURS, NOT JAMES', AND THUS MAY BE
        //REMOVED WITHOUT BREAKING THE CAMERA'S BASIC FUNCTIONALITY

        public void UpdateViewPort(Vector2 currentCameraPos)
        {
            //m_viewPortRect = new Rectangle((int)currentCameraPos.X, (int)currentCameraPos.Y, m_viewPortRect.Width, m_viewPortRect.Height);
            m_viewPortRect = ViewPort.Bounds;
        }

        public bool ObjectIsVisible(Rectangle bounds)
        {
            return (m_viewPortRect.Intersects(bounds));
        }
    }
}
