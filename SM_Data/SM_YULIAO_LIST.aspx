<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="SM_YULIAO_LIST.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_YULIAO_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ���Ͽ���ѯ&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function PushConfirm() {
            var retVal = confirm("ȷ������ѡ����Ŀ�������ɳ��ⵥ��");
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
                                
                                <td align="center">
                                    <asp:Button ID="btnPush" runat="server" Text="����" OnClick="btnPush_OnClick" OnClientClick="return PushConfirm()"
                                        Visible="false" />
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
                                        <strong>����</strong>
                                    </td>
                                    <td>
                                        <strong>����</strong>
                                    </td>
                                    <td>
                                        <strong>���</strong>
                                    </td>
                                     <td>
                                        <strong>����(mm)</strong>
                                    </td>
                                     <td>
                                        <strong>���(mm)</strong>
                                    </td>
                                    <td>
                                        <strong>����</strong>
                                    </td>
                                    <td>
                                        <strong>ͼ��</strong>
                                    </td>
                                    <td>
                                        <strong>����(T)</strong>
                                    </td>
                                   
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                                    <td>
                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="checkBoxCss" /><%#Container.ItemIndex+1%>
                                        <input type="hidden" id="hidId" runat="server" value='<%#Eval("ID") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                    </td>
                                        <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("CAIZHI")%>'></asp:Label>
                                    </td>
                                        <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("GUIGE")%>'></asp:Label>
                                    </td>
                                        <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Length")%>'></asp:Label>
                                    </td>
                                        <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Weight")%>'></asp:Label>
                                    </td>
                                     
                                    <td>
                                        <asp:Label ID="lblNumber" runat="server" Text='<%#Eval("NUMBER")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("TUXING")%>'></asp:Label>
                                    </td>
                                       <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("Weight")%>'></asp:Label>
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
