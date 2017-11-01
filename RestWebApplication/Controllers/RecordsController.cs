using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodingAssignment;

namespace RestWebApplication.Controllers
{
    public class RecordsController : ApiController
    {
        // Record DB Singleton
        RecordDatabase db = RecordDatabase.Instance;

        public RecordsController()
        {
        }

        // POST a new record
        public void Post([FromBody]string value)
        {
            db.AddFormattedText(value);
        }

        [Route("records/gender")]
        [HttpGet]
        public IEnumerable<Record> SortByGender()
        {
            return db.SortByGenderThenByLastName(); ;
        }

        [Route("records/birthdate")]
        [HttpGet]
        public IEnumerable<Record> SortByBirthdate()
        {
            return db.SortByBirthDate(); ;
        }

        [Route("records/name")]
        [HttpGet]
        public IEnumerable<Record> SortByName()
        {
            return db.SortByLastNameDescending(); ;
        }
    }
}
