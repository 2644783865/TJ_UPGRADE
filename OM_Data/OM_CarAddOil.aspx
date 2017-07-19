<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_CarAddOil.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_CarAddOil" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    新增车辆加油记录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript" language="javascript">
function change(input)
{
//debugger;
 var DANJIA=document.getElementById('<%=txtUprice.ClientID%>').value;
 var shuliang=document.getElementById('<%=txtOilNum.ClientID%>').value;

 if(isNaN(DANJIA)||isNaN(shuliang))
 {
 alert("请填写正确数值！！！");
 if(isNaN(DANJIA))
 {
 document.getElementById('<%=txtUprice.ClientID%>').style.background = "red";
 }
 if(isNaN(shuliang))
 {
 document.getElementById('<%=txtOilNum.ClientID%>').style.background = "red";
 }
 }
 else
 {
 if(DANJIA==""||shuliang=="")
 {
 document.getElementById('<%=txtMoney.ClientID%>').value=0;
 }
 else
 {
 var  ss=changeTwoDecimal(DANJIA *shuliang);
 
 document.getElementById('<%=txtMoney.ClientID%>').value=ss.toString();
 }
 }
 var xfmoney=document.getElementById('<%=txtMoney.ClientID%>').value;

var cardye=document.getElementById('<%=lbl_bye.ClientID%>').value;
 var  ss=changeTwoDecimal(cardye*1.0-xfmoney*1.0);
 var leixing=$("#<%=ddl_leixing.ClientID %> option:selected").val();
 if(leixing=="cash")
{}
else
{
 document.getElementById('<%=zzye.ClientID%>').value=ss.toString();
 }
 
// 计算油耗
// var lc=document.getElementById('<%=licheng.ClientID %>').value;
//  
//  if(isNaN(lc))
//  {
//   alert("请填写正确公里数！！！");
// if(isNaN(lc))
// {
// document.getElementById('<%=licheng.ClientID%>').style.background = "red";
// }
// else
// {
// var yh=
// }
//  }
}
function ye(input)
{
var bye=document.getElementById('<%=lbl_bye.ClientID%>').value;

var leixing=$("#<%=ddl_leixing.ClientID %> option:selected").val();
if(leixing=="cash")
{

}
else
{

 

   var  ss=changeTwoDecimal(bye*1.0);
   document.getElementById('<%=lbl_bye.ClientID%>').value=ss.toString()
   

}
}
function changeTwoDecimal(v) 
{   
 if (isNaN(v)) 
 {//参数为非数字   
      return 0;   
   }   
  var fv = parseFloat(v);   
   fv = Math.round(fv * 100) / 100; //四舍五入，保留两位小数    
   var fs = fv.toString();   
    var fp = fs.indexOf('.');   
     if (fp < 0) 
     {       
      fp = fs.length;      
        fs += '.';   
         }    
         while (fs.length <= fp + 2)
          { //小数位小于两位，则补0      
            fs += '0';  
           }   
           return fs;
    }
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnOK" runat="server" Width="100px" Text="确 定" OnClick="btnOK_OnClick"
                                CssClass="button-outer" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReturn" runat="server" Width="100px" Text="返 回" OnClick="btnReturn_OnClick"
                                CausesValidation="False" CssClass="button-outer" />
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <table id="Table1" align="center" cellpadding="4" width="800px" cellspacing="1" runat="server"
        class="toptable grid" border="1">
        <tr>
            <td width="100px">
                车牌号：
            </td>
            <td>
                <asp:DropDownList ID="ddlcarnum" runat="server" Width="300px" />
            </td>
        </tr>
        <tr>
            <td width="100px">
                加油方式：
            </td>
            <td>
                <asp:DropDownList ID="ddl_leixing" runat="server" Width="60px" AutoPostBack="true"
                    OnSelectedIndexChanged="ddl_leixing_OnSelectedIndexChanged">
                    <asp:ListItem Text="加油卡" Value="card"></asp:ListItem>
                    <asp:ListItem Text="现金" Value="cash"></asp:ListItem>
                </asp:DropDownList>
                <%--<input id="leixing" runat="server" readonly="readonly" visible="false" style="width: 300px" />--%>
                <asp:DropDownList ID="ddl_card" runat="server" Width="240px" AutoPostBack="true"
                    OnSelectedIndexChanged="ddl_card_OnSelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="before_ye" runat="server" Text="充值前卡余额"></asp:Label>
            </td>
            <td>
                <%--<asp:Label ID="lbl_bye" runat="server" Enabled="false" Width="300px"></asp:Label>--%>
                <input id="lbl_bye" runat="server" readonly="readonly" style="width: 300px" />
                <span id="span3" runat="server" class="Error">*</span>
            </td>
        </tr>
