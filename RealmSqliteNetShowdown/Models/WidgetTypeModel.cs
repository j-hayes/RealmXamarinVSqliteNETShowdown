using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace WidgetList.Abstract.Widgets
{

   public class WidgetTypeModel
    {
        public WidgetTypeModel() 
        {
            WidgetListings = new List<WidgetListingModel>(4);
        }

        [PrimaryKey]
        public int Id { get; set; }
        public string Description { get;  set; }
        [Indexed]
        public string Name { get; set; }
        public string Category { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<WidgetListingModel> WidgetListings { get; set; }
        public string UPCCode { get; set; } //barcode
    }
}
