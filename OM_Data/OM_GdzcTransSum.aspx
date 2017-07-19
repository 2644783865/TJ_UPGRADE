<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_GdzcTransSum.aspx.cs"
    MasterPageFile="~/Masters/RightCotentMaster.Master" Inherits="ZCZJ_DPF.OM_Data.OM_GdzcTransSum" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �̶��ʲ�ת�ƻ���&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var id = $(this).find("input[name*=DH1]").val();
                window.open("OM_GdzcTrans.aspx?action=read&id=" + id);
            });
        })
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:RadioButtonList runat="server" ID="rblSPZT" AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Text="ȫ��" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="�����" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="��ͨ��" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="�Ѳ���" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    ���ƣ�
                                    <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                    ���������
                                    <asp:TextBox ID="txtModel" runat="server" Text=""></asp:TextBox>&nbsp;
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnQuery" runat="server" Text="��  ѯ" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnReset" runat="server" Text="��  ��" OnClick="btnReset_OnClick" />
                                </td>
                                <%--<td><asp:Button ID="btnAdd" runat="server" Text="�������" OnClick="btnAdd_click" /></td>--%>
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
                    <table id="tab" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        style="white-space: normal" border="1">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                                    <td>
                                        <strong>���</strong>
                                    </td>
                                    <td>
                                        <strong>ת�Ƶ���</strong>
                                    </td>
                                    <td>
                                        <strong>���</strong>
                                    </td>
                                    <td>
                                        <strong>����</strong>
                                    </td>
                                    <td>
                                        <strong>�������</strong>
                                    </td>
                                    <td>
                                        <strong>ǰʹ����</strong>
                                    </td>
                                    <td>
                                        <strong>ǰʹ�ò���</strong>
                                    </td>
                                    <td>
                                        <strong>ǰʹ��ʱ��</strong>
                                    </td>
                                    <td>
                                        <strong>ǰ��ŵص�</strong>
                                    </td>
                                    <td>
                                        <strong>��ʹ����</strong>
                                    </td>
                                    <td>
                                        <strong>��ʹ�ò���</strong>
                                    </td>
                                    <td>
                                        <strong>��ʹ��ʱ��</strong>
                                    </td>
                                    <td>
                                        <strong>�ִ�ŵص�</strong>
                                    </td>
                                    <td>
                                        <strong>������</strong>
                                    </td>
                                    <td style="width: 180px">
                                        <strong>ת��ԭ��</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'"
                                    title="˫���鿴��ϸ��Ϣ">
                                    <td>
                                        <%#Container.ItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="DH" Text='<%#Eval("DH")%>'></asp:Label>
                                        <input type="hidden" runat="server" id="DH1" name="DH1" value='<%#Eval("DH")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblbh" runat="server" Text='<%#Eval("ZYBIANHAO")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblModel" runat="server" Text='<%#Eval("MODEL")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblsyr" runat="server" Text='<%#Eval("FORMERNAME")%>'></asp:Label><asp:Label
                                            ID="lblsyrid" runat="server" Visible="false" Text='<%#Eval("FORMERID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblsybm" runat="server" Text='<%#Eval("FBM")%>'></asp:Label><asp:Label
                                            ID="lblsybumenid" runat="server" Visible="false" Text='<%#Eval("FBMID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbldate1" runat="server" Text='<%#Eval("TIME1")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblp1" runat="server" Text='<%#Eval("FPLACE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbllatter" runat="server" Text='<%#Eval("LATTERNAME")%>'></asp:Label><asp:Label
                                            ID="Label2" runat="server" Visible="false" Text='<%#Eval("LATTERID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbllbm" runat="server" Text='<%#Eval("LBM")%>'></asp:Label><asp:Label
                                            ID="Label4" runat="server" Visible="false" Text='<%#Eval("LBMID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbldate2" runat="server" Text='<%#Eval("TIME2")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblp2" runat="server" Text='<%#Eval("LPLACE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbljbr" runat="server" Text='<%#Eval("JBR")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblReason" runat="server" Text='<%#Eval("REASON")%>'></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
