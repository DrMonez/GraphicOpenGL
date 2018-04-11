using System;
using System.Collections.Generic;
using SharpGL;
using System.Drawing;

namespace Template_SharpGL1
{
    partial class Hexagon
    {
        List<List<float[]>> sections; // Массив сечений
        List<List<float>> coordinates; // Массив координат центров сечений
        int N_points; // Число точек сечения фигуры (6)
        int N_sections; // Число сечений (переменное)
        float half_length; // Половина длины фигуры в целом
        float rotate_angle; // Угол поворота
        float scale; // Масштаб
        float last_size; // Масштаб последней секции
        bool neenGrid; // Вкл/Выкл сетки
        bool norm; // Вкл/Выкл нормалей
        bool tex;
        bool smooth;

        List<List<float[]>> smothNormals; // Массив координат центров сечений
        SharpGL.SceneGraph.Assets.Texture texture;
        List<float> textureX;
        List<float> textureY;

        void initParam() // Инициализация
        {
            sections = new List<List<float[]>> { };
            coordinates = new List<List<float>> { };
            coordinates.Add(new List<float> {0,0,0});

            smothNormals = new List<List<float[]>> { }; ;
            textureX = new List<float> { };
            textureY = new List<float> { };

            half_length = 0;
            rotate_angle = 0;
            N_sections = 1;
            neenGrid = false;
            norm = false;
            tex = false;
            smooth = false;
            scale = 1.0f;
        }

        public void CreateTex(OpenGL context, Bitmap image)
        {
            texture = new SharpGL.SceneGraph.Assets.Texture();
            texture.Create(context, image);
            context.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
            context.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_REPEAT);
            context.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
            context.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
            context.TexEnv(OpenGL.GL_TEXTURE_ENV, OpenGL.GL_TEXTURE_ENV_MODE, OpenGL.GL_REPLACE);
            texture.Bind(context);
        }

        public Hexagon(OpenGL gl)
        {
            initParam();
            reModelSection();
            CreateTex(gl, (Bitmap)Image.FromFile("default.png"));
        }

        public void CalcTexCoor()
        {
            Vector3f buf = new Vector3f();
            float len = 0;
            textureY.Clear();

            textureX.Clear();
            textureX.Add(0);
            for (int i = 1; i < N_points; i++)
            {
                buf.x = sections[0][i][0] - sections[0][i - 1][0];
                buf.y = sections[0][i][1] - sections[0][i - 1][1];
                buf.z = sections[0][i][2] - sections[0][i - 1][2];
                len += buf.NormOfVector(buf);
                textureX.Add(len);
            }
            for (int i = 1; i < N_points; i++)
            {
                textureX[i] /= len;
            }

            textureY.Add(0);
            len = 0;
            for (int i = 1; i < N_sections; i++)
            {
                buf.x = sections[i][0][0] - sections[i - 1][0][0];
                buf.y = sections[i][0][1] - sections[i - 1][0][1];
                buf.z = sections[i][0][2] - sections[i - 1][0][2];
                len += buf.NormOfVector(buf);
                textureY.Add(len);
            }
            for (int i = 1; i < N_sections; i++)
            {
                textureY[i] /= len;
            }
        }

