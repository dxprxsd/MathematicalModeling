using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabotaServerZapod // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("n");
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("m");
            int m = int.Parse(Console.ReadLine());
            
            Console.WriteLine("array a");
            string[] a_input_splitted = Console.ReadLine().Split(' ');
            int[] a = new int[a_input_splitted.Length];
            for (int i = 0; i < a_input_splitted.Length; i++)
            {
                a[i] = int.Parse(a_input_splitted[i]);  
            }
            Console.WriteLine("array b");
            string[] b_input_splitted = Console.ReadLine().Split(' ');
            int[] b = new int[b_input_splitted.Length];
            for (int i = 0; i < b_input_splitted.Length; i++)
            {
                b[i] = int.Parse(b_input_splitted[i]);
            }
            Console.WriteLine("matrix costs");
            List<string> lines = new List<string>();
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                lines.Add(input);
            }
            Console.WriteLine("done");

            n = lines.Count;
            m = lines[0].Split(' ').Length;
            double[,] costs = new double[n, m];

            for (int i = 0; i < n; i++)
            {
                string[] values = lines[i].Split(' ');
                for (int j = 0; j < m; j++)
                {
                    costs[i, j] = int.Parse(values[j]);
                }
            }
            */


            //array a
            //14 14 14 14
            //array b
            //13 5 13 12 13
            //matrix costs
            //16 26 12 24 3
            //5 2 19 27 2
            //29 23 25 16 8
            //2 25 14 15 21




            int choice;

            int[] a = { 90, 400, 110 };
            int[] b = { 140, 300, 160 };

            double[,] costs = { { 2, 5, 2 }, { 4, 1, 5 }, { 3, 6, 8 } };

            //int[] a = { 14, 14, 14, 14 };
            //int[] b = { 13, 5, 13, 12, 13 };

            //double[,] costs = { { 16, 26, 12, 24, 3 }, { 5, 2, 19, 27, 2 }, { 29, 23, 25, 16, 8 }, { 2, 25, 14, 15, 21 } };

            //int[] a = { 10, 20, 30 };
            //int[] b = { 15, 20, 25 };

            //int[] a = { 90, 400, 110 };
            //int[] b = { 140, 300, 160 };

            if (Utility.CheckClosure(a, b))
            {
                Console.WriteLine("Транспортная задача является закрытой");
            }
            else
            {
                Console.WriteLine("Транспортная задача является открытой");
            }

            //double[,] costs = { { 5, 3, 1 }, { 3, 2, 4 }, { 4, 1, 2 } };
            //double[,] costs = { { 2, 5, 2 }, { 4, 1, 5 }, { 3, 6, 8 } };

            Console.WriteLine("\nВыберите номер задания для его выполнения:\n" +
                "1 - Метод северо-западного угла\n" +
                "2 - Метод минимального элемента\n" +
                "3 - Метод аппроксимации Фогеля\n");

            choice = Convert.ToInt32(Console.ReadLine());
            int[,] supply_plan = new int[a.Length, b.Length];
            switch (choice)
            {
                case 1:
                    supply_plan = Utility.NorthWestCornerMethod(a, b, costs);
                    break;
                case 2:
                    supply_plan = Utility.MinElementMethod(a, b, costs);
                    break;
                case 3:
                    supply_plan = Utility.FogelMethod(a, b, costs);
                    break;
            }

            for (int i = 0; i < supply_plan.GetLength(0); i++)
            {
                for (int j = 0; j < supply_plan.GetLength(1); j++)
                {
                    Console.Write($"{supply_plan[i, j]} ");
                }
                Console.WriteLine();

            }

            if (Utility.CheckIfVyrozhdeno(supply_plan))
            {
                Console.WriteLine("Опорный план вырожденный.");
            }
            else
            {
                Console.WriteLine("Опорный план невырожденный.");
            }

            Console.WriteLine($"Целевая функция: {Utility.CalculateCelevayaFunction(costs, supply_plan)}");

            // УДАЛИТЬ: !!!!!!!!!!!
            int[,] supply_plan1 = { { 90, 0, 0 }, { 0, 300, 100 }, { 50, 0, 60 } };

            //return (isOptimalnoe, V_j, U_i, ocenki);
            (bool, double[], double[], double[,]) resultat = Utility.CheckOptimalnost(costs, supply_plan1);
            bool isOptimalnoe = resultat.Item1;
            double[] V_j = resultat.Item2;
            double[] U_i = resultat.Item3;
            double[,] ocenki = resultat.Item4;
            Console.WriteLine();
            for (int j = 0; j < V_j.GetLength(0); j++)
            {
                Console.WriteLine($"V_j[{j}] = {V_j[j]}");
            }
            Console.WriteLine();
            for (int i = 0; i < U_i.GetLength(0); i++)
            {
                Console.WriteLine($"U_i[{i}] = {U_i[i]}");
            }
            Console.WriteLine();
            for (int i = 0; i < ocenki.GetLength(0); i++)
            {
                for (int j = 0; j < ocenki.GetLength(1); j++)
                {
                    if (supply_plan[i, j] <= 0)
                    {
                        Console.WriteLine($"delta_{i}{j} = {ocenki[i, j]}");
                    }
                }
            }
            //разкоментить решение
            //Console.WriteLine();
            //if (isOptimalnoe)
            //{
            //    Console.WriteLine("оптимальное");
            //}
            //else
            //{
            //    Console.WriteLine("неоптимальное");
            //}

            if (!isOptimalnoe)
            {
                Console.WriteLine("Опорный план не оптимален.");

                while (true)
                {
                    // Запрос нового опорного плана от пользователя
                    Console.WriteLine("Введите новый опорный план (размерность N x M):");
                    int n = supply_plan.GetLength(0);
                    int m = supply_plan.GetLength(1);
                    int[,] new_supply_plan = new int[n, m];
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            Console.Write($"[{i + 1}, {j + 1}]: ");
                            new_supply_plan[i, j] = int.Parse(Console.ReadLine());
                        }
                    }

                    // Проверяем новый опорный план на оптимальность
                    (isOptimalnoe, V_j, U_i, ocenki) = Utility.CheckOptimalnost(costs, new_supply_plan);

                    if (isOptimalnoe)
                    {
                        Console.WriteLine("Новый опорный план является оптимальным.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Новый опорный план не является оптимальным.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Опорный план оптимален.");
            }
            Console.ReadKey();
        }


            


            //int[,] supply_plan = NorthWestCornerMethod(a, b, costs);

            //int[,] supply_plan = MinElementMethod(a, b, costs);

            //for (int i = 0; i < supply_plan.GetLength(0); i++)
            //{
            //    for (int j = 0; j < supply_plan.GetLength(1); j++)
            //    {
            //        Console.Write($"{supply_plan[i, j]} ");
            //    }
            //    Console.WriteLine();

            //}
            //Console.ReadKey();

            // ...
        }

    }

    internal static class Utility
    {
        public static bool CheckClosure(int[] a, int[] b)
        {
            int sum_a = a.Sum();
            int sum_b = b.Sum();

            if (sum_a == sum_b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckIfVyrozhdeno(int[,] supply_plan)
        {
            // GetLength(0) - возвращает размер первого измерения массива
            // GetLength(1) - возвращает размер второго измерения массива
            int n = supply_plan.GetLength(0);
            int m = supply_plan.GetLength(1);

            int kolvo_polozh_komponent = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (supply_plan[i, j] > 0)
                    {
                        kolvo_polozh_komponent++;
                    }
                }
            }

            if (kolvo_polozh_komponent == n + m - 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static double CalculateCelevayaFunction(double[,] costs, int[,] supply_plan)
        {
            double result = 0;

            for (int i = 0; i < costs.GetLength(0); i++)
            {
                for (int j = 0; j < costs.GetLength(1); j++)
                {
                    if (supply_plan[i, j] > 0)
                    {
                        result += costs[i, j] * supply_plan[i, j];
                    }
                }
            }

            return result;
        }

        public static int[,] NorthWestCornerMethod(int[] a, int[] b, double[,] costs)
        {
            int n = a.Length;
            int m = b.Length;
            int[,] supply_plan = new int[n, m]; // матрица опорного плана

            // цикл от i = 0, j = 0 до того, пока i или j не дойдут до конца
            // то есть цикл идет, пока i < n  И  j < m 

            for (int i = 0, j = 0; i < n && j < m;)
            {
                int value;

                // ищем что меньше a или b
                if (a[i] < b[j])
                {
                    value = a[i];
                }
                else
                {
                    value = b[j];
                }

                // обновляем значение в опорном плане
                supply_plan[i, j] = value;
                // обновляем a,b                
                b[j] -= value;
                a[i] -= value;

                // если b == 0, то прыгаем в соседний правый столбец
                if (b[j] == 0)
                {
                    j++;
                }
                // если a == 0, то прыгаем в соседнюю нижнюю строку
                else if (a[i] == 0)
                {
                    i++;
                }

            }

            return supply_plan;
        }

        public static int[,] MinElementMethod(int[] a, int[] b, double[,] costs)
        {
            int n = a.Length;
            int m = b.Length;
            // матрица для опорного плана
            int[,] supply_plan = new int[n, m];
            // матрица, в которой храним bool: true/false, был ли вручную изменен конкретный элемент 
            // потому что изначально матрица инициализирована нулями, мы не будем знать
            // какой ноль мы поставили сами, а какой там стоит с инициализации
            bool[,] bools_suply_plan = new bool[n, m];

            // цикл, пока все элементы вспомогательной матрицы не станут true
            // то есть пока мы не заполним вручную все элементы
            while (!CheckIfMatrixAllTrue(bools_suply_plan))
            {
                //все позиции мин элементов среди незанятых ячеек
                List<(int, int)> positions = FindMinElementPositions(costs, bools_suply_plan);
                int min_j = int.MaxValue;
                int index_of_min_j = -1;
                // positions

                // 1 1 5
                // 8 1 6
                // 1 7 6

                // positions = [ (0, 0), (0, 1), (1, 1), (2, 0) ]
                //                i  j    i  j    i  j    i  j

                // min_j_positions = [ (0, 0), (2, 0) ]
                //                      i  j    i  j

                // positions = [ (i, j), (i, j) .... ]
                // 

                // среди positions ищем элемент с минимальным j  (то есть самый левый)
                // + так как мы найдем самый первый из минимального j (так как if со знаком <)
                //    то он будет и самым верхним
                // -> index_of_min_j указывает на индекс элемента в массиве позиций минимальных элементов
                //                                    который находится слева и сверху
                for (int k = 0; k < positions.Count; k++)
                {
                    if (positions[k].Item2 < min_j)
                    {
                        min_j = positions[k].Item2;
                        index_of_min_j = k;
                    }
                }

                int min_i = positions.ElementAt(index_of_min_j).Item1;
                //int min_j = positions.ElementAt(index_of_min_j).Item2;

                // min_i, min_j указывают на индексы самого левого верхнего минимального элемента среди незанятых ячеек.
                int value;
                // по методу проверка a или b что меньше
                if (a[min_i] < b[min_j])
                {
                    value = a[min_i];
                }
                else
                {
                    value = b[min_j];
                }

                a[min_i] -= value;
                b[min_j] -= value;
                // заполняем нужным значением и ставим true
                supply_plan[min_i, min_j] = value;
                bools_suply_plan[min_i, min_j] = true;

                // если а = 0, то заполняем строку нулями
                // но значения supply_plan уже нули, поэтому мы просто меняем значения
                // вспомогательной матрицы bools_supply_plan на true, что значит, что мы этот ноль поставили вручную
                if (a[min_i] == 0)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (bools_suply_plan[min_i, j] == false)
                        {
                            bools_suply_plan[min_i, j] = true;
                        }
                    }
                }
                // b = 0, аналогично заполнение столбца нулями
                else if (b[min_j] == 0)
                {
                    for (int i = 0; i < n; i++)
                    {
                        if (bools_suply_plan[i, min_j] == false)
                        {
                            bools_suply_plan[i, min_j] = true;
                        }
                    }
                }
            }

            return supply_plan;
        }

        // чекаем вспомогательную матрицу булов, все ли ячейки были заполнены
        // если хоть одна ячейка не заполнена (то есть она false)
        public static bool CheckIfMatrixAllTrue(bool[,] bools)
        {
            for (int i = 0; i < bools.GetLength(0); i++)
            {
                for (int j = 0; j < bools.GetLength(1); j++)
                {
                    if (bools[i, j] == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static List<(int, int)> FindMinElementPositions(double[,] costs, bool[,] bools)
        {
            // positions - список позиций элементов равных минимальному

            // positions - список из экземпляров, в котором первый int - это i, второй int - это j

            // ПРИМЕР
            //
            // 0 1 2   i
            //
            // 1 1 5   0 
            // 8 1 6   1
            // 1 7 6   2
            //         j

            // positions = [ (0, 0), (0, 1), (1, 1), (2, 0) ]
            //                i  j    i  j    i  j    i  j

            List<(int, int)> positions = new List<(int, int)>();
            double min = double.MaxValue;

            // идем по матрице costs и ищем позиции элементов, равных минимальному (+ ищем минимальный параллельно)
            // ! ! проверяем только среди незанятых ячеек
            //      то есть bools[i, j] == false, false -> значение не было заполнено
            //                                    true -> в ячейку было вручную поставлено значение

            for (int i = 0; i < costs.GetLength(0); i++)
            {
                // double[,] costs = { { 16, 26, 12, 24, 3 }, { 5, 2, 19, 27, 2 }, { 29, 23, 25, 16, 8 }, { 2, 25, 14, 15, 21 } };
                //                        0   1   2   3  4      0  1  2   3   4
                //                              0                     1                      2

                for (int j = 0; j < costs.GetLength(1); j++)
                {
                    // если значение минимального элемента уменьшилось еще дальше, то все предыдущие позиции сбрасываем
                    if (bools[i, j] == false && costs[i, j] < min)
                    {
                        min = costs[i, j];
                        positions.Clear();
                        positions.Add((i, j));
                    }
                    // сохраняем позиции элементов равных текущему минимальному
                    else if (bools[i, j] == false && costs[i, j] == min)
                    {
                        positions.Add((i, j));
                    }
                }
            }

            return positions;
        }


        public static int[,] FogelMethod(int[] a, int[] b, double[,] costs)
        {
            int n = a.Length;
            int m = b.Length;
            // supply_plan инициализирован нулями
            int[,] supply_plan = new int[n, m];
            // bools_supply_plan инициализирован false, в нем мы храним информацию о занятости ячейки
            bool[,] bools_supply_plan = new bool[n, m];

            // Цикл, пока все элементы матрицы не будут заняты
            while (!CheckIfMatrixAllTrue(bools_supply_plan))
            {
                // Высчитываем штрафы и обрабатываем полученный результат
                ((double[], List<(int, int)>), (double[], List<(int, int)>)) result = CalculateShtrafy(costs, bools_supply_plan);

                //return ( (d_i, d_i_min_element_positions), (d_j, d_j_min_element_positions) );
                //          Item1        Item2                Item1           Item2
                //                 ITEM1                             ITEM2                       
                double[] d_i = result.Item1.Item1;
                List<(int, int)> d_i_min_element_positions = result.Item1.Item2;
                double[] d_j = result.Item2.Item1;
                List<(int, int)> d_j_min_element_positions = result.Item2.Item2;

                // Ищем максимальный штраф, инициализируем мин. значением double
                double max_shtraf_value = double.MinValue;

                int index_i = -1; //-1 потому что используется как флаг для обозначения отсутствия значений
                int index_j = -1;

                // Ищем максимальный штраф среди штрафов d_i
                for (int j = 0; j < m; j++)
                {
                    if (max_shtraf_value < d_i[j])
                    {
                        max_shtraf_value = d_i[j];
                        index_i = d_i_min_element_positions[j].Item1; // d_i_min_elements_positions[j] = (index_i, index_j)
                        index_j = d_i_min_element_positions[j].Item2;
                    }
                }

                // Ищем максимальный штраф среди штрафов d_j
                for (int i = 0; i < n; i++)
                {
                    if (max_shtraf_value < d_j[i])
                    {
                        max_shtraf_value = d_j[i];
                        index_i = d_j_min_element_positions[i].Item1;
                        index_j = d_j_min_element_positions[i].Item2;
                    }
                }

                // если a этой строки < b этого столбца
                if (a[index_i] < b[index_j])
                {
                    supply_plan[index_i, index_j] = a[index_i]; // то ставим значение минимального из них (a)
                    b[index_j] -= a[index_i]; // вычитаем новое значение из этого столбца b
                    a[index_i] = 0; // так как взяли а из этой строки, то можем его обнулить (а - а = 0)
                    for (int j = 0; j < m; j++)
                    {
                        bools_supply_plan[index_i, j] = true; // ставим нули во всю строку, значит обновляем значение занятости ячеек этой строки на true
                    }
                }
                // если a этой строки >= b этого столбца
                else
                {
                    supply_plan[index_i, index_j] = b[index_j]; // аналогично (выше)
                    a[index_i] -= b[index_j];
                    b[index_j] = 0;
                    for (int i = 0; i < n; i++)
                    {
                        bools_supply_plan[i, index_j] = true;
                    }
                }
            }

            return supply_plan;
        }

        // В этом методе мы подсчитываем штрафы d_i, d_j и возвращаем результат в следующем формате: 
        // ((d_i, d_i_min_element_positions), (d_j, d_j_min_element_positions))
        // где d_i_min_elements_positions и d_j_min_elements_positions - листы, хранящие (i, j) самого наименьшего
        // для подсчитанного штрафа (в строке или в столбце)
        public static ((double[], List<(int, int)>), (double[], List<(int, int)>)) CalculateShtrafy(double[,] costs, bool[,] bools_supply_plan)
        {
            // GetLength(0) - возвращает размер первого измерения массива
            // GetLength(1) - возвращает размер второго измерения массива
            int n = costs.GetLength(0);
            int m = costs.GetLength(1);
            // Инициализируем массив штрафов d_i количеством столбцов m
            double[] d_i = new double[m];
            // Инициализируем массив штрафов d_j количеством строк n
            double[] d_j = new double[n];
            List<(int, int)> d_i_min_element_positions = new List<(int, int)>();
            List<(int, int)> d_j_min_element_positions = new List<(int, int)>();

            // d[i] = 3  5

            // d_i_elements ... = (0,0)   (0, 1)

            // Запускаем внешний цикл по строкам, внутренний по столбцам
            // Будем здесь искать штрафы d_j и d_j_min_element_positions
            for (int i = 0; i < n; i++)
            {
                // Так как нужно найти минимальное число, инициализируем как double.MaxValue - максималзначение
                double min1 = double.MaxValue; // в min1 храним самое минимальное значение
                int min1_index_j = -1; // храним индекс j, где нашли min1 (индекс i не изменяется во время этого цикла)
                double min2 = double.MaxValue; // в min2 храним второе минимальное значение
                int min2_index_j = -1; // храним индекс j, где нашли min2 (индекс i не изменяется во время этого цикла)

                for (int j = 0; j < m; j++)
                {
                    // Если ячейка не занята
                    if (!bools_supply_plan[i, j])
                    {
                        // Если текущее значение меньше самого минимального (min1)
                        if (costs[i, j] < min1)
                        {
                            // Минимальное 1 становится минимальным 2
                            // А новое значение становится минимальным 1
                            min2 = min1;
                            min2_index_j = min1_index_j;
                            min1 = costs[i, j];
                            min1_index_j = j;
                        }
                        // Если текущее значение меньше второго минимального и не равно первому минимальному (нужны различные минимальные)
                        else if (costs[i, j] < min2 && costs[i, j] != min1)
                        {
                            min2 = costs[i, j]; // Обновляем значение второго минимального
                            min2_index_j = j; // и его индекс
                        }
                    }
                }

                // не нашли ни одного минимального элемента, значит все элементы заняты и штраф равен нулю
                if (min1_index_j == -1 && min2_index_j == -1)
                {
                    d_j[i] = 0; // штраф нельзя посчитать, все элементы заняты
                    d_j_min_element_positions.Add((-1, -1)); // добавляем (-1, -1) просто, чтобы не сбились индексы в листе (чтобы не пропускать элемент тут)
                }
                // был найден только первый минимальный элемент
                else if (min2_index_j == -1)
                {
                    d_j[i] = min1;
                    d_j_min_element_positions.Add((i, min1_index_j));
                }
                // были найдены оба минимальных элемента
                else
                {
                    d_j[i] = min2 - min1;
                    d_j_min_element_positions.Add((i, min1_index_j));
                }
            }

            // Запускаем внешний цикл по столбцам, внутренний по строкам
            // Будем здесь искать штрафы d_i и d_i_min_element_positions
            // далее аналогично коду выше
            for (int j = 0; j < m; j++)
            {
                double min1 = double.MaxValue;
                int min1_index_i = -1;
                double min2 = double.MaxValue;
                int min2_index_i = -1;

                for (int i = 0; i < n; i++)
                {
                    if (!bools_supply_plan[i, j])
                    {
                        if (costs[i, j] < min1)
                        {
                            min2 = min1;
                            min2_index_i = min1_index_i;
                            min1 = costs[i, j];
                            min1_index_i = i;
                        }
                        else if (costs[i, j] < min2 && costs[i, j] != min1)
                        {
                            min2 = costs[i, j];
                            min2_index_i = i;
                        }
                    }
                }
                // не нашли ни одного минимального элемента, значит все элементы заняты и штраф равен нулю
                if (min1_index_i == -1 && min2_index_i == -1)
                {
                    d_i[j] = 0; // штраф нельзя посчитать, все элементы заняты
                    d_i_min_element_positions.Add((-1, -1));
                }
                // был найден только первый минимальный элемент
                else if (min2_index_i == -1)
                {
                    d_i[j] = min1;
                    d_i_min_element_positions.Add((min1_index_i, j));
                }
                // были найдены оба минимальных элемента
                else
                {
                    d_i[j] = min2 - min1;
                    d_i_min_element_positions.Add((min1_index_i, j));
                }

            }
            return ((d_i, d_i_min_element_positions), (d_j, d_j_min_element_positions));
        }

        public static bool CheckIfAllPotentialsTrue(bool[] V_j_bool, bool[] U_i_bool)
        {
            for (int j = 0; j < V_j_bool.GetLength(0); j++)
            {
                if (!V_j_bool[j])
                {
                    return false;
                }
            }

            for (int i = 0; i < U_i_bool.GetLength(0); i++)
            {
                if (!U_i_bool[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static (bool, double[], double[], double[,]) CheckOptimalnost(double[,] costs, int[,] supply_plan)
        {
            int n = costs.GetLength(0);
            int m = costs.GetLength(1);
            double[] V_j = new double[m];
            bool[] V_j_bool = new bool[m];
            double[] U_i = new double[n];
            bool[] U_i_bool = new bool[n];
            U_i_bool[0] = true;

            while (!CheckIfAllPotentialsTrue(V_j_bool, U_i_bool))
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (supply_plan[i, j] > 0 && !(V_j_bool[j] && U_i_bool[i]))
                        {
                            if (V_j_bool[j])
                            {
                                // V_j[j] + U_i[i] = c_ij
                                U_i[i] = costs[i, j] - V_j[j];
                                U_i_bool[i] = true;
                            }
                            else if (U_i_bool[i])
                            {
                                V_j[j] = costs[i, j] - U_i[i];
                                V_j_bool[j] = true;
                            }
                        }
                    }
                }
            }

            double[,] ocenki = new double[n, m];
            bool isOptimalnoe = true;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (supply_plan[i, j] <= 0)
                    {
                        ocenki[i, j] = U_i[i] + V_j[j] - costs[i, j];
                        if (ocenki[i, j] > 0)
                        {
                            isOptimalnoe = false;
                        }
                    }
                }
            }

            return (isOptimalnoe, V_j, U_i, ocenki);
        }
    }
  