<%--        <tr>
            <td>
                <asp:Label ID="je" runat="server" Text="充值金额"></asp:Label>
            </td>
            <td>
                <input id="txt_czje" runat="server" onchange="ye(this)" style="width: 300px" />
                <span id="span4" runat="server" class="Error">*</span>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ye" runat="server" Text="充值后卡余额"></asp:Label>
            </td>
            <td>
                <input id="txt_cardye" runat="server" readonly="readonly" style="width: 300px" />
                <span id="span2" runat="server" class="Error">*</span>
            </td>
        </tr>--%>
        <tr>
            <td>
                <asp:Label ID="lbl_licheng" runat="server" Text="加油前公里数"></asp:Label>
            </td>
            <td>
                <input id="licheng" runat="server" style="width: 300px" />
                <span id="span5" runat="server" class="Error">*</span>
            </td>
        </tr>
        <tr>
            <td>
                加油类型
            </td>
            <td>
                <asp:DropDownList ID="ddl_oil" runat="server" Width="300px">
                    <asp:ListItem Text="93#" Value="93#"></asp:ListItem>
                    <asp:ListItem Text="97#" Value="97#"></asp:ListItem>
                    <asp:ListItem Text="0#" Value="0#"></asp:ListItem>
                    <asp:ListItem Text="-10#" Value="-10#"></asp:ListItem>
                    <asp:ListItem Text="-20" Value="-20#"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="100px">
                日期：
            </td>
            <td>
                <asp:TextBox ID="txtrq" runat="server" Width="300px" class="easyui-datebox" editable="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100px">
                司机：
            </td>
            <td>
                <asp:DropDownList ID="txtDriver" runat="server" Width="300PX" />
                <%--<asp:TextBox ID="txtDriver" runat="server" Width="300px"></asp:TextBox>--%>
            </td>
        </tr>
        <tr>
            <td width="100px">
                单价：
            </td>
            <td id="price">
                <input id="txtUprice" runat="server" onkeyup="change(this)" style="width: 300px" />
                元/升（￥/L）
            </td>
        </tr>
        <tr>
            <td width="100px">
                加油量：
            </td>
            <td id="num">
                <input id="txtOilNum" runat="server" onkeyup="change(this)" style="width: 300px" />
                升（L）
            </td>
        </tr>
        <%--<tr>
            <td width="100px">
                油耗：
            </td>
            <td id="Td1">
                <input id="youhao" runat="server" readonly="readonly" style="width: 300px" />
                升/千米（L/KM）
            </td>
        </tr>--%>
        <tr>
            <td width="100px">
                花费金额：
            </td>
            <td>
                <input id="txtMoney" runat="server" readonly="readonly" style="width: 300px" />
                元（￥）
            </td>
        </tr>
        <tr>
            <td width="100px">
                <asp:Label ID="lbl_zzye" runat="server" Text="花费后卡金额："></asp:Label>
            </td>
            <td>
                <input id="zzye" runat="server" readonly="readonly" style="width: 300px" />
                元（￥）
            </td>
        </tr>
        <tr>
            <td width="100px">
                备注：
            </td>
            <td>
                <asp:TextBox ID="txtNote" runat="server" Width="300px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
