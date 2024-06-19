using System.Windows;
using System.Globalization;
using System.Windows.Forms;
using System;
using System.Numerics;

namespace vijenera
{
    public partial class MainWindow : Window
    {
        string keys = "";
        string language = "";
        const int k_rus = 43;
        const int k_angl = 36;

        char[] Alfavit_rus_a = new char[k_rus] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        char[] Alfavit_angl_a = new char[k_angl] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        //char[] Alfavit_rus_A = new char[33] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };
        //char[] Alfavit_angl_A = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        //char[] Alfavit_numbers = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };


        public MainWindow()
        {
            InitializeComponent();
            textbox_show_text.IsReadOnly = true;
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        { 
            string test_shifr = textbox_write_text.Text;
            string[] test_words = test_shifr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (Language(test_words, Alfavit_rus_a, Alfavit_angl_a) == 0)
            {
                if (Proverka_keys(Alfavit_rus_a) == keys.Length)//язык русский
                {
                    
                    if (!Proverka_text(test_words, Alfavit_rus_a))
                        System.Windows.MessageBox.Show("Пишите только на русском!");
                    else
                        Shifr_vijenera(keys, test_words, Alfavit_rus_a, k_rus);
                        language = "rus";
                }
                else
                {
                    System.Windows.MessageBox.Show("Ключ не корректен, исправьте!");
                    return;
                }

            }
            else if (Language(test_words, Alfavit_rus_a, Alfavit_angl_a) == 1)
            {
                if (Proverka_keys(Alfavit_angl_a) == keys.Length)//язык англ
                {
                    if (!Proverka_text(test_words, Alfavit_angl_a))
                        System.Windows.MessageBox.Show("Пишите только на английском!");
                    else
                        Shifr_vijenera(keys, test_words, Alfavit_angl_a, k_angl);
                    language = "angl";
                }
                else
                {
                    System.Windows.MessageBox.Show("Ключ не корректен, исправьте!");
                    return;
                }
            }
            else if (Language(test_words, Alfavit_rus_a, Alfavit_angl_a) == 9)
            {
                System.Windows.MessageBox.Show("Пишите русские или английские буквы!");
            }
        }

        private void Rus_Click(object sender, RoutedEventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("ru-RU"));
            System.Windows.MessageBox.Show("Пишите пожалуйста весь текст только на русском");
            language = "rus";
        }

        private void Eng_Click(object sender, RoutedEventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("en-EN"));
            System.Windows.MessageBox.Show("Пишите пожалуйста весь текст только на английском");
            language = "angl";
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            textbox_show_text.Clear();
            textbox_show_text_deshifr.Clear();
        }

        private void Deshifr_button_Click(object sender, RoutedEventArgs e)
        {
            keys = textbox_keys.Text;

            string test_shifr = textbox_show_text.Text;
            string[] test_words = test_shifr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (language == "rus")
                Deshifr_vijenera(keys, test_words, Alfavit_rus_a, k_rus);
            else
                Deshifr_vijenera(keys, test_words, Alfavit_angl_a, k_angl);
        }

        public void Deshifr_vijenera(string keys, string[] test_words, char[] Alfavit_1, int k)
        {
            int index = 0;
            int new_index = 0;

            char[] keys_char = keys.ToCharArray();
            int[] keys_int = new int[keys_char.Length];

            int id = 0;
            foreach (char m in keys_char)
                for (int s = 0; s < Alfavit_1.Length; s++)
                    if (m == Alfavit_1[s])
                    {
                        keys_int[id] = Array.IndexOf(Alfavit_1, Alfavit_1[s]);
                        id++;
                    }

            id = 0;
            for (int i = 0; i < test_words.Length; i++)
            {
                char[] one_word = test_words[i].ToCharArray();
                foreach (char b in one_word)
                {
                    for (int m = 0; m < Alfavit_1.Length; m++)
                    {
                        if (b == Alfavit_1[m])
                        {
                            index = Array.IndexOf(Alfavit_1, Alfavit_1[m]);
                            try
                            {
                                new_index = (index + k - keys_int[id]) % k;
                                id++;
                            }
                            catch
                            {
                                id = 0;
                                new_index = (index + k - keys_int[id]) % k;
                                id++;
                            }
                            textbox_show_text_deshifr.Text += Alfavit_1[new_index];
                            break;
                        }
                    }

                    //for (int t = 0; t < Alfavit_numbers.Length; t++)
                    //{
                    //    if (b == Alfavit_numbers[t])
                    //    {
                    //        index = Array.IndexOf(Alfavit_numbers, Alfavit_numbers[t]);
                    //        try
                    //        {
                    //            new_index = (index + 10 - keys_int[id]) % 10;
                    //            id++;
                    //        }
                    //        catch
                    //        {
                    //            id = 0;
                    //            new_index = (index + 10 - keys_int[id]) % 10;
                    //            id++;
                    //        }
                    //        if (new_index < 0) new_index += 10;
                    //        textbox_show_text_deshifr.Text += Alfavit_numbers[new_index];
                    //        break;
                    //    }
                    //}
                }
                textbox_show_text_deshifr.Text += ' ';
            }
        }

