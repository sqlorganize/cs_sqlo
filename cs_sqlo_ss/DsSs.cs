﻿using cs_sqlo;
    using Microsoft.Data.SqlClient;

namespace cs_sqlo_ss
{
    public class DbSs : Db
    {
        SqlConnection _conn;

        /*
         * config["connection_string"] = "server=127.0.0.1;uid=root;pwd=12345;database=test"
         */
        public DbSs(Dictionary<string, object> config) : base(config)
        {
        }

        public SqlConnection conn() => _conn;


        public override EntityQuery query(string entity_name)
        {
            return new EntityQuerySs(this, entity_name);
        }


        public override Mapping mapping(string entity_name, string field_id)
        {
            return new MappingSs(this, entity_name, field_id);
        }

        public override Condition condition(string entity_name, string field_id)
        {
            return new ConditionSs(this, entity_name, field_id);
        }

        public override Values values(string entity_name, string field_id)
        {
            return new ValuesSs(this, entity_name, field_id);
        }
    }
}