        public void CalculateNormals()
        {
            smothNormals.Clear();
            Vector3f a, b, c, result;

            //1ое сечение
            //1ая

            a = new Vector3f(sections[0][0][0], sections[0][0][1], sections[0][0][2]);
            b = new Vector3f(sections[0][1][0], sections[0][1][1], sections[0][1][2]);
            c = new Vector3f(sections[0][2][0], sections[0][2][1], sections[0][2][2]);
            result = new Vector3f();

            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            result = a;

            a = new Vector3f(sections[0][N_points - 1][0], sections[0][N_points - 1][1], sections[0][N_points - 1][2]);
            b = new Vector3f(sections[1][0][0], sections[1][0][1], sections[1][0][2]);
            c = new Vector3f(sections[0][0][0], sections[0][0][1], sections[0][0][2]);
            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            result += a;

            a = new Vector3f(sections[0][0][0], sections[0][0][1], sections[0][0][2]);
            b = new Vector3f(sections[1][0][0], sections[1][0][1], sections[1][0][2]);
            c = new Vector3f(sections[0][1][0], sections[0][1][1], sections[0][1][2]);
            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            result += a;


            result = result.NormalisationOfVector(result); // Нормируем вектор

            smothNormals.Add(new List<float[]> { });
            smothNormals[0].Add(new float[3] { result.x, result.y, result.z });


            //все посередине
            for (int i = 1; i < N_points - 1; i++)
            {
                a = new Vector3f(sections[0][i][0], sections[0][i][1], sections[0][i][2]);
                b = new Vector3f(sections[0][i - 1][0], sections[0][i - 1][1], sections[0][i - 1][2]);
                c = new Vector3f(sections[1][i][0], sections[1][i][1], sections[1][i][2]);
                result = new Vector3f();

                c = a - c; // Получение векторов
                b = b - a;

                a = a.Cross(b, c); // Векторное умножение
                result = a;


                a = new Vector3f(sections[0][0][0], sections[0][0][1], sections[0][0][2]);
                b = new Vector3f(sections[0][1][0], sections[0][1][1], sections[0][1][2]);
                c = new Vector3f(sections[0][2][0], sections[0][2][1], sections[0][2][2]);
                c = a - c; // Получение векторов
                b = b - a;

                a = a.Cross(b, c); // Векторное умножение
                result += a;

                a = new Vector3f(sections[0][i][0], sections[0][i][1], sections[0][i][2]);
                b = new Vector3f(sections[1][i][0], sections[1][i][1], sections[1][i][2]);
                c = new Vector3f(sections[0][i + 1][0], sections[0][i + 1][1], sections[0][i + 1][2]);
                c = a - c; // Получение векторов
                b = b - a;

                a = a.Cross(b, c); // Векторное умножение
                result += a;

                result = result.NormalisationOfVector(result); // Нормируем вектор
                smothNormals[0].Add(new float[3] { result.x, result.y, result.z });
            }
            //последняя
            a = new Vector3f(sections[0][0][0], sections[0][0][1], sections[0][0][2]);
            b = new Vector3f(sections[0][1][0], sections[0][1][1], sections[0][1][2]);
            c = new Vector3f(sections[0][2][0], sections[0][2][1], sections[0][2][2]);
            result = new Vector3f();

            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            result = a;

            a = new Vector3f(sections[0][N_points - 2][0], sections[0][N_points - 2][1], sections[0][N_points - 2][2]);
            b = new Vector3f(sections[1][N_points - 1][0], sections[1][N_points - 1][1], sections[1][N_points - 1][2]);
            c = new Vector3f(sections[0][N_points - 1][0], sections[0][N_points - 1][1], sections[0][N_points - 1][2]);
            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            result += a;

            a = new Vector3f(sections[0][N_points - 1][0], sections[0][N_points - 1][1], sections[0][N_points - 1][2]);
            b = new Vector3f(sections[1][N_points - 1][0], sections[1][N_points - 1][1], sections[1][N_points - 1][2]);
            c = new Vector3f(sections[0][0][0], sections[0][0][1], sections[0][0][2]);
            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            result += a;


            result = result.NormalisationOfVector(result); // Нормируем вектор

            smothNormals[0].Add(new float[3] { result.x, result.y, result.z });


            for (int i = 1; i < N_sections - 1; i++)
            {
                smothNormals.Add(new List<float[]> { });
                //1ая точка сечения

                a = new Vector3f(sections[i][N_points - 1][0], sections[i][N_points - 1][1], sections[i][N_points - 1][2]);
                b = new Vector3f(sections[i + 1][0][0], sections[i + 1][0][1], sections[i + 1][0][2]);
                c = new Vector3f(sections[i][0][0], sections[i][0][1], sections[i][0][2]);
                c = a - c; // Получение векторов
                b = b - a;

                a = a.Cross(b, c); // Векторное умножение
                result = a;

                a = new Vector3f(sections[i][0][0], sections[i][0][1], sections[i][0][2]);
                b = new Vector3f(sections[i + 1][0][0], sections[i + 1][0][1], sections[i + 1][0][2]);
                c = new Vector3f(sections[i][1][0], sections[i][1][1], sections[i][1][2]);
                c = a - c; // Получение векторов
                b = b - a;

                a = a.Cross(b, c); // Векторное умножение
                result += a;


                result = result.NormalisationOfVector(result); // Нормируем вектор

                smothNormals[i].Add(new float[3] { result.x, result.y, result.z });
                //Очтальные точки сечения

                for (int j = 1; j < N_points - 1; j++)
                {
                    a = new Vector3f(sections[i][j][0], sections[i][j][1], sections[i][j][2]);
                    b = new Vector3f(sections[i][j - 1][0], sections[i][j - 1][1], sections[i][j - 1][2]);
                    c = new Vector3f(sections[i + 1][j][0], sections[i + 1][j][1], sections[i + 1][j][2]);
                    result = new Vector3f();

                    c = a - c; // Получение векторов
                    b = b - a;

                    a = a.Cross(b, c); // Векторное умножение
                    result = a;

                    a = new Vector3f(sections[i][j][0], sections[i][j][1], sections[i][j][2]);
                    b = new Vector3f(sections[i + 1][j][0], sections[i + 1][j][1], sections[i + 1][j][2]);
                    c = new Vector3f(sections[i][j + 1][0], sections[i][j + 1][1], sections[i][j + 1][2]);
                    c = a - c; // Получение векторов
                    b = b - a;

                    a = a.Cross(b, c); // Векторное умножение
                    result += a;

                    result = result.NormalisationOfVector(result); // Нормируем вектор
                    smothNormals[i].Add(new float[3] { result.x, result.y, result.z });
                }


                //последняя точка

                a = new Vector3f(sections[i][N_points - 2][0], sections[i][N_points - 2][1], sections[i][N_points - 2][2]);
                b = new Vector3f(sections[i + 1][N_points - 1][0], sections[i + 1][N_points - 1][1], sections[i + 1][N_points - 1][2]);
                c = new Vector3f(sections[i][N_points - 1][0], sections[i][N_points - 1][1], sections[i][N_points - 1][2]);
                c = a - c; // Получение векторов
                b = b - a;

                a = a.Cross(b, c); // Векторное умножение
                result = a;

                a = new Vector3f(sections[i][N_points - 1][0], sections[i][N_points - 1][1], sections[i][N_points - 1][2]);
                b = new Vector3f(sections[i + 1][N_points - 1][0], sections[i + 1][N_points - 1][1], sections[i + 1][N_points - 1][2]);
                c = new Vector3f(sections[i][0][0], sections[i][0][1], sections[i][0][2]);
                c = a - c; // Получение векторов
                b = b - a;

                a = a.Cross(b, c); // Векторное умножение
                result += a;


                result = result.NormalisationOfVector(result); // Нормируем вектор

                smothNormals[i].Add(new float[3] { result.x, result.y, result.z });
            }


            //Последнее сечение 
            a = new Vector3f(sections[N_sections - 1][0][0], sections[N_sections - 1][0][1], sections[N_sections - 1][0][2]);
            b = new Vector3f(sections[N_sections - 1][2][0], sections[N_sections - 1][2][1], sections[N_sections - 1][2][2]);
            c = new Vector3f(sections[N_sections - 1][1][0], sections[N_sections - 1][1][1], sections[N_sections - 1][1][2]);
            result = new Vector3f();

            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            result = a;

            a = new Vector3f(sections[N_sections - 1][N_points - 1][0], sections[N_sections - 1][N_points - 1][1], sections[N_sections - 1][N_points - 1][2]);
            b = new Vector3f(sections[N_sections - 1][0][0], sections[N_sections - 1][0][1], sections[N_sections - 1][0][2]);
            c = new Vector3f(sections[N_sections - 2][0][0], sections[N_sections - 2][0][1], sections[N_sections - 2][0][2]);
            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            result += a;

            a = new Vector3f(sections[N_sections - 1][0][0], sections[N_sections - 1][0][1], sections[N_sections - 1][0][2]);
            b = new Vector3f(sections[N_sections - 1][1][0], sections[N_sections - 1][1][1], sections[N_sections - 1][1][2]);
            c = new Vector3f(sections[N_sections - 2][0][0], sections[N_sections - 2][0][1], sections[N_sections - 2][0][2]);
            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            result += a;


            result = result.NormalisationOfVector(result); // Нормируем вектор

            smothNormals.Add(new List<float[]> { });
            smothNormals[N_sections - 1].Add(new float[3] { result.x, result.y, result.z });


            //все посередине
            for (int i = 1; i < N_points - 1; i++)
            {
                a = new Vector3f(sections[N_sections - 1][i][0], sections[N_sections - 1][i][1], sections[N_sections - 1][i][2]);
                b = new Vector3f(sections[N_sections - 2][i][0], sections[N_sections - 2][i][1], sections[N_sections - 2][i][2]);
                c = new Vector3f(sections[N_sections - 1][i - 1][0], sections[N_sections - 1][i - 1][1], sections[N_sections - 1][i - 1][2]);
                result = new Vector3f();

                c = a - c; // Получение векторов
                b = b - a;

                a = a.Cross(b, c); // Векторное умножение
                result = a;


                a = new Vector3f(sections[N_sections - 1][0][0], sections[N_sections - 1][0][1], sections[N_sections - 1][0][2]);
                b = new Vector3f(sections[N_sections - 1][2][0], sections[N_sections - 1][2][1], sections[N_sections - 1][2][2]);
                c = new Vector3f(sections[N_sections - 1][1][0], sections[N_sections - 1][1][1], sections[N_sections - 1][1][2]);
                c = a - c; // Получение векторов
                b = b - a;

                a = a.Cross(b, c); // Векторное умножение
                result += a;

                a = new Vector3f(sections[N_sections - 1][i][0], sections[N_sections - 1][i][1], sections[N_sections - 1][i][2]);
                b = new Vector3f(sections[N_sections - 1][i + 1][0], sections[N_sections - 1][i + 1][1], sections[N_sections - 1][i + 1][2]);
                c = new Vector3f(sections[N_sections - 2][i][0], sections[N_sections - 2][i][1], sections[N_sections - 2][i][2]);
                c = a - c; // Получение векторов
                b = b - a;

                a = a.Cross(b, c); // Векторное умножение
                result += a;

                result = result.NormalisationOfVector(result); // Нормируем вектор
                smothNormals[N_sections - 1].Add(new float[3] { result.x, result.y, result.z });
            }
            //последняя
            a = new Vector3f(sections[N_sections - 1][0][0], sections[N_sections - 1][0][1], sections[N_sections - 1][0][2]);
            b = new Vector3f(sections[N_sections - 1][2][0], sections[N_sections - 1][2][1], sections[N_sections - 1][2][2]);
            c = new Vector3f(sections[N_sections - 1][1][0], sections[N_sections - 1][1][1], sections[N_sections - 1][1][2]);
            result = new Vector3f();

            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            result = a;

            a = new Vector3f(sections[N_sections - 1][N_points - 2][0], sections[N_sections - 1][N_points - 2][1], sections[N_sections - 1][N_points - 2][2]);
            b = new Vector3f(sections[N_sections - 1][N_points - 1][0], sections[N_sections - 1][N_points - 1][1], sections[N_sections - 1][N_points - 1][2]);
            c = new Vector3f(sections[N_sections - 2][N_points - 1][0], sections[N_sections - 2][N_points - 1][1], sections[N_sections - 2][N_points - 1][2]);
            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            result += a;

            a = new Vector3f(sections[N_sections - 1][N_points - 1][0], sections[N_sections - 1][N_points - 1][1], sections[N_sections - 1][N_points - 1][2]);
            b = new Vector3f(sections[N_sections - 1][0][0], sections[N_sections - 1][0][1], sections[N_sections - 1][0][2]);
            c = new Vector3f(sections[N_sections - 2][N_points - 1][0], sections[N_sections - 2][N_points - 1][1], sections[N_sections - 2][N_points - 1][2]);
            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            result += a;


            result = result.NormalisationOfVector(result); // Нормируем вектор

            smothNormals[N_sections - 1].Add(new float[3] { result.x, result.y, result.z });
        }

