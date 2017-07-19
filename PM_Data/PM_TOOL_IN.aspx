<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_TOOL_IN.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_TOOL_IN" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ��������ѯ&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box-inner">
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <div class="box_right">
                    <div class="box-title">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    ���ƣ�
                                    <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnQuery" runat="server" Text="��  ѯ" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
                                </td>
                                <td runat="server" id="add">
                                    <asp:HyperLink ID="addinbill" runat="server" ForeColor="Red" NavigateUrl="~/PM_Data/PM_TOOL_INBILL.aspx?action=add">
                                        <asp:Label ID="Label" runat="server" Text="�������"> </asp:Label></asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-wrapper">
                <div class="box-outer">
                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle">
                                    <td>
                                        ���
                                    </td>
                                    <td>
                                        ��ⵥ��
                                    </td>
                                    <td>
                                        �������
                                    </td>
                                    <td>
                                        ����
                                    </td>
                                    <td>
                                        ����ͺ�
                                    </td>
                                    <td>
                                        �������
                                    </td>
                                    <td>
                                        ����Ա
                                    </td>
                                    <td>
                                        ��������
                                    </td>
                                    <td>
                                        ��ע
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr align="center">
                                    <td>
                                        <%#Container.ItemIndex+1%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInCode" runat="server" Text='<%#Eval("INCODE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("TYPE")%>
                                    </td>
                                    <td>
                                        <%#Eval("NAME")%>
                                    </td>
                                    <td>
                                        <%#Eval("MODEL")%>
                                    </td>
                                    <td>
                                        <%#Eval("INNUM")%>
                                    </td>
                                    <td>
                                        <%#Eval("RECEIVER")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInDate" runat="server" Text='<%#Eval("INDATE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("NOTE")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                        û����ص��������Ϣ!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnQuery" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
