<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="PM_Expense_Audit.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Expense_Audit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    生产工时——审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link rel="Stylesheet" type="text/css" href="../Assets/main.css" />
    <link type="text/css" href="FixTable.css" rel="stylesheet" />
    <link href="../Assets/Setting.css" rel="stylesheet" type="text/css" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RightContent">
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table width="100%">
                                <tr>
                                    <td>
                                        审批状态：
                                        <asp:DropDownList ID="rbl_type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbl_type_OnSelectedIndexChanged">
                                            <asp:ListItem Text="我的审批任务" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="未审批" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="审批中" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="已通过" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="已驳回" Value="4"></asp:ListItem>
                                        </asp:DropDownList> 
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <table width="100%" align="center" class="nowrap cptable fullwidth">
                            <asp:Repeater ID="PM_GongShi_Audit_Repeater" runat="server" OnItemDataBound="PM_GongShi_Audit_Repeater_ItemDataBound">
                                <HeaderTemplate>
                                    <tr align="center" class="tableTitle" style="background-color: #5CACEE">
                                        <td>
                                            <strong>行号</strong>
                                        </td>
                                        <td>
                                            <strong>任务单号</strong>
                                        </td>
                                        <td>
                                            <strong>顾客名称</strong>
                                        </td>
                                        <td>
                                            <strong>合同号</strong>
                                        </td>
                                        <td>
                                            <strong>图号</strong>
                                        </td>
                                        <td>
                                            <strong>备注</strong>
                                        </td>
                                        <td>
                                            <strong>总计工时</strong>
                                        </td>
                                        <td runat="server" id="haudit">
                                            <strong>审核</strong>
                                        </td>
                                        <td runat="server" id="hlookup">
                                            <strong>查看</strong>
                                        </td>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                        <td runat="server" id="row_num">
                                            <asp:Label ID="rownum" runat="server" Text='<%#Container.ItemIndex + 1 %>'></asp:Label>
                                            &nbsp;
                                        </td>
                                        <asp:Label ID="DocuNum" Visible="false" runat="server" Text='<%#Eval("DOCUNUM")%>' />
                                        <td>
                                            <asp:TextBox ID="TSA_ID" runat="server" Text='<%#Eval("TSA_ID")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CM_CUSNAME" runat="server" Text='<%#Eval("CM_CUSNAME")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="CM_CONTR" runat="server" Text='<%#Eval("CM_CONTR")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TSA_MAP" runat="server" Text='<%#Eval("TSA_MAP")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="GS_NOTE" runat="server" Text='<%#Eval("GS_NOTE")%>' />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="GS_HOURS" runat="server" Text='<%#Eval("GS_HOURS")%>' />
                                        </td>
                                        <td runat="server" id="baudit">
                                            <asp:HyperLink ID="hyp_edit" runat="server">
                                                <asp:Label ID="Label1" runat="server" Text="审核"></asp:Label></asp:HyperLink>
                                        </td>
                                        <td runat="server" id="blookup">
                                            <asp:HyperLink ID="HyperLink_lookup" runat="server">
                                                <asp:Label ID="PUR_DD" runat="server" Text="查看"></asp:Label></asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                            没有记录!</asp:Panel>
                        <uc1:UCPaging ID="UCPaging1" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
