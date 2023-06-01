using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Contora.Data;

public partial class Agent
{
    public Agent(int id, string firstName, string lastName, string? middleName, int departmentId,
        int positionId, int rankId, int statusId, string? phone, string? address)
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

    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public int DepartmentId { get; set; }

    public int PositionId { get; set; }

    public int RankId { get; set; }

    public int StatusId { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Case> CasePrimaryAgents { get; set; } = new List<Case>();

    public virtual ICollection<Case> CaseSecondaryAgents { get; set; } = new List<Case>();

    public virtual Department Department { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;

    public virtual Rank Rank { get; set; } = null!;

    public virtual Agentstatus Status { get; set; } = null!;

    public static async Task Create_async(Agent value, CancellationToken token)
    {
        using var contex = new ContoraContext();
        await contex.AddAsync(value, token);
        await contex.SaveChangesAsync(token);
    }

    public static async Task<List<Agent>> Read_All_async(CancellationToken token)
    {
        using var context = new ContoraContext();
        return await context.Agents
            .AsNoTracking()
            .ToListAsync(token);
    }

    public static async Task<List<Agent>> Read_ById_async(int id, CancellationToken token)
    {
        using var context = new ContoraContext();
        return await context.Agents
            .AsNoTracking()
            .Where(a => a.Id == id)
            .ToListAsync(token);
    }

    public static async Task<List<Agent>> Read_ByFirstName_async(string fName, CancellationToken token)
    {
        using var contex = new ContoraContext();
        return await contex.Agents
            .AsNoTracking()
            .Where(a => a.FirstName == fName)
            .ToListAsync(token);
    }

    public static async Task<List<Agent>> Read_ByLastName_async(string lName, CancellationToken token)
    {
        using var contex = new ContoraContext();
        return await contex.Agents
            .AsNoTracking()
            .Where(a => a.LastName == lName)
            .ToListAsync(token);
    }

    public static async Task<List<Agent>> Read_ByDepartmentId_async(int depId, CancellationToken token)
    {
        using var contex = new ContoraContext();
        return await contex.Agents
        .AsNoTracking()
            .Where(a => a.DepartmentId == depId)
            .ToListAsync(token);
    }

    public static async Task<List<Agent>> Read_ByPositionId_async(int posId, CancellationToken token)
    {
        using var contex = new ContoraContext();
        return await contex.Agents
            .AsNoTracking()
            .Where(a => a.PositionId == posId)
            .ToListAsync(token);
    }

    public static async Task<List<Agent>> Read_ByStatusId_async(int statusId, CancellationToken token)
    {
        using var contex = new ContoraContext();
        return await contex.Agents
            .AsNoTracking()
            .Where(a => a.StatusId == statusId)
            .ToListAsync(token);
    }

    public static async Task Update_async(Agent value, CancellationToken token)
    {
        using var contex = new ContoraContext();
        var rez = await contex.Agents.SingleOrDefaultAsync(a => a.Id == value.Id, token);
        if (rez != null)
        {
            rez.FirstName = value.FirstName;
            rez.LastName = value.LastName;
            rez.MiddleName = value.MiddleName;
            rez.DepartmentId = value.DepartmentId;
            rez.PositionId = value.PositionId;
            rez.RankId = value.RankId;
            rez.StatusId = value.StatusId;
            rez.Phone = value.Phone;
            rez.Address = value.Address;
            await contex.SaveChangesAsync(token);
        }
    }

    public static async Task Delete_ById_async(int id, CancellationToken token)
    {
        using var contex = new ContoraContext();
        await contex.Agents.Where(x => x.Id == id).ExecuteDeleteAsync(token);
    }
}
