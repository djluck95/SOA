using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage
{
    interface IDataAccessMethods
    {
        IEnumerable<Race> GetRaces();
        Task<Race> GetRace(int id);
        Task UpdateRace(int id, Race race);
        Task<Race> AddRace(Race race);
        Task<Race> DeleteRace(int id);
    }
}
