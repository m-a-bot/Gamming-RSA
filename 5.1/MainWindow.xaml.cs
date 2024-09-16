using Base;
using GammingCipher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _5._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TypeAlphabet currentTypeAlphabet;

        string numbers = "0123456789";

        string rusAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        string engAlphabet = "abcdefghijklmnopqrstuvwxyz";

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new NViewModel();
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            string alphabet = "";
            if (RusLanguage.IsChecked.Value)
            {
                currentTypeAlphabet = TypeAlphabet.Rus;
                alphabet = rusAlphabet + numbers;
            }
            if (EngLanguage.IsChecked.Value)
            {
                currentTypeAlphabet = TypeAlphabet.Eng;
                alphabet = engAlphabet + numbers;
            }

            string source = SourceText.Text;

            string lSource = source.ToLower();

            if (source == "")
            {
                MessageBox.Show("Нет текста");
                return;
            }


            if (currentTypeAlphabet == TypeAlphabet.Rus && Tools.AnyCharacterOfStringBelongToAlphabet(lSource, engAlphabet))
            {
                MessageBox.Show("Вводите текст только на русском языке");
                return;
            }
            if (currentTypeAlphabet == TypeAlphabet.Eng && Tools.AnyCharacterOfStringBelongToAlphabet(lSource, rusAlphabet))
            {
                MessageBox.Show("Вводите текст только на английском языке");
                return;
            }

            string key = Key.Text;

            string lKey = key.ToLower();

            if (key.Length == 0)
            {
                MessageBox.Show("Нет ключа");
                return;
            }

            if (!Tools.AllCharactersOfStringBelongToAlphabet(lKey, alphabet))
            {
                MessageBox.Show("Неправильный ключ");
                return;
            }

            byte[] keyBytes;
            Tools.GetBytes(lKey, out keyBytes, alphabet);

            GammingCryptographer gamming = new GammingCryptographer(keyBytes);

            byte[] sourceBytes;
            Tools.GetBytes(SourceText.Text, out sourceBytes, alphabet);

            byte[] encryptedBytes = gamming.EncryptBlockCouplingMode(sourceBytes);

            EncryptedText.Text = Tools.GetBitsString(encryptedBytes);

            MessageBox.Show("Текст зашифрован");
        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            string alphabet = "";
            if (RusLanguage.IsChecked.Value)
            {
                currentTypeAlphabet = TypeAlphabet.Rus;
                alphabet = rusAlphabet + numbers;
            }
            if (EngLanguage.IsChecked.Value)
            {
                currentTypeAlphabet = TypeAlphabet.Eng;
                alphabet = engAlphabet + numbers;
            }

            string encryptedText = EncryptedText.Text;

            if (encryptedText == "")
            {
                MessageBox.Show("Нет текста");
                return;
            }

            string key = Key.Text;

            string lKey = key.ToLower();

            if (key.Length == 0)
            {
                MessageBox.Show("Нет ключа");
                return;
            }

            if (!Tools.AllCharactersOfStringBelongToAlphabet(lKey, alphabet))
            {
                MessageBox.Show("Неправильный ключ");
                return;
            }

            byte[] keyBytes;
            Tools.GetBytes(lKey, out keyBytes, alphabet);

            GammingCryptographer gamming = new GammingCryptographer(keyBytes);

            byte[] encryptedBytes = Tools.BitsStringToBytes(EncryptedText.Text);

            byte[] decryptedBytes = gamming.DecryptBlockCouplingMode(encryptedBytes);

            BinaryDecryptedText.Text = Tools.GetBitsString(decryptedBytes);

            DecryptedText.Text = Tools.GetStringFromBytes(decryptedBytes, alphabet);

            MessageBox.Show("Текст расшифрован");
        }


        private void EngLanguage_Checked(object sender, RoutedEventArgs e)
        {
            NViewModel.CurrentAlphabet = engAlphabet + numbers;
        }

        private void RusLanguage_Checked(object sender, RoutedEventArgs e)
        {
            NViewModel.CurrentAlphabet = rusAlphabet + numbers;
        }
    }
}
