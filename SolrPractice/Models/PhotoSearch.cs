using SolrNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolrPractice.Models
{

    public class PhotoSearch
    {
        public string MultiPhotoStatusTypeName { get; set; }
        [SolrField("ProductId")]
        public string ProductId { get; set; }
        public string ProductNumber { get; set; }
        public string IsDefaultImage { get; set; }
        public string[] Created { get; set; }
        [SolrField("Name")]
        public string[] Name { get; set; }
        public string Brand { get; set; }
        [SolrField("CategoryCustomerId")]
        public string[] CategoryCustomerId { get; set; }
        [SolrField("PhotoId")]
        public string PhotoId { get; set; }
        [SolrField("MultiPhotoTypeId")]
        public int MultiPhotoTypeId { get; set; }
        public int MultiCustomerId { get; set; }
        public int MultiPhotoFileFormatId { get; set; }
        public string MultiPhotoStatusTypeId { get; set; }
        public string ProductNumberStructureId { get; set; }
        public string MultiPhotoFileFormatName { get; set; }
        public int Height { get; set; }
        public string LastUpdated { get; set; }
        public int Width { get; set; }
        [SolrUniqueKey("id")] public string id { get; set; }
        [SolrField("_version_")] public long _version_ { get; set; }
    }


   

}
