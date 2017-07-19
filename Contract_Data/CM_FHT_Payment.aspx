<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/PopupBase.master" CodeBehind="CM_FHT_Payment.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_FHT_Payment" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">
    <table width="100%">
    <tr>
    <td> <asp:Label ID="lblAction" runat="server"></asp:Label> </td>    
    <td align="center">
        <asp:Label ID="lblRemind" runat="server" Text="操作成功！" ForeColor="Red" Visible="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;
    </td>
    <td> <div id="zdgb" style="display:none"><span id="tiao" runat="server"></span><span>秒后自动关闭...</span></div></td>
    <td align="right">
     <asp:Button ID="btnQRZC" runat="server"  Text="确 认" onclick="btnQRZC_Click" OnClientClick="javascript:return confirm('确认提交吗？')" />
        &nbsp;&nbsp;</td>
    </tr>
    </table>    
<script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>  
<script language="javascript" type="text/javascript">
function AutoClose()
{
    zdgb.style.display="block";
    var t1=3;
    countDown(t1);
}
  function countDown(secs)
{
   ctl00_RightContentTitlePlace_tiao.innerText=secs;
   if(--secs>=0)
   {
     setTimeout("countDown("+secs+")",1000);
   }
   else
   {
     window.opener=null;
     window.open('','_self');
     window.close();
   }
}
</script>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<asp:Panel ID="palFKDetail" runat="server">
<table align="center" cellpadding="4" cellspacing="1" class="toptable grid" width="100%">
<tr>
<td >请款单号：&nbsp;<asp:Label ID="lblQKDH" runat="server"></asp:Label></td>
<td width="65px" align="right">支付状态：</td>
<td>
<asp:RadioButtonList ID="rblState" runat="server" RepeatDirection="Horizontal" >
    <asp:ListItem Value="0" Selected="True">保存</asp:ListItem>
    <asp:ListItem Value="1">正在签字</asp:ListItem>
</asp:RadioButtonList>
</td>   
</tr>
</table>
<table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1" width="100%">
<tr>
<td>请款部门</td>
<td>
    <asp:DropDownList ID="ddl_qkbm" runat="server">
    </asp:DropDownList>
    </td>
<td>请款用途</td>
<td>
    <asp:TextBox ID="txtCR_USE" runat="server"></asp:TextBox>
    </td>
</tr>
<tr>
<td>请款金额</td>
<td>
    <asp:TextBox ID="txtQKJE" runat="server" onblur="javascript:check_num(this)" Text="0"></asp:TextBox></td>
    <td>请款人</td>
    <td>
        <asp:TextBox ID="txtQKR" runat="server" ></asp:TextBox></td>
</tr>
<tr>
<td>请款日期</td>
<td>
    <asp:TextBox ID="txtQKRQ" runat="server" ></asp:TextBox>
    <cc1:CalendarExtender ID="calendar_qkrq" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="txtQKRQ"></cc1:CalendarExtender>
    </td>
<td>已支付</td>
<td><asp:TextBox ID="txtYZF" runat="server"  Enabled="false" Text="0"></asp:TextBox>
    </td>
</tr>
<tr>
<td>本次支付金额</td>
<td><asp:TextBox ID="txtZFJE" runat="server"  onblur="check_num(this)" OnTextChanged="txtZFJE_TextChanged" AutoPostBack="true" Text="0"></asp:TextBox>
    </td>
<td>支付日期</td>
<td><asp:TextBox ID="txtZFRQ" runat="server" ></asp:TextBox>
<cc1:CalendarExtender ID="calendar_zfrq" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"  
                        TodaysDateFormat="yyyy年MM月dd日"  TargetControlID="txtZFRQ"></cc1:CalendarExtender>
    </td>
</tr>
<tr>
<td>凭证</td>
<td >
<table>
    <tr>
    <td>
        <asp:RadioButtonList ID="rblPZ" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
        <asp:ListItem Text="暂无" Value="0" Selected="True"></asp:ListItem>
        <asp:ListItem Text="有" Value="1"></asp:ListItem>
        </asp:RadioButtonList>&nbsp;</td>
    <td>
        <asp:TextBox ID="txtPZH" runat="server"></asp:TextBox>
     </td>
     </tr>
    </table>
    </td>
 <td>收款单位</td>
 <td>
     <asp:TextBox ID="txtSKDW" runat="server"></asp:TextBox></td>
</tr>
<tr>
<td >备注</td>
<td colspan="3">
    <asp:TextBox ID="txtBZ" runat="server" Height="100px" Width="80%" TextMode="MultiLine"></asp:TextBox></td>
</tr>
</table>
</asp:Panel>            
</asp:Content>