using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///ALDOrderRequest 的摘要说明
/// </summary>
public class ALDOrderRequest
{
    public int? Locale { get; set; }
    public Guid? UserID { get; set; }
    public int? BusinessZoneID { get; set; }
    public string Token { get; set; }
    public object Parameters { get; set; }
}