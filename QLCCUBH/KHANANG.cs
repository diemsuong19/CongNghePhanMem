//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLCCUBH
{
    using System;
    using System.Collections.Generic;
    
    public partial class KHANANG
    {
        public string MaNhaCungCap { get; set; }
        public string MaHangHoa { get; set; }
        public string GhiChu { get; set; }
    
        public virtual HANGHOA HANGHOA { get; set; }
        public virtual NHACUNGCAP NHACUNGCAP { get; set; }
    }
}