        public void Shifr_vijenera(string keys, string[] test_words, char[] Alfavit_1, int k)
        {
            int index = 0;
            int new_index = 0;

            char[] keys_char = keys.ToCharArray();
            int[] keys_int = new int[keys_char.Length];

            int id = 0;
            foreach (char m in keys_char)
                for (int s = 0; s < Alfavit_1.Length; s++)
                    if (m == Alfavit_1[s])
                    {
                        keys_int[id] = Array.IndexOf(Alfavit_1, Alfavit_1[s]);
                        id++;
                    }


            id = 0;
            for (int i = 0; i < test_words.Length; i++)
            {
                char[] one_word = test_words[i].ToCharArray();
                foreach (char b in one_word)
                {
                    for (int m = 0; m < Alfavit_1.Length; m++)
                    {
                        if (b == Alfavit_1[m])
                        {
                            index = Array.IndexOf(Alfavit_1, Alfavit_1[m]);
                            try
                            {
                                new_index = (index + keys_int[id]) % k;
                                id++;
                            }
                            catch
                            {
                                id = 0;
                                new_index = (index + keys_int[id]) % k;
                                id++;
                            }
                            textbox_show_text.Text += Alfavit_1[new_index];
                            break;
                        }
                    }

                    //for (int t = 0; t < Alfavit_numbers.Length; t++)
                    //{
                    //    if (b == Alfavit_numbers[t])
                    //    {
                    //        index = Array.IndexOf(Alfavit_numbers, Alfavit_numbers[t]);
                    //        try
                    //        {
                    //            new_index = (index + keys_int[id]) % 10;
                    //            id++;
                    //        }
                    //        catch
                    //        {
                    //            id = 0;
                    //            new_index = (index + keys_int[id]) % 10;
                    //            id++;
                    //        }
                    //        textbox_show_text.Text += Alfavit_numbers[new_index];
                    //        break;
                    //    }
                    //}
                }
                textbox_show_text.Text += ' ';
            }
        }
        public bool Proverka_text(string[] test_words, char[] Alfavit_1)
        {
            int size_text = 0;
            int size_text_real = 0;
            for (int i = 0; i < test_words.Length; i++)
            {
                char[] one_word = test_words[i].ToCharArray();
                foreach (char b in one_word)
                {
                    for (int m = 0; m < Alfavit_1.Length; m++)
                    {
                        if (b == Alfavit_1[m])
                        {
                            size_text_real++;
                            break;
                        }
                    }
                    //for (int t = 0; t < Alfavit_numbers.Length; t++)
                    //{
                    //    if (b == Alfavit_numbers[t])
                    //    {
                    //        size_text_real++;
                    //        break;
                    //    }
                    //}
                    size_text++;
                }
            }
            if (size_text == size_text_real)
                return true;
            else
                return false;
        }

        public int Language(string[] test_words, char[] Alfavit_1, char[] Alfavit_2)
        {
            int res = 9;

            for (int i = 0; i < test_words.Length; i++)
            {
                char[] one_word = test_words[i].ToCharArray();
                foreach (char b in one_word)
                {
                    for (int m = 0; m < Alfavit_1.Length; m++)
                    {
                        if (b == Alfavit_1[m])
                        {
                            res = 0;
                            return res;
                        }
                    }
                    for (int m = 0; m < Alfavit_2.Length; m++)
                    {
                        if (b == Alfavit_2[m])
                        {
                            res = 1;
                            return res;
                        }
                    }
                    //for (int t = 0; t < Alfavit_numbers.Length; t++)
                    //{
                    //    if (b == Alfavit_numbers[t])
                    //    {
                    //        res = 2;
                    //        break;
                    //    }
                    //}
                }
            }
            return res;
        }
        public int Proverka_keys(char[] Alfavit_1)
        {
            keys = textbox_keys.Text;
            int size_keys = 0;
            foreach (char h in keys)
                foreach (char k in Alfavit_1)
                    if (h == k)
                        size_keys++;

            return size_keys;
        }
    }
}




















