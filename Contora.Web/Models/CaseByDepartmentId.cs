using Contora.Core;

namespace Contora.Web.Models
{
    public class CaseByDepartmentId
    {
        private int departmentId;

        public int DepartmentId
        {
            get => departmentId;
            set
            {
                departmentId = value < 1 ? throw new ArgumentException("ID отдела не может быть 0 или отрицательным.") : value;
            }
        }

        public async Task<List<Case>> GetCases(CancellationToken token)
        {
            return await Case.Read_ByDepartmentId_async(this.DepartmentId, token);
        }
    }
}
