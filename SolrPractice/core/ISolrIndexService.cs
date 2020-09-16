using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolrPractice.core
{
    public interface ISolrIndexService<T>
    {
        bool AddUpdate(T document);
        bool Delete(T document);
        SolrQueryResults<T> GetAll();

        SolrQueryResults<T> GetByField(string fieldName, string val);
        SolrQueryResults<T> GetByFieldWithMultiValues(string fieldName, string[] val);
        SolrQueryResults<T> GetAllWithPagination(int offSet, int numberOfRows);

        SolrQueryResults<T> GetWithMultipleCriteria(Dictionary<string, string> criteriaWithKeyValuesPairs);





    }
}
