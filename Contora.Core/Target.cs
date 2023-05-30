namespace Contora.Core
{
    public class Target
    {
        public Target(int id, string firstName, string lastName, string? middleName, int caseId, string? phone, DateOnly? birthdate, string? address, string? additionalInfo)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            MiddleName = middleName;
            CaseId = caseId;
            Phone = phone;
            Birthdate = birthdate;
            Address = address;
            AdditionalInfo = additionalInfo;
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

        public static async Task Create_async(int id, string fName, string lName, string mName,
            int caseId, string phone, string bDay, string address, string addInfo, CancellationToken token)
        {
            await Data.Target.Create_async(0, fName, lName, mName, caseId, phone,
                string.IsNullOrEmpty(bDay) ? null : DateOnly.Parse(bDay),
                address, addInfo, token);
        }

        public static async Task Create_async(Core.Target target, CancellationToken token)
        {
            //В БД поле ID автоинкремент.
            target.Id = 0;
            await Data.Target.Create_async(MapperToData(target), token);
        }

        public static async Task<List<Core.Target>> Read_All_async(CancellationToken token)
        {
            var rez = await Data.Target.Read_All_async(token);
            return rez.Select(MapperFromData).ToList();
        }

        public static async Task<List<Core.Target>> Read_ById_async(int id, CancellationToken token)
        {
            var rez = await Data.Target.Read_ById_async(id, token);
            return rez.Select(MapperFromData).ToList();
        }

        public static async Task Update_async(Core.Target target, CancellationToken token)
        {
            await Data.Target.Update_async(MapperToData(target), token);
        }

        public static async Task Delete_byId_async(int id, CancellationToken token)
        {
            await Data.Target.Delete_byId_async(id, token);
        }

        private static Core.Target MapperFromData(Data.Target target)
        {
            return new Target(target.Id, target.FirstName, target.LastName, target.MiddleName,
                target.CaseId, target.Phone, target.Birthdate, target.Address, target.AdditionalInfo);
        }

        //По аналогии с Hillel_hw_23.Core.Agent тут тоже можно накидать проверки.
        private static Data.Target MapperToData(Core.Target target)
        {
            return new Data.Target(target.Id, target.FirstName, target.LastName, target.MiddleName,
                target.CaseId, target.Phone, target.Birthdate, target.Address, target.AdditionalInfo);
        }

    }
}
