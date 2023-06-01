namespace Contora.Core;

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

    public Agent() { }

    public Agent(int id, string firstName, string lastName, string? middleName, int departmentId, int positionId, int rankId, int statusId, string? phone, string? address)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        DepartmentId = departmentId;
        PositionId = positionId;
        RankId = rankId;
        StatusId = statusId;
        Phone = phone;
        Address = address;
    }

    public int Id { get => id; set => id = value; }
    public string FirstName
    {
        get => firstName;
        set
        {
            firstName = string.IsNullOrEmpty(value) ? throw new ArgumentException("Имя не должно быть пустым.") :
                value.Length > 50 ? throw new ArgumentException("Имя должно быть менее 50 символов.") : value;
        }
    }
    public string LastName
    {
        get => lastName;
        set
        {
            lastName = string.IsNullOrEmpty(value) ? throw new ArgumentException("Фамилия не должна быть пустой.") :
                value.Length > 50 ? throw new ArgumentException("Фамилия должна быть не более 50 символов.") : value;
        }
    }
    public string? MiddleName
    {
        get => middleName;
        set
        {
            middleName = string.IsNullOrEmpty(value) ? null :
                value.Length > 50 ? throw new ArgumentException("Отчество должно быть не более 50 символов.") : value;
        }
    }
    public int DepartmentId
    {
        get => departmentId;
        set
        {
            departmentId = value < 1 ? throw new ArgumentException("ID отдела не может быть 0 или отрицательным.") : value;
        }
    }
    public int PositionId
    {
        get => positionId;
        set
        {
            positionId = value < 1 ? throw new ArgumentException("ID должности не может быть 0 или отрицательным.") : value;
        }
    }
    public int RankId
    {
        get => rankId;
        set
        {
            rankId = value < 1 ? throw new ArgumentException("ID звания не может быть 0 или отрицательным.") : value;
        }
    }
    public int StatusId
    {
        get => statusId;
        set
        {
            statusId = value < 1 ? throw new ArgumentException("ID статуса не может быть 0 или отрицательным.") : value;
        }
    }
    public string? Phone
    {
        get => phone;
        set
        {
            phone = string.IsNullOrEmpty(value) ? null :
                value.Length > 20 ? throw new ArgumentException("Телефон должен быть менее 20 сиволов.") : value;
        }
    }
    public string? Address
    {
        get => address;
        set
        {
            address = string.IsNullOrEmpty(value) ? null :
                value.Length > 100 ? throw new ArgumentException("Адрес должен быть менее 100 сиволов.") : value;
        }
    }

    public static async Task Create_async(Agent agent, CancellationToken token)
    {
        await Data.Agent.Create_async(MapperToDataAgent(agent), token);
    }

    public static async Task<List<Agent>> Read_All_async(CancellationToken token)
    {
        var rez = await Data.Agent.Read_All_async(token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    public static async Task<List<Agent>> Read_ById_async(int id, CancellationToken token)
    {
        var rez = await Data.Agent.Read_ById_async(id, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    public static async Task<List<Agent>> Read_ByFirstName_async(string fName, CancellationToken token)
    {
        var rez = await Data.Agent.Read_ByFirstName_async(fName, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    public static async Task<List<Agent>> Read_ByLastName_async(string lName, CancellationToken token)
    {
        var rez = await Data.Agent.Read_ByLastName_async(lName, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    public static async Task<List<Agent>> Read_ByDepartmentId_async(int depId, CancellationToken token)
    {
        var rez = await Data.Agent.Read_ByDepartmentId_async(depId, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    public static async Task<List<Agent>> Read_ByPositionId_async(int posId, CancellationToken token)
    {
        var rez = await Data.Agent.Read_ByPositionId_async(posId, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    public static async Task<List<Agent>> Read_ByStatusId_async(int statusId, CancellationToken token)
    {
        var rez = await Data.Agent.Read_ByStatusId_async(statusId, token);
        return rez.Select(MapperFromDataAgent).ToList();
    }

    public static async Task Update_async(Agent agent, CancellationToken token)
    {
        await Data.Agent.Update_async(MapperToDataAgent(agent), token);
    }

    public static async Task Delete_ById_async(int id, CancellationToken token)
    {
        await Data.Agent.Delete_ById_async(id, token);
    }

    private static Agent MapperFromDataAgent(Data.Agent agent)
    {
        return new Core.Agent(agent.Id, agent.FirstName, agent.LastName, agent.MiddleName, 
            agent.DepartmentId, agent.PositionId, agent.RankId, agent.StatusId,
            agent.Phone, agent.Address);
    }

    private static Data.Agent MapperToDataAgent(Core.Agent agent)
    {
        return new Data.Agent(agent.Id, agent.FirstName, agent.LastName ,agent.MiddleName, 
            agent.DepartmentId, agent.PositionId, agent.RankId, agent.StatusId, 
            agent.Phone, agent.Address);
    }
}
