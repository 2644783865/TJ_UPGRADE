<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master" CodeBehind="PM_TOOL_LIST.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_TOOL_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ���߿���ѯ&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function PushConfirm()
        {
            var retVal=confirm("ȷ������ѡ����Ŀ�������ɳ��ⵥ��");
            return retVal;
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
                                <td align="left">
                                    <label>�������</label>
                                    <asp:DropDownList ID="ddltooltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltooltype_SelectedIndexChanged">
                                    <asp:ListItem Text="ȫ��" Value="%" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="���ӳ���" Value="���ӳ���"></asp:ListItem>
                                    <asp:ListItem Text="���ӹ���Ƭ" Value="���ӹ���Ƭ"></asp:ListItem>
                                    <asp:ListItem Text="��ͷ" Value="��ͷ"></asp:ListItem>
                                    <asp:ListItem Text="˿׶" Value="˿׶"></asp:ListItem>
                                    <asp:ListItem Text="����" Value="����"></asp:ListItem>
                                    <asp:ListItem Text="ϳ��" Value="ϳ��"></asp:ListItem>
                                    <asp:ListItem Text="�ʵ�" Value="�ʵ�"></asp:ListItem>
                                    <asp:ListItem Text="��湤��" Value="��湤��"></asp:ListItem>
                                    <asp:ListItem Text="�Ѻ����" Value="�Ѻ����"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnPush" runat="server" Text="����" OnClick="btnPush_OnClick" OnClientClick="return PushConfirm()" Visible="false" />
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
                                        <strong>���</strong>
                                    </td>
                                    <td>
                                        <strong>�������</strong>
                                    </td>
                                    <td>
                                        <strong>����</strong>
                                    </td>
                                    <td>
                                        <strong>����ͺ�</strong>
                                    </td>
                                    <td>
                                        <strong>����</strong>
                                    </td>
                                    <td>
                                        <strong>��λ</strong>
                                    </td>
                                    <td>
                                        <strong>��ע</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex+1%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("TYPE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNumber" runat="server" Text='<%#Eval("NUMBER")%>'></asp:Label>
                                    </td>
                                     <td>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("UNIT")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNote" runat="server" Text='<%#Eval("NOTE")%>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        û����ص�����Ϣ!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>