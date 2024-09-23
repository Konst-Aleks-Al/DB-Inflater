using App.Helpers;
using App.Models;
using App.Serializers;

namespace App {

    internal class Program {

        private static string? _input;
        private static byte _readMode;
        private static readonly byte[] READ_MODES = [1, 2];


        static void Main(string[] args) {

            // Инициализируем приложение в соответствии с режимом запуска.
            // 0 - Инициализация в интерактивном режиме. При запуске с ярлыка, исполняемого файла или консоль без ввода параметров.
            // default - Инициализация в автономном режиме. При открытии файлов или папок с помощью ярлыка или исполняемого файла,
            // а также при запуске через консоль с вводом параметров.
            switch (args.Length) {

                case 0:
                    CleanInit();
                    break;

                default:
                    InitWithParams(args);
                    break;
            }
        }


        /// <summary>
        /// Инициализирует приложение в интерактивном режиме.
        /// </summary>
        private static void CleanInit() {

            // Запрашиваем выбор режима чтения файлов
            _input = GetReadMode();

            // Валидируем инпут режима чтения.
            // При некорректном инпуте запрашиваем повторно.
            while (!TryParseReadMode() || !ValidateReadMode()) {

                Console.WriteLine("Недопустимый режим чтения.");
                _input = GetReadMode();
            };

            // Запрашиваем путь к файл(у/ам)
            string path = GetPath();

            // Валидируем инпут адреса файла/директории.
            // При некорректном инпуте запрашиваем повторно.
            while (!ValidatePath(path)) {

                Console.WriteLine($"Указанный {((_readMode == 1) ? "файл" : "директория")} не существует.");
                path = GetPath();
            }

            // Запускаем парсер в соответствии с режимом чтения
            switch (_readMode) {

                case 1:
                    ProcessFile(path);
                    break;

                case 2:
                    ProcessFolder(path);
                    break;
            }
        }


        /// <summary>
        /// Запрашивает пользователя выбрать режим чтения.
        /// </summary>
        /// <returns> Значение режима чтения типа <see langword="string"/>. </returns>
        private static string GetReadMode() {

            Console.WriteLine("Выберите режим чтения:\nОдин файл - введите '1'\nДиректория - введите '2'");
            return Console.ReadLine()!;
        }


        /// <summary>
        /// Проверяет, поддаётся ли ввод пользователя конвератции в режим чтения.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> - если ввод пользователя конвертируется успешно,
        ///     <see langword="false"/> - в ином случае.
        /// </returns>
        private static bool TryParseReadMode() {

            return byte.TryParse(_input, out _readMode);
        }


        /// <summary>
        /// Проверяет, является ли введённый режим чтения допустимым.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> - если режим представлен в списке допустимых вариантов,
        ///     <see langword="false"/> - в ином случае.
        /// </returns>
        private static bool ValidateReadMode() {

            return READ_MODES.Contains(_readMode);
        }


        /// <summary>
        /// Запрашивает у пользователя ввод абсолютного пути до файла/директории.
        /// </summary>
        /// <returns> Адрес XML файл(а/ов) типа <see langword="string"/>. </returns>
        private static string GetPath() {

            Console.WriteLine($"Введите путь до {((_readMode == 1) ? "файла" : "директории")}");
            return Console.ReadLine()!;
        }


