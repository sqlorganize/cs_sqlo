﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_sqlo
{
    public class Field
    {
        protected Db _db;
        protected string _name;

        /* nombre de la entidad */
        protected string _entity_name;

        /* 
        si es clave foranea: Nombre de la entidad referenciada por la clave foranea 
        */
        protected string? _entity_ref_name;

        /* 
         si es clave foranea: Nombre del campo referenciado (habitualmente "id")
         */
        protected string? _field_ref_name;
        protected string? _alias;

        /* 
        tipo de datos generico 
            int
            blob
            string
            boolean
            float
            text
            timestamp
            date               
         */
        protected string _type;


        /* 
        string con el tipo de field
            "pk": Clave primaria
            "nf": Field normal
            "mo": Clave foranea muchos a uno
            "oo": Clave foranea uno a uno
        */
        protected string _field_type;

        /* valor por defecto */
        protected object? _default;

        /* longitud maxima permitida */
        //protected int? _length;  

        /* valor maximo permitido */
        //protected object? _max;  

        /* valor minimo permitido */
        //protected object? _min;  

        /* lista de valores permitidos */
        //List<object> _values;

        public Field(Db db, string entity_name, string field_name)
        {
            _db = db;
            _name = field_name;
            _entity_name = entity_name;

            Dictionary<string, object> config = _db.fields_field(entity_name, field_name);
            this.SetConfig(config);
        }

        public string name() => this._name;

        public string? alias() => this._alias;

        public Entity entity() => this._db.entity(_entity_name);

        public Entity entity_ref() => this._db.entity(_entity_ref_name!);

        public bool is_main()
        {
            return this._db.entity(_entity_name).main().Contains(name());
        }
        public string type()
        {
            return _type;
        }

        public object? default_value()
        {
            return _default;
        }
    }
}