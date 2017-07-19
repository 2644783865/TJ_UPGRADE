<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="OM_FGDZCZY_SP.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_FGDZCZY_SP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �ǹ̶��ʲ�ת������
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var id = $(this).find("input[name*=DH1]").val();
                window.open("OM_FGdzcTrans.aspx?action=read&id=" + id);
            });
        })
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblRW" AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="ȫ��" Value="0"></asp:ListItem>
                                <asp:ListItem Text="�ҵ���������" Value="1" Selected="True"></asp:ListItem>
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
            <div style="width: 100%; overflow: auto">
                <table id="tab" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                    style="white-space: normal" border="1">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_OnItemDataBound">
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
                                <td style="width: 100px">
                                    <strong>ת��ԭ��</strong>
                                </td>
                                <td>
                                    <strong>����</strong>
                                </td>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="baseGadget" onmouseover="mover(this)" onmouseout="mout(this)" title="˫���鿴��ϸ��Ϣ">
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
                                <td>
                                    <asp:HyperLink ID="link_bh" runat="server" Visible="false" CssClass="link" NavigateUrl='<%#"OM_FGdzcTrans.aspx?action=check&id="+Eval("DH") %>'>
                                        <asp:Image ID="image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                        ����</asp:HyperLink>
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
    </div>
</asp:Content>
