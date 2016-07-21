using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace WidgetList.Abstract.Widgets
{

    public class WidgetListingModel
    {

        [PrimaryKey] 
        public int Id { get; set; }

        [ForeignKey(typeof(WidgetTypeModel))]
        public int WidgetTypeId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public WidgetTypeModel WidgetType { get; set; }
        [SQLite.Net.Attributes.Indexed]
        public string WidgetTypeName { get; set; }
        public double Price { get; set; }
        public int QuantityOnHand { get; set; }
        [Ignore]
        public string PriceStr
        {
            get
            {
                return Price.ToString();
            }
        }

    }
}