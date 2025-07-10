
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IrzGuardPro.Utility
{
    public static class RSAcrypt
    {
        #region Поля и свойства

        /// <summary>
        /// Ключ доступа.
        /// </summary>
        private static string Key;

        #endregion

        #region Методы

        /// <summary>
        /// Шифрования кода устройства. Ответ на первый код.
        /// </summary>
        /// <param name="text">текс для шифрования. Код устройства.</param>
        /// <returns>Ответ на кодустройства.</returns>
        private static string EncryptSecondaryCode(string text)
        {
            if (text.Length == 5 || text.Length == 4 )
            {
                int a = text[0] - '0';
                int b = text[1] - '0';
                int c = text[2] - '0';
                int d = text[3] - '0';

                // Произвольное преобразование (можно изменить)
                int part1 = (a * b + c * d) % 100;
                int part2 = (a + b + c + d) % 100;

                return $"{part1:D2}{part2:D2}"; // Объединяем в 4 цифры
            }
            else 
            {
                return "-1";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool EqualsKey(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(Key.Trim())) 
                return false;

            if (text.Equals(EncryptSecondaryCode(Key.Trim())))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool EqualsKey(string text, string key)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(key.Trim()))
                return false;

            if (EncryptSecondaryCode(text).Equals(EncryptSecondaryCode(key.Trim())))
                return true;
            else
                return false;
        }


        /// <summary>
        /// Метод шифрудет данные с указаным публичным  ключем
        /// </summary>
        /// <param name="text">Текс для расшидрования</param>
        /// <returns>Закодированная строка.</returns>
        public static string Encrypt(string text)
        {
            text = text.ToLower();
            int total = 0;

            foreach (char c in text)
            {
                int num;
                if (char.IsDigit(c))
                {
                    num = c - '0';
                }
                else if (char.IsLetter(c))
                {
                    num = c - 'a' + 10;
                }
                else
                {
                    continue; // Пропускаем недопустимые символы
                }
                total = (total * 33 + num) % 10000; // Хеширование
            }
            string code = string.Format("{0:d4}", total);
            Key = code;
            return code;
        }

        #endregion

        #region Констуркторы

        //public RSAcrypt() 
        //{

        //}

        #endregion
    }
}
