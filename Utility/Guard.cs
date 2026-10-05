using System.Reflection.Metadata.Ecma335;

namespace IrzGuardPro.Utility
{
    /// <summary>
    /// Защитник, будет генерировать пароль, а также проверять переданный код.
    /// </summary>
    public static class Guard
    {
        #region Поля и свойсвта

        private static int pass = 000;

        /// <summary>
        /// Пароль.
        /// </summary>
        public static int Pass
        {
            get => pass;
            set
            {
                if (int.IsPositive(value) && value <= 999)
                {
                    pass = value;
                }
                else
                    pass = 000;
            }
        }

        /// <summary>
        /// Ключ доступа.
        /// </summary>
        private static string Key;

        #endregion

        #region  Методы 

        /// <summary>
        /// Генератор пароля.
        /// </summary>
        /// <param name="LevelAccess">Уровень доступа (0..2)</param>
        /// <param name="dateTime">Дата и время для генерации</param>
        /// <returns> Трехзначный пароль если есть такой уровень доступа, иначе 0.</returns>
        public static int GeneratePass(int LevelAccess, DateTime dateTime)
        {
         
            if(LevelAccess < 0)
                return 0;
            int Key_XOR = GenerationCode(LevelAccess, dateTime);
            //XOR с ключом.
            Pass = Key_XOR;

            return Pass;
        }

        /// <summary>
        /// Генерирует код доступа по алгоритму.
        /// </summary>
        /// <param name="LevelAccess">Уровень доступа.</param>
        /// <param name="dateTime">Дата и время, на какое время был запрос.</param>
        /// <returns>код доступа.</returns>
        public static int GenerationCode(int LevelAccess, DateTime dateTime) 
        {
            if (LevelAccess < 0 || LevelAccess >= 3 || dateTime == DateTime.UnixEpoch)
                return 000;

            int Base = (dateTime.Date.Year % 100 * dateTime.Date.Month * dateTime.Day * dateTime.Hour) % 1000;
            int Key = (dateTime.Date.Year % 100 + dateTime.Date.Month + dateTime.Day + dateTime.Hour) % 1000;

            //Сдвиг по уровню доступа
            int ditgit1 = (Base / 100 + LevelAccess) % 10;
            int ditgit2 = (Base / 10 % 10 + LevelAccess) % 10;
            int ditgit3 = (Base % 10 + LevelAccess) % 10;

            int Key_XOR = (ditgit1 * 100 + ditgit2 * 10 + ditgit3 ^ Key) % 1000;

            if (Key_XOR < 100)
                Key_XOR += 100;
            return Key_XOR;
        }

        /// <summary>
        /// Шифрования кода устройства. Ответ на первый код.
        /// </summary>
        /// <param name="text">текс для шифрования. Код устройства.</param>
        /// <returns>Ответ на кодустройства.</returns>
        private static string EncryptSecondaryCode(string text)
        {
            if (text.Length == 5 || text.Length == 4)
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
        /// Проверяет переданную строку с ключем.
        /// </summary>
        /// <param name="text">Проверяймая строка.</param>
        /// <returns>True, если строка совпадает с ключом иначе, false.</returns>
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
        /// Проверяет переданную строку с ключем.
        /// </summary>
        /// <param name="text">Проверяймая строка.</param>
        /// <param name="key">Полученный ключ.</param>
        /// <returns>True, если строка совпадает с ключом иначе, false.</returns>
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
        /// Метод шифрудет данные с указаным публичным  ключем. 
        /// </summary>
        /// <param name="text">Текс для расшидрования</param>
        /// <returns>Закодированная строка в формате 4 значного числа (0000).</returns>
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

        /// <summary>
        /// Проверка пароля.
        /// </summary>
        /// <param name="dateTime">Время.</param>
        /// <param name="CheckingPass">Проверяемый пароль.</param>
        /// <param name="LevelAccess">Уровень доступа полученный.</param>
        /// <returns>True если пароль прошел проверку, иначе False.</returns>
        public static bool CheckPass(DateTime dateTime, int CheckingPass, out int LevelAccess)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Equals(GeneratePass(i, dateTime), CheckingPass))
                {
                    LevelAccess = i;
                    return true;
                }
            }
            LevelAccess = -1;
            return false;
        }

        /// <summary>
        /// Возвращает пароль в трехзначном формате ("000").
        /// </summary>
        /// <returns>Пароль.</returns>
        public static string GetPass()
        {
            return string.Format("{0:d3}", Pass);
        }

        #endregion
    }
}
