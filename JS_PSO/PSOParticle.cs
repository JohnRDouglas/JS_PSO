using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JS_PSO {
    
    public class PSOParticle {
        PSOParticle[] myNeighborhood;
        double[] myPosits;
        double[] myVelocities;
        double[] myBestPosits;
        double[] myUpperBounds;
        double[] myLowerBounds;
        double myFitness = double.PositiveInfinity;
        double myBestFitness;
        double myVelocityFactor;
        int myDimension;
        Random rnd;
        PSOParticle myGlobalBest;

        // Create a particle just to hold the posits, for GlobalBest
        public PSOParticle(double[] Posits)
        {
            myPosits = new double[Posits.Length];
            for (int i = 0; i < Posits.Length; i++) {
                myPosits[i] = Posits[i];
            }
        }

        public PSOParticle(int Dimension, ref double[] upperBounds, ref double[] lowerBounds, double MaxVel, ref Random Rnd)
        {
            if (upperBounds.Length != Dimension || lowerBounds.Length != Dimension)
                throw new Exception("Error: Dimension of bounds do not match dimension of particle");
            myUpperBounds = upperBounds;
            myLowerBounds = lowerBounds;

            myDimension = Dimension;
            myPosits = new double[myDimension];
            myVelocities = new double[myDimension];
            myBestPosits = new double[myDimension];
            myVelocityFactor = MaxVel;

            rnd = Rnd;
            
            for (int i = 0; i < myDimension; i++) {
                myPosits[i] = lowerBounds[i] + rnd.NextDouble() * (myUpperBounds[i] - myLowerBounds[i]);
                myVelocities[i] = (myUpperBounds[i] - myLowerBounds[i]) * (1 - 2 * rnd.NextDouble());
                myBestPosits[i] = myPosits[i];
            }

            myBestFitness = myFitness;
        }

        public void iterate(double fitness)
        {
            this.Fitness = fitness;

            for (int i = 0; i < myDimension; i++) {
                myVelocities[i] = myVelocities[i] * 0.5 * (1 + rnd.NextDouble()) +
                                    1.4945 * rnd.NextDouble() * (myGlobalBest.BestPosits[i] - myPosits[i]) +
                                    1.4945 * rnd.NextDouble() * (myBestPosits[i] - myPosits[i]);

                if (Math.Abs(myVelocities[i]) > myVelocityFactor * (myUpperBounds[i] - myLowerBounds[i])) {
                    myVelocities[i] = (1 - 2*rnd.NextDouble()) * myVelocityFactor * (myUpperBounds[i] - myLowerBounds[i]);

                }

                myPosits[i] += myVelocities[i];

                if (myPosits[i] > myUpperBounds[i]) {
                    myPosits[i] = myUpperBounds[i] - (myPosits[i] - myUpperBounds[i]);
                    myVelocities[i] *= -1;
                }

                if (myPosits[i] < myLowerBounds[i]) {
                    myPosits[i] = myLowerBounds[i] + (myLowerBounds[i] - myPosits[i]);
                    myVelocities[i] *= -1;
                }
                
            }

        }

        public void ResetParticle(bool ResetPosits = false)
        {
            myFitness = double.PositiveInfinity;
            myBestFitness = double.PositiveInfinity;

            for (int i = 0; i < myDimension; i++) {
                myVelocities[i] = (1 - 2 * rnd.NextDouble()) * myVelocityFactor * (myUpperBounds[i] - myLowerBounds[i]);
                if(ResetPosits)
                    myPosits[i] = myLowerBounds[i] + rnd.NextDouble() * (myUpperBounds[i] - myLowerBounds[i]);
                myBestPosits[i] = myPosits[i];
            }
        }

        public double Fitness
        {
            get { return myFitness; }
            set
            {
                if (value < myBestFitness) {
                    myBestFitness = value;
                    for (int i = 0; i < myDimension; i++) {
                        myBestPosits[i] = myPosits[i];
                    }
                }
            }
        }

        public double[] Posits
        {
            get {
                return myPosits;
            }
        }

        public double[] BestPosits
        {
            get
            {
                return myBestPosits;
            }
        }

        public PSOParticle GlobalBest
        {
            set
            {
                myGlobalBest = value;
            }
        }

        public PSOParticle Copy
        {
            get
            {
                return new PSOParticle(myPosits);
            }
        }
    }
}
