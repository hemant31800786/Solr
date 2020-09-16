using SolrNet;
using SolrNet.Commands.Parameters;
using SolrNet.Exceptions;
using SolrPractice.core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace SolrPractice.Persistance
{
    public class SolrIndexService<T, TSolrOperations> : ISolrIndexService<T>
     where TSolrOperations : ISolrOperations<T>
    {
        private readonly TSolrOperations _solr;
        public SolrIndexService(ISolrOperations<T> solr)
        {
            _solr = (TSolrOperations)solr;
        }
        public bool AddUpdate(T document)
        {
            try
            {
                // If the id already exists, the record is updated, otherwise added                         
                _solr.Add(document);
                _solr.Commit();
                return true;
            }
            catch (SolrNetException)
            {
                //Log exception
                return false;

            }
        }

        public bool Delete(T document)
        {
            try
            {
                //Can also delete by id                
                _solr.Delete(document);
                _solr.Commit();
                return true;
            }
            catch (SolrNetException)
            {
                //Log exception
                return false;
            }
        }

        public SolrQueryResults<T> GetAll()
        {

            return _solr.Query(new SolrQuery("*:*"));
        }

        public SolrQueryResults<T> GetAllWithPagination(int offSet, int numberOfRows)
        {
            return _solr.Query("*:*", new QueryOptions
            {
                StartOrCursor = new StartOrCursor.Start(offSet),  // offset
                Rows = numberOfRows,


            });
        }

        public SolrQueryResults<T> GetByField(string fieldName, string val)
        {
            //Quoted allow special character
            return _solr.Query(new SolrQueryByField(fieldName, val) { Quoted = false });

        }

        public SolrQueryResults<T> GetByFieldWithMultiValues(string fieldName, string[] val)

        {
            string paramter = String.Join(",", val);
            return _solr.Query(new SolrQueryInList(fieldName, paramter));
        }

        //ToDo...
        public SolrQueryResults<T> GetWithMultipleCriteria(Dictionary<string, string> criteriaWithKeyValuesPairs)
        {
            //var queryOptions = new QueryOptions();
            //foreach (string key in criteriaWithKeyValuesPairs.Keys)
            //        {
            //            queryOptions.AddFilterQueries(new SolrQueryByField(key, criteriaWithKeyValuesPairs[key]));
            //        }

            return _solr.Query(SolrQuery.All, new QueryOptions
            {
                FilterQueries = new ISolrQuery[] {
                                new SolrQueryByField("Name", "bana"),
                               new SolrQueryInList("PhotoId", "3866672", "3987775", "3205119"),
                        }
            });

        }



        //Some More Command to implement 
        //  "Any value"  = new SolrHasValueQuery("name")
        //Query by distance =new SolrQueryByDistance("store", pointLatitude = 45.15, pointLongitude = -93.85, distance = 5, accuracy = CalculationAccuracy.BoundingBox);

        //operators =new SolrQuery("solr") && new SolrQuery("name:desc");

        //Multiple values == solr.Query(new SolrQueryInList("PhotoId", "3866672", "3987775", "3205119"));

        //Handle special character =solr.Query(new SolrQueryByField("Name", "Asparges Grøn") { Quoted = false });
        //ByFiled =solr.Query(new SolrQueryByField("PhotoId", "3866672"));


        //await solr.DeleteAsync(SolrQuery.All);
        //await solr.AddAsync(p);
        //await solr.CommitAsync();



        //public static List<int> GroupingSerach(Dictionary<string, string> dictPars, int start, int rows,
        // DateTime startTime, DateTime endTime, out int count)
        //{

        //    var solr = ServiceLocator.Current.GetInstance<ISolrOperations<LogItems>>();
        //    var queryOptions = new QueryOptions();

        //    var groupingParameters = new GroupingParameters();
        //    groupingParameters.Fields = new Collection<string> { "logs_id" };
        //    groupingParameters.Ngroups = true; 

        //    var timeRange = new SolrQueryByRange<DateTime>("logs_time", startTime, endTime);
        //    queryOptions.AddFilterQueries(timeRange);
        //    foreach (string key in dictPars.Keys)
        //    {
        //        queryOptions.AddFilterQueries(new SolrQueryByField(key, dictPars[key]));
        //    }

        //    queryOptions.OrderBy = new Collection<SortOrder> { new SortOrder("logs_id", Order.DESC) };
        //    queryOptions.Grouping = groupingParameters;
        //    queryOptions.Start = start;
        //    queryOptions.Rows = rows;
        //    SolrQueryResults<LogItems> res = solr.Query(SolrQuery.All, queryOptions);
        //    GroupedResults<LogItems> items = res.Grouping["logs_id"];
        //    count = items.Ngroups ?? 0;
        //    return items.Groups.Select(item => Convert.ToInt32(item.GroupValue)).ToList();
        //}
    }
}
