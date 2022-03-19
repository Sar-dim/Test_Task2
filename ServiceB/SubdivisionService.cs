using ServiceApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceB
{
    public class SubdivisionService : ISubdivisionService
    {
        private readonly ApplicationContext _context;

        public SubdivisionService(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task<List<subdivision>> GetAll()
        {
            var entities = _context.subdivisions.ToList();
            var actualStatusSubdivisions =  await StatusService.SelectAll();
            entities.ForEach(x => x.status = actualStatusSubdivisions.Where(y => x.id == y.Id).First().Status ? 1 : 0);
            entities = entities.OrderBy(x => x.ownerid).ToList();
            return entities;
        }

        public async Task InsertSubdivisions(List<subdivision> subdivisions)
        {
            if (subdivisions != null)
            {
                var entities = _context.subdivisions.ToList();
                for (int i = subdivisions.Count - 1; i >= 0; i--)
                {
                    if (entities.Where(x => subdivisions[i].name == x.name).FirstOrDefault() != null)
                    {
                        subdivisions.Remove(subdivisions[i]);
                    }
                }
                await _context.subdivisions.AddRangeAsync(subdivisions);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ChangeDivisionsStatusInfinity()
        {
            var actualStatusSubdivisions = new List<ActualStatusSubdivision>();
            _context.subdivisions.ToList().ForEach(x =>
            actualStatusSubdivisions.Add(new ActualStatusSubdivision(x.id, x.name, x.status == 0 ? false : true, x.ownerid)));

            do
            {
                await StatusService.ChangeStatus(actualStatusSubdivisions);
            } while (true);
        }
    }
}
