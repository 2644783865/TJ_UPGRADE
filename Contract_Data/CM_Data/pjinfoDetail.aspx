<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="pjinfoDetail.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.pjinfoDetail_new" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/DatePicker.js" type="text/javascript"></script>
    <asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" />
<div class="RightContentTitle">�޸Ļ��½���Ŀ��Ϣ</div>
<div class="box-inner"><div class="box_right"><div class='box-title'><table width='100%' ><tr><td> 
         ��Ŀ��Ϣ(��<span class="red">*</span>�ŵ�Ϊ������)</td></tr></table></div></div></div>
                  <div class="box-wrapper">
            <div class="box-outer">
<table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
<tr>
<td>��Ŀ����<span class="red">*</span></td><td><asp:TextBox ID="tb_PJ_NAME" runat="server" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="tb_PJ_NAME" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
<td>��Ŀ���<span class="red">*</span></td><td><asp:TextBox ID="tb_PJ_CODE" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="tb_PJ_CODE" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>

</tr>
<tr>
<td>ά���ˣ�����дά����ID��Ϊ����ֵ��<span class="red">*</span></td><td><asp:TextBox ID="tb_PJ_MANCLERK" runat="server"/>
    <asp:CompareValidator ID="CompareValidator1" runat="server" 
        ControlToValidate="tb_PJ_MANCLERK" ErrorMessage="*" Operator="DataTypeCheck" 
        Type="Integer" ValueToCompare="0"></asp:CompareValidator>
    </td>

<td>��Ŀ״̬<span class="red">*</span></td>
<td>
    <asp:DropDownList ID="ddl_PJ_STATE" runat="server" Width="100px">
        <asp:ListItem Value="0">������</asp:ListItem>
        <asp:ListItem Value="1">�깤</asp:ListItem>
        <asp:ListItem Value="2">ͣ��</asp:ListItem>
    </asp:DropDownList>
</td></tr>

<tr>
<td>ǩ�����ڣ�<span class="red">*</span></td><td>
    <input id="cal_PJ_FILLDATE" runat="server" type="text" onclick="setday(this)" value="" readonly="readonly" />
</td><td>��ʼ���ڣ�<span class="red">*</span></td><td><input id="cal_PJ_STARTDATE" type="text" runat="server" onclick="setday(this)" value="" readonly="readonly" /></td>
</tr>
<tr>
<td>�������ڣ�<span class="red">*</span></td><td><input id="cal_PJ_CONTRACTDATE" type="text" runat="server" onclick="setday(this)" value="" readonly="readonly" /></td>

<td>ʵ��������ڣ�<span class="red">*</span></td><td><input id="cal_PJ_REALFINISHDATE" type="text" runat="server" onclick="setday(this)" value="" readonly="readonly" /></td></tr>
<tr>
<td colspan='2'>��ע��</td></tr>
<tr>
<td colspan='2'>
    <asp:TextBox
        ID="ta_PJ_NOTE" runat="server" TextMode="MultiLine" Columns="80" Rows="8" ></asp:TextBox></td>
</tr>
</table>
<asp:Label ID="lbl_Info" runat="server" EnableViewState="False" CssClass="red"></asp:Label> <br />
                <asp:Button ID="btn_Submit" runat="server" Text="�ύ" 
                    onclick="btn_Submit_Click" /> &nbsp;
                <input type="button" id="btn_Back" value="����" onclick="window.location.href='pjinfo.aspx'" />
 </div></div>
</asp:Content>
