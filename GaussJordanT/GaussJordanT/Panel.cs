using System;
using System.Collections.Generic;
using System.Text;

namespace GaussJordanT
{
    class Panel
    {

        public void Menu()
        {
            Console.Title = "Gauss Jordan";
            int numL, numC;

            //interacao com usuario
            
            do
            {
                Console.Clear();
                Console.WriteLine("O numero digitado deve ser maior que 0");
                Console.Write("Digite o número de equações do Sistema Linear: ");
                numL = Convert.ToInt32(Console.ReadLine());
            } while (numL<=0);

            do
            {
                Console.Clear();
                Console.WriteLine("O numero digitado deve ser maior que 0");
                Console.Write("Digite o número de variáveis do Sistema Linear: ");
                numC = Convert.ToInt32(Console.ReadLine());
            } while (numC <= 0);
              
            decimal[,] s = new decimal[numL, numC + 1]; //instanciando uma matriz para comportar o sistema linear

            Console.Clear();
            //alimentando o sistema linear com valores via usuario
            for (int i = 0; i < s.GetLength(0); i++)
            {
                for (int j = 0; j < s.GetLength(1); j++)
                {
                    if (j == numC)
                    {
                        Console.Write("Digite o valor do termo independente da linha {0}: ", i + 1);
                        s[i, j] = Convert.ToDecimal(Console.ReadLine());
                        Console.Clear();
                        ImprimirSistema(s);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write("Digite um valor para o índice {0},{1}: ", i + 1, j + 1);
                        s[i, j] = Convert.ToDecimal(Console.ReadLine());
                    }

                }
            }

            Calculos c = new Calculos(s);
            ImprimirSistema(s);
            c.GaussJordan();
            
        }

        //imprimindo o sistema linear
        public void ImprimirSistema(decimal[,] s)
        {
            //System.Threading.Thread.Sleep(2200);
            //Console.Clear(); 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nSituação atual do Sistema Lienar\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            string a;
            for (int i = 0; i < s.GetLength(0); i++)
            {
                a = "| ";
                for (int j = 0; j < s.GetLength(1); j++)
                {
                    if (j == s.GetLength(1) - 1)
                    {
                        Console.Write("¦ " + s[i, j].ToString("F") + " ");
                    }
                    else
                    {
                        Console.Write(a + s[i, j].ToString("F") + " ");
                    }
                }
                Console.Write("|\n");
            }
        }
         

    }
}
