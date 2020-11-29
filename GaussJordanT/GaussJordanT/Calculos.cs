using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GaussJordanT
{
    class Calculos
    {
        Panel p = new Panel();
        private decimal[,] sistema;
        public decimal[] vet1; //Vetor para as operações do gauss-jordan 
        public bool Verifica;
        public Calculos(decimal[,] s)
        {
            sistema = s; // passando o sistema digitado para o sistema que será modificado
            vet1 = new decimal[sistema.GetLength(1)]; // Passando a quantidade de colunas que tem no sistema para o vet1

        }
        public void Arrumar_Casa(int v) // Metodo para organizar o sistema antes mesmo de começar qualquer tipo de operação. 
        {
            decimal[] vetorAux = new decimal[sistema.GetLength(1)];
            for (int j = 0; j < sistema.GetLength(1); j++)
            {
                vet1[j] = sistema[v, j]; // Passando valor da linha i para o vet1j 
            }
            //soluções:
            int contador = 0;
            for (int i = 0; i < sistema.GetLength(1) - 1; i++) // for para contar quantos elementos da linha são iguais a zero
            {
                if (vet1[i] == 0) // se o elemento da linha i e da coluna (sem ser o resultante)
                {
                    contador++;
                    
                }
            }
            int cont_Verifica = 0;
            for (int i = sistema.GetLength(0)-1; i < sistema.GetLength(0); i++)
            {
                for (int j = 0; j < sistema.GetLength(1)-1; j++)
                {
                    if (sistema[i,j] == 0)
                    {
                        cont_Verifica++;
                    }
                }
            }
            if (cont_Verifica == sistema.GetLength(1)-1)
            {
                Verifica = true;
            }

                if (v < sistema.GetLength(0) - 1 && contador == sistema.GetLength(1) - 1 && Verifica != true) // se a linha passada não for a ultima e o contador for igual a quantidade de colunas
                {
                   
                    for (int i = 0; i < sistema.GetLength(1); i++)
                    {
                        vetorAux[i] = sistema[v, i]; // passando a linha zerada 
                        vet1[i] = sistema[sistema.GetLength(0) - 1, i]; //vet1 recebe o valor da ultima linha
                        sistema[sistema.GetLength(0) - 1, i] = vetorAux[i]; //ultima linha recebe a linha que está zerada
                        sistema[v, i] = vet1[i]; // passando a ultima linha para o lugar de onde a zerada estava 
                    }
                }  
            Check(v);   
        }

        public void Check(int v) // Organiza a diagonal. 
        {
            decimal[] vetorAux = new decimal[sistema.GetLength(1)];
            int auxI = v + 1;
            for (int j = 0; j < sistema.GetLength(1) - 1; j++)
            {
                while (v == j && auxI <= sistema.GetLength(0) - 1 && sistema[v, j] == 0) // Se aux for menor ou igual a ultima linha da matriz e a o lugar onde o pivo deveria estar 
                {                                                              // é igual a zero então 
                    if (sistema[auxI, v] != 0) // Se o elemento da coluna do pivo não 
                    {
                        for (int x = 0; x < sistema.GetLength(1); x++)
                        {
                            vetorAux[x] = sistema[v, x]; // Passa a linha para vetorAux
                            vet1[x] = sistema[auxI, x]; // vet1 recebe a linha de baixo
                            sistema[auxI, x] = vetorAux[x]; //  sistema troca linha de baixo pela de cima (linha de baixo recebendo a de cima)
                            sistema[v, x] = vet1[x];
                        }
                    }
                    auxI++;
                }
            }
        }

        public void GaussJordan()
        {
            Console.Clear();
            p.ImprimirSistema(sistema);
            for (int vi = 0; vi < sistema.GetLength(0); vi++) // Arrumar casa antes de começar a realizar qualquer tipo de calculo
            {
                Arrumar_Casa(vi);
            }
            Console.WriteLine();
            p.ImprimirSistema(sistema);

            // Declaração de variaveis 
            decimal aux;
            decimal[] vetor_aux = new decimal[sistema.GetLength(1)];

            //Principal 
            for (int i = 0; i < sistema.GetLength(0); i++)
            {
                for (int j = 0; j < sistema.GetLength(1) - 1; j++)
                {
                    if (i == j) // se estivermos em um elemento da diagonal principal e ele for diferente de zero: 
                    {
                        Check(i); // passando check para organizar a matriz
                        if (sistema[i, j] != 0)
                        {

                            for (int x = 0; x < sistema.GetLength(1); x++) // for para receber a linha do pivo
                            {
                                vet1[x] = sistema[i, x];
                            }
                            aux = sistema[i, j]; // aux recebe o elemento que está no lugar do pivo
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nCálculo da linha {0}:\n", i);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            for (int u = j; u < sistema.GetLength(1); u++) // For para multiplicar os outros elementos da linha e mostrar os calculos da linha
                            {
                                Console.WriteLine("{0} = ({0}) * {1} / {2}", vet1[u].ToString("F"), 1, aux.ToString("F"));
                                sistema[i, u] = (sistema[i, u] * 1) / aux; // multiplicando a linha do pivo 
                            }
                            p.ImprimirSistema(sistema);

                            for (int x = 0; x < sistema.GetLength(1); x++)
                            {
                                vet1[x] = sistema[i, x]; // passando novamente a linha do pivo para novos testes
                            }
                            int auxI = i + 1; // auxI = a linha abaixo do pivo

                            // while para multiplicar e elementos abaixo da linha do pivo
                            while (auxI <= sistema.GetLength(0) - 1)
                            {
                               
                                if (sistema[auxI, j] != 0) // se o elemento abaixo do pivo não for 0 então:
                                {
                                    for (int o = 0; o < sistema.GetLength(1); o++)
                                    {
                                        vetor_aux[o] = sistema[auxI, o]; // vetor auxiliar recebendo linha abaixo da linha do pivo
                                    }
                                    aux = sistema[auxI, j]; // recebendo elemento que está abaixo do pivo da linha atual
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nCálculo da linha abaixo do pivo:\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    for (int y = 0; y < sistema.GetLength(1); y++) // for para fazer calculo da linha
                                    {
                                        Console.WriteLine("{0} = {0} + ({1}) * {2} * -1", sistema[auxI, y].ToString("F"), vet1[y].ToString("F"), vetor_aux[j].ToString("F"));
                                        sistema[auxI, y] += vet1[y] * aux * -1; // multiplicando o pivo (e sua linha) com o elemento * -1 

                                    }
                                }
                                auxI++; // aux++ para verificar se tem elemento abaixo da linha do pivo
                            }

                            auxI = i - 1;
                            // while para zerar o elemento acima do pivo
                            while (auxI >= 0)  // aux for maior ou igual a zero 
                            {
                                if (sistema[auxI, j] != 0) // se o elemento acima da linha do pivo não for zero então: 
                                {
                                    for (int o = 0; o < sistema.GetLength(1); o++)
                                    {
                                        vetor_aux[o] = sistema[auxI, o]; // recebendo a linha acima da linha do pivo 
                                    }
                                    aux = sistema[auxI, j]; //aux recebendo elemento acima do pivo da linha atual 
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("\nCálculo da linha acima do pivo:\n");
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    for (int y = 0; y < sistema.GetLength(1); y++) // for para fazer o calculo da linha
                                    {
                                        Console.WriteLine("{0} = {0} + ({1})*{2}*-1", sistema[auxI, y].ToString("F"), vet1[y].ToString("F"), vetor_aux[j].ToString("F"));
                                        sistema[auxI, y] += vet1[y] * aux * -1; // multiplicando o pivo (e sua linha) com o elemento * -1 
                                    }
                                }
                                auxI--; // vai para linha mais acima da do pivo
                            }
                                p.ImprimirSistema(sistema);
                        }
                        Arrumar_Casa(i);
                        
                    }
                }
            }

            p.ImprimirSistema(sistema);
            test();

        }

        public int Classificacao()
        {
            int cont = 0; 
            for (int j = 0; j < sistema.GetLength(1)-1; j++)
            {
                if (sistema[sistema.GetLength(0) - 1, j] == 0)
                {
                    cont++;
                }
            }
            if (cont == sistema.GetLength(1) - 1 && sistema[sistema.GetLength(0)-1, sistema.GetLength(1)-1] != 0) // Termo independente diferente de zero, e os outros elementos da linha = 0
            {
                return 1;
            }
            else if (cont == sistema.GetLength(1)-1 && sistema[sistema.GetLength(0)-1, sistema.GetLength(1)-1] == 0) // Todos elementos, incluindo termo independente = 0
            {
                return 2;
            }
            else 
            {
                return 3;
                    
            }
        }
        public void test() 
        { 
            int c = Classificacao();
            switch (c)
            {
                case 1:
                    Console.WriteLine("\nSistema Linear inserido é Impossível");
                    break;
                case 2:
                    Console.WriteLine("\nSistema Linear inserido é Possível e Indeterminado");
                    break;
                case 3:
                    Console.WriteLine("\nSistema Linear inserido é Possível e Determinado");
                    Console.WriteLine("Conjunto solução: ");
                    for (int i = 0; i<sistema.GetLength(0); i++)
                    {
                        for (int j = 0; j<sistema.GetLength(1); j++)
                        {
                            if (j == sistema.GetLength(1)-1)
                            {
                                Console.WriteLine("{0}", sistema[i, j].ToString("F"));
                            }
}
                    }
                    break;
            }
        }

    }
}
