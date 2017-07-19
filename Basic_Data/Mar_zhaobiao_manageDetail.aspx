<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mar_zhaobiao_manageDetail.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.Mar_zhaobiao_manageDetail" MasterPageFile="~/Masters/RightCotentMaster.master"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
 <meta http-equiv="pragma" content="no-cache">
<meta http-equiv="cache-control" content="no-cache">
 <meta http-equiv="expires" content="0">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <script src="../JS/DatePicker.js" type="text/javascript"></script>
    <asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" />
<div class="RightContentTitle">修改或新建招标物料信息</div>
<div class="box-inner"><div class="box_right"><div class='box-title'><table width='100%' ><tr><td> 
         招标物料信息(带<span class="red">*</span>号的为必填项)</td></tr></table></div></div></div>
                  <div class="box-wrapper">
            <div class="box-outer">
<table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
<tr>
<td>物料编码<span class="red">*</span></td><td><asp:TextBox ID="ib_marid" runat="server" AutoPostBack="True"  OnTextChanged="ib_marid_Textchanged"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="ib_marid" ErrorMessage="*"></asp:RequiredFieldValidator>
    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="ib_marid"
      ServicePath="~/PC_Data/PC_Data_AutoComplete.asmx" CompletionSetCount="10"  MinimumPrefixLength="1"
      ServiceMethod="GetCompletemarbyhc">
    </cc1:AutoCompleteExtender>   
    </td>
<td>供应商编码<span class="red">*</span></td><td><asp:TextBox ID="ib_supply" runat="server" AutoPostBack="True" OnTextChanged="ib_supply_Textchanged"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="ib_supply" ErrorMessage="*"></asp:RequiredFieldValidator>
       <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="ib_supply"
      ServicePath="~/PC_Data/PC_Data_AutoComplete.asmx" CompletionSetCount="10"  MinimumPrefixLength="1"
      ServiceMethod="GetCompleteProvider">
    </cc1:AutoCompleteExtender>  
        
    </td>
</tr>
<tr>
<td>价格<span class="red">*</span></td><td><asp:TextBox ID="ib_price" runat="server" ></asp:TextBox>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="ib_price" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>

<td>税率<span class="red">*</span></td>
<td>
    <asp:TextBox ID="ib_taxrate" runat="server" Text="17"></asp:TextBox>  
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="ib_taxrate" ErrorMessage="*"></asp:RequiredFieldValidator>   
</td></tr>

<tr>
<td>起始日期<span class="red">*</span></td><td>
    <input id="ib_date" runat="server" type="text" onclick="setday(this)" value="" readonly="readonly" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ControlToValidate="ib_date" ErrorMessage="*"></asp:RequiredFieldValidator>   
</td>
<td>截止日期<span class="red">*</span></td>
<td><input id="ib_fidate" type="text" runat="server" onclick="setday(this)" value="" readonly="readonly" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
        ControlToValidate="ib_fidate" ErrorMessage="*"></asp:RequiredFieldValidator>   
</td>
</tr>
<%--<tr>
<td>物料长度</td><td> <asp:TextBox ID="ib_length" runat="server" Text="0"></asp:TextBox></td>

<td>物料宽度</td><td> <asp:TextBox ID="ib_width" runat="server" Text="0"></asp:TextBox></td></tr>--%>
<tr>
<td colspan='2'>备注：</td>
<td>状态：
</td>
<td>
 <asp:RadioButtonList ID="rblstatus" RepeatColumns="2" runat="server" >                          
                        <asp:ListItem Text="停用" value="1"></asp:ListItem>              
                        <asp:ListItem Text="在用" value="0"></asp:ListItem>                      
                    </asp:RadioButtonList>
</td>
</tr>
<tr>
<td colspan='2'>
    <asp:TextBox ID="ib_note" runat="server" TextMode="MultiLine" Columns="80" Rows="8" ></asp:TextBox></td>
</tr>
</table>
<asp:Label ID="lbl_Info" runat="server" EnableViewState="False" CssClass="red"></asp:Label> <br />
                <asp:Button ID="btn_Submit" runat="server" Text="提交" 
                    onclick="btn_Submit_Click" /> &nbsp;
                <input type="button" id="btn_Back" value="返回" onclick="window.close()" />
 </div></div>
</asp:Content>
