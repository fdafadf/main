
using OpenTK;
using System;
using System.Linq;

namespace Basics.Physics.Test.UI
{
    class CodeProject
    {
        public static RenderableModel Test(int p, int c, int n)
        {
            //InitializeComponent();
            //Text += " - Press S F Left Right";
            float X = 0.525731112119133696f;
            float Z = 0.850650808352039932f;


            /* vertex data array */
            float[][] vdata = new float[12][] {
              new float[3]{-X, 0.0f, Z},  new float[3]{X, 0.0f, Z}, new float[3] {-X, 0.0f, -Z}, new float[3] {X, 0.0f, -Z},
              new float[3]{0.0f, Z, X}, new float[3] {0.0f, Z, -X}, new float[3] {0.0f, -Z, X}, new float[3] {0.0f, -Z, -X},
              new float[3] {Z, X, 0.0f}, new float[3] {-Z, X, 0.0f}, new float[3] {Z, -X, 0.0f}, new float[3] {-Z, -X, 0.0f}};


            /* triangle indices */
            uint[][] tindices = new uint[20][]{
              new uint[3] {1,4,0}, new uint[3] {4,9,0}, new uint[3] {4,5,9}, new uint[3] {8,5,4}, new uint[3] {1,8,4},
              new uint[3] {1,10,8}, new uint[3] {10,3,8}, new uint[3] {8,3,5}, new uint[3] {3,2,5}, new uint[3] {3,7,2},
              new uint[3] {3,10,7}, new uint[3] {10,6,7}, new uint[3]  {6,11,7},  new uint[3]{6,0,11}, new uint[3] {6,1,0},
              new uint[3] {10,1,6}, new uint[3] {11,0,9}, new uint[3] {2,11,9}, new uint[3] {5,2,9}, new uint[3] {11,2,7}};

            //ClientSize = new Size(640, 480);
            //init();
            //reshape(640, 480);

            Vector3 ArrayToVector(float[] a) { return new Vector3(a[0], a[1], a[2]); };
            Vector3 color = new Vector3(1, 1, 0);
            uint[] indices = tindices.SelectMany(v => v).ToArray();
            Vertex[] vertices = vdata.Select(a => new Vertex(ArrayToVector(a), color, ArrayToVector(a))).ToArray();

            return new RenderableModel(vertices, indices, p, c, n);
        }

        public static RenderableModel Test2(int p, int c, int n)
        {
            //InitializeComponent();
            //Text += " - Press S F Left Right";
            float X = 0.525731112119133696f;
            float Z = 0.850650808352039932f;


            /* vertex data array */
            float[][] vdata = new float[12][] {
              new float[3]{-X, 0.0f, Z},  new float[3]{X, 0.0f, Z}, new float[3] {-X, 0.0f, -Z}, new float[3] {X, 0.0f, -Z},
              new float[3]{0.0f, Z, X}, new float[3] {0.0f, Z, -X}, new float[3] {0.0f, -Z, X}, new float[3] {0.0f, -Z, -X},
              new float[3] {Z, X, 0.0f}, new float[3] {-Z, X, 0.0f}, new float[3] {Z, -X, 0.0f}, new float[3] {-Z, -X, 0.0f}};


            /* triangle indices */
            uint[][] tindices = new uint[20][]{
              new uint[3] {1,4,0}, new uint[3] {4,9,0}, new uint[3] {4,5,9}, new uint[3] {8,5,4}, new uint[3] {1,8,4},
              new uint[3] {1,10,8}, new uint[3] {10,3,8}, new uint[3] {8,3,5}, new uint[3] {3,2,5}, new uint[3] {3,7,2},
              new uint[3] {3,10,7}, new uint[3] {10,6,7}, new uint[3]  {6,11,7},  new uint[3]{6,0,11}, new uint[3] {6,1,0},
              new uint[3] {10,1,6}, new uint[3] {11,0,9}, new uint[3] {2,11,9}, new uint[3] {5,2,9}, new uint[3] {11,2,7}};

            //ClientSize = new Size(640, 480);
            //init();
            //reshape(640, 480);

            for (int i = 0; i < 20; i++)
            {

                //subdivide(ref vdata[tindices[i][0]],
                //          ref vdata[tindices[i][1]],
                //          ref vdata[tindices[i][2]],
                //             subdiv);

            }

            Vector3 ArrayToVector(float[] a) { return new Vector3(a[0], a[1], a[2]); };
            Vector3 color = new Vector3(1, 1, 0);
            uint[] indices = tindices.SelectMany(v => v).ToArray();
            Vertex[] vertices = vdata.Select(a => new Vertex(ArrayToVector(a), color, ArrayToVector(a))).ToArray();

            return new RenderableModel(vertices, indices, p, c, n);
        }


