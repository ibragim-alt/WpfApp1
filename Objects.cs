//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Objects
    {
        public int ObjectID { get; set; }
        public string AddressCity { get; set; }
        public string AdressStreet { get; set; }
        public string AdressHouse { get; set; }
        public string AddressNumber { get; set; }
        public Nullable<double> CoordinateLatitude { get; set; }
        public Nullable<double> CoordinateLongitude { get; set; }
    
        public virtual Apartments Apartments { get; set; }
        public virtual Houses Houses { get; set; }
        public virtual Lands Lands { get; set; }
    }
}
