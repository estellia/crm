<%@ Page Title="" Language="C#" MasterPageFile="~/common/Site.master" AutoEventWireup="true" CodeFile="user_show.aspx.cs" Inherits="user_user_show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="tit_con"><span>客户信息</span><a href="#"></a></div>
 <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
  <tr>
    <td width="150" align="right" class="td_co">编码：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">名称：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">状态：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
</table>

 <div class="tit_con"><span>门店信息</span><a href="#"></a></div>
 <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
  <tr>
    <td width="150" align="right" class="td_co">编码：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">名称：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">状态：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">在线：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
</table>

 <div class="tit_con"><span>客户信息</span><a href="#"></a></div>
 <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
  <tr>
    <td width="150" align="right" class="td_co">编码：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">名称：</td>
    <td class="td_lp"><input class="ban_in" type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">状态：</td>
    <td><div class="box_r"></div><input class="fl" type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">状态：</td>
    <td><div class="box_r"></div><asp:TextBox ID="dsds" Text="aaa" runat="server" CssClass="fl"/></td>
  </tr>
  <tr>
    <td align="right" class="td_co">设备型号：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">设备号：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">数据交换地址：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">备用数据交换地址：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">失效日期：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
</table>

 <div class="tit_con"><span>客户信息</span><a href="#"></a></div>
 <table border="0" cellspacing="0" cellpadding="0" class="con_tab">
  <tr>
    <td width="150" align="right" class="td_co">创建人：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">创建时间：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">最后修改人：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">最后修改时间：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
  <tr>
    <td align="right" class="td_co">最后操作时间：</td>
    <td class="td_lp"><input type="text" name="textfield" id="textfield" /></td>
  </tr>
</table>
 </div>
  <div class="bf"><input name="提交" type="submit" class="input_bc" value="保 存" />
  <input name="提交" type="submit" class="input_fh" value="返 回" />
  </div>

</asp:Content>

