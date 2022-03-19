using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceB
{
    public interface ISubdivisionService
    {
        Task<List<subdivision>> GetAll();
        Task ChangeDivisionsStatusInfinity();
        Task InsertSubdivisions(List<subdivision> subdivisions);
    }
}
