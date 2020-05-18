using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections;

namespace BusinessLibrary
{
    public static class MultipleResultSets
    {
       
        public static MultipleResultSetWrapper MultipleResults(this DbContext db, string storedProcedure)
        {
            return new MultipleResultSetWrapper(db, storedProcedure);
        }
        public class MultipleResultSetWrapper
        {
            private readonly DbContext _db;
            private readonly string _storedProcedure;
            public List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>> _resultSets;
            public MultipleResultSetWrapper(DbContext db, string storedProcedure)
            {
                _db = db;
                _storedProcedure = storedProcedure;
                _resultSets = new List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>>();
            }
            public MultipleResultSetWrapper With<TResult>()
            {
                _resultSets.Add((adapter, reader) => adapter
                    .ObjectContext
                    .Translate<TResult>(reader)
                    .ToList());

                return this;
            }
            public List<IEnumerable> Execute(params string[] parameter)
            {
                string _storedProcedureparam;
                var results = new List<IEnumerable>();
                using (var connection = _db.Database.Connection)
                {
                    try
                    {
                        var command = connection.CreateCommand();
                        int Length = parameter.Length / 2; _storedProcedureparam = "";
                        for (int counter = 0; counter < Length; counter++)
                        {
                            if (counter == Length - 1)
                            {
                                _storedProcedureparam += parameter[counter] + "=" + "'" + parameter[Length + counter] + "'";
                            }
                            else
                            {
                                _storedProcedureparam += parameter[counter] + "=" + "'" + parameter[Length + counter] + "'" + ',';
                            }
                        }

                        command.CommandText = "EXEC " + _storedProcedure + " " + _storedProcedureparam;
                        command.CommandTimeout = 100000;
                        // connection.Close();
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            var adapter = ((IObjectContextAdapter)_db);
                            foreach (var resultSet in _resultSets)
                            {

                                results.Add(resultSet(adapter, reader));
                                reader.NextResult();
                            }
                        }
                    }
                    catch (Exception ex) { throw ex; }
                    finally
                    {
                        connection.Close();
                    }

                    return results;
                }
            }

        }

    }
}
