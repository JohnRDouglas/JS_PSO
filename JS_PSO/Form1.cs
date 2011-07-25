using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace JS_PSO {
    public partial class Form1 : Form {
        PSOContext myPSOContext;
        Thread PSOThread;
        double[] myScores;

        public Form1() {
            InitializeComponent();

            myPSOContext = new PSOContext(30, 2, .25, new double[] { swarmView1.Width, swarmView1.Height }, new double[] { 0, 0 });
            swarmView1.init(ref myPSOContext);
            PSOThread = new Thread(new ThreadStart(RunPSO));
        }

        private void swarmView1_Load(object sender, EventArgs e)
        {

        }

        public delegate void DelegateRefreshSwarmView();

        private void RefreshSwarmView()
        {
            if (this.InvokeRequired) {
                DelegateRefreshSwarmView rsv = new DelegateRefreshSwarmView(RefreshSwarmView);
                this.Invoke(rsv, null);
            } else {
                swarmView1.Refresh();
            }
        }

        private void RunPSO()
        {
            myScores = new double[myPSOContext.Particles.Length];

            do {
                for (int i = 0; i < myScores.Length; i++) {
                    myScores[i] = FitnessFunction(myPSOContext.Particles[i].Posits);
                    myPSOContext.Particles[i].Fitness = myScores[i];
                    if (myScores[i] <= 0.001)
                        return;
                }

                // Exit criteria would go here

                myPSOContext.iterate(myScores);

                RefreshSwarmView();
                Thread.Sleep(30);
            } while (true);
        }

        double FitnessFunction(double[] Posits)
        {
            return Math.Sqrt(Math.Pow(Posits[0] - swarmView1.targetPoint.X, 2) + Math.Pow(Posits[1] - swarmView1.targetPoint.Y, 2));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PSOThread != null && PSOThread.IsAlive)
                PSOThread.Abort();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            myPSOContext.ResetParticles(true);
            if (PSOThread.IsAlive == false) {
                PSOThread = new Thread(new ThreadStart(RunPSO));
                PSOThread.Start();
            }
        }
    }
}
