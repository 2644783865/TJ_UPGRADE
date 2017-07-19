<%@ Page Language="C#" MasterPageFile="~/Masters/auditMaster.Master" AutoEventWireup="true" CodeBehind="PowerAudit.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.PowerAudit" Title="无标题页" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript" language="javascript">
          
     </script>
    <div style="width: 100%">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Label ID="audittitle" runat="server" Text="权限变更申请"></asp:Label><%--审批类型，每次添加新的审批时，必须修改--%>
                </td>
            </tr>
            <tr>
                <td id="td1" align="right">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="btnsave_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
            border="1">
            <tr>
                <td align="center">
                    单号：
                </td>
                <td align="center">
                    <asp:Label ID="txt_contentno" runat="server"></asp:Label>
                </td>
                <td align="center">
                    姓名：
                </td>
                <td align="center">
                    <asp:TextBox ID="stname" OnTextChanged="Textname_TextChanged" AutoPostBack="true" onclick="this.select();" runat="server"  runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                        DelimiterCharacters="" Enabled="True" MinimumPrefixLength="1" ServiceMethod="get_st_id"
                        ServicePath="~/Ajax.asmx" FirstRowSelected="true" TargetControlID="stname"
                        UseContextKey="True">
                    </asp:AutoCompleteExtender>
                    <asp:Label ID="stid" runat="server" Visible="false"></asp:Label>
                </td>
                <td align="center">
                    部门
                </td>
                <td align="center">
                    <asp:TextBox ID="txt_dep" runat="server" Enabled="false"></asp:TextBox>
                    <asp:HiddenField ID="hiddepid" runat="server" />
                    <asp:HiddenField ID="hidstate" runat="server" />
                </td>
                <td align="center">
                    职位：
                </td>
                <td align="center">
                    <asp:Label ID="DEP_POSITION" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    角色：
                </td>
                <td>
                    <asp:Label ID="role" runat="server"></asp:Label>
                </td>
                <td>
                    权限变更内容：
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txt_content" runat="server" Height="60px" TextMode="MultiLine" Width="100%"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div style="height:20px" align="left">具体权限内容如下：</div>
    <div style="width:100%">
         <iframe src="../Basic_Data/QX_Role_Detail.aspx?Action=view&&r_id=<%=roleid %>" style="width:100%; height:500px"></iframe>
    </div>
</asp:Content>
