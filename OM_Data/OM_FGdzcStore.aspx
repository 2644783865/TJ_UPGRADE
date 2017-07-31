<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_FGdzcStore.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_FGdzcStore" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �ǹ̶��ʲ�����ѯ&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <%--  <script type="text/javascript" language="javascript">
        function PushConfirm() {
            var retVal = confirm("ȷ������ѡ����Ŀ����������ⵥ��");
            return retVal;
        }
    </script>--%>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="right">
                            �ǹ̶��ʲ���ţ�
                            <asp:textBox ID="txtID" runat="server" Text=""></asp:textBox>&nbsp;&nbsp;&nbsp;
                            ʹ���ˣ�
                            <asp:TextBox ID="txtPer" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            ���ƣ�
                            <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            ���������
                            <asp:TextBox ID="txtModel" runat="server" Text=""></asp:TextBox>&nbsp; 
                            ����2��
                            <asp:TextBox ID="txtType" runat="server" Text=""></asp:TextBox>&nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnQuery" runat="server" Text="��  ѯ" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="��  ��" OnClick="btnReset_OnClick" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnExport" runat="server" Text="����" OnClick="btnExport_OnClick" />&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            
                        </td>
                        <td>
                            <asp:Button ID="btnTransf" runat="server" Text="�ǹ̶��ʲ�ת��" BackColor="LightGreen" OnClick="btnTransf_click" />
                        </td>
                        <td>
                            <asp:Button ID="btnBaofei" runat="server" Text="�ǹ̶��ʲ�����" BackColor="LightGreen" OnClick="btnBaofei_Click" />
                        </td>
                        <%--<td align="center">
                                    <asp:Button ID="btnPush" runat="server" Text="����" OnClick="btnPush_OnClick" OnClientClick="return PushConfirm()"
                                        Visible="false" />
                                </td>--%>
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
                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                            <td>
                                <strong>���</strong>
                            </td>
                            <td>
                                <strong>�ǹ̶��ʲ����</strong>
                            </td>
                            <td>
                                <strong>����</strong>
                            </td>
                            <td>
                                <strong>�������</strong>
                            </td>
                            <td>
                                <strong>����1</strong>
                            </td>
                            <td>
                                <strong>����2</strong>
                            </td>
                            <td>
                                <strong>ʹ����</strong>
                            </td>
                            <td>
                                <strong>ʹ�ò���</strong>
                            </td>
                            <td>
                                <strong>��ʼʱ��</strong>
                            </td>
                            <td>
                                <strong>ʹ������(��)</strong>
                            </td>
                            <td>
                                <strong>��ֵ(Ԫ)</strong>
                            </td>
                            <td>
                                <strong>��ŵص�</strong>
                            </td>
                            <td>
                                <strong>��ע</strong>
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                            <td>
                                <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%><asp:Label
                                    ID="lbID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblbh" runat="server" Text='<%#Eval("BIANHAO")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                            </td>
                             <td>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("TYPE")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("TYPE2")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblsyr" runat="server" Text='<%#Eval("SYR")%>'></asp:Label><asp:Label
                                    ID="lblsyrid" runat="server" Visible="false" Text='<%#Eval("SYRID")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblsybm" runat="server" Text='<%#Eval("SYBUMEN")%>'></asp:Label><asp:Label
                                    ID="lblsybumenid" runat="server" Visible="false" Text='<%#Eval("SYBUMENID")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("SYDATE")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblnx" runat="server" Text='<%#Eval("NX")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbljz" runat="server" Text='<%#Eval("JIAZHI")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblplace" runat="server" Text='<%#Eval("PLACE")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblnote" runat="server" Text='<%#Eval("NOTE")%>'></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Panel ID="NoDataPanel" runat="server">
                û�����������Ϣ!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
