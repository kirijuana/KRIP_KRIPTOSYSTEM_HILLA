using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KRIP_KRIPTOSYSTEM_HILLA
{
  
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Shifr_Click(object sender, EventArgs e)
        {
            char[] text = Text.Text.ToCharArray();
            char[] ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567=+!?".ToCharArray();
            char[] alpha = "abcdefghijklmnopqrstuvwxyz1234567=+!?".ToCharArray();
            char[] alph = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя_,.-".ToCharArray();
            char[] ALPH = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ_,.-".ToCharArray();
            char[] key = keyBox.Text.ToCharArray();            
            
            int[,] array_key = new int[Convert.ToInt32(Math.Sqrt(key.Length)), Convert.ToInt32(Math.Sqrt(key.Length))];

            for (int j = 0, k = 0, z = 0; j < key.Length; j++)
            {

                for (int i = 0; i < alph.Length; i++)
                {
                    if(z == Convert.ToInt32(Math.Sqrt(key.Length)))
                    {
                        z = 0;
                        k++;
                    }
                    if (key[j] == alph[i] || key[j] == ALPH[i])
                    {
                        array_key[k, z] = i;
                        break;
                    }
                }

                for (int i = 0; i < alpha.Length; i++)
                {
                    if (z == Convert.ToInt32(Math.Sqrt(key.Length)))
                    {
                        z = 0;
                        k++;
                    }
                    if (key[j] == alpha[i] || key[j] == ALPHA[i])
                    {
                        array_key[k, z] = i;
                        break;
                    }
                }
                z++;
            }

            //for (int j = 0, k = 0, z = 0; j < key.Length; j++)
            //{

            //    for (int i = 0; i < alph.Length; i++)
            //    {
            //        if (z == Convert.ToInt32(Math.Sqrt(key.Length)))
            //        {
            //            z = 0;
            //            k++;
            //        }
            //        if (key[j] == alph[i] || key[j] == ALPH[i])
            //        {
            //            array_key[k, z] = i;
            //            z++;
            //            break;
            //        }
            //    }
            //}


            int length_n_gramm = 0;                  // вычисляем кол-во символов в тексте, принадлежащих к алфавиту
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < alph.Length; j++)
                {
                    if (text[i] == alph[j] || text[i] == ALPH[j] || text[i] == alpha[j] || text[i] == ALPHA[j])
                    {
                        length_n_gramm++;
                        break;
                    }

                }
            }

            if (text.Length % Convert.ToInt32(Math.Sqrt(key.Length)) != 0)
            {
                length_n_gramm += Convert.ToInt32(Math.Sqrt(key.Length)) - text.Length % Convert.ToInt32(Math.Sqrt(key.Length));
            }

            int[] n_gramm = new int[length_n_gramm];
            int x = 0;
            for (int j = 0; j < text.Length; j++)
            {
                for (int i = 0; i < alph.Length; i++)
                {
                    if (text[j] == alph[i] || text[j] == ALPH[i] || text[j] == alpha[i] || text[j] == ALPHA[i])
                    {
                        n_gramm[x] = i;
                        x++;
                        break;
                    }
                }
            }

            if (x < length_n_gramm)
            {
                for (; x < n_gramm.Length; x++)
                    n_gramm[x] = 35;
            }

            int n = length_n_gramm / Convert.ToInt32(Math.Sqrt(key.Length)); // кол-во n-грамм
            int[] n_gramm_C = new int[length_n_gramm];
            int c = 0;
            int number_n_gramm = 0;
            do
            {                
                for (int i = 0, mul = 0; i < Convert.ToInt32(Math.Sqrt(key.Length)); i++)
                {
                    mul = 0;
                    for (int j = 0, k = number_n_gramm; j < Convert.ToInt32(Math.Sqrt(key.Length)); j++)
                    {
                        mul = mul + (n_gramm[k] * array_key[j, i]);
                        k++;
                    }
                    
                    n_gramm_C[c] = mul = mul % 37;
                    c++;
                }
                n--;
                number_n_gramm += Convert.ToInt32(Math.Sqrt(key.Length));
            } while (n != 0);

            char[] shifr = new char[text.Length];
            bool check = false; // проверка на принадлежность символа к какому-либо алфавиту
            for (int i = 0, k = 0; i < text.Length; i++)
            {
                for (int j = 0; j < alph.Length; j++)
                {
                    if (text[i] == alph[j])
                    {
                        shifr[i] = alph[n_gramm_C[k]];
                        k++;
                        check = true;
                    }
                    else if (text[i] == ALPH[j])
                    {
                        shifr[i] = ALPH[n_gramm_C[k]];
                        k++;
                        check = true;
                    }
                    else if (text[i] == alpha[j])
                    {
                        shifr[i] = alpha[n_gramm_C[k]];
                        k++;
                        check = true;
                    }
                    else if (text[i] == ALPHA[j])
                    {
                        shifr[i] = ALPHA[n_gramm_C[k]];
                        k++;
                        check = true;
                    }
                }
                if (!check)
                    shifr[i] = text[i];
                check = false;
            }

            string text1 = new string(shifr);
            ShifrText.Text = text1;
        }

        private void button_unShifr_Click(object sender, EventArgs e)
        {
            double determ(int[,] Arr, int size)         //функция поиска определителя
            {
                int i, j;
                double det = 0;       //переменная определителя
                if (size == 1)     // 1-е условие , размер 1
                {
                    det = Arr[0, 0];
                }
                else if (size == 2)    // 2-е условие , размер 2
                {
                    det = Arr[0, 0] * Arr[1, 1] - Arr[0, 1] * Arr[1, 0];    //
                }
                else
                {
                    int[,] matr = new int[size - 1, size - 1]; //создание динамического массива
                    for (i = 0; i < size; ++i)
                    {
                        for (j = 0; j < size - 1; ++j)
                        {
                            if (j < i)
                            {
                                matr[i, j] = Arr[i, j];
                            }
                            else
                                matr[i, j] = Arr[i, j + 1];
                        }
                        det += Math.Pow(-1, (i + j)) * determ(matr, size - 1) * Arr[i, size - 1];    //подсчеты
                    }                  
                }
                return det; //возвращаем значение определителя
            }

            char[] shifr = Text.Text.ToCharArray();
            char[] ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567=+!?".ToCharArray();
            char[] alpha = "abcdefghijklmnopqrstuvwxyz1234567=+!?".ToCharArray();
            char[] alph = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя_,.-".ToCharArray();
            char[] ALPH = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ_,.-".ToCharArray();
            char[] key = keyBox.Text.ToCharArray();

            int[,] array_key = new int[Convert.ToInt32(Math.Sqrt(key.Length)), Convert.ToInt32(Math.Sqrt(key.Length))];

            for (int j = 0, k = 0, z = 0; j < key.Length; j++)
            {
                for (int i = 0; i < alph.Length; i++)
                {
                    if (z == Convert.ToInt32(Math.Sqrt(key.Length)))
                    {
                        z = 0;
                        k++;
                    }
                    if (key[j] == alph[i] || key[j] == ALPH[i])
                    {
                        array_key[k, z] = i;                     
                        break;
                    }
                }
                for (int i = 0; i < alpha.Length; i++)
                {
                    if (z == Convert.ToInt32(Math.Sqrt(key.Length)))
                    {
                        z = 0;
                        k++;
                    }
                    if (key[j] == alpha[i] || key[j] == ALPHA[i])
                    {
                        array_key[k, z] = i;                      
                        break;
                    }
                }
                z++;
            }
            double K = 0; // opredelitel'
             
            if(Convert.ToInt32(Math.Sqrt(key.Length)) == 2)
            {
                K = array_key[0, 0] * array_key[0, 1] - array_key[0, 1] * array_key[1, 0];
            }
            else if(Convert.ToInt32(Math.Sqrt(key.Length)) == 3)
            {
                K = (array_key[0, 0] * array_key[1, 1] * array_key[2, 2] + array_key[2, 0] * array_key[0, 1] * array_key[1, 2] + array_key[1, 0] * array_key[2, 1] * array_key[0, 2] - array_key[2, 0] * array_key[1, 1] * array_key[0, 2] - array_key[0, 0] * array_key[2, 1] * array_key[1, 2] - array_key[1, 0] * array_key[0, 1] * array_key[2, 2]) % 37;
                //K = determ(array_key, Convert.ToInt32(Math.Sqrt(key.Length)));
            }
            if (K < 0)
                K = K * (-1);
            if (K < 37)
                K = 37 - K;

            int[,] det_array_key = new int[Convert.ToInt32(Math.Sqrt(key.Length)), Convert.ToInt32(Math.Sqrt(key.Length))];
            int n = Convert.ToInt32(Math.Sqrt(key.Length));
            for (int index1 = 0; index1 < n; index1++)
                for (int index2 = 0; index2 < n; index2++)
                {
                    int k = index1, k1 = index2;
                    int[,] temp = new int[n - 1, n - 1];
                    int s = 0, s1 = 0;
                    for (int i = 0; i < n; ++i)//строим матрицу минора участвующего в вычислении алгебраисческого дополнения
                        if (i != k)
                        {
                            s1 = 0;
                            for (int j = 0; j < n; ++j)
                                if (j != k1)
                                {
                                    temp[s, s1] = array_key[i, j];
                                    s1++;
                                }

                            s++;
                        }
                    double res = 0;
                    res = (Math.Pow(-1, k + 1 + k1 + 1) * determ(temp, n - 1));
                    res = res % 37;
                    if(res < 0)
                    {
                        res = res + 37;
                    }
                    det_array_key[index2, index1] = Convert.ToInt32(res);
                }

            int K_1 = 1; // обратный к определителю
            for (int i = 1; K * i % 37 != 1; i++)
                K_1++;

            for (int i = 0; i < Convert.ToInt32(Math.Sqrt(key.Length)); i++)
            {
                for (int j = 0; j < Convert.ToInt32(Math.Sqrt(key.Length)); j++)
                {
                    det_array_key[i, j] = det_array_key[i, j] * K_1 % 37;
                }
            }

            int length_n_gramm = 0;                  // вычисляем кол-во символов в тексте из алфавита
            for (int i = 0; i < shifr.Length; i++)
            {
                for (int j = 0; j < alph.Length; j++)
                {
                    if (shifr[i] == alph[j] || shifr[i] == ALPH[j] || shifr[i] == alpha[j] || shifr[i] == ALPHA[j])
                    {
                        length_n_gramm++;
                        break;
                    }

                }
            }

            //проверка, можно ли разбить shifrtext на n-граммы одинакового размера
            if (length_n_gramm % Convert.ToInt32(Math.Sqrt(key.Length)) != 0)
            {
                length_n_gramm += Convert.ToInt32(Math.Sqrt(key.Length)) - shifr.Length % Convert.ToInt32(Math.Sqrt(key.Length));
            }

            int[] n_gramm = new int[length_n_gramm]; // разделяем шифртекст на н-граммы
            int x = 0;
            for (int j = 0; j < shifr.Length; j++)
            {
                for (int i = 0; i < alph.Length; i++)
                {
                    if (shifr[j] == alph[i] || shifr[j] == ALPH[i] || shifr[j] == alpha[i] || shifr[j] == ALPHA[i])
                    {
                        n_gramm[x] = i;
                        x++;
                        break;
                    }
                }
            }

            if (x < length_n_gramm) // дозаполняем последнюю n-грамму
            {
                for (; x < n_gramm.Length; x++)
                    n_gramm[x] = 35;
            }

            int quantity_n_gramm = length_n_gramm / Convert.ToInt32(Math.Sqrt(key.Length)); // кол-во n-грамм
            int[] n_gramm_M = new int[length_n_gramm];
            int c = 0;
            int number_n_gramm = 0;
            do
            {
                for (int i = 0, mul = 0; i < Convert.ToInt32(Math.Sqrt(key.Length)); i++)
                {
                    mul = 0;
                    for (int j = 0, k = number_n_gramm; j < Convert.ToInt32(Math.Sqrt(key.Length)); j++)
                    {
                        mul = mul + (n_gramm[k] * det_array_key[j, i]);
                        k++;
                    }

                    n_gramm_M[c] = mul = mul % 37;
                    c++;
                }
                quantity_n_gramm--;
                number_n_gramm += Convert.ToInt32(Math.Sqrt(key.Length));
            } while (quantity_n_gramm != 0);

            char[] text = new char[shifr.Length];
            bool check = false; // проверка на принадлежность символа к какому-либо алфавиту
            for (int i = 0, k = 0; i < shifr.Length; i++)
            {
                for (int j = 0; j < alph.Length; j++)
                {
                    if (shifr[i] == alph[j])
                    {
                        text[i] = alph[n_gramm_M[k]];
                        k++;
                        check = true;
                    }
                    else if (shifr[i] == ALPH[j])
                    {
                        text[i] = ALPH[n_gramm_M[k]];
                        k++;
                        check = true;
                    }
                    else if (shifr[i] == alpha[j])
                    {
                        text[i] = alpha[n_gramm_M[k]];
                        k++;
                        check = true;
                    }
                    else if (shifr[i] == ALPHA[j])
                    {
                        text[i] = ALPHA[n_gramm_M[k]];
                        k++;
                        check = true;
                    }
                }
                if (!check)
                    text[i] = shifr[i];
                check = false;
            }

            string text1 = new string(text);
            ShifrText.Text = text1;
        }
    }
}
