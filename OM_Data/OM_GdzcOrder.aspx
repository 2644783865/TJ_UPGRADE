<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_GdzcOrder.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.Master"
    Inherits="ZCZJ_DPF.OM_Data.OM_GdzcOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �̶��ʲ��ɹ�����&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../SM_Data/SM_JS/SelectCondition.js" type="text/javascript"></script>

    <script src="../SM_Data/SM_JS/superTables.js" type="text/javascript"></script>

    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <link href="../SM_Data/StyleFile/superTables.css" rel="stylesheet" type="text/css" />
    <link href="../SM_Data/StyleFile/superTables_Default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function PushConfirm() {
            var retVal = confirm("ȷ������ѡ����Ŀ����������ⵥ��");
            return retVal;
        }
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="right">
                            ���ƣ�
                            <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            ���������
                            <asp:TextBox ID="txtModel" runat="server" Text=""></asp:TextBox>&nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnQuery" runat="server" Text="��  ѯ" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="��  ��" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnPush" runat="server" Text="����" OnClick="btnPush_OnClick" OnClientClick="return PushConfirm()">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table id="GridView1" width="100%" align="center" cellpadding="4" cellspacing="1"
                        class="toptable grid" border="1">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle">
                                    <td>
                                        ���
                                    </td>
                                    <td>
                                        �������
                                    </td>
                                    <td>
                                        ����
                                    </td>
                                    <td>
                                        �������
                                    </td>
                                    <td>
                                        ��������
                                    </td>
                                    <td>
                                        ������
                                    </td>
                                    <td>
                                        ����
                                    </td>
                                    <td>
                                        ��ע
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr align="center" style="cursor: pointer" class="baseGadget" onmouseover="this.className='highlight'"
                                    onmouseout="this.className='baseGadget'" ondblclick="<%#showYg(Eval("CODE").ToString())%>"
                                    title="˫���鿴����">
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("CODE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("NAME")%>
                                    </td>
                                    <td>
                                        <%#Eval("MODEL")%>
                                    </td>
                                    <td>
                                        <%#Eval("NUM")%>
                                    </td>
                                    <td>
                                        <%Eval("AGENT")%>
                                    </td>
                                    <td>
                                        <%#Eval("DEPARTMENT")%>
                                    </td>
                                    <td>
                                        <%#Eval("NOTE")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server" Visible="False">
                        û����ؼ�¼!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