        /* vertex data array */
        float[][] vdata = new float[12][];

        /* triangle indices */
        int[][] tindices = new int[20][];


        float[] mat_specular = { 0.5f, 0.5f, 0.5f, 1.0f };
        float[] mat_diffuse = { 0.8f, 0.6f, 0.4f, 1.0f };
        float[] mat_ambient = { 0.8f, 0.6f, 0.4f, 1.0f };
        float mat_shininess = 20.0f;	// unused if specular is 0 

        float[] light_ambient = { 0.2f, 0.2f, 0.2f, 1.0f };
        float[] light_diffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
        float[] light_specular = { 0.4f, 0.4f, 0.4f, 1.0f };

        //   float[] light_position = { 100f, 50f, -1.0f, 0.0f }; // directional

        int flat = 1;			/* 0 = smooth shading, 1 = flat shading */
        int subdiv = 0;			/* number of subdivisions */

        //private void openGLControl1_Paint(object sender, PaintEventArgs e)
        //{
        //    Draw();
        //}

        void Draw()
        {

            //GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);
            //GL.glLoadIdentity();
            //GL.glTranslatef(0f, 0.0f, 0);
            ////  GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, light_position);
            //
            ///* drawIco(); */
            ///* drawSphere(); */
            //GL.glLightfv(GL.GL_LIGHT0, GL.GL_AMBIENT, light_ambient);
            //GL.glLightfv(GL.GL_LIGHT0, GL.GL_DIFFUSE, light_diffuse);
            //GL.glLightfv(GL.GL_LIGHT0, GL.GL_SPECULAR, light_specular);
            //
            //GL.glMaterialfv(GL.GL_FRONT, GL.GL_SPECULAR, mat_specular);
            //GL.glMaterialfv(GL.GL_FRONT, GL.GL_AMBIENT, mat_ambient);
            //GL.glMaterialfv(GL.GL_FRONT, GL.GL_DIFFUSE, mat_diffuse);
            //GL.glMaterialf(GL.GL_FRONT, GL.GL_SHININESS, mat_shininess);
            //
            //
            //GL.glEnable(GL.GL_LIGHTING);	/* enable lighting */
            //GL.glEnable(GL.GL_LIGHT0);		/* enable light 0 */

            for (int i = 0; i < 20; i++)
            {

                subdivide(ref vdata[tindices[i][0]],
                          ref vdata[tindices[i][1]],
                          ref vdata[tindices[i][2]],
                             subdiv);

            }


            //GL.glDisable(GL.GL_LIGHTING);	/* glDisable lighting */
            //GL.glDisable(GL.GL_LIGHT0);		/* glDisable light 0 */
            //
            //GL.glFlush();
            //
        }

        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    //openGLControl1.Invalidate();
        //}

