namespace Contora.Core;

/// <summary>
/// Объект представления таблицы Target в БД, который содержит дополнительную бизнес-логику.
/// </summary>
public class Target
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Target"/> class.
    /// </summary>
    /// <param name="id">ИД.</param>
    /// <param name="firstName">Имя.</param>
    /// <param name="lastName">Фамилия.</param>
    /// <param name="middleName">Отчество.</param>
    /// <param name="caseId">ИД дела.</param>
    /// <param name="phone">Телефон.</param>
    /// <param name="birthdate">Дата рождения.</param>
    /// <param name="address">Адрес.</param>
    /// <param name="additionalInfo">Дополнительная информация.</param>
    public Target(int id, string firstName, string lastName, string? middleName, int caseId, string? phone, DateOnly? birthdate, string? address, string? additionalInfo)
    {
        this.Id = id;
        this.FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        this.LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        this.MiddleName = middleName;
        this.CaseId = caseId;
        this.Phone = phone;
        this.Birthdate = birthdate;
        this.Address = address;
        this.AdditionalInfo = additionalInfo;
    }

    /// <summary>
    /// Gets or sets иД в БД.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets Имя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets Фамилию.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets Отчество.
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Gets or sets ИД дела.
    /// </summary>
    public int CaseId { get; set; }

    /// <summary>
    /// Gets or sets телефон.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets дату рождения.
    /// </summary>
    public DateOnly? Birthdate { get; set; }

    /// <summary>
    /// Gets or sets адрес.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets дополнительную информацию.
    /// </summary>
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// Добавление в БД новой записи.
    /// </summary>
    /// <param name="target">Объект <see cref="Target"/> класса.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public static async Task Create_async(Core.Target target, CancellationToken token)
    {
        // В БД поле ID автоинкремент.
        target.Id = 0;
        await Data.Target.Create_async(MapperToData(target), token);
    }

    /// <summary>
    /// Запрос на чтение всех записей из таблицы Target.
    /// </summary>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<List<Core.Target>> Read_All_async(CancellationToken token)
    {
        var rez = await Data.Target.Read_All_async(token);
        return rez.Select(MapperFromData).ToList();
    }

    /// <summary>
    /// Поиск записей в таблице Target по ИД.
    /// </summary>
    /// <param name="id">ИД в БД.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<List<Core.Target>> Read_ById_async(int id, CancellationToken token)
    {
        var rez = await Data.Target.Read_ById_async(id, token);
        return rez.Select(MapperFromData).ToList();
    }

    /// <summary>
    /// Обновление выбраной записи в таблице Target.
    /// </summary>
    /// <param name="target">Объект типа <see cref="Target"/>, который необходимо обновить в БД.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public static async Task Update_async(Core.Target target, CancellationToken token)
    {
        await Data.Target.Update_async(MapperToData(target), token);
    }

    /// <summary>
    /// Удаление записи в таблице Target по ИД.
    /// </summary>
    /// <param name="id">ИД в БД.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public static async Task Delete_ById_async(int id, CancellationToken token)
    {
        await Data.Target.Delete_ById_async(id, token);
    }

    /// <summary>
    /// Мапинг объекта Target из Data в Core уровень.
    /// </summary>
    /// <param name="value">Объект типа <see cref="Data.Target"/>.</param>
    /// <returns>Объект типа <see cref="Target"/>.</returns>
    private static Core.Target MapperFromData(Data.Target value)
    {
        return new Target(
            value.Id,
            value.FirstName,
            value.LastName,
            value.MiddleName,
            value.CaseId,
            value.Phone,
            value.Birthdate,
            value.Address,
            value.AdditionalInfo);
    }

    /// <summary>
    /// Мапинг объекта Target из Core в Data уровень.
    /// </summary>
    /// <param name="value">Объект типа <see cref="Target"/>.</param>
    /// <returns>Объект типа <see cref="Data.Target"/>.</returns>
    private static Data.Target MapperToData(Core.Target value)
    {
        return new Data.Target(
            value.Id,
            value.FirstName,
            value.LastName,
            value.MiddleName,
            value.CaseId,
            value.Phone,
            value.Birthdate,
            value.Address,
            value.AdditionalInfo);
    }
}
