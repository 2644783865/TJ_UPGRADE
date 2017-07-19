<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="enginfoDetail.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.enginfoDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/DatePicker.js" type="text/javascript"></script>
 <meta http-equiv="pragma" content="no-cache">
<meta http-equiv="cache-control" content="no-cache">
 <meta http-equiv="expires" content="0">
    <asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" />
<div class="RightContentTitle">修改或新建工程信息</div>
<div class="box-inner"><div class="box_right"><div class='box-title'>
<table width='100%' >
       <tr>
          <td width='50%'> 
            工程信息(带<span class="red">*</span>号的为必填项)</td>          
         </tr>
         </table></div></div></div>
<div class="box-wrapper">
<div class="box-outer">
<table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
<tr>
<td>工程代号<span class="red">*</span></td><td colspan='4'><asp:TextBox ID="eng_CODE" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="eng_CODE" ErrorMessage="*"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
      ErrorMessage="工程代号只能由数字和大写字母组成" ValidationExpression="^[A-Z0-9]+$" 
      ControlToValidate="eng_CODE"></asp:RegularExpressionValidator>     
        </td>
</tr>
<tr>
<td>工程名称<span class="red">*</span></td><td colspan='4'><asp:TextBox ID="eng_NAME" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="eng_NAME" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td>工程大类</td>
        <td style="width:80px">
        <asp:DropDownList ID="ddlEnginType" runat="server" onselectedindexchanged="ddlEnginType_SelectedIndexChanged" AutoPostBack="true">
        <asp:ListItem>-请选择-</asp:ListItem>
        <asp:ListItem>回转窑</asp:ListItem>
        <asp:ListItem>球、立磨</asp:ListItem>
        <asp:ListItem>篦冷机</asp:ListItem>
        <asp:ListItem>堆取料机</asp:ListItem>
        <asp:ListItem>钢结构及非标</asp:ListItem>
        <asp:ListItem>电气及其他</asp:ListItem>
        </asp:DropDownList>
        </td>
        <td>
            <asp:RadioButton ID="RadioButton1" runat="server"  Text="添加小类" GroupName="view"
                AutoPostBack="True" oncheckedchanged="RadioButton1_CheckedChanged"/>
            <asp:RadioButton ID="RadioButton2" runat="server"  Text="选取小类" GroupName="view"
                AutoPostBack="True" oncheckedchanged="RadioButton2_CheckedChanged" Checked="true"/>
        </td>
        <td><span>工程小类</span></td>
        <td><asp:DropDownList ID="ddlEnginsmType" runat="server">
       </asp:DropDownList>
       <asp:TextBox ID="tbengType" runat="server" Visible="false" Text=""></asp:TextBox></td>

</tr>
<tr>
 <td colspan='5'>备注：</td></tr>
<tr>
<td colspan='5'>
    <asp:TextBox
        ID="eng_NOTE" runat="server" TextMode="MultiLine" Columns="100" Rows="8" ></asp:TextBox></td>
</tr>
</table>
<asp:Label ID="lbl_Info" runat="server" EnableViewState="False" CssClass="red"></asp:Label> <br />
                <asp:Button ID="btn_Submit" runat="server" Text="提交" onclick="btn_Submit_Click" /> &nbsp;
                <input type="button" id="btn_Back" value="返回" onclick="window.close()" />
 </div></div>
</asp:Content>
