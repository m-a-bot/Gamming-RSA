using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
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

namespace RSA
{
    public enum TypeAlphabet
    {
        Rus,
        Eng
    }

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
        }

        private async void buttonGenerate_Click(object sender, RoutedEventArgs e)
        {
            var pqL = pqLength.Text;

            if (pqL == string.Empty || !Tools.AllCharactersOfStringBelongToAlphabet(pqL, numbers))
            {
                MessageBox.Show("Неправильная запись количества битов");
                return;
            }

            int lengthInteger = int.Parse(pqL);

            int pass = 256;

            BigInteger p, q, n, phi, publicE, privateE;

            var findSimpleInteger = async (int lengthInteger, int pass) => {

                return await Task.Run(() =>
                {
                    BigInteger integer = Tools.Rand(lengthInteger);
                    while (!Tools.TestMillerRabin(integer, lengthInteger, pass))
                        integer = Tools.Rand(lengthInteger);

                    return integer;
                });
                
            };

            var result = await Task.WhenAll(findSimpleInteger(lengthInteger, pass), findSimpleInteger(lengthInteger, pass));

            p = result[0];
            q = result[1];

            n = p * q;

            phi = (p - 1) * (q - 1);

            pSimp.Text = p.ToString();

            qSimp.Text = q.ToString();

            nSimp.Text = n.ToString();

            phiSimp.Text = phi.ToString();

            var keys = await FindED(lengthInteger/3, phi);

            publicE = keys[0];
            privateE = keys[1];

            publicKeyText.Text = publicE.ToString();
            secretKeyText.Text = privateE.ToString();

            MessageBox.Show("Ok");
        }

        private async Task<BigInteger[]> FindED(int lengthInteger, BigInteger phi)
        {
            return await Task.Run(() =>
            {
                // e
                BigInteger publicKey = Tools.Rand(lengthInteger);
                // d
                BigInteger privateKey, nod;

                nod = Tools.EnhancedGCD(publicKey, phi, out privateKey);
                while (1 >= publicKey || publicKey >= phi || nod != 1)
                {
                    publicKey = Tools.Rand(lengthInteger);
                    nod = Tools.EnhancedGCD(publicKey, phi, out privateKey);
                }

                if (privateKey < 0)
                    privateKey += phi;

                return new BigInteger[] {
                publicKey,
                privateKey
                };
            });
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
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

            string NT = nSimp.Text,
                publicKeyT = this.publicKeyText.Text;

            if (publicKeyT == string.Empty || ! Tools.AllCharactersOfStringBelongToAlphabet(publicKeyT, numbers))
            {
                MessageBox.Show("Нет открытого ключа");
                return;
            }
            

            BigInteger N = BigInteger.Parse(NT),
                publicKey = BigInteger.Parse(publicKeyT);


            string _bInt = Tools.BigIntFromString(lSource, alphabet);
            BigInteger bigIntSource = BigInteger.Parse(_bInt);

            BigInteger C = RSACryptographer.Encrypt(bigIntSource, publicKey, N);

            EncryptedText.Text = C.ToString();
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
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

            if (encryptedText == string.Empty || !Tools.AllCharactersOfStringBelongToAlphabet(encryptedText, numbers))
            {
                MessageBox.Show("Нет шифротекста");
                return;
            }

            string NT = nSimp.Text,
                secretKeyT = this.secretKeyText.Text;

            if (secretKeyT == string.Empty || !Tools.AllCharactersOfStringBelongToAlphabet(secretKeyT, numbers))
            {
                MessageBox.Show("Нет секретного ключа");
                return;
            }

            BigInteger N = BigInteger.Parse(NT),
            secretKey = BigInteger.Parse(secretKeyT);


            BigInteger C = BigInteger.Parse(encryptedText);

            string decryptedSource = RSACryptographer.Decrypt(C, secretKey, N).ToString();

            var bukovky = (string source, string alphabet, int length) => { 
                
                string result = string.Empty;

                for (int shift = 0; shift < source.Length; shift+= length )
                {
                    try
                    {
                        int index = int.Parse(source.Substring(shift, length)) % alphabet.Length;

                        result += alphabet[index];

                    } catch { }
                }

                return result;
            };

            DecryptedText.Text = bukovky(decryptedSource.Substring(1, decryptedSource.Length-1), alphabet, 2);
        }
    }
}
