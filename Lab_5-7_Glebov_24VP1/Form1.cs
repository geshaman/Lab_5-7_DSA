using System;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Windows.Forms;

namespace Lab_5_7_Glebov_24VP1
{
    public partial class Form1 : Form
    {
        private long[] array;
        private int N = 10000000; 
        int iterations = 1000000;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            array = new long[N];
            long minElem = 0;

            for (int i = 0; i < N; i++)
            {
                array[i] = rnd.Next((int)minElem, (int)minElem + 5);
                minElem = array[i];
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            binary_search_non_optimal();
            binary_search_optimal();
            binary_interpol_search();
            //sequential_binary_search();
            //sequential_ordered_binary_search();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void binary_search_non_optimal()
        {
            int key = (int)numericUpDown2.Value;
            int resultIndex = -1;

            int start = Environment.TickCount;

            for (int j = 0; j < iterations; j++)
            {
                // Неоптимальный бинарный поиск
                int L = 0;
                int R = N - 1;

                while (L <= R)
                {

                    int i = (L + R) / 2;

                    if (array[i] == key)
                    {
                        resultIndex = i;
                        break;
                    }

                    if (array[i] > key)
                        R = i - 1;
                    else
                        L = i + 1;

                }
            }

            int resultTicks = Environment.TickCount - start;

            textBox1.Text = resultTicks.ToString();

            if (resultIndex == -1)
                textBox2.Text = "Элемент не найден";
            else
                textBox2.Text = resultIndex.ToString();
        }
        private void binary_search_optimal()
        {
            int key = (int)numericUpDown2.Value;
            int resultIndex = -1;

            int start = Environment.TickCount;

            for (int j = 0; j < iterations; j++)
            {
                // Оптимальный бинарный поиск
                int L = 0;
                int R = N - 1;

                while (L < R)
                {
                    int i = (L + R) / 2;
                    if (key <= array[i])
                        R = i;
                    else
                        L = i + 1;
                }
                if (array[R] == key)
                    resultIndex = R;
                else
                    resultIndex = -1;
            }

            int resultTicks = Environment.TickCount - start;

            textBox3.Text = resultTicks.ToString();

            if (resultIndex == -1)
                textBox4.Text = "Элемент не найден";
            else
                textBox4.Text = resultIndex.ToString();
        }

        private void binary_interpol_search()
        {
            iterations = 100000;
            int key = (int)numericUpDown2.Value;
            long resultIndex = -1;

            int start = Environment.TickCount;

            for (int j = 0; j < iterations; j++)
            {
                // Интерполяционный бинарный поиск
                long L = 0;
                long R = N - 1;
                resultIndex = -1;
                while (key >= array[L] && key <= array[R])
                {
                    long i = (L + (key - array[L]) * (R - L)) / (array[R] - array[L]);
                    
                    if (i < L) i = L;
                    if (i > R) i = R;

                    if (key == array[i])
                    {
                        resultIndex = i;
                        break;
                    }

                    else if (key < array[i])
                    {
                        R = i - 1;
                    }
                    else
                    {
                        L = i + 1;
                    }
                    
                }

                if (key == array[L])
                {
                    resultIndex = L;
                }

                else if (key == array[R])
                {
                    resultIndex = R;
                }
            }

            int resultTicks = Environment.TickCount - start;

            textBox5.Text = resultTicks.ToString();

            if (resultIndex == -1)
                textBox6.Text = "Элемент не найден";
            else
                textBox6.Text = resultIndex.ToString();
        }
        
        private void sequential_binary_search()
        {
            int key = (int)numericUpDown2.Value;
            int resultIndex = -1;

            int start = Environment.TickCount;

            for (int j = 0; j < iterations; j++)
            {
                // "Последовательный" бинарный поиск
                int P = 1;
                int B = N / 2; 

                while (B > 0)
                {
                    while ((P + B < N) && (array[P + B] <= key))
                    {
                        P += B;
                    }
                    B /= 2;
                }

                if (array[P] == key)
                    resultIndex = P;
            }

            int resultTicks = Environment.TickCount - start;

            textBox7.Text = resultTicks.ToString();

            if (resultIndex == -1)
                textBox8.Text = "Элемент не найден";
            else
                textBox8.Text = resultIndex.ToString();
        }
        private void sequential_ordered_binary_search()
        {
            iterations = 70;
            
            int key = (int)numericUpDown2.Value;
            int resultIndex = -1;

            int start = Environment.TickCount;

            for (int j = 0; j < iterations; j++)
            {
                // Последовательный бинарный поиск в упорядоченном массиве
                int P = 0;
                while((P < N) && (array[P] < key))
                {
                    P++;
                }
                if ((P < N) && (array[P] == key))
                {
                    resultIndex = P;
                }
            }

            int resultTicks = Environment.TickCount - start;

            textBox11.Text = resultTicks.ToString();

            if (resultIndex == -1)
                textBox12.Text = "Элемент не найден";
            else
                textBox12.Text = resultIndex.ToString();
        }
    }
}
