using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Util;

namespace Logic.Db.ActionObjects {
    public class AddAthleteLogic : IActionObject {

        //public Athlete AthleteAdd { get; private set; }
        private DBConnection _conn;
        private readonly AthleteDto _athleteAdd;

        public AddAthleteLogic(ref DBConnection conn, AthleteDto athleteP) {
            _athleteAdd = athleteP;
            _conn = conn;
        }
        public void Execute() {
            AthletesUtil.InsertAthletesTable(ref _conn, _athleteAdd);
        }

    }
}
