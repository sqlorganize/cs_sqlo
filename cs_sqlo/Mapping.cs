﻿using Microsoft.VisualBasic;
using System.Reflection;

namespace cs_sqlo
{
    /*
    Mapear campos para que sean entendidos por el motor de base de datos.

    Define SQL, cada motor debe tener su propia clase mapping de forma tal que
    sea traducido de forma correcta.

    Ejemplo de subclase opcional:

    -class ComisionMapping: Mapping:
        def numero(self):
           return '''
    CONCAT("+self.pf()+"sed.numero, "+self.pt()+".division)
'''

    Las subclases deben soportar la sintaxis del motor que se encuentran utilizando.
    */
    public abstract class Mapping : EntityOptions
    {
        public Mapping(Db _db, string _entity_name, string _field_id) : base(_db, _entity_name, _field_id)
        {
        }

        /*
        Realizar mapeo de field_name

        Verifica la existencia de metodo eclusivo, si no exite, buscar metodo
        predefinido.

        # ejemplo
        mapping.map("nombre")
        mapping.map("fecha_alta.max.y"); //aplicar max, luego y
        mapping.map("edad.avg")
        */
        public string map(string field_name)
        {
            List<string> p = field_name.Split('.').ToList();

            if(p.Count == 1) {
                return _default(field_name);
            }

            string method = p[p.Count - 1]; //se traduce el metodo ubicado mas a la derecha (el primero en traducirse se ejecutara al final)
            p.RemoveAt(p.Count - 1);
            switch (method)
            {
                case "count":
                    return _count(String.Join(".", p.ToArray()));

                default:
                    return this.map(String.Join(".", p.ToArray()));
            }


        }

        /**
         * mapeo por defecto
         */
        public abstract string _default(string field_name);

        public abstract string _count(string field_name);


    }
}