        /// <summary>
        /// Проверяет, сущесвует ли файла/директории по указанному адресу.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> - если файл/директория по указанному пути существует,
        ///     <see langword="false"/> - в ином случае.
        /// </returns>
        private static bool ValidatePath(string path) {

            if (_readMode == 1 && File.Exists(path)) {
                return true;
            }

            if (_readMode == 2 && Directory.Exists(path)) {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Инициализирует приложение в автономом режиме.
        /// </summary>
        private static void InitWithParams(string[] args) {

            // Проверка на запрос подсказки к параметрам запуска
            if (HelpRequired(args)) {

                ShowHelp();
                return;
            }

            // Объявляем поля для валидации и сортировки адресов
            List<string> validPaths = [];
            List<string> filePaths = [];
            List<string> directoryPaths = [];

            // Перебираем параметры, проверяем валидность адресов
            foreach (string arg in args) {

                if (!ValidateParam(arg)) {
                    Console.WriteLine($"{arg} не является параметром, адресом файла или директории, и был пропущен.");
                    continue;
                };

                validPaths.Add(arg);
            }

            // Фильтруем адреса по спискам
            FilterPaths(validPaths, ref filePaths, ref directoryPaths);

            // Запускаем обработку
            foreach (string path in filePaths) {
                ProcessFile(path);
            }
            foreach (string path in directoryPaths) {
                ProcessFolder(path);
            }
        }


        /// <summary>
        /// Проверяет, был ли произведён вызов параметра help в параметрах запуска.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> - если вызов был произведён,
        ///     <see langword="false"/> - в ином случае.
        /// </returns>
        private static bool HelpRequired(string[] args) {

            return args.Select(arg => arg.ToLowerInvariant()).Intersect(ApplicationParams.Help.Flags).Any();
        }


        /// <summary>
        /// Выводит в консоль информацию о параметрах запуска.
        /// </summary>
        private static void ShowHelp() {

            ApplicationParams.ParamList.ForEach(param => {

                Console.WriteLine(param.Name);
                Console.WriteLine(param.Description);

                foreach (string flag in param.Flags) {
                    Console.Write($"{flag} ");
                }

                Console.WriteLine();
            });

            Console.WriteLine("Параметрами могут выступать отделяемые пробелами адреса файлов или директорий в любом количестве и порядке.");
        }


        /// <summary>
        /// Проверяет, является ли указанный в качестве параметра адрес существующим файлом/директорией.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> - если адрес указывает на существующий файл/директорию,
        ///     <see langword="false"/> - в ином случае.
        /// </returns>
        private static bool ValidateParam(string arg) {

            return (File.Exists(arg) || Directory.Exists(arg));
        }


        /// <summary>
        /// Фильтрует адреса директорий и файлов.
        /// </summary>
        private static void FilterPaths(List<string> pathList, ref List<string> filesPaths, ref List<string> foldersPaths) {

            foreach (string path in pathList) {

                if (File.Exists(path)) {
                    filesPaths.Add(path);
                }

                if (Directory.Exists(path)) {
                    foldersPaths.Add(path);
                }
            }
        }


        /// <summary>
        /// Вызывает методы чтения, обработки и занесения данных в БД для указанного файла.
        /// </summary>
        private static void ProcessFile(string filePath) {

            if (!IsXml(filePath)) {
                Console.WriteLine($"Файл {filePath} не является Xml и был пропущен.");
                return;
            }

            // Парсим содержимое XML
            XmlOrderList? orders = XmlParser.TryParse(filePath);

            // Ранний возврат на отсутствии данных.
            if (orders is null) {
                return;
            }

            // Производим соединение с БД.
            using DatabaseContext db = new();

            // Преобразуем данные из Xml в классы данных с проверкой на их наличие в БД.
            XmlDbSetConverter converter = new(new DatabaseParser(db));
            DbSet data = converter.ConvertWithUniqueCheck(orders);

            // Проверяем, есть ли данные для обновления БД, вносим изменения и сохраняем.
            if (data is not null && !(data.IsEmpty())) {
                DatabaseInflater inflater = new(db);
                inflater.InsertData(data);
                inflater.SaveChanges();
            }
            
            // Удаляем из памяти процесс соединения с БД.
            db.Dispose();
        }


        /// <summary>
        /// Вызывает методы чтения, обработки и занесения данных в БД для всех XML файлов директории.
        /// </summary>
        private static void ProcessFolder(string folderPath) {

            // Получаем список адресов файлов директории
            var files = Directory.GetFiles(folderPath);

            foreach (var xml in files) {

                if (!IsXml(xml)) {
                    continue; 
                }

                ProcessFile(xml);
            }
        }


        /// <summary>
        /// Проверяет, является ли расширением файла XML.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> - для файлов с расширением XML,
        ///     <see langword="false"/> - в ином случае.
        /// </returns>
        private static bool IsXml(string filePath) {

            return Path.GetExtension(filePath).Equals(".xml", StringComparison.CurrentCultureIgnoreCase);
        } 
    }
}
