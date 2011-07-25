using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JS_PSO {
    public partial class SwarmView : UserControl {
        PSOContext myContext;
        public Point targetPoint;

        public SwarmView()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        public SwarmView(ref PSOContext Context)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            myContext = Context;
            targetPoint = new Point(Width / 2, Height / 2);

        }

        public void init(ref PSOContext Context)
        {
            myContext = Context;
            targetPoint = new Point(Width / 2, Height / 2);
        }

        private void SwarmView_Paint(object sender, PaintEventArgs e)
        {

            if (myContext == null || myContext.Particles == null) {
                e.Graphics.DrawEllipse(new Pen(Color.Red, 1), new Rectangle(new Point(targetPoint.X - 2, targetPoint.Y - 2), new Size(4, 4)));
                return;
            }

            Point[] points = new Point[myContext.Particles.Length];
            Pen pBestPen = new Pen(Color.Green, 1);
            Pen pLocPen = new Pen(Color.Blue, 1);
            Pen targetPen = new Pen(Color.Red, 1);
            Size s = new Size(4, 4);

            for (int i = 0; i < myContext.Particles.Length; i++) {
                points[i] = new Point((int)myContext.Particles[i].Posits[0] - 2, (int)myContext.Particles[i].Posits[1] - 2);
                e.Graphics.DrawEllipse(pLocPen, new Rectangle(points[i], s));
            }

            for (int i = 0; i < myContext.Particles.Length; i++) {
                points[i] = new Point((int)myContext.Particles[i].BestPosits[0] - 2, (int)myContext.Particles[i].BestPosits[1] - 2);
                e.Graphics.DrawEllipse(pBestPen, new Rectangle(points[i], s));
            }

            e.Graphics.DrawEllipse(targetPen, new Rectangle(new Point(targetPoint.X - 2, targetPoint.Y - 2), s));

            e.Graphics.DrawString("Best Fitness: " + myContext.GlobalBest.BestFitness.ToString("0.000"), new Font("Airal", 10), Brushes.Green, 10, 10);

        }

        private void SwarmView_Click(object sender, EventArgs e)
        {
            targetPoint = ((MouseEventArgs)e).Location;
            myContext.ResetParticles();
        }
    }
}
