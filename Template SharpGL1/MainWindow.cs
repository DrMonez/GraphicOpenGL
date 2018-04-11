using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;

namespace Template_SharpGL1
{
    public partial class MainWindow : Form
    {
        struct mouseCoords
        {
            public float X;
            public float Y;
            public mouseCoords(float _X, float _Y)
            {
                X = _X;
                Y = _Y;
            }
        };
        // Объекты OpenGL и камеры
        OpenGL gl;
        Camera cam;

        Hexagon hexagon;
        bool isMouseDown; // Проверка нажатия левой кнопки мыши
        mouseCoords lastMouseCoords;
        int light; // Источник света (0 - отключен, 1 - точечный, 2 - прожекторный)
        float a = 0; // Чтобы все вращалось, уи, залипательно
        bool isRotate; // Надо ли, чтобы все вращалось, уи, залипательно
        float x, y, z;
        int buff; // Тип буфера (1 - одинарная буферизация, 2 - двойная буферизация)
        bool lookMode; // какая-то фигушка:з
        bool remodel;

        public MainWindow() // Главное окно
        {
            InitializeComponent(); // Автоматическая инициализация (не лезть) хд

            GL.MouseWheel += GL_MouseWheel; // так надо

            isMouseDown = false;
            isRotate = false;
            light = 0;
            buff = 1;
            lookMode = false;
            remodel = false;

            cam = new Camera();

            hexagon = new Hexagon(gl);
        }

        

        private void drawCube() // Отрисовка кубика, это не по заданию, это мы игрались
        {
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(1.0, 0, 0);
            gl.Vertex(1, -1, -1);
            gl.Vertex(1, 1, -1);
            gl.Vertex(1, 1, 1);
            gl.Vertex(1, -1, 1);

            gl.Color(0, 1.0, 0);
            gl.Vertex(-1, -1, -1);
            gl.Vertex(-1, 1, -1);
            gl.Vertex(-1, 1, 1);
            gl.Vertex(-1, -1, 1);
            gl.Color(0, 0, 1.0);
            gl.Vertex(-1, -1, -1);
            gl.Vertex(1, -1, -1);
            gl.Vertex(1, -1, 1);
            gl.Vertex(-1, -1, 1);

            gl.Color(1.0, 1.0, 0);
            gl.Vertex(-1, 1, -1);
            gl.Vertex(1, 1, -1);
            gl.Vertex(1, 1, 1);
            gl.Vertex(-1, 1, 1);

            gl.Color(0, 1.0, 1.0);
            gl.Vertex(-1, 1, -1);
            gl.Vertex(1, 1, -1);
            gl.Vertex(1, -1, -1);
            gl.Vertex(-1, -1, -1);

            gl.Color(1.0, 0, 1.0);
            gl.Vertex(-1, 1,1);
            gl.Vertex(1, 1, 1);
            gl.Vertex(1, -1, 1);
            gl.Vertex(-1, -1, 1);
            gl.End();
        }

        private void openGLControl1_Load(object sender, EventArgs e) // Загрузка окна
        {
            gl = GL.OpenGL; // Передача функций окна объекту gl
            gl.ClearColor(1.0f, 0.98f, 0.98f, 1); // Установка фона окна
            
            gl.Viewport(0, 0, GL.Width, GL.Height); // (Начало координат окна для рисования, ширина и высота этого окна)

            gl.MatrixMode(OpenGL.GL_PROJECTION); // Режим редактирования сцены
            gl.LoadIdentity(); // Обнуляем матрицу
            gl.Perspective(60, (float)GL.Width / (float)GL.Height, 0.1, 200); // Описание характеристик перспективы

            gl.MatrixMode(OpenGL.GL_MODELVIEW); // Режим просмотра сцены
            gl.LoadIdentity();
            
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_NORMALIZE);
        }
        
