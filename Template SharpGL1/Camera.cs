using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template_SharpGL1
{
    class Camera
    {
        public float scale;
        public float angleV, angleH;
        public float dX, dY, dZ;
        public Camera() // Конструктор
        {
            dX = dY = dZ = 0;
            angleV = angleH = 0;
            scale = 1;
        }

        public void Zoom(float dscale) // Изменение  масштаба
        {
            scale += dscale * scale * 0.001f;
            if (scale < 0)
                scale = 1e-10f;
        }

        public void Translate(float dx, float dy, float dz) // Перенос камеры
        {
            dX += dx;
            dY += dy;
            dZ += dz;
        }

        public void Rotate(float dVert, float dHor) // Поворот камеры
        {
            if (angleV > 90 && angleV < 270)
                dHor *= -1;
            angleH += dHor;
            if (angleH < 0)
                angleH += 360;
            else
                angleH %= 360;
            angleV += dVert;
            if (angleV < 0)
                angleV += 360;
            else
                angleV %= 360;

        }
    }
}