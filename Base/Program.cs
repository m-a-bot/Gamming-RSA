using Base;
using System.Text;

int key = 0b010011;

string numbers = "0123456789"; //1

//string rusAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

//string engAlphabet = "abcdefghijklmnopqrstuvwxyz";

//var encoding = Encoding.UTF8;

//byte[] bytes = encoding.GetBytes(engAlphabet+ rusAlphabet+ numbers);

//byte[] encrypted = new byte[bytes.Length];

//byte[] decrypted = new byte[bytes.Length];

//for (int i=0; i<bytes.Length; i++)
//{
//    encrypted[i] = (byte)(bytes[i] ^ key);
//}

//for (int i = 0; i < bytes.Length; i++)
//{
//    decrypted[i] = (byte)(encrypted[i] ^ key);
//}

//string decryptedText = Encoding.UTF8.GetString(decrypted);

//for (int i = 0; i < decryptedText.Length; i++)
//{
//    var item = bytes[i] - decrypted[i];
//    Console.Write(decryptedText[i]);// Convert.ToString(item, 2).PadLeft(8, '0'));
//}

int[] list = { 1, 2, 3, 4, 5, 6, 7, 8 };

int[] result = Tools.Sample(list, 4);

foreach (var item in result)
{
    Console.WriteLine(item);
}