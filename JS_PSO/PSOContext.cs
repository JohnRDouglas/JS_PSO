using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JS_PSO {
    public class PSOContext {
        Random rnd;
        PSOParticle[] myParticles;
        int myPopSize;
        int myDimension;
        int myNeighborhoodSize;
        double[] myUpperBounds;
        double[] myLowerBounds;

        PSOParticle myGlobalBest;
        double myBestFitness = double.PositiveInfinity;

        public PSOContext(int PopSize, int Dimensions, double MaxVel, double[] UpperBounds, double[] LowerBounds)
        {
            rnd = new Random();
            myPopSize = PopSize;
            myDimension = Dimensions;
            myUpperBounds = UpperBounds;
            myLowerBounds = LowerBounds;
            myParticles = new PSOParticle[myPopSize];
            for (int i = 0; i < myPopSize; i++) {
                myParticles[i] = new PSOParticle(Dimensions, ref myUpperBounds, ref myLowerBounds, MaxVel, ref rnd);
            }

            myGlobalBest = myParticles[rnd.Next(myPopSize)];

        }

        public void iterate(double[] scores)
        {
            if (scores.Length != myParticles.Length)
                throw new Exception("Array length does not match population size.");

            for (int i = 0; i < myParticles.Length; i++) {
                if (scores[i] < myBestFitness) {
                    myBestFitness = scores[i];
                    myGlobalBest = myParticles[i];
                    //myGlobalBest.Fitness = myBestFitness;
                }
            }

            for (int i = 0; i < myParticles.Length; i++) {
                myParticles[i].GlobalBest = myGlobalBest;
                myParticles[i].iterate(scores[i]);
            }
        }

        public void ResetParticles(bool ResetPosits = false)
        {
            myBestFitness = double.PositiveInfinity;

            for (int i = 0; i < myParticles.Length; i++) {
                myParticles[i].ResetParticle();
            }
        }

        public PSOParticle[] Particles
        {
            get
            {
                return myParticles;
            }
        }

    }
}
