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

namespace GammingWithGenerator
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

            this.DataContext = new ViewModel();
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            int lengthBinaryString = BinarySourceText.Text.Length;

            if (lengthBinaryString == 0)
            {
                MessageBox.Show("Нет текста");
                return;
            }

            Generator generator = new Generator(lengthBinaryString);

            BinaryKey.Text = generator.Bits;
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

            if (BinaryKey.Text.Length == 0)
            {
                MessageBox.Show("Нет ключа");
                return;
            }

            byte[] key = Tools.BitsStringToBytes(BinaryKey.Text);

            GammingCryptographer gamming = new GammingCryptographer(key);

            byte[] sourceBytes;
            Tools.GetBytes(SourceText.Text, out sourceBytes);

            byte[] encryptedBytes = gamming.Encrypt(sourceBytes);

            EncryptedText.Text = Tools.GetBitsString(encryptedBytes);
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

            if (BinaryKey.Text.Length == 0)
            {
                MessageBox.Show("Нет ключа");
                return;
            }

            byte[] key = Tools.BitsStringToBytes(BinaryKey.Text);

            GammingCryptographer gamming = new GammingCryptographer(key);

            byte[] encryptedBytes = Tools.BitsStringToBytes(EncryptedText.Text);

            byte[] decryptedBytes = gamming.Decrypt(encryptedBytes);

            BinaryDecryptedText.Text = Tools.GetBitsString(decryptedBytes);

            DecryptedText.Text = Tools.GetStringFromBytes(decryptedBytes);
        }
    }
}
