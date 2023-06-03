namespace Contora.Core
{
    /// <summary>
    /// Объект представления таблицы Case в БД, который содержит дополнительную бизнес-логику.
    /// </summary>
    public class Case
    {
        private int id;
        private int departmentId;
        private int primaryAgentId;
        private int? secondaryAgentId;
        private DateOnly dateOpen;
        private DateOnly? dateClose;

        /// <summary>
        /// Initializes a new instance of the <see cref="Case"/> class.
        /// </summary>
        public Case()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Case"/> class.
        /// </summary>
        /// <param name="id">ИД в БД.</param>
        /// <param name="departmentId">ИД отдела.</param>
        /// <param name="primaryAgentId">ИД основного агента.</param>
        /// <param name="secondaryAgentId">ИД второстепенного агента.</param>
        /// <param name="dateOpen">Дата открытия дела.</param>
        /// <param name="dateClose">Дата закрытия дела.</param>
        public Case(int id, int departmentId, int primaryAgentId, int? secondaryAgentId, DateOnly dateOpen, DateOnly? dateClose)
        {
            this.Id = id;
            this.DepartmentId = departmentId;
            this.PrimaryAgentId = primaryAgentId;
            this.SecondaryAgentId = secondaryAgentId;
            this.DateOpen = dateOpen;
            this.DateClose = dateClose;
        }

        /// <summary>
        /// Gets or sets ИД в БД.
        /// </summary>
        public int Id { get => this.id; set => this.id = value; }

        /// <summary>
        /// Gets or sets ИД отдела.
        /// </summary>
        public int DepartmentId
        {
            get => this.departmentId;
            set => this.departmentId = value < 1 ? throw new ArgumentException("ID отдела не может быть 0 или отрицательным.") : value;
        }

        /// <summary>
        /// Gets or sets ИД основного агента.
        /// </summary>
        public int PrimaryAgentId
        {
            get => this.primaryAgentId;
            set => this.primaryAgentId = value < 1 ? throw new ArgumentException("ID агента не может быть 0 или отрицательным.") : value;
        }

        /// <summary>
        /// Gets or sets ИД второстепенного агента.
        /// </summary>
        public int? SecondaryAgentId
        {
            get => this.secondaryAgentId;
            set => this.secondaryAgentId = value == null ? null : value < 1 ? throw new ArgumentException("ID агента не может быть 0 или отрицательным.") : value;
        }

        /// <summary>
        /// Gets or sets дата открытия дела.
        /// </summary>
        public DateOnly DateOpen
        {
            get => this.dateOpen;
            set => this.dateOpen = value > DateOnly.FromDateTime(DateTime.Now) ? throw new ArgumentException("Дата открытия дела не может быть позже текущей даты.") : value;
        }

        /// <summary>
        /// Gets or sets дата закрытия дела.
        /// </summary>
        public DateOnly? DateClose
        {
            get => this.dateClose;
            set => this.dateClose = value < this.DateOpen ? throw new ArgumentException("Дата закрытия дела не может быть ранеше даты октрытия дела.")
                : value > DateOnly.FromDateTime(DateTime.Now) ? throw new ArgumentException("Дата закрытия дела не может быть позже текущей даты.") : value;
        }

        /// <summary>
        /// Добавление в БД новой записи.
        /// </summary>
        /// <param name="value">Объект <see cref="Case"/> класса, котрый необходимо добавить в БД.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task Create_async(Case value, CancellationToken token)
        {
            // В БД поле ID автоинкремент.
            value.Id = 0;
            await Data.Case.Create_async(MapperToDataCase(value), token);
        }

        /// <summary>
        /// Запрос на чтение всех записей из таблицы Case.
        /// </summary>
        /// <param name="token">Токен отмены.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<List<Case>> Read_All_async(CancellationToken token)
        {
            var rez = await Data.Case.Read_All_async(token);
            return rez.Select(MapperFromDataCase).ToList();
        }

        /// <summary>
        /// Поиск записей в таблице Case по ИД.
        /// </summary>
        /// <param name="id">ИД в БД.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<List<Case>> Read_ById_async(int id, CancellationToken token)
        {
            var rez = await Data.Case.Read_ById_async(id, token);
            return rez.Select(MapperFromDataCase).ToList();
        }

        /// <summary>
        /// Поиск записей в таблице Case по ИД основного или второстепенного агента.
        /// </summary>
        /// <param name="agentId">ИД агента.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<List<Case>> Read_ByBothAgentId_async(int agentId, CancellationToken token)
        {
            var rez = await Data.Case.Read_ByBothAgentId_async(agentId, token);
            return rez.Select(MapperFromDataCase).ToList();
        }

        /// <summary>
        /// Обновление выбраной записи в таблице Case.
        /// </summary>
        /// <param name="value">Объект типа Case, который необходимо обновить в БД.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task Update_async(Case value, CancellationToken token)
        {
            await Data.Case.Update_async(MapperToDataCase(value), token);
        }

        /// <summary>
        /// Закрывает дело по ИД указанной датой.
        /// </summary>
        /// <param name="id">ИД в БД.</param>
        /// <param name="closeDate">Дата закрытия дела.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task Update_CloseCase_async(int id, DateOnly closeDate, CancellationToken token)
        {
            var rez = await Data.Case.Read_ById_async(id, token);
            if (rez.Count > 0)
            {
                await Data.Case.Update_CloseCase_async(rez[0], closeDate, token);
            }
        }

        /// <summary>
        /// Закрывает дело по ИД указанной датой.
        /// </summary>
        /// <param name="id">ИД в БД.</param>
        /// <param name="closeDate">Дата закрытия дела.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task Update_CloseCase_async(int id, DateTime closeDate, CancellationToken token)
        {
            var rez = await Data.Case.Read_ById_async(id, token);
            if (rez.Count > 0)
            {
                await Data.Case.Update_CloseCase_async(rez[0], DateOnly.FromDateTime(closeDate), token);
            }
        }

        /// <summary>
        /// Удаление записи в таблице Case по ИД.
        /// </summary>
        /// <param name="id">ИД в БД.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task Delete_ById_async(int id, CancellationToken token)
        {
            await Data.Case.Delete_ById_async(id, token);
        }

        /// <summary>
        /// Мапинг объекта Case из Data в Core уровень.
        /// </summary>
        /// <param name="value">Объект типа <see cref="Data.Case"/>.</param>
        /// <returns>Объект типа <see cref="Case"/>.</returns>
        public static Case MapperFromDataCase(Data.Case value)
        {
            return new Case(value.Id, value.DepartmentId, value.PrimaryAgentId, value.SecondaryAgentId, value.DateOpen, value.DateClose);
        }

        /// <summary>
        /// Мапинг объекта Case из Core в Data уровень.
        /// </summary>
        /// <param name="value">Объект типа <see cref="Case"/>.</param>
        /// <returns>Объект типа <see cref="Data.Case"/>.</returns>
        public static Data.Case MapperToDataCase(Case value)
        {
            return new Data.Case(value.Id, value.DepartmentId, value.PrimaryAgentId, value.SecondaryAgentId, value.DateOpen, value.DateClose);
        }
    }
}
