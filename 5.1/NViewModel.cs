using Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GammingCipher
{
    public class NViewModel : INotifyPropertyChanged
    {
        private string bitsSource;

        private string bitsKey;

        private string _source = "";

        public static string CurrentAlphabet { get; set; } = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя0123456789";

        public string SourceText
        {
            get { return _source; }
            set
            {
                _source = value;

                byte[] bSource;

                Tools.GetBytes(_source.ToLower(), out bSource, CurrentAlphabet);

                BinarySourceText = Tools.GetBitsString(bSource);
                

                OnPropertyChanged();
            }
        }

        public string BinarySourceText
        {
            get {

                return bitsSource; 
            }

            set
            {
                bitsSource = value;
                OnPropertyChanged();
            }

        }

        private string _key = "";

        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;

                byte[] bKey;

                Tools.GetBytes(_key.ToLower(), out bKey, CurrentAlphabet);

                BinaryKey = Tools.GetBitsString(bKey);

                OnPropertyChanged();
            }
        }

        public string BinaryKey
        {
            get
            {

                return bitsKey;
            }

            set
            {
                bitsKey = value;
                OnPropertyChanged();
            }

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
