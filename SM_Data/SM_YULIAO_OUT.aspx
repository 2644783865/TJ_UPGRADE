<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="SM_YULIAO_OUT.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_YULIAO_OUT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ���ϳ����ѯ&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function ToStore() {
            window.open("SM_YULIAO_LIST.aspx?FLAG=ToStore");
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table width="80%">
                            <tr>
                                <td align="left">
                                    ���ƣ�
                                    <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                </td>
                                    <td align="left">
                                    ���ʣ�
                                    <asp:TextBox ID="txtCAIZHI" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                </td>
                                    <td align="left">
                                    ���
                                    <asp:TextBox ID="txtGUIGE" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnQuery" runat="server" Text="��  ѯ" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="��  ��" OnClick="btnReset_OnClick" />
                                </td>
                                <td align="center">
                                    <input id="ToStore" type="button" value="�����" onclick="ToStore()" runat="server" />
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
                                        ���ⵥ��
                                    </td>
                                      <td>
                                        ���ϱ���
                                    </td>
                                    <td>
                                        ����
                                    </td>
                                      <td>
                                        ����
                                    </td>
                                      <td>
                                        ���
                                    </td>
                                      <td>
                                        ����
                                    </td>
                                      <td>
                                        ���
                                          <td>
                                        ͼ��
                                    </td>
                                      <td>
                                        ����
                                    </td>
                                    </td>
                                    <td>
                                        ��������
                                    </td>
                                    <td>
                                        �����
                                    </td>
                                    <td>
                                        ������
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
                                        <%#Eval("OUTCODE")%>
                                    </td>
                                    <td>
                                        <%#Eval("Marid")%>
                                    </td> <td>
                                        <%#Eval("Name")%>
                                    </td> <td>
                                        <%#Eval("CAIZHI")%>
                                    </td> <td>
                                        <%#Eval("GUIGE")%>
                                    </td> <td>
                                        <%#Eval("LENGTH")%>
                                    </td> <td>
                                        <%#Eval("WIDTH")%>
                                    </td> <td>
                                        <%#Eval("TUXING")%>
                                    </td> <td>
                                        <%#Eval("WEIGHT")%>
                                    </td> 
                                   
                                    <td>
                                        <%#Eval("OUTNUM")%>
                                    </td>
                                    <td>
                                        <%#Eval("PJNAME")%>
                                    </td>
                                    <td>
                                        <%#Eval("OUTPER")%>
                                    </td>
                                    <td>
                                        <%#Eval("OUTDATE")%>
                                    </td>
                                    <td>
                                        <%#Eval("DOCUPER")%>
                                    </td>
                                    <td>
                                        <%#Eval("NOTE")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server" Visible="false">
                        û����س�����Ϣ!</asp:Panel>
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
