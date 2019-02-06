using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;


namespace Storage
{
    public class DataAccessMethods : IDataAccessMethods
    {
        private readonly RaceContext _context;

        public DataAccessMethods()
        {
            _context = new RaceContext();
        }

        public IEnumerable<Race> GetRaces()
        {
            return _context.Race.OrderByDescending(d => d.Date);
        }

        public async Task<Race> GetRace(int id)
        {
            return await _context.Race.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateRace(int id, Race race)
        {
            var RaceFromDb = _context.Race.FirstOrDefault(c => c.Id == id);
            if (RaceFromDb == null)
                //return NotFound();

            RaceFromDb.Date = race.Date;
            RaceFromDb.DistanceInMeters = race.DistanceInMeters;
            RaceFromDb.TimeInSeconds = race.TimeInSeconds;
            try
            {
                _context.Race.AddOrUpdate(RaceFromDb);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RaceExists(id))
                {
                    //return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Race> AddRace(Race race)
        {
            _context.Race.Add(race);
            await _context.SaveChangesAsync();

            return race;
        }

        public async Task<Race> DeleteRace(int id)
        {
            var Race = await _context.Race.SingleOrDefaultAsync(m => m.Id == id);
            if (Race == null)
            {
                return null;
            }

            _context.Race.Remove(Race);
            await _context.SaveChangesAsync();

            return Race;
        }

        private bool RaceExists(int id)
        {
            return _context.Race.Any(e => e.Id == id);
        }
    }
}
