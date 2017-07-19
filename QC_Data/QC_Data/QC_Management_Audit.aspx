<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true"
    CodeBehind="QC_Management_Audit.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_Management_Audit" %>

<%@ Register Src="~/Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    管理评审
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/jscript">
        function openLink(url) {
            var returnVlue = window.showModalDialog("QC_Management_Audit_Edit.aspx?action=add&Id=" + url, '', "dialogHeight:185px;dialogWidth:500px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 30%; height: 24px">
                            <b>中材(天津)重型机械有限公司管理评审</b>
                        </td>
                        <%--<td align="right">
                            按名称查询：
                            <asp:DropDownList ID="DropDownList2" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            <asp:Button ID="Button1" runat="server" Text="查询" />
                        </td>--%>
                        <td align="right">
                            <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="openLink(0)" CssClass="link">新增管审</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div>
        <table cellpadding="4" cellspacing="1" class="toptable grid" border="1">
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <tr align="center" class="tableTitle headcolor">
                        <td>
                            <strong>序号</strong>
                        </td>
                        <td>
                            <strong>名称</strong>
                        </td>
                        <td>
                            <strong>时间</strong>
                        </td>
                        <td>
                            <strong>备注</strong>
                        </td>
                        <td>
                            <strong>附件</strong>
                        </td>
                        <td>
                            <strong>查看</strong>
                        </td>
                        <td>
                            <strong>删除</strong>
                        </td>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr ondblclick="javascript:window.showModalDialog('QC_Internal_Audit_Edit.aspx?action=edit&id=<%#Eval("PRO_ID")%>','','DialogWidth=500px;DialogHeight=185px')"
                        title="双击修改" style="cursor: pointer">
                        <td width="50px" align="center">
                            <%#Eval("ID_Num") %>
                        </td>
                        <td width="150px" align="center">
                            <%#Eval("PRO_NAME")%>
                        </td>
                        <td width="150px" align="center">
                            <%#Eval("PRO_TIME")%>
                        </td>
                        <td width="500px">
                            <%#Eval("PRO_NOTE")%>
                        </td>
                        <td align="center">
                            <%#Eval("PRO_FUJIAN")%>
                            <asp:HiddenField ID="HiddenFieldType" runat="server" Visible="false" />
                        </td>
                        <td width="80px" align="center">
                            <asp:HyperLink ID="HyperLinkDetail" runat="server" NavigateUrl='<%#"~/QC_Data/QC_Management_Audit_Detail.aspx?ProId="+Eval("PRO_ID") %>'>
                                <asp:Image ID="Image11" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />查看</asp:HyperLink>
                        </td>
                        <td width="80px" align="center">
                            <asp:LinkButton ID="LinkButDel" runat="server" OnClick="LinkButDel_Click" CommandArgument='<%#Eval("PRO_ID")%>'>
                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />删除</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <asp:Panel ID="NoDataPanel" runat="server">
            没有记录!</asp:Panel>
        <uc1:UCPaging runat="server" ID="UCPaging1" />
    </div>
</asp:Content>
