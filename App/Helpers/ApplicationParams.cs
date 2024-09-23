namespace App.Helpers {

    public class ApplicationParams {

        private static readonly Param _help = new() {
            Name = "help",
            Flags = ["--help", "-help", "help"],
            Description = "Предоставляет перечень и описание всех доступных параметров запуска."
        };

        private static readonly List<Param> _paramsList = [_help];

        public static Param Help { get { return _help; } }
        public static List<Param> ParamList { get { return _paramsList; } }

        public class Param {

            public required string Name { get; set; }
            public required string[] Flags { get; set; }
            public required string Description { get; set; }

        }
    }
}