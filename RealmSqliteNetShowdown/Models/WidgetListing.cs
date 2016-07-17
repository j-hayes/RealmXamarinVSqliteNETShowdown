using PropertyChanged;
using Realms;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace WidgetList.Abstract.Widgets
{
    [ImplementPropertyChanged]
    public class WidgetListing : RealmObject
    {
        [Realms.Indexed]
        public int Id { get; set; }
        public WidgetType WidgetType { get; set; }
        [Realms.Indexed]
        public string WidgetTypeName { get; set; }
        public double Price { get; set; }
        public int QuantityOnHand { get; set; }
        [Ignored]
        public string PriceStr { 
            get
            {
                return Price.ToString();
            }
        }
     
    }

    public class WidgetListingModel
    {

        [PrimaryKey] 
        public int Id { get; set; }

        [ForeignKey(typeof(WidgetTypeModel))]
        public int WidgetTypeId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public WidgetTypeModel WidgetType { get; set; }
        public string WidgetTypeName { get; set; }
        public double Price { get; set; }
        public int QuantityOnHand { get; set; }
        public string PriceStr
        {
            get
            {
                return Price.ToString();
            }
        }

    }
}