        private void DoLight()
        {
            if (light != 0)
            {
                gl.Enable(OpenGL.GL_LIGHTING); // Подключение освещения

                gl.Enable(OpenGL.GL_COLOR_MATERIAL); // Подключения материалов

                if (light == 1) // Точечный свет
                {
                    gl.Disable(OpenGL.GL_LIGHT1); // Отключаем прожекторные, если он включён
                    float[] pos = { 0, 5, 0, 1 }; // Позиция для источника света
                    gl.Enable(OpenGL.GL_LIGHT0); // Подключаем точечный источник света
                    gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, pos); // Включили свет, уииии
                }
                if (light == 2) // Прожекторное освещение
                {
                    gl.Disable(OpenGL.GL_LIGHT0); // Отключаем точечный, если он включён

                    gl.LightModel(OpenGL.GL_LIGHT_MODEL_TWO_SIDE, OpenGL.GL_FALSE); // Двусторонняя модель освещения (расчёт для всех точек фигуры)
                    float[] light_direction = new float[4] { 0, 0, 10, 1 }; // Позиция источника света
                    float[] ambient = new float[4] { 0.1f, 0.1f, 0.1f, 1.0f }; // Фоновая составляющая
                    
                    float[] light_diffuse = new float[3] { 1.0f, 1.0f, 1.0f };  // Диффузная составляющая для света
                    float[] specular = new float[4] { 1, 1, 1, 1 }; // Зеркальная составляющая
                    gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_POSITION, light_direction);
                    gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_CONSTANT_ATTENUATION, 1);
                    gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_LINEAR_ATTENUATION, 0);
                    gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_QUADRATIC_ATTENUATION, 0);
                    gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_SPOT_CUTOFF, 5.0f);
                    gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_SPOT_DIRECTION, new float[3] { 0, 0, -5 });

                    gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_SPECULAR, specular);
                    gl.Light(OpenGL.GL_LIGHT1, OpenGL.GL_DIFFUSE, light_diffuse);
                    gl.Enable(OpenGL.GL_LIGHT1);
                }


                float[] color = new float[4] { 1, 1, 1, 1 }; // красный цвет
                float[] shininess = new float[1] { 30 };
                gl.Material(OpenGL.GL_FRONT, OpenGL.GL_DIFFUSE, color); // цвет 
                gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SPECULAR, color); // отраженный свет
                gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SHININESS, shininess); // степень отраженного света
            }
            else
            {
                gl.Disable(OpenGL.GL_LIGHTING); // Уходя, не забывайте выключать свет
                gl.Disable(OpenGL.GL_COLOR_MATERIAL); // Отключение материалов
            }
        }

        private void Buff() // Буферизация
        {
            if (buff==1) // Одинарная -//-
            {
                gl.Disable(OpenGL.GL_DEPTH_TEST);
                gl.Disable(OpenGL.GL_DOUBLEBUFFER);
                gl.Enable(OpenGL.GL_DEPTH_TEST);
            }
            if(buff==2) // Двойная -//-
            {
                gl.Disable(OpenGL.GL_DEPTH_TEST);
                gl.Enable(OpenGL.GL_DEPTH_TEST);
                gl.Enable(OpenGL.GL_DOUBLEBUFFER);
                
            }
        }

        private void ProjectionWork() // Ремоделинг вида обзора на сцену
        {
            if (lookMode)
            {
                gl.MatrixMode(OpenGL.GL_PROJECTION); // Переходим в режим редактирования сцены
                gl.LoadIdentity(); // Обнуляем матрицу
                gl.Ortho(-GL.Width / (double)GL.Height * 5, GL.Width / (double)GL.Height * 5, -5, 5, 0.1, 200); // Задаем параметры отрогонализации
                gl.MatrixMode(OpenGL.GL_MODELVIEW); // Перехоим в режим просмотра сцены
                gl.LoadIdentity();
            }
            else
            {
                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.LoadIdentity();
                gl.Perspective(60, (float)GL.Width / (float)GL.Height, 0.1, 200); // Задаем параметры перспективы 
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
            }
            remodel = false;
        }

        private void GL_OpenGLDraw(object sender, RenderEventArgs args) // Отрисовка
        {
            
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT); // Очистка экрана (буфера)

            DoLight(); // Делай свет
            Buff(); // Делаем буферизацию

            if (remodel) ProjectionWork(); // Если надо, перестройка проекции

            gl.PushMatrix(); // Добавлем матрицу, которую будем менять
            gl.Translate(0, 0, -10); // Перенос начала координат
            gl.Rotate(a + cam.angleV, a / 2 + cam.angleH, 0); // Поворот 
            if (isRotate) a += 2; // Если надо, то уи, все вращается, залипательно
            gl.Scale(cam.scale, cam.scale, cam.scale); // Изменение масштаба
            gl.Translate(0, 0, -hexagon.getHalfLength()/2.0f); // Перенос камеры в центр объекта
            //drawCube();
            hexagon.draw(gl); // Прорисовка всего объекта (бегин здесь, и энд здесь)
            gl.PopMatrix(); // Удаление матрицы
            
            gl.Flush(); // Конец отрисовки

        }

        private void openGLControl1_Resize(object sender, EventArgs e) // Изменение размера окна
        {
            gl = GL.OpenGL;

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(60, (float)GL.Width / (float)GL.Height, 0.1, 200);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
        }

        private void GL_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void GL_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void GL_MouseMove(object sender, MouseEventArgs e) // Отслеживание вращения камеры
        {
            if (isMouseDown) // Зажатая кнопка мыши
            {
                float dx =  e.X - lastMouseCoords.X;
                float dy =  e.Y - lastMouseCoords.Y;
                cam.Rotate(dy * 0.40f, dx * 0.40f);
                lastMouseCoords.X = e.X;
                lastMouseCoords.Y = e.Y;
            }
        }

        private void GL_MouseLeave(object sender, EventArgs e)
        {

        }

        private void GL_MouseDown(object sender, MouseEventArgs e) // Нажали на кнопку мыши
        {
            lastMouseCoords.X = e.X;
            lastMouseCoords.Y = e.Y;
            isMouseDown = true;           
        }

        private void GL_MouseEnter(object sender, EventArgs e)
        {

        }

        private void GL_MouseUp(object sender, MouseEventArgs e) // Отпустили кнопку мыши
        {
            isMouseDown = false;
        }

        private void GL_MouseWheel(object sender, MouseEventArgs e) // Обработка колёсика
        {
            cam.Zoom(e.Delta);
        }

        private void isRotationButton(object sender, EventArgs e) // Поведение при нажатии на кнопку "Вращение"
        {
            if (isRotate) isRotate = false;
            else isRotate = true;
        }

        private void textToOnlyNumbers(ref string text) // Проверка, на цифру в поле
        { //если чо-то левое то обнуление полей
            string buff = text;
            string result = "";
            int lettersCount = text.Count();
            bool dotNotWas = true;
            for (int i = 0; i < lettersCount; i++)
                if (buff[i] >= '0' && buff[i] <= '9' || buff[i] == ',' && dotNotWas)
                {
                    if (buff[i] == ',' && dotNotWas)
                        dotNotWas = false;
                    result += buff[i];
                }
            if (result == "")
                result = "0";
            text = result;
        }

        private void anyTextBox_TextChanged(object sender, EventArgs e) // Обработка текста в поле
        {
            TextBox textbox = (TextBox)sender;
            string text = textbox.Text;
            textToOnlyNumbers(ref text);
            textbox.Text = text;
            int lettersCount = text.Count();

            if (textbox == xTextBox)
            {
                x = Convert.ToInt32(text, 10); 
                return;
            }
            if (textbox == yTextBox)
            {
                y = Convert.ToInt32(text, 10);
                return;
            }
            if (textbox == zTextBox)
            {
                z = Convert.ToInt32(text, 10);
                return;
            }
            if(textbox == angleTextBox)
            {
                hexagon.setRotateAngle(Convert.ToSingle(text));
                return;
            }
        }

        private void trackBarLight_Scroll(object sender, EventArgs e) // Переключает источники света на ползунке
        {
            light = trackBarLight.Value;
        }

        private void trackBarLight_GiveFeedback(object sender, GiveFeedbackEventArgs e) // Автоматически сгенерировалось (скайлайн атакует хд)
        {
            light = trackBarLight.Value;
        }

        private void normButton_Click(object sender, EventArgs e) // Обработка клавиши нормы
        {
            hexagon.setNorm();
        }

        private void isBuildButton(object sender, EventArgs e) // Поведение при нажатии на кнопку "Построить"
        {
            hexagon.setCoord(x, y, z);
            hexagon.CalculateNormals();
        }

        private void isGridButton(object sender, EventArgs e) // Поведение при нажатии на кнопку "Сетка"
        {
            hexagon.gridSwitch();
        }

        private void isBufButton(object sender, EventArgs e) // Обработка клавиши буфера и клавиши изменения модели проекции
        {
            if (sender == bufButton)
                buff = 1;
            if (sender == doubleBufButton)
                buff = 2;
            if (sender == lookModeButton)
            { lookMode = !lookMode; remodel = true; }

        }

        private void SmoothButton(object sender, EventArgs e)
        {
            if (hexagon.getCountOfSections() > 1)
            {
                hexagon.CalculateNormals();
                hexagon.Smooth();
            }
        }

        private void TextureButton(object sender, EventArgs e)
        {
            hexagon.Texture(gl);
        }

    }
}
