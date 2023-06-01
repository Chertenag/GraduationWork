using Contora.Data;

namespace Contora.Core
{
    public class Case
    {
        private int id;
        private int departmentId;
        private int primaryAgentId;
        private int? secondaryAgentId;
        private DateOnly dateOpen;
        private DateOnly? dateClose;

        public Case() { }

        public Case(int id, int departmentId, int primaryAgentId, int? secondaryAgentId, DateOnly dateOpen, DateOnly? dateClose)
        {
            Id = id;
            DepartmentId = departmentId;
            PrimaryAgentId = primaryAgentId;
            SecondaryAgentId = secondaryAgentId;
            DateOpen = dateOpen;
            DateClose = dateClose;
        }

        public int Id { get => id; set => id = value; }

        public int DepartmentId
        {
            get => departmentId;
            set => departmentId = value < 1 ? throw new ArgumentException("ID отдела не может быть 0 или отрицательным.") : value;
        }

        public int PrimaryAgentId
        {
            get => primaryAgentId;
            set => primaryAgentId = value < 1 ? throw new ArgumentException("ID агента не может быть 0 или отрицательным.") : value;
        }

        public int? SecondaryAgentId
        {
            get => secondaryAgentId;
            set => secondaryAgentId = value == null ? null : value < 1 ? throw new ArgumentException("ID агента не может быть 0 или отрицательным.") : value;
        }

        public DateOnly DateOpen
        {
            get => dateOpen;
            set => dateOpen = value > DateOnly.FromDateTime(DateTime.Now) ? throw new ArgumentException("Дата открытия дела не может быть позже текущей даты.") : value;
        }

        public DateOnly? DateClose { 
            get => dateClose; 
            set => dateClose = value < this.DateOpen ? throw new ArgumentException("Дата закрытия дела не может быть ранеше даты октрытия дела.")
                : value > DateOnly.FromDateTime(DateTime.Now) ? throw new ArgumentException("Дата закрытия дела не может быть позже текущей даты.") : value;
        }

        public static async Task Create_async(Case value, CancellationToken token)
        {
            await Data.Case.Create_async(MapperToDataCase(value), token);
        }

        public static async Task<List<Case>> Read_All_async(CancellationToken token)
        {
            var rez = await Data.Case.Read_All_async(token);
            return rez.Select(MapperFromDataCase).ToList();
        }

        public static async Task<List<Case>> Read_ById_async(int id, CancellationToken token)
        {
            var rez = await Data.Case.Read_ById_async(id, token);
            return rez.Select(MapperFromDataCase).ToList();
        }

        public static async Task<List<Case>> Read_ByDepartmentId_async(int agentId, CancellationToken token)
        {
            var rez = await Data.Case.Read_ByBothAgentId_async(agentId, token);
            return rez.Select(MapperFromDataCase).ToList();
        }

        public static async Task Update_async(Case value, CancellationToken token)
        {
            await Data.Case.Update_async(MapperToDataCase(value), token);
        }

        public static async Task Update_CloseCase_async(int id, DateOnly closeDate, CancellationToken token)
        {
            var rez = await Data.Case.Read_ById_async(id, token);
            if (rez.Count > 0)
            {
                await Data.Case.Update_CloseCase_async(rez[0], closeDate, token);
            }
        }

        public static async Task Update_CloseCase_async(int id, DateTime closeDate, CancellationToken token)
        {
            var rez = await Data.Case.Read_ById_async(id, token);
            if (rez.Count > 0)
            {
                await Data.Case.Update_CloseCase_async(rez[0], DateOnly.FromDateTime(closeDate), token);
            }
        }

        public static async Task Delete_ById_async(int id, CancellationToken token)
        {
            await Data.Case.Delete_ById_async(id, token);
        }

        public static Case MapperFromDataCase(Data.Case value)
        {
            return new Case(value.Id, value.DepartmentId, value.PrimaryAgentId, value.SecondaryAgentId, value.DateOpen, value.DateClose);
        }

        public static Data.Case MapperToDataCase(Case value)
        {
            return new Data.Case(value.Id, value.DepartmentId, value.PrimaryAgentId, value.SecondaryAgentId, value.DateOpen, value.DateClose);
        }
    }
}
