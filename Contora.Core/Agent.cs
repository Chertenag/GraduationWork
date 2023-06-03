namespace Contora.Core;

/// <summary>
/// Объект представления таблицы Agent в БД, который содержит дополнительную бизнес-логику.
/// </summary>
public class Agent
{
    private int id;
    private string firstName;
    private string lastName;
    private string? middleName;
    private int departmentId;
    private int positionId;
    private int rankId;
    private int statusId;
    private string? phone;
    private string? address;

    /// <summary>
    /// Initializes a new instance of the <see cref="Agent"/> class.
    /// </summary>
    public Agent()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Agent"/> class.
    /// </summary>
    /// <param name="id">ИД.</param>
    /// <param name="firstName">Имя.</param>
    /// <param name="lastName">Фамилия.</param>
    /// <param name="middleName">Отчество.</param>
    /// <param name="departmentId">ИД отдела.</param>
    /// <param name="positionId">ИД должности.</param>
    /// <param name="rankId">ИД звания.</param>
    /// <param name="statusId">ИД статуса.</param>
    /// <param name="phone">Телефон.</param>
    /// <param name="address">Адрес.</param>
    public Agent(int id, string firstName, string lastName, string? middleName, int departmentId, int positionId, int rankId, int statusId, string? phone, string? address)
    {
        this.Id = id;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.MiddleName = middleName;
        this.DepartmentId = departmentId;
        this.PositionId = positionId;
        this.RankId = rankId;
        this.StatusId = statusId;
        this.Phone = phone;
        this.Address = address;
    }

    /// <summary>
    /// Gets or sets ИД в БД.
    /// </summary>
    public int Id { get => this.id; set => this.id = value; }

    /// <summary>
    /// Gets or sets Имя.
    /// </summary>
    public string FirstName
    {
        get => this.firstName;
        set
        {
            this.firstName = string.IsNullOrEmpty(value) ? throw new ArgumentException("Имя не должно быть пустым.") :
                value.Length > 50 ? throw new ArgumentException("Имя должно быть менее 50 символов.") : value;
        }
    }

    /// <summary>
    /// Gets or sets Фамилия.
    /// </summary>
    public string LastName
    {
        get => this.lastName;
        set
        {
            this.lastName = string.IsNullOrEmpty(value) ? throw new ArgumentException("Фамилия не должна быть пустой.") :
                value.Length > 50 ? throw new ArgumentException("Фамилия должна быть не более 50 символов.") : value;
        }
    }

    /// <summary>
    /// Gets or sets Отчество.
    /// </summary>
    public string? MiddleName
    {
        get => this.middleName;
        set
        {
            this.middleName = string.IsNullOrEmpty(value) ? null :
                value.Length > 50 ? throw new ArgumentException("Отчество должно быть не более 50 символов.") : value;
        }
    }

    /// <summary>
    /// Gets or sets ИД отдела.
    /// </summary>
    public int DepartmentId
    {
        get => this.departmentId;
        set
        {
            this.departmentId = value < 1 ? throw new ArgumentException("ID отдела не может быть 0 или отрицательным.") : value;
        }
    }

    /// <summary>
    /// Gets or sets ИД должности.
    /// </summary>
    public int PositionId
    {
        get => this.positionId;
        set
        {
            this.positionId = value < 1 ? throw new ArgumentException("ID должности не может быть 0 или отрицательным.") : value;
        }
    }

    /// <summary>
    /// Gets or sets ИД звания.
    /// </summary>
    public int RankId
    {
        get => this.rankId;
        set
        {
            this.rankId = value < 1 ? throw new ArgumentException("ID звания не может быть 0 или отрицательным.") : value;
        }
    }

    /// <summary>
    /// Gets or sets ИД статуса.
    /// </summary>
    public int StatusId
    {
        get => this.statusId;
        set
        {
            this.statusId = value < 1 ? throw new ArgumentException("ID статуса не может быть 0 или отрицательным.") : value;
        }
    }

    /// <summary>
    /// Gets or sets Телефон.
    /// </summary>
    public string? Phone
    {
        get => this.phone;
        set
        {
            this.phone = string.IsNullOrEmpty(value) ? null :
                value.Length > 20 ? throw new ArgumentException("Телефон должен быть менее 20 сиволов.") : value;
        }
    }

    /// <summary>
    /// Gets or sets Адрес.
    /// </summary>
    public string? Address
    {
        get => this.address;
        set
        {
            this.address = string.IsNullOrEmpty(value) ? null :
                value.Length > 100 ? throw new ArgumentException("Адрес должен быть менее 100 сиволов.") : value;
        }
    }

