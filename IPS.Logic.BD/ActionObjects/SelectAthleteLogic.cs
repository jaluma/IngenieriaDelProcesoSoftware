using System;
using System.Collections.Generic;
using System.Text;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Util;

namespace Logic.Db.ActionObjects {
    public class SelectAthleteLogic : IActionObject {
        private DBConnection _conn;

        public SelectAthleteLogic(ref DBConnection conn) {
            _conn = conn;
        }
        public void Execute() {
            AthletesService.SelectAthleteTable(ref _conn);
        }
    }
}
