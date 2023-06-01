using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace Contora.Data;

public partial class Target
{
    public Target(int id, string firstName, string lastName, string? middleName, int caseId,
        string? phone, DateOnly? birthdate, string? address, string? additionalInfo)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        CaseId = caseId;
        Phone = phone;
        Birthdate = birthdate;
        Address = address;
        AdditionalInfo = additionalInfo;
    }

    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public int CaseId { get; set; }

    public string? Phone { get; set; }

    public DateOnly? Birthdate { get; set; }

    public string? Address { get; set; }

    public string? AdditionalInfo { get; set; }

    public virtual Case Case { get; set; } = null!;

    public static async Task Create_async(int id, string fName, string lName, string? mName,
        int caseId, string? phone, DateOnly? bDay, string? address, string? addInfo, CancellationToken token)
    {
        using (var contex = new ContoraContext())
        {
            await contex.Targets.AddAsync(new Target(id, fName, lName, mName, caseId, phone, bDay, address, addInfo), token);
            await contex.SaveChangesAsync(token);
        }
    }

    public static async Task Create_async(Target target, CancellationToken token)
    {
        using (var contex = new ContoraContext())
        {
            await contex.Targets.AddAsync(target, token);
            await contex.SaveChangesAsync(token);
        }
    }

    public static async Task<List<Target>> Read_All_async(CancellationToken token)
    {
        using (var context = new ContoraContext())
        {
            var targets = await context.Targets
            .AsNoTracking()
            .ToListAsync(token);
            return targets;
        }
    }

    public static async Task<List<Target>> Read_ById_async(int id, CancellationToken token)
    {
        using (var context = new ContoraContext())
        {
            var targets = await context.Targets
                .AsNoTracking()
                .Where(t => t.Id == id)
                .ToListAsync(token);
            return targets;
        }
    }

    public static async Task Update_async(Target target, CancellationToken token)
    {
        using (var contex = new ContoraContext())
        {
            var rez = await contex.Targets.SingleOrDefaultAsync(x => x.Id == target.Id, token);
            if (rez != null)
            {
                rez.FirstName = target.FirstName;
                rez.LastName = target.LastName;
                rez.MiddleName = target.MiddleName;
                rez.CaseId = target.CaseId;
                rez.Phone = target.Phone;
                rez.Birthdate = target.Birthdate;
                rez.Address = target.Address;
                rez.AdditionalInfo = target.AdditionalInfo;
                await contex.SaveChangesAsync(token);
            }
        }
    }

    public static async Task Delete_ById_async(int id, CancellationToken token)
    {
        using (var contex = new ContoraContext())
        {
            await contex.Targets.Where(x => x.Id == id).ExecuteDeleteAsync(token);
        }
    }

}