    /// <summary>
    /// Добавление в БД новой записи.
    /// </summary>
    /// <param name="agent">Объект <see cref="Agent"/> класса.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns> <see cref="Task"/>.</returns>
    public static async Task Create_async(Agent agent, CancellationToken token)
    {
        // В БД поле ID автоинкремент.
        agent.Id = 0;
        await Data.Agent.Create_async(MapperToDataAgent(agent), token);
    }

    /// <summary>
    /// Запрос на чтение всех записей из таблицы Agent.
    /// </summary>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<List<Agent>> Read_All_async(CancellationToken token)
    {
        var rez = await Data.Agent.Read_All_async(token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    /// <summary>
    /// Поиск записей в таблице Agent по ИД.
    /// </summary>
    /// <param name="id">ИД в БД.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<List<Agent>> Read_ById_async(int id, CancellationToken token)
    {
        var rez = await Data.Agent.Read_ById_async(id, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    /// <summary>
    /// Поиск записей в таблице Agent по имени.
    /// </summary>
    /// <param name="fName">Имя.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<List<Agent>> Read_ByFirstName_async(string fName, CancellationToken token)
    {
        var rez = await Data.Agent.Read_ByFirstName_async(fName, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    /// <summary>
    /// Поиск записей в таблице Agent по фамилии.
    /// </summary>
    /// <param name="lName">Фамилия.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<List<Agent>> Read_ByLastName_async(string lName, CancellationToken token)
    {
        var rez = await Data.Agent.Read_ByLastName_async(lName, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    /// <summary>
    /// Поиск записей в таблице Agent по полному имени.
    /// </summary>
    /// <param name="fName">Имя.</param>
    /// <param name="lName">Фамилия.</param>
    /// <param name="mName">Отчество.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<List<Agent>> Read_ByFullName_async(string fName, string lName, string? mName, CancellationToken token)
    {
        if (string.IsNullOrEmpty(fName))
        {
            throw new ArgumentException($"Имя не может быть неопределенным или пустым.", nameof(fName));
        }

        if (string.IsNullOrEmpty(lName))
        {
            throw new ArgumentException($"Фамилия не может быть неопределенной или пустой.", nameof(lName));
        }

        var rez = await Data.Agent.Read_ByFullName_async(fName, lName, mName, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    /// <summary>
    /// Поиск записей в таблице Agent по ИД отдела.
    /// </summary>
    /// <param name="depId">ИД отдела.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<List<Agent>> Read_ByDepartmentId_async(int depId, CancellationToken token)
    {
        var rez = await Data.Agent.Read_ByDepartmentId_async(depId, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    /// <summary>
    /// Поиск записей в таблице Agent по ИД должности.
    /// </summary>
    /// <param name="posId">ИД должности.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<List<Agent>> Read_ByPositionId_async(int posId, CancellationToken token)
    {
        var rez = await Data.Agent.Read_ByPositionId_async(posId, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    /// <summary>
    /// Поиск записей в таблице Agent по ИД статуса.
    /// </summary>
    /// <param name="statusId">ИД статуса.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<List<Agent>> Read_ByStatusId_async(int statusId, CancellationToken token)
    {
        var rez = await Data.Agent.Read_ByStatusId_async(statusId, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    /// <summary>
    /// Обновление выбраной записи в таблице Agent.
    /// </summary>
    /// <param name="agent">Объект типа Agent, который необходимо обновить в БД.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public static async Task Update_async(Agent agent, CancellationToken token)
    {
        await Data.Agent.Update_async(MapperToDataAgent(agent), token);
    }

    /// <summary>
    /// Удаление записи в таблице Agent по ИД.
    /// </summary>
    /// <param name="id">ИД в БД.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public static async Task Delete_ById_async(int id, CancellationToken token)
    {
        await Data.Agent.Delete_ById_async(id, token);
    }

    /// <summary>
    /// Мапинг объекта Agent из Data в Core уровень.
    /// </summary>
    /// <param name="value">Объект типа <see cref="Data.Agent"/>.</param>
    /// <returns>Объект типа <see cref="Agent"/>.</returns>
    private static Agent MapperFromDataAgent(Data.Agent value)
    {
        return new Core.Agent(
            value.Id,
            value.FirstName,
            value.LastName,
            value.MiddleName,
            value.DepartmentId,
            value.PositionId,
            value.RankId,
            value.StatusId,
            value.Phone,
            value.Address);
    }

    /// <summary>
    /// Мапинг объекта Agent из Core в Data уровень.
    /// </summary>
    /// <param name="value">Объект типа <see cref="Agent"/>.</param>
    /// <returns>Объект типа <see cref="Data.Agent"/>.</returns>
    private static Data.Agent MapperToDataAgent(Core.Agent value)
    {
        return new Data.Agent(
            value.Id,
            value.FirstName,
            value.LastName,
            value.MiddleName,
            value.DepartmentId,
            value.PositionId,
            value.RankId,
            value.StatusId,
            value.Phone,
            value.Address);
    }
}
