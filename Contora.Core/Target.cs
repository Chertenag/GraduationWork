namespace Contora.Core;

public class Target
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Target"/> class.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="middleName"></param>
    /// <param name="caseId"></param>
    /// <param name="phone"></param>
    /// <param name="birthdate"></param>
    /// <param name="address"></param>
    /// <param name="additionalInfo"></param>
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

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public int CaseId { get; set; }
    public string? Phone { get; set; }
    public DateOnly? Birthdate { get; set; }
    public string? Address { get; set; }
    public string? AdditionalInfo { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fName"></param>
    /// <param name="lName"></param>
    /// <param name="mName"></param>
    /// <param name="caseId"></param>
    /// <param name="phone"></param>
    /// <param name="bDay"></param>
    /// <param name="address"></param>
    /// <param name="addInfo"></param>
    /// <param name="token"></param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public static async Task Create_async(int id, string fName, string lName, string mName,
        int caseId, string phone, string bDay, string address, string addInfo, CancellationToken token)
    {
        await Data.Target.Create_async(0, fName, lName, mName, caseId, phone,
            string.IsNullOrEmpty(bDay) ? null : DateOnly.Parse(bDay),
            address, addInfo, token);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="target"></param>
    /// <param name="token"></param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public static async Task Create_async(Core.Target target, CancellationToken token)
    {
        // В БД поле ID автоинкремент.
        target.Id = 0;
        await Data.Target.Create_async(MapperToData(target), token);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="token"></param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<List<Core.Target>> Read_All_async(CancellationToken token)
    {
        var rez = await Data.Target.Read_All_async(token);
        return rez.Select(MapperFromData).ToList();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token"></param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<List<Core.Target>> Read_ById_async(int id, CancellationToken token)
    {
        var rez = await Data.Target.Read_ById_async(id, token);
        return rez.Select(MapperFromData).ToList();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="target"></param>
    /// <param name="token"></param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public static async Task Update_async(Core.Target target, CancellationToken token)
    {
        await Data.Target.Update_async(MapperToData(target), token);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <param name="token"></param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public static async Task Delete_ById_async(int id, CancellationToken token)
    {
        await Data.Target.Delete_ById_async(id, token);
    }

    private static Core.Target MapperFromData(Data.Target target)
    {
        return new Target(
            target.Id,
            target.FirstName,
            target.LastName,
            target.MiddleName,
            target.CaseId,
            target.Phone,
            target.Birthdate,
            target.Address,
            target.AdditionalInfo);
    }

    private static Data.Target MapperToData(Core.Target target)
    {
        return new Data.Target(
            target.Id,
            target.FirstName,
            target.LastName,
            target.MiddleName,
            target.CaseId,
            target.Phone,
            target.Birthdate,
            target.Address,
            target.AdditionalInfo);
    }
}