        void init()
        {
            //GL.glShadeModel(GL.GL_SMOOTH);	/* enable smooth shading */
            //
            //GL.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            //GL.glClearDepth(1.0f);									        // Depth Buffer Setup
            //
            //GL.glEnable(GL.GL_DEPTH_TEST);							        // Enables Depth Testing
            //GL.glDepthFunc(GL.GL_LEQUAL);								    // The Type Of Depth Testing To Do

        }
        void reshape(int w, int h)
        {
            //float aspect = (float)w / (float)h;
            //GL.glViewport(0, 0, w, h);
            //
            //GL.glMatrixMode(GL.GL_PROJECTION);
            //GL.glLoadIdentity();
            //if (w <= h)
            //    GL.glOrtho(-1.25, 1.25, -1.25 * aspect, 1.25 * aspect, -2.0, 2.0);
            //else
            //    GL.glOrtho(-1.25 * aspect, 1.25 * aspect, -1.25, 1.25, -2.0, 2.0);
            //GL.glMatrixMode(GL.GL_MODELVIEW);
            //
            //GL.gluLookAt(0.5, 0.5, -1.5, /* eye */
            //        0.0, 0.0, 0.0,  /* at */
            //    0.0, 1.0, 0.0); /* up */
            //
            //GL.glMatrixMode(GL.GL_MODELVIEW);
            //GL.glLoadIdentity();


        }

        /* normalize a vector of non-zero length */
        void normalize(float[] v)
        {
            //float d = (float)Math.Sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]);
            ///* omit explict check for division by zero */
            //
            //v[0] /= d; v[1] /= d; v[2] /= d;
        }

        /* normalized cross product of non-parallel vectors */
        void normCrossProd(float[] u, float[] v, float[] n)
        {
            n[0] = u[1] * v[2] - u[2] * v[1];
            n[1] = u[2] * v[0] - u[0] * v[2];
            n[2] = u[0] * v[1] - u[1] * v[0];
            normalize(n);
        }

        void normFace(float[] v1, float[] v2, float[] v3)
        {
            float[] d1 = new float[3], d2 = new float[3], n = new float[3];
            int k;
            for (k = 0; k < 3; k++)
            {
                d1[k] = v1[k] - v2[k];
                d2[k] = v2[k] - v3[k];
            }
            normCrossProd(d1, d2, n);
            //GL.glNormal3fv(n);
        }

        /* draw triangle using face normals */
        void drawTriangleFlat(float[] v1, float[] v2, float[] v3)
        {

            //GL.glBegin(GL.GL_TRIANGLES);
            //normFace(v1, v2, v3);
            //GL.glVertex3fv(v1);
            //GL.glVertex3fv(v2);
            //GL.glVertex3fv(v3);
            //GL.glEnd();

        }

        /* draw triangle using sphere normals */
        void drawTriangleSmooth(float[] v1, float[] v2, float[] v3)
        {
            //GL.glBegin(GL.GL_TRIANGLES);
            //GL.glNormal3fv(v1);
            //GL.glVertex3fv(v1);
            //GL.glNormal3fv(v2);
            //GL.glVertex3fv(v2);
            //GL.glNormal3fv(v3);
            //GL.glVertex3fv(v3);
            //GL.glEnd();
        }


        /* recursively subdivide face depth times */
        /* and draw the resulting triangles */
        void subdivide(ref float[] v1, ref float[] v2, ref float[] v3, int depth)
        {
            float[] v12 = new float[3], v23 = new float[3], v31 = new float[3];


            if (depth == 0)
            {
                if (flat == 1)
                    drawTriangleFlat(v1, v2, v3);
                else
                    drawTriangleSmooth(v1, v2, v3);

            }


            /* calculate midpoints of each side */
            for (int i = 0; i < 3; i++)
            {
                v12[i] = (v1[i] + v2[i]) / 2.0f;
                v23[i] = (v2[i] + v3[i]) / 2.0f;
                v31[i] = (v3[i] + v1[i]) / 2.0f;
            }
            // extrude midpoints to lie on unit sphere 
            normalize(v12);
            normalize(v23);
            normalize(v31);

            // recursively subdivide new triangles 
            if (depth != 0)
            {
                subdivide(ref v1, ref v12, ref v31, depth - 1);
                subdivide(ref v2, ref v23, ref v12, depth - 1);
                subdivide(ref v3, ref v31, ref v23, depth - 1);
                subdivide(ref v12, ref v23, ref v31, depth - 1);
            }

        }
    }
}