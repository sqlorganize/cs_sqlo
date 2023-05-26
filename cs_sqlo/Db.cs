﻿using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace cs_sqlo
{
    public class Db
    {
        protected Dictionary<string, object>? _config;
        protected Dictionary<string, object> _fields_config = new();
        protected Dictionary<string, object> _entity = new();
        protected Dictionary<string, object> _tools = new();
        protected Dictionary<string, object> _field = new();
        protected Dictionary<string, object> _mapping = new();
        protected Dictionary<string, object> _condition = new();
        protected Dictionary<string, object> _tree = new();
        protected Dictionary<string, object> _relations = new();
        protected Dictionary<string, object> _entities_config = new();

        public Db(Dictionary<string, object> config)
        {
            _config = config;
            using (StreamReader r = new StreamReader(_config["path_model"] + "entity-tree.json")) {
                _tree = (Dictionary<string, object>)JsonConvert.DeserializeObject(r.ReadToEnd());
            }

            using (StreamReader r = new StreamReader(_config["path_model"] + "entity-relations.json")) {
                _relations = (Dictionary<string, object>)JsonConvert.DeserializeObject(r.ReadToEnd());
            }

            using (StreamReader r = new StreamReader(_config["path_model"] + "_entities.json"))
            {
                _entities_config = (Dictionary<string, object>)JsonConvert.DeserializeObject(r.ReadToEnd());
            }

            if (File.Exists(_config["path_model"] + "entities.json"))
            {
                using (StreamReader r = new StreamReader(_config["path_model"] + "entities.json"))
                {
                    Dictionary<string, object> ee = (Dictionary<string, object>)JsonConvert.DeserializeObject(r.ReadToEnd());
                    foreach (KeyValuePair<string, object> e in ee)
                    {
                        if (_entities_config.ContainsKey(e.Key))
                        {
                            List<Dictionary<string, object>> p = new();
                            p.Add((Dictionary<string, object>)_entities_config[e.Key]);
                            p.Add((Dictionary<string, object>)ee[e.Key]);
                            _entities_config[e.Key] = Utils.merge_dicts(p);
                        }
                    }
                }

            }
    }


}