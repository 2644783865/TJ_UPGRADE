<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="EQU_Part_In.aspx.cs" Inherits="ZCZJ_DPF.ESM_Data.EQU_Part_In" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �豸��������ѯ&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function ToOrder()
        {
            window.open("EQU_Part_Order.aspx?FLAG=ToOrder");
        }
    </script>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box-inner">
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
                                    <asp:Button ID="btnReset" runat="server" Text="��  ��" OnClick="btnReset_OnClick" />
                                </td>
                                <td align="center">
                                    <input id="ToOrder" type="button" value="������" onclick="ToOrder()" runat="server" />
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
                                        ��������
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
                                        �Ƶ���
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
                                        <asp:Label ID="lblInCode" runat="server" Text='<%#Eval("InCode")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("Name")%>
                                    </td>
                                    <td>
                                        <%#Eval("Type")%>
                                    </td>
                                    <td>
                                        <%#Eval("InNum")%>
                                    </td>
                                    <td>
                                        <%#Eval("Receiver")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInDate" runat="server" Text='<%#Eval("InDate")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblInDoc" runat="server" Text='<%#Eval("InDocu")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("Note")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                        û����������Ϣ!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnQuery" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
