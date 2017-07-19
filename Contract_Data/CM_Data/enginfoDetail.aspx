<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="enginfoDetail.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.enginfoDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/DatePicker.js" type="text/javascript"></script>
    <asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" />
<div class="RightContentTitle">修改或新建工程信息</div>
<div class="box-inner"><div class="box_right"><div class='box-title'><table width='100%' >
       <tr>
          <td> 
            工程信息(带<span class="red">*</span>号的为必填项)</td>
          <td colspan="2"> <asp:HyperLink ID="hpTask"  runat="server">新建任务单</asp:HyperLink></td>
         </tr>
         </table></div></div></div>
<div class="box-wrapper">
<div class="box-outer">
<table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
<tr>
<td>工程名称<span class="red">*</span></td><td><asp:TextBox ID="eng_NAME" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="eng_NAME" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
<td>项目编号<span class="red">*</span></td>
<td>
    <asp:DropDownList ID="eng_PJID" runat="server" Width="100px"  >
    
    </asp:DropDownList>
   
</td>

</tr>
<tr>
<td>工程全名<span class="red">*</span></td><td><asp:TextBox  ID="eng_FULLNAME" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="eng_FULLNAME" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>

<td>工程编号<span class="red">*</span></td><td><asp:TextBox  ID="eng_CODE"  runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="eng_CODE" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
    </tr>
    
    <tr>
<td>维护人（请填写维护人ID，为整数值）<span class="red">*</span></td><td><asp:TextBox ID="eng_MANCLERK" runat="server"/>
    <asp:CompareValidator ID="CompareValidator5" runat="server" 
        ControlToValidate="eng_MANCLERK" ErrorMessage="*" Operator="DataTypeCheck" 
        Type="Integer" ValueToCompare="0"></asp:CompareValidator>
    </td>

<td>工程状态<span class="red">*</span></td>
<td>
    <asp:DropDownList ID="eng_STATE" runat="server" Width="100px">
        <asp:ListItem Value="0">进行中</asp:ListItem>
        <asp:ListItem Value="1">完工</asp:ListItem>
        <asp:ListItem Value="2">停工</asp:ListItem>
    </asp:DropDownList>
</td></tr>

<tr>
<td>开始日期：<span class="red">*</span></td><td>
    <input id="eng_STARTDATE" runat="server" type="text" onclick="setday(this)" value="" readonly="readonly" />
</td>
<td>交工日期：<span class="red">*</span></td><td><input id="eng_CONTRACTDATE" type="text" runat="server" onclick="setday(this)" value="" readonly="readonly" /></td>
</tr>
<tr>
<td>实际完成日期：<span class="red">*</span></td><td><input id="eng_REALFINISHDATE" type="text" runat="server" onclick="setday(this)" value="" readonly="readonly" /></td>
<td>项目名称<span class="red">*</span></td>
<td>
    <asp:DropDownList ID="eng_PJNAME" runat="server" Width="100px"  >
    
    </asp:DropDownList>
   
</td>
</tr>
<tr>
<td colspan='2'>备注：</td></tr>
<tr>
<td colspan='2'>
    <asp:TextBox
        ID="eng_NOTE" runat="server" TextMode="MultiLine" Columns="80" Rows="8" ></asp:TextBox></td>
</tr>
</table>
<asp:Label ID="lbl_Info" runat="server" EnableViewState="False" CssClass="red"></asp:Label> <br />
                <asp:Button ID="btn_Submit" runat="server" Text="提交" onclick="btn_Submit_Click" /> &nbsp;
                <input type="button" id="btn_Back" value="返回" onclick="window.location.href='enginfo.aspx'" />
 </div></div>
</asp:Content>
