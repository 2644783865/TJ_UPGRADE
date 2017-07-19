<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true"
    CodeBehind="CM_EditOut.aspx.cs" Inherits="ZCZJ_DPF.CM_Data.CM_EditOut" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �޸ĳ�����Ϣ
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <base target="_self" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField runat="server" ID="UserID" />
    <div class="box-wrapper">
        <div class="box-outer">
            <table>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnAdd" Text="������" BackColor="LightGreen" OnClick="btnAdd_OnClick" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnDelete" Text="ɾ����" OnClick="btnDelete_OnClick" />
                    </td>
                </tr>
            </table>
            <div style="overflow: auto; width: 100%">
                <table id="gr" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                    border="1">
                    <asp:Repeater runat="server" ID="rptCK">
                        <HeaderTemplate>
                            <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                <td>
                                    <strong>���</strong>
                                </td>
                                <td>
                                    <strong>��������</strong>
                                </td>
                                <td>
                                    <strong>������Ŀ</strong>
                                </td>
                                <td>
                                    <strong>����ʱ��</strong>
                                </td>
                                <td>
                                    <strong>��ע</strong>
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)">
                                <td>
                                    <asp:CheckBox runat="server" ID="cbxXuHao" Text='<%# Convert.ToInt32(Container.ItemIndex +1) %>'
                                        TextAlign="Right" CssClass="checkBoxCss" />
                                    <asp:HiddenField runat="server" ID="ID" Value='<%#Eval("ID")%>' />
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CM_OUT" Text='<%#Eval("CM_OUT")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CM_OUTNUM" Text='<%#Eval("CM_OUTNUM")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CM_OUTDATE" class="easyui-datebox" onfocus="this.blur()"
                                        Text='<%#Eval("CM_OUTDATE")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="CM_NOTE" Text='<%#Eval("CM_NOTE")%>' ToolTip='<%#Eval("CM_NOTE")%>'></asp:TextBox>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center">
                û�м�¼!</asp:Panel>
            <br />
            <div style="text-align: center">
                <asp:Button ID="btnConfirm" runat="server" Text="ȷ ��" OnClick="btnConfirm_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnAll" runat="server" Text="ȫ������" OnClick="btnAll_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" value="�� ��" onclick="window.close()" />
            </div>
        </div>
    </div>
</asp:Content>
