using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuron_Project
{
    class Neuron
    {
        //-1 ile 1 arasında rastgele ağırlık oluşturan metod
        double[] randomW() 
        {
            double[] weights = new double[2];
            Random r = new Random();
            double min = -1;
            double max = 1;
            for (int i = 0; i<2; i++)
            {
                weights[i] = 0;
                weights[i] += r.NextDouble() * (max - min) + min;
                Console.WriteLine(weights[i]);
            }

            return weights;
        }
        //Nöronu eğitmek için kullanılacak eğitim verilerinin belirlendiği metod
        double[,] trainingInputData()
        {
            double[,] inputs = new double[8, 2];
            
            inputs[0, 0] = 6;
            inputs[0, 1] = 5;
            inputs[1, 0] = 2;
            inputs[1, 1] = 4;
            inputs[2, 0] = -3;
            inputs[2, 1] = -5;
            inputs[3, 0] = -1;
            inputs[3, 1] = -1;
            inputs[4, 0] = 1;
            inputs[4, 1] = 1;
            inputs[5,0] = -2;
            inputs[5, 1] = 7;
            inputs[6, 0] = -4;
            inputs[6, 1] = -2;
            inputs[7, 0] = -6;
            inputs[7, 1] = 3;

            return inputs;
        }
        //Nörona girecek verilerin beklenen çıktılarının belirlendiği metod
        int[] targetData(double[,] inputs)
        {
            int[] target = new int[inputs.Length/2];

            for(int i = 0; i<inputs.Length/2; i++)
            {
                if ((inputs[i,0] + inputs[i,1]) < 0)
                {
                    target[i] = -1;
                }
                else
                {
                    target[i] = 1;
                }
            }


            return target;
        }
        //Nörona giren verilerin çıktılarının belirlendiği metod
        double[] outputData(double[] weights, double[,] inputs)
        {
            int dataCount = inputs.Length/2;
            double[] output = new double[dataCount];
            int x;
            for (int i = 0; i<dataCount; i++)
            {
                double total = ((inputs[i, 0] / 10) * weights[0]) + ((inputs[i, 1] / 10) * weights[1]);
                if (total < 0.5)
                {
                    x = -1;
                }
                else
                {
                    x = 1;
                }
                output[i] = x;
            }

            return output;

        }
        //Nöronun eğitildiği ve ağırlıkların döndürüldüğü metod
        double[] education(double[] weights, double[,] inputs, int[] targets, double[] outputs)
        {
            double constant = 0.05;
            
           
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] != outputs[i])
                {
                weights[0] += constant * (targets[i] - outputs[i]) * inputs[i, 0];
                weights[1] += constant * (targets[i] - outputs[i]) * inputs[i, 1];
                }                   
                else
                {
                    continue;
                }
                    
            }


            return weights;
        }

        //Test verilerinin doğruluğunun belirlendiği metod
        double testData(int[] targets, double[] outputs)
        {
            double accuracy = 0;
            for (int i = 0; i<outputs.Length; i++)
            {
                if (outputs[i] == targets[i])
                {
                    accuracy+= 1;
                }
            }
            accuracy = accuracy / outputs.Length;
            return accuracy;
        }
         

        static void Main(string[] args)
        {
            //Eğitim verileri için yazılan eşitlikler
            Neuron n = new Neuron();
            double[] weights = n.randomW();
            double[,] trainInputs = n.trainingInputData();
            int[] targets = n.targetData(trainInputs);
            double[] outputs = n.outputData(weights, trainInputs);

            //Test verileri için yazılan eşitlikler
            double[] myW = weights;
            double[,] inputs = new double[,] { { 4, 5 }, { -1, 9 }, { -8, 6 }, { 6, 9 }, { -4, 2 } };
            int[] myT = n.targetData(inputs);
            double[] myO = n.outputData(myW, inputs);
            
            int counter = 0;
            while(counter != 100)
            {
                weights = n.education(weights, trainInputs, targets, outputs);
                outputs = n.outputData(weights, trainInputs);
                myO = n.outputData(weights, inputs);
                counter++;
                if(counter == 10 || counter == 100)
                {
                    double accuracy = 0;
                    for (int i = 0; i<outputs.Length; i++)
                    {
                        if(outputs[i] == targets[i])
                        {
                            accuracy += 1;
                        }
                    }
                    if (counter == 10)
                    {
                        Console.WriteLine("10. Epoch:");
                    }
                    else
                    {
                        Console.WriteLine("100. Epoch:");
                    }
                    Console.WriteLine("Training Data Accuracy: %" + accuracy / outputs.Length * 100);
                    Console.WriteLine("Test Data Accuracy: %" + n.testData(myT, myO) * 100);
                }
            }


            
                
            

            Console.ReadKey();
        }


    }



    
}
