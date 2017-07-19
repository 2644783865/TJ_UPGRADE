<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_GdzcIn.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.Master"
    Inherits="ZCZJ_DPF.OM_Data.OM_GdzcIn" Title="������" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �̶��ʲ�����ѯ&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function ToOrder() {
            window.open("OM_GdzcOrderDetail.aspx?FLAG=add&id=1");
        }

        $(function() {
            $("#tab tr:not(:first)").dblclick(function() {
                var id = $(this).find("input[name*=INCODE]").val();
                window.open("OM_GdzcOrderDetail.aspx?FLAG=read&id=" + id);
            });
        })
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td align="right">
                            <asp:HyperLink runat="server" ID="hplAdd" NavigateUrl="~/OM_Data/OM_GdzcOrderDetail.aspx?FLAG=add&id=1">
                                <asp:Image border="0" hspace="2" ImageAlign="AbsMiddle" Style="cursor: hand" runat="server"
                                    ID="image1" ImageUrl="~/Assets/images/create.gif" />
                                �������
                            </asp:HyperLink>
                            <%--<input id="ToOrder" type="button" value="�������" onclick="ToOrder()" runat="server" />--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <table width="100%">
                    <tr>
                        <td style="color: Red">
                            <strong>δ������Ŀ��<asp:Label ID="num" runat="server"></asp:Label></strong>
                        </td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="rblSPZT" AutoPostBack="true" OnSelectedIndexChanged="btnQuery_OnClick"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="ȫ��" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="������" Value="1"></asp:ListItem>
                                <asp:ListItem Text="��ͨ��" Value="2"></asp:ListItem>
                                <asp:ListItem Text="�Ѳ���" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            ���ƣ�
                            <asp:TextBox ID="txtName" runat="server" Text=""></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            ���������
                            <asp:TextBox ID="txtModel" runat="server" Text=""></asp:TextBox>&nbsp; ����1��
                            <asp:TextBox ID="txtType" runat="server" Text=""></asp:TextBox>&nbsp;
                        </td>
                        <td align="left">
                            <asp:Button ID="btnQuery" runat="server" Text="��  ѯ" OnClick="btnQuery_OnClick" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnReset" runat="server" Text="��  ��" OnClick="btnReset_OnClick" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <table id="tab" width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                    <HeaderTemplate>
                        <tr align="center" class="tableTitle" style="background-color: #B9D3EE">
                            <td>
                                ���
                            </td>
                            <td>
                                ��ⵥ��
                            </td>
                            <td>
                                �̶��ʲ����
                            </td>
                            <td>
                                ����
                            </td>
                            <td>
                                ����1
                            </td>
                            <td>
                                ����2
                            </td>
                            <td>
                                �������
                            </td>
                            <%--<td>
                                        �������
                                    </td>--%>
                            <td>
                                ʹ����
                            </td>
                            <td>
                                ʹ�ò���
                            </td>
                            <td>
                                �ص�
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
                            <%-- <td>
                                        �޸�
                                    </td>--%>
                            <td>
                                ��ӱ��
                            </td>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr align="center" onmouseover="mover(this)" onmouseout="mout(this)" title="˫���鿴��ϸ��Ϣ">
                            <td>
                                <%#Container.ItemIndex+1%>
                            </td>
                            <td>
                                <asp:Label ID="lblInCode" runat="server" Text='<%#Eval("INCODE")%>'></asp:Label>
                                <input type="hidden" runat="server" id="INCODE" name="INCODE" value='<%#Eval("INCODE")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblb_bh" runat="server" Text='<%#Eval("BIANHAO") %>'></asp:Label>
                            </td>
                            <td>
                                <%#Eval("NAME")%>
                            </td>
                            <td>
                                <%#Eval("TYPE")%>
                            </td>
                             <td>
                                <%#Eval("TYPE2")%>
                            </td>
                            <td>
                                <%#Eval("MODEL")%>
                            </td>
                            <%--<td>
                                        <%#Eval("INNUM")%>
                                    </td>--%>
                            <td>
                                <%#Eval("SYR")%>
                            </td>
                            <td>
                                <%#Eval("SYBUMEN")%>
                            </td>
                            <td>
                                <%#Eval("PLACE") %>
                            </td>
                            <td>
                                <asp:Label ID="lblInDate" runat="server" Text='<%#Eval("INDATE")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblInDoc" runat="server" Text='<%#Eval("CREATER")%>'></asp:Label>
                            </td>
                            <td>
                                <%#Eval("NOTE")%>
                            </td>
                            <%-- <td>
                                        <asp:HyperLink ID="link_xiugai" Visible="false" runat="server" CssClass="link" NavigateUrl='<%#"OM_GdzcOrderDetail.aspx?FLAG=mod&id="+Eval("INCODE") %>'></asp:HyperLink>
                                    </td>--%>
                            <td>
                                <asp:HyperLink ID="link_bh" runat="server" Visible="false" CssClass="link" NavigateUrl='<%#"OM_GdzcOrderDetail.aspx?FLAG=addbh&id="+Eval("INCODE") %>'>
                                    <asp:Image ID="image2" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/create.gif" />
                                    ��ӱ��</asp:HyperLink>
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
</asp:Content>
