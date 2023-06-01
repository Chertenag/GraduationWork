using Microsoft.EntityFrameworkCore;

namespace Contora.Data;

public partial class Case
{
    public Case(int id, int departmentId, int primaryAgentId, int? secondaryAgentId, DateOnly dateOpen, DateOnly? dateClose)
    {
        Id = id;
        DepartmentId = departmentId;
        PrimaryAgentId = primaryAgentId;
        SecondaryAgentId = secondaryAgentId;
        DateOpen = dateOpen;
        DateClose = dateClose;
    }

    public int Id { get; set; }

    public int DepartmentId { get; set; }

    public int PrimaryAgentId { get; set; }

    public int? SecondaryAgentId { get; set; }

    public DateOnly DateOpen { get; set; }

    public DateOnly? DateClose { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Agent PrimaryAgent { get; set; } = null!;

    public virtual Agent? SecondaryAgent { get; set; }

    public virtual ICollection<Target> Targets { get; set; } = new List<Target>();

    public static async Task Create_async(Case value, CancellationToken token)
    {
        using var contex = new ContoraContext();
        await contex.AddAsync(value, token);
        await contex.SaveChangesAsync(token);
    }

    public static async Task<List<Case>> Read_All_async(CancellationToken token)
    {
        using var contex = new ContoraContext();
        return await contex.Cases.AsNoTracking().ToListAsync(token);
    }

    public static async Task<List<Case>> Read_ById_async(int id, CancellationToken token)
    {
        using var contex = new ContoraContext();
        return await contex.Cases.AsNoTracking().Where(c => c.Id == id).ToListAsync(token);
    }

    public static async Task<List<Case>> Read_ByDepartmentId_async(int departmentId, CancellationToken token)
    {
        using var contex = new ContoraContext();
        return await contex.Cases.AsNoTracking().Where(c => c.DepartmentId == departmentId).ToListAsync(token);
    }

    public static async Task<List<Case>> Read_ByBothAgentId_async(int agentId, CancellationToken token)
    {
        using var contex = new ContoraContext();
        return await contex.Cases.AsNoTracking().Where(c => c.PrimaryAgentId == agentId || c.SecondaryAgentId == agentId).ToListAsync(token);
    }

    public static async Task Update_async(Case value, CancellationToken token)
    {
        using var contex = new ContoraContext();
        var rez = await contex.Cases.SingleOrDefaultAsync(c => c.Id == value.Id);
        if (rez != null)
        {
            rez.DepartmentId = value.DepartmentId;
            rez.PrimaryAgentId = value.PrimaryAgentId;
            rez.SecondaryAgentId = value.SecondaryAgentId;
            rez.DateOpen = value.DateOpen;
            rez.DateClose = value.DateClose;
        }
        await contex.SaveChangesAsync(token);
    }

    public static async Task Update_CloseCase_async(Case value, DateOnly closeDate, CancellationToken token)
    {
        using var contex = new ContoraContext();
        contex.Update(value);
        value.DateClose = closeDate;
        await contex.SaveChangesAsync(token);
    }

    public static async Task Delete_ById_async(int id, CancellationToken token)
    {
        using var contex = new ContoraContext();
        await contex.Cases.Where(c => c.Id == id).ExecuteDeleteAsync(token);
    }

}
