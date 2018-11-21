﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Logic.Db.Properties {
    using System;


    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }

        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Logic.Db.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a bb.
        /// </summary>
        internal static string bb {
            get {
                return ResourceManager.GetString("bb", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca un recurso adaptado de tipo System.Byte[].
        /// </summary>
        internal static byte[] Database {
            get {
                object obj = ResourceManager.GetObject("Database", resourceCulture);
                return ((byte[]) (obj));
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a database.db.
        /// </summary>
        internal static string DbFileName {
            get {
                return ResourceManager.GetString("DbFileName", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a Logic.BD\Connection\.
        /// </summary>
        internal static string DbPath {
            get {
                return ResourceManager.GetString("DbPath", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca un recurso adaptado de tipo System.Byte[].
        /// </summary>
        internal static byte[] DefaultCategories {
            get {
                object obj = ResourceManager.GetObject("DefaultCategories", resourceCulture);
                return ((byte[]) (obj));
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT count(*) FROM Athlete WHERE ATHLETE_DNI=@DNI.
        /// </summary>
        internal static string SQL_COUNT_ATHLETE_BY_DNI {
            get {
                return ResourceManager.GetString("SQL_COUNT_ATHLETE_BY_DNI", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a DELETE FROM Athlete WHERE ATHLETE_DNI=@DNI.
        /// </summary>
        internal static string SQL_DELETE_ATHLETE {
            get {
                return ResourceManager.GetString("SQL_DELETE_ATHLETE", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a .
        /// </summary>
        internal static string SQL_DELETE_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_DELETE_COMPETITION", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a insert into competitionCategory values (@COMPETITION_ID, @ABSOLUT_CATEGORY_ID).
        /// </summary>
        internal static string SQL_ENROLL_ABSOLUTE_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_ENROLL_ABSOLUTE_COMPETITION", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a insert into competitionDates values (@COMPETITION_ID, @INITIAL_DATE, @FINISH_DATE, @COMPETITION_PRICE).
        /// </summary>
        internal static string SQL_ENROLL_COMPETITION_DATES {
            get {
                return ResourceManager.GetString("SQL_ENROLL_COMPETITION_DATES", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a insert into competitionRefund values (@COMPETITION_ID,@DATE_REFUND,@REFUND).
        /// </summary>
        internal static string SQL_ENROLL_REFUNDS_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_ENROLL_REFUNDS_COMPETITION", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select * from category where category_name = @CATEGORY_NAME and  category_min_age = @CATEGORY_MIN_AGEand category_max_age = @CATEGORY_MAX_AGE and gender = @GENDER.
        /// </summary>
        internal static string SQL_GET_ABSOLUTE_ID {
            get {
                return ResourceManager.GetString("SQL_GET_ABSOLUTE_ID", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT * FROM COMPETITION WHERE COMPETITION_NAME =@COMPETITION_NAME AND COMPETITION_TYPE = @COMPETITION_TYPE AND COMPETITION_KM =@COMPETITION_KM AND COMPETITION_DATE =@COMPETITION_DATE AND COMPETITION_NUMBER_PLACES =@COMPETITION_NUMER AND COMPETITION_STATUS =@COMPETITION_STATUS AND COMPETITION_RULES =@COMPETITION_RULES AND COMPETITION_SLOPE =@COMPETITION_SLOPE AND COMPETITION_NUMBER_MILESTONE =@COMPETITION_NUMBER_MILESTONE.
        /// </summary>
        internal static string SQL_GET_COMPETITION_ID {
            get {
                return ResourceManager.GetString("SQL_GET_COMPETITION_ID", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select competition_rules from competition where competition_id=@COMPETITION_ID.
        /// </summary>
        internal static string SQL_GET_RULES {
            get {
                return ResourceManager.GetString("SQL_GET_RULES", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a insert into absolutCategories values (null, @NAME, @ID_M,@ID_F).
        /// </summary>
        internal static string SQL_INSERT_ABSOLUTE {
            get {
                return ResourceManager.GetString("SQL_INSERT_ABSOLUTE", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO Athlete VALUES(@DNI, @NAME, @SURNAME, @BIRTH_DATE, @GENDER).
        /// </summary>
        internal static string SQL_INSERT_ATHLETE {
            get {
                return ResourceManager.GetString("SQL_INSERT_ATHLETE", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a insert into Category values (@CATEGORY_NAME, @CATEGORY_MIN_AGE,@CATEGORY_MAX_AGE,@GENDER).
        /// </summary>
        internal static string SQL_INSERT_CATEGORY {
            get {
                return ResourceManager.GetString("SQL_INSERT_CATEGORY", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a .
        /// </summary>
        internal static string SQL_INSERT_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_INSERT_COMPETITION", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a insert into enroll values (@DNI, @COMPETITION_ID, @STATUS, date(&apos;now&apos;), null).
        /// </summary>
        internal static string SQL_INSERT_ENROLL {
            get {
                return ResourceManager.GetString("SQL_INSERT_ENROLL", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO HasParticipated(COMPETITION_ID, ATHLETE_DNI)
        ///select COMPETITION_ID, ATHLETE_DNI
        ///from Enroll
        ///where status=@STATUS.
        /// </summary>
        internal static string SQL_INSERT_HAS_PARTICIPATED {
            get {
                return ResourceManager.GetString("SQL_INSERT_HAS_PARTICIPATED", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a insert into DateInscription values(@INITIAL_DATE, @FINISH_DATE).
        /// </summary>
        internal static string SQL_INSERT_INSCRIPTION_DATE {
            get {
                return ResourceManager.GetString("SQL_INSERT_INSCRIPTION_DATE", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a Select Competition.COMPETITION_NAME, Enroll.STATUS, Enroll.DATE_INSCRIPTION,
        /// Enroll.DORSAL from Enroll, Competition where Athlete_dni = @DNI
        ///and Competition.COMPETITION_ID = Enroll.COMPETITION_ID.
        /// </summary>
        internal static string SQL_SELECT_ALL_COMP_INSCRIPTED {
            get {
                return ResourceManager.GetString("SQL_SELECT_ALL_COMP_INSCRIPTED", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select * from Competition.
        /// </summary>
        internal static string SQL_SELECT_ALL_COMPETITIONS {
            get {
                return ResourceManager.GetString("SQL_SELECT_ALL_COMPETITIONS", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT ATHLETE_DNI, ATHLETE_NAME, ATHLETE_SURNAME, ATHLETE_BIRTH_DATE, ATHLETE_GENDER FROM Athlete.
        /// </summary>
        internal static string SQL_SELECT_ATHLETE {
            get {
                return ResourceManager.GetString("SQL_SELECT_ATHLETE", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT distinct Athlete.ATHLETE_DNI, Athlete.ATHLETE_NAME, 
        ///Athlete.ATHLETE_SURNAME, 
        /// Competition.Competition_name, Athlete.ATHLETE_GENDER
        /// FROM Athlete, hasParticipated, competition, enroll
        /// where Athlete.ATHLETE_DNI = @DNI and enroll.athlete_dni =@DNI and hasparticipated.competition_id = competition.competition_id.
        /// </summary>
        internal static string SQL_SELECT_ATHLETE_BY_DNI {
            get {
                return ResourceManager.GetString("SQL_SELECT_ATHLETE_BY_DNI", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT * FROM ENROLL
        ///WHERE COMPETITION_ID=@COMPETITION_ID
        ///ORDER BY DATE_INSCRIPTION, STATUS.
        /// </summary>
        internal static string SQL_SELECT_ATHLETE_INSCRIPTION {
            get {
                return ResourceManager.GetString("SQL_SELECT_ATHLETE_INSCRIPTION", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT * FROM ENROLL
        ///WHERE STATUS = @STATUS AND COMPETITION_ID=@COMPETITION_ID
        ///ORDER BY DATE_INSCRIPTION.
        /// </summary>
        internal static string SQL_SELECT_ATHLETES_STATUS {
            get {
                return ResourceManager.GetString("SQL_SELECT_ATHLETES_STATUS", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT DORSAL, ATHLETE_DNI, ATHLETE_NAME, ATHLETE_SURNAME, ATHLETE_GENDER, INITIAL_TIME, FINISH_TIME, age FROM HasParticipated NATURAL JOIN Enroll NATURAL JOIN Athlete NATURAL JOIN GET_AGE_NOW 
        ///WHERE COMPETITION_ID=@COMPETITION_ID AND ATHLETE_DNI=GET_AGE_NOW.ATHLETE_DNI.
        /// </summary>
        internal static string SQL_SELECT_ATHLETES_TIMES {
            get {
                return ResourceManager.GetString("SQL_SELECT_ATHLETES_TIMES", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT DORSAL, ATHLETE_DNI, ATHLETE_NAME, ATHLETE_SURNAME, ATHLETE_GENDER, INITIAL_TIME, FINISH_TIME FROM HasParticipated NATURAL JOIN Enroll NATURAL JOIN Athlete NATURAL JOIN CompetitionCategory NATURAL JOIN Category
        ///WHERE COMPETITION_ID=@COMPETITION_ID AND CATEGORY_MIN_AGE &lt;= @AGE AND @AGE &gt;= CATEGORY_MAX_AGE AND ((ATHLETE_GENDER = @GENDER AND CATEGORY_GENDER = @GENDER) OR CATEGORY_GENDER IS NULL).
        /// </summary>
        internal static string SQL_SELECT_ATHLETES_TIMES_BY_CATEGORY {
            get {
                return ResourceManager.GetString("SQL_SELECT_ATHLETES_TIMES_BY_CATEGORY", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT DORSAL, ATHLETE_DNI, ATHLETE_NAME, ATHLETE_SURNAME, ATHLETE_GENDER, INITIAL_TIME, FINISH_TIME, age FROM HasParticipated NATURAL JOIN Enroll NATURAL JOIN Athlete NATURAL JOIN GET_AGE_NOW 
        ///WHERE COMPETITION_ID=@COMPETITION_ID AND ATHLETE_DNI=GET_AGE_NOW.ATHLETE_DNI  AND ((AGE &gt;= @CATEGORY_MIN_AGE_M AND AGE &lt;= @CATEGORY_MAX_AGE_M) OR (AGE &gt;= @CATEGORY_MIN_AGE_F AND AGE &lt;= @CATEGORY_MAX_AGE_F)).
        /// </summary>
        internal static string SQL_SELECT_ATHLETES_TIMES_BY_GENDER {
            get {
                return ResourceManager.GetString("SQL_SELECT_ATHLETES_TIMES_BY_GENDER", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT * FROM Category
        ///WHERE CATEGORY_ID = @ID.
        /// </summary>
        internal static string SQL_SELECT_CATEGORIES {
            get {
                return ResourceManager.GetString("SQL_SELECT_CATEGORIES", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select ABSOLUT_CATEGORY_ID, NAME, ID_M, ID_F FROM AbsolutCategories, CompetitionCategory
        ///where CompetitionCategory.COMPETITION_ID = @COMPETITION_ID AND CompetitionCategory.ABSOLUT_CATEGORY_ID = AbsolutCategories.ID.
        /// </summary>
        internal static string SQL_SELECT_CATEGORY_BY_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_SELECT_CATEGORY_BY_COMPETITION", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT * FROM CATEGORY WHERE CATEGORY_NAME =@CATEGORY_NAME AND  CATEGORY_MIN_AGE =@CATEGORY_MIN_AGE AND CATEGORY_MAX_AGE =@CATEGORY_MAX_AGE AND GENDER =@GENDER.
        /// </summary>
        internal static string SQL_SELECT_CATEGORY_CHILD {
            get {
                return ResourceManager.GetString("SQL_SELECT_CATEGORY_CHILD", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select CATEGORY_NAME FROM GET_CATEGORY_COMPETITION where athlete_dni =@DNI and COMPETITION_ID=@COMPETITION_ID.
        /// </summary>
        internal static string SQL_SELECT_CATEGORY_IN_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_SELECT_CATEGORY_IN_COMPETITION", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT * FROM Competition.
        /// </summary>
        internal static string SQL_SELECT_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_SELECT_COMPETITION", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select COMPETITION_ID, COMPETITION_NAME, COMPETITION_TYPE, COMPETITION_KM, COMPETITION_PRICE, COMPETITION_DATE from Competition where COMPETITION_ID=@ID.
        /// </summary>
        internal static string SQL_SELECT_COMPETITION_BY_ID {
            get {
                return ResourceManager.GetString("SQL_SELECT_COMPETITION_BY_ID", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select category_name from get_category_competition where competition_id=@COMPETITION_ID.
        /// </summary>
        internal static string SQL_SELECT_COMPETITION_CATEGORY {
            get {
                return ResourceManager.GetString("SQL_SELECT_COMPETITION_CATEGORY", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select COMPETITION_ID, COMPETITION_NAME, COMPETITION_TYPE, COMPETITION_KM, COMPETITION_PRICE, INITIAL_DATE, FINISH_DATE, COMPETITION_DATE from Competition natural join CompetitionDates
        ///where date(&apos;now&apos;) not BETWEEN INITIAL_DATE and FINISH_DATE and date(&apos;now&apos;) &lt; COMPETITION_DATE.
        /// </summary>
        internal static string SQL_SELECT_COMPETITION_FINISH_LIST {
            get {
                return ResourceManager.GetString("SQL_SELECT_COMPETITION_FINISH_LIST", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a SELECT COMPETITION_ID, COMPETITION_NAME, COMPETITION_TYPE, COMPETITION_KM, COMPETITION_PRICE, COMPETITION_DATE, COMPETITION_STATUS, count(*) as INSCRITOS FROM Competition natural join Enroll
        ///WHERE COMPETITION_STATUS=@STATUS
        ///group by COMPETITION_ID.
        /// </summary>
        internal static string SQL_SELECT_COMPETITION_STATUS {
            get {
                return ResourceManager.GetString("SQL_SELECT_COMPETITION_STATUS", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select COMPETITION_ID, COMPETITION_NAME, COMPETITION_TYPE, COMPETITION_KM, COMPETITION_PRICE, INITIAL_DATE, FINISH_DATE, COMPETITION_DATE 
        ///from Competition natural join CompetitionDates 
        ///where date(&apos;now&apos;) BETWEEN INITIAL_DATE and FINISH_DATE and COMPETITION_STATUS&lt;&gt;&apos;FINISH&apos; and COMPETITION_NUMBER_PLACES &gt; (select count(*) from enroll group by competition_id) AND COMPETITION_ID not in (SELECT COMPETITION_ID FROM ENROLL WHERE ATHLETE_DNI=@DNI).
        /// </summary>
        internal static string SQL_SELECT_COMPETITION_TO_INSCRIBE {
            get {
                return ResourceManager.GetString("SQL_SELECT_COMPETITION_TO_INSCRIBE", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select  Competition.COMPETITION_NAME,Athlete.athlete_gender from Competition, hasparticipated, athlete where Competition.COMPETITION_ID= hasparticipated.COMPETITION_ID and hasparticipated.athlete_dni = @DNI and athlete.athlete_dni = @DNI.
        /// </summary>
        internal static string SQL_SELECT_COMPETITIONS_PARTICIPATED {
            get {
                return ResourceManager.GetString("SQL_SELECT_COMPETITIONS_PARTICIPATED", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select COUNT(*) from Competition natural join Enroll
        ///where COMPETITION_ID = @COMPETITION_ID AND DORSAL IS NOT NULL.
        /// </summary>
        internal static string SQL_SELECT_COUNT_DORSALS_BY_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_SELECT_COUNT_DORSALS_BY_COMPETITION", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select max(dorsal) from enroll where COMPETITION_ID=@COMPETITION_ID.
        /// </summary>
        internal static string SQL_SELECT_MAX_DORSAL {
            get {
                return ResourceManager.GetString("SQL_SELECT_MAX_DORSAL", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select COMPETITION_ID, COMPETITION_NAME, COMPETITION_TYPE, COMPETITION_KM, COMPETITION_PRICE, INITIAL_DATE, FINISH_DATE, COMPETITION_DATE, COMPETITION_RULES from Competition natural join CompetitionDates where (date(&apos;now&apos;) between INITIAL_DATE and FINISH_DATE) and  Competition.COMPETITION_STATUS=&apos;OPEN&apos;.
        /// </summary>
        internal static string SQL_SELECT_OPEN_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_SELECT_OPEN_COMPETITION", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a select ATHLETE_DNI, COMPETITION_ID, COMPETITION_PRICE, DATE_INSCRIPTION from Enroll natural join Competition where status in (&apos;PRE-REGISTERED&apos;, &apos;PREREGISTERED&apos;,&apos;CANCELED&apos;).
        /// </summary>
        internal static string SQL_SELECT_OUTSTANDING {
            get {
                return ResourceManager.GetString("SQL_SELECT_OUTSTANDING", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a update Enroll
        ///set dorsal=@DORSAL
        ///where COMPETITION_ID=@COMPETITION_ID and ATHLETE_DNI=@DNI and dorsal is null.
        /// </summary>
        internal static string SQL_UPDATE_DORSAL {
            get {
                return ResourceManager.GetString("SQL_UPDATE_DORSAL", resourceCulture);
            }
        }

        /// <summary>
        ///   Busca una cadena traducida similar a update Enroll set status=@STATUS where competition_id=@COMPETITION_ID and athlete_dni=@DNI.
        /// </summary>
        internal static string SQL_UPDATE_INSCRIPTION_STATUS {
            get {
                return ResourceManager.GetString("SQL_UPDATE_INSCRIPTION_STATUS", resourceCulture);
            }
        }

        internal static string SQL_MAX_ID_DATES {
            get {
                return ResourceManager.GetString("SQL_MAX_ID_DATES", resourceCulture);
            }
        }

        internal static string SQL_SELECT_ATHLETES_HAS_PARTICIPATED {
            get {
                return ResourceManager.GetString("SQL_SELECT_ATHLETES_HAS_PARTICIPATED", resourceCulture);
            }
        }

        internal static string SQL_SELECT_DNI_FROM_DORSAL {
            get {
                return ResourceManager.GetString("SQL_SELECT_DNI_FROM_DORSAL", resourceCulture);
            }
        }

        internal static string SQL_SELECT_NOT_CANCELED_INSCRIPTIONS {
            get {
                return ResourceManager.GetString("SQL_SELECT_NOT_CANCELED_INSCRIPTIONS", resourceCulture);
            }
        }

        internal static string SQL_INSERT_TIMES {
            get {
                return ResourceManager.GetString("SQL_INSERT_TIMES", resourceCulture);
            }
        }

        internal static string SQL_UPDATE_TIMES {
            get {
                return ResourceManager.GetString("SQL_UPDATE_TIMES", resourceCulture);
            }
        }

        internal static string SQL_SELECT_COMPETITION_PRICE {
            get {
                return ResourceManager.GetString("SQL_SELECT_COMPETITION_PRICE", resourceCulture);
            }
        }

        internal static string SQL_SELECT_CATEGORY_BY_COMP_AND_ATHLETE {
            get {
                return ResourceManager.GetString("SQL_SELECT_CATEGORY_BY_COMP_AND_ATHLETE", resourceCulture);
            }
        }

        internal static string SQL_INSERT_PARTIAL_TIMES {
            get {
                return ResourceManager.GetString("SQL_INSERT_PARTIAL_TIMES", resourceCulture);
            }
        }

        internal static string SQL_SELECT_HAS_PARTICIPATED {
            get {
                return ResourceManager.GetString("SQL_SELECT_HAS_PARTICIPATED", resourceCulture);
            }
        }

        internal static string SQL_SELECT_PARTIAL_TIMES {
            get {
                return ResourceManager.GetString("SQL_SELECT_PARTIAL_TIMES", resourceCulture);
            }
        }

        internal static string SQL_UPDATE_PARTIAL_TIMES {
            get {
                return ResourceManager.GetString("SQL_UPDATE_PARTIAL_TIMES", resourceCulture);
            }
        }

        internal static string SQL_FINISH_COMPETITION {
            get {
                return ResourceManager.GetString("SQL_FINISH_COMPETITION", resourceCulture);
            }
        }



        internal static string SQL_SELECT_STATUS_ENROLL {
            get {
                return ResourceManager.GetString("SQL_SELECT_STATUS_ENROLL", resourceCulture);
            }
        }



    }
}