        void reModelSection() // Расчет координат 6-угольника
        {
            N_points = 6;
            sections.Clear();
            float loc_scale = 1;
            for (int sec = 0; sec < N_sections; sec++)
            {
                addSection(loc_scale);
                loc_scale *= scale;
                for (int i = 0; i < N_points; i++)
                {
                    sections[sec][i][0] += coordinates[sec][0];
                    sections[sec][i][1] += coordinates[sec][1];
                    sections[sec][i][2] += coordinates[sec][2];
                }
            }
            CalcTexCoor();
        }

        void addSection(float loc_scale)  // Добавление сечения 
        {
            float mainAngle = 360.0f / N_points;
            sections.Add(new List<float[]> { });
            int sec_num = sections.Count - 1;
            for (int i = 0; i < N_points; i++)
            {
                sections[sec_num].Add(
                    new float[3]
                    {
                        loc_scale*(float)Math.Cos((i*mainAngle+sec_num*rotate_angle)*Math.PI/180.0),
                        loc_scale*(float)Math.Sin((i*mainAngle+sec_num*rotate_angle) *Math.PI/180.0),
                        0
                    }
                );
            }
            last_size = loc_scale;
        }
        
        public void draw(OpenGL gl) // Отрисовка
        {
            gl.Color(0.56f, 0.33f, 0.74f); // Цвет фигуры

            gl.Disable(OpenGL.GL_TEXTURE_2D);
            if(tex) gl.Enable(OpenGL.GL_TEXTURE_2D);

            Vector3f a, b, c;
            a = new Vector3f(sections[0][0][0], sections[0][0][1], sections[0][0][2]);
            b = new Vector3f(sections[0][1][0], sections[0][1][1], sections[0][1][2]);
            c = new Vector3f(sections[0][2][0], sections[0][2][1], sections[0][2][2]);

            c = a - c; // Получение векторов
            b = b - a;

            a = a.Cross(b, c); // Векторное умножение
            a = a.NormalisationOfVector(a); // Нормируем вектор

            gl.Begin(OpenGL.GL_POLYGON); // Аналог LineLoop
            for (int i = 0; i < N_points; i++)
            {
                gl.Normal(a.x, a.y, a.z); // Приписываем первому полигону нормаль
                if (tex) gl.TexCoord((sections[0][i][0] + 1) / 2.0f, (sections[0][i][1] + 1) / 2.0f);
                gl.Vertex(sections[0][i]); // Рисуем точки
            }
            gl.End();

            if (norm)
            {
                gl.Color(0, 0, 0);
                gl.Begin(OpenGL.GL_LINES);
                for (int i = 0; i < N_points; i++)
                {
                    if(smooth) gl.Vertex(sections[0][i][0] + smothNormals[0][i][0], sections[0][i][1] + smothNormals[0][i][1], sections[0][i][2] + smothNormals[0][i][2]);
                    else gl.Vertex(sections[0][i][0] + a.x, sections[0][i][1] + a.y, sections[0][i][2] + a.z);
                    gl.Vertex(sections[0][i]);
                }
                gl.End();
                gl.Color(0.56f, 0.33f, 0.74f);
            }
            
            gl.Begin(OpenGL.GL_POLYGON);
            for (int i = 0; i < N_points; i++)
            {
                gl.Normal(-a.x, -a.y, -a.z);  // Приписываем последнему полигону нормаль
                if (tex) gl.TexCoord((sections[coordinates.Count - 1][i][0] + 1) / 2.0f, (sections[coordinates.Count - 1][i][1] + 1) / 2.0f);
                gl.Vertex(sections[coordinates.Count - 1][i]);
            }
            gl.End();

            if (norm)
            {
                gl.Color(0, 0, 0);
                gl.Begin(OpenGL.GL_LINES);
                for (int i = 0; i < N_points; i++)
                {
                    if(smooth) gl.Vertex(sections[coordinates.Count - 1][i][0] + smothNormals[N_sections - 1][i][0], sections[coordinates.Count - 1][i][1] + smothNormals[N_sections - 1][i][1], sections[coordinates.Count - 1][i][2] + smothNormals[N_sections - 1][i][2]);
                    else gl.Vertex(sections[coordinates.Count - 1][i][0] - a.x, sections[coordinates.Count - 1][i][1] - a.y, sections[coordinates.Count - 1][i][2] - a.z);
                    gl.Vertex(sections[coordinates.Count - 1][i]);
                }
                gl.End();
            }
            if (tex)
            {
                for (int sec = 0; sec < N_sections - 1; sec++)
                {
                    gl.Begin(OpenGL.GL_TRIANGLES);
                    gl.Color(0.56f, 0.33f, 0.74f);
                    for (int i = 0; i < N_points - 1; i++)
                    {
                        a = new Vector3f(sections[sec][i][0], sections[sec][i][1], sections[sec][i][2]);
                        b = new Vector3f(sections[sec + 1][i][0], sections[sec + 1][i][1], sections[sec + 1][i][2]);
                        c = new Vector3f(sections[sec][i + 1][0], sections[sec][i + 1][1], sections[sec][i + 1][2]);

                        c = a - c;
                        b = b - a;

                        a = a.Cross(b, c);
                        a = a.NormalisationOfVector(a);

                        gl.Normal(a.x, a.y, a.z);
                        gl.TexCoord(textureX[i], textureY[sec]);//ТЕКСТУРЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫЫ
                        gl.Vertex(sections[sec][i]);

                        gl.TexCoord(textureX[i], textureY[sec + 1]);
                        gl.Vertex(sections[sec + 1][i]);

                        gl.TexCoord(textureX[i + 1], textureY[sec]);
                        gl.Vertex(sections[sec][i + 1]);


                        gl.TexCoord(textureX[i], textureY[sec + 1]);
                        gl.Vertex(sections[sec + 1][i]);

                        gl.TexCoord(textureX[i + 1], textureY[sec]);
                        gl.Vertex(sections[sec][i + 1]);

                        gl.TexCoord(textureX[i + 1], textureY[sec + 1]);
                        gl.Vertex(sections[sec + 1][i + 1]);


                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_TRIANGLES);

                    a = new Vector3f(sections[sec][N_points - 1][0], sections[sec][N_points - 1][1], sections[sec][N_points - 1][2]);
                    b = new Vector3f(sections[sec + 1][N_points - 1][0], sections[sec + 1][N_points - 1][1], sections[sec + 1][N_points - 1][2]);
                    c = new Vector3f(sections[sec][0][0], sections[sec][0][1], sections[sec][0][2]);

                    c = a - c;
                    b = b - a;

                    a = a.Cross(b, c);
                    a = a.NormalisationOfVector(a);
                    gl.Normal(a.x, a.y, a.z);
                    gl.TexCoord(textureX[N_points - 1], textureY[sec]);
                    gl.Vertex(sections[sec][N_points - 1]);
                    gl.TexCoord(textureX[N_points - 1], textureY[sec + 1]);
                    gl.Vertex(sections[sec + 1][N_points - 1]);
                    gl.TexCoord(textureX[0], textureY[sec]);
                    gl.Vertex(sections[sec][0]);

                    gl.TexCoord(textureX[N_points - 1], textureY[sec + 1]);
                    gl.Vertex(sections[sec + 1][N_points - 1]);
                    gl.TexCoord(textureX[0], textureY[sec]);
                    gl.Vertex(sections[sec][0]);
                    gl.TexCoord(textureX[0], textureY[sec + 1]);
                    gl.Vertex(sections[sec + 1][0]);
                    gl.End();

                }
            }
            else
            {
                for (int sec = 0; sec < N_sections - 1; sec++)
                {
                    gl.Begin(OpenGL.GL_TRIANGLES);
                    gl.Color(0.56f, 0.33f, 0.74f);
                    for (int i = 0; i < N_points - 1; i++)
                    {
                        a = new Vector3f(sections[sec][i][0], sections[sec][i][1], sections[sec][i][2]);
                        b = new Vector3f(sections[sec + 1][i][0], sections[sec + 1][i][1], sections[sec + 1][i][2]);
                        c = new Vector3f(sections[sec][i + 1][0], sections[sec][i + 1][1], sections[sec][i + 1][2]);

                        c = a - c;
                        b = b - a;

                        a = a.Cross(b, c);
                        a = a.NormalisationOfVector(a);

                        gl.Normal(a.x, a.y, a.z);
                        gl.Vertex(sections[sec][i]);
                        gl.Vertex(sections[sec + 1][i]);
                        gl.Vertex(sections[sec][i + 1]);


                        gl.Vertex(sections[sec + 1][i]);
                        gl.Vertex(sections[sec][i + 1]);
                        gl.Vertex(sections[sec + 1][i + 1]);


                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_TRIANGLES);

                    a = new Vector3f(sections[sec][N_points - 1][0], sections[sec][N_points - 1][1], sections[sec][N_points - 1][2]);
                    b = new Vector3f(sections[sec + 1][N_points - 1][0], sections[sec + 1][N_points - 1][1], sections[sec + 1][N_points - 1][2]);
                    c = new Vector3f(sections[sec][0][0], sections[sec][0][1], sections[sec][0][2]);

                    c = a - c;
                    b = b - a;

                    a = a.Cross(b, c);
                    a = a.NormalisationOfVector(a);
                    gl.Normal(a.x, a.y, a.z);
                    gl.Vertex(sections[sec][N_points - 1]);
                    gl.Vertex(sections[sec + 1][N_points - 1]);
                    gl.Vertex(sections[sec][0]);


                    gl.Vertex(sections[sec + 1][N_points - 1]);
                    gl.Vertex(sections[sec][0]);
                    gl.Vertex(sections[sec + 1][0]);
                    gl.End();

                    if (norm)
                    {
                        if (!smooth)
                        {
                            gl.Color(0, 0, 0);
                            gl.Begin(OpenGL.GL_LINES);
                            for (int i = 0; i < N_points - 1; i++)
                            {
                                a = new Vector3f(sections[sec][i][0], sections[sec][i][1], sections[sec][i][2]);
                                b = new Vector3f(sections[sec + 1][i][0], sections[sec + 1][i][1], sections[sec + 1][i][2]);
                                c = new Vector3f(sections[sec][i + 1][0], sections[sec][i + 1][1], sections[sec][i + 1][2]);

                                c = a - c;
                                b = b - a;

                                a = a.Cross(b, c);
                                a = a.NormalisationOfVector(a);

                                gl.Vertex(sections[sec][i][0] + a.x, sections[sec][i][1] + a.y, sections[sec][i][2] + a.z);
                                gl.Vertex(sections[sec][i]);
                                gl.Vertex(sections[sec + 1][i][0] + a.x, sections[sec + 1][i][1] + a.y, sections[sec + 1][i][2] + a.z);
                                gl.Vertex(sections[sec + 1][i]);
                                gl.Vertex(sections[sec][i + 1][0] + a.x, sections[sec][i + 1][1] + a.y, sections[sec][i + 1][2] + a.z);
                                gl.Vertex(sections[sec][i + 1]);


                                gl.Vertex(sections[sec + 1][i][0] + a.x, sections[sec + 1][i][1] + a.y, sections[sec + 1][i][2] + a.z);
                                gl.Vertex(sections[sec + 1][i]);
                                gl.Vertex(sections[sec][i + 1][0] + a.x, sections[sec][i + 1][1] + a.y, sections[sec][i + 1][2] + a.z);
                                gl.Vertex(sections[sec][i + 1]);
                                gl.Vertex(sections[sec + 1][i + 1][0] + a.x, sections[sec + 1][i + 1][1] + a.y, sections[sec + 1][i + 1][2] + a.z);
                                gl.Vertex(sections[sec + 1][i + 1]);


                            }

                            gl.End();
                            gl.Begin(OpenGL.GL_LINES);

                            a = new Vector3f(sections[sec][N_points - 1][0], sections[sec][N_points - 1][1], sections[sec][N_points - 1][2]);
                            b = new Vector3f(sections[sec + 1][N_points - 1][0], sections[sec + 1][N_points - 1][1], sections[sec + 1][N_points - 1][2]);
                            c = new Vector3f(sections[sec][0][0], sections[sec][0][1], sections[sec][0][2]);

                            c = a - c;
                            b = b - a;

                            a = a.Cross(b, c);
                            a = a.NormalisationOfVector(a);
                            gl.Vertex(sections[sec][N_points - 1][0] + a.x, sections[sec][N_points - 1][1] + a.y, sections[sec][N_points - 1][2] + a.z);
                            gl.Vertex(sections[sec][N_points - 1]);
                            gl.Vertex(sections[sec + 1][N_points - 1][0] + a.x, sections[sec + 1][N_points - 1][1] + a.y, sections[sec + 1][N_points - 1][2] + a.z);
                            gl.Vertex(sections[sec + 1][N_points - 1]);
                            gl.Vertex(sections[sec][0][0] + a.x, sections[sec][0][1] + a.y, sections[sec][0][2] + a.z);
                            gl.Vertex(sections[sec][0]);

                            gl.Vertex(sections[sec + 1][N_points - 1][0] + a.x, sections[sec + 1][N_points - 1][1] + a.y, sections[sec + 1][N_points - 1][2] + a.z);
                            gl.Vertex(sections[sec + 1][N_points - 1]);
                            gl.Vertex(sections[sec][0][0] + a.x, sections[sec][0][1] + a.y, sections[sec][0][2] + a.z);
                            gl.Vertex(sections[sec][0]);
                            gl.Vertex(sections[sec + 1][0][0] + a.x, sections[sec + 1][0][1] + a.y, sections[sec + 1][0][2] + a.z);
                            gl.Vertex(sections[sec + 1][0]);
                            gl.End();
                        }
                    }
                }
            }

            if(smooth)
            {
                if (norm)
                {
                    for (int sec = 1; sec < N_sections - 1; sec++)
                    {

                        gl.Color(0, 0, 0);
                        gl.Begin(OpenGL.GL_LINES);
                        for (int i = 0; i < N_points; i++)
                        {
                            gl.Vertex(sections[sec][i]);
                            gl.Vertex(sections[sec][i][0] + smothNormals[sec][i][0], sections[sec][i][1] + smothNormals[sec][i][1], sections[sec][i][2] + smothNormals[sec][i][2]);
                        }
                        gl.End();
                    }
                }
            }

            if (neenGrid) // Отрисовка сетки
            {
                gl.Scale(1.0041f, 1.0041f, 1);
                gl.Color(0, 0, 0);
                int sec;
                for (sec = 0; sec < N_sections - 1; sec++)
                {
                    gl.Begin(OpenGL.GL_LINE_STRIP);
                    for (int i = 0; i < N_points; i++)
                        gl.Vertex(sections[sec][i]);
                    gl.Vertex(sections[sec][0]);
                    for (int i = 0; i < N_points - 1; i++)
                    {
                        gl.Vertex(sections[sec + 1][i]);
                        gl.Vertex(sections[sec][i + 1]);
                    }
                    gl.Vertex(sections[sec + 1][N_points - 1]);
                    gl.Vertex(sections[sec][0]);
                    gl.End();
                }
                sec = N_sections - 1;
                gl.Begin(OpenGL.GL_LINE_LOOP);
                for (int i = 0; i < N_points; i++)
                    gl.Vertex(sections[sec][i]);
                gl.End();
            }

        }

        //Геттеры и сеттеры

        public bool setRotateAngle(float val)
        {
            rotate_angle = val;
            reModelSection();
            return true;
        }

        public float getHalfLength() // Получение половины длины фигуры для смещения камеры в центр
        {
            return half_length;
        }
        
        public bool gridSwitch() // Построение сетки
        {
            // Меняем флаг на обратный
            neenGrid = !neenGrid;
            return neenGrid;
        }

        public void setNorm() // Построение нормалей 
        {
            norm = !norm;
        }

        public void setCoord(float x, float y, float z) // Построение нового сечения
        {
            // Добавление координат нового сечения
            coordinates.Add(new List<float> { x, y, z });
            // Увеличения количества секций
            N_sections++;
            // Меняем позицию половины длины фигуры (для камеры)
            half_length = z - sections[0][0][2];
            // Пересчитываем координаты сечений
            reModelSection();
        }

        public void Smooth()
        {
            smooth = !smooth;
        }

        public void Texture(OpenGL gl)
        {
            tex = !tex;
            draw(gl);
        }

        public int getCountOfSections()
        {
            return sections.Count;
        }
    }
}
