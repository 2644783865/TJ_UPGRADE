<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PM_fahuo.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_fahuo"
    MasterPageFile="~/Masters/RightCotentMaster.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ��Ʒ����ѯ&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function PushConfirm() {
            var retVal = confirm("ȷ������ѡ����Ʒ�������ɳ��ⵥ��");
            return retVal;
        }

        function CheckNum(obj) {
            var pattem = /^[1-9][0-9]*$/; //������֤
            var testnum = obj.value;
            if (!pattem.test(testnum)) {
                alert("��������ȷ����ֵ������");
                obj.value = "1";
            }
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
                                    <asp:HiddenField ID="docnum" runat="server" />
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
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_OnItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle">
                                    <td>
                                        <strong>���</strong>
                                    </td>
                                    <td>
                                        <strong>���</strong>
                                    </td>
                                    <td>
                                        <strong>��Ŀ����</strong>
                                    </td>
                                    <td>
                                        <strong>��ͬ��</strong>
                                    </td>
                                    <td>
                                        <strong>���񵥺�</strong>
                                    </td>
                                    <td>
                                        <strong>����</strong>
                                    </td>
                                    <td>
                                        <strong>ͼ��</strong>
                                    </td>
                                    <td>
                                        <strong>��Ʒ����</strong>
                                    </td>
                                    <td>
                                        <strong>��������</strong>
                                    </td>
                                    <td>
                                        <strong>�ѱȼ�����</strong>
                                    </td>
                                    <td>
                                        <strong>�������</strong>
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
                                        <asp:Label ID="CM_FID" runat="server" Text='<%#Eval("CM_FID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="CM_ID" runat="server" Text='<%#Eval("CM_ID")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="KC_SINGNUM" runat="server" Text='<%#Eval("KC_SINGNUMBER")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="CM_BIANHAO" runat="server" Text='<%#Eval("CM_BIANHAO")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblProj" runat="server" Text='<%#Eval("KC_PROJ")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblContr" runat="server" Text='<%#Eval("BM_PJID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTsa" runat="server" Text='<%#Eval("BM_ENGID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="KC_ZONGXU" runat="server" Text='<%#Eval("KC_ZONGXU")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMap" runat="server" Text='<%#Eval("BM_TUHAO")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEngname" runat="server" Text='<%#Eval("BM_CHANAME")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCknum" onblur="CheckNum(this)" Text='<%#Eval("CM_FHNUM") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtYfnum" runat="server" Text='<%#Eval("BM_YFNUM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblkcnum" runat="server" Text='<%#Eval("KC_KCNUM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblnote" runat="server" Text='<%#Eval("TSA_IDNOTE")%>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        û����س�Ʒ��Ϣ!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
