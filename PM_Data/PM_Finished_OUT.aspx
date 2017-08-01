<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="PM_Finished_OUT.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Finished_OUT" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    ��Ʒ�������&nbsp;&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
        function ToStore() {
            window.open("PM_fahuo.aspx?FLAG=ToStore");
        }
    </script>
        <ContentTemplate>
            <div class="box-inner">
                <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                <div class="box_right">
                    <div class="box-title">
                        <table width="100%">
                            <tr>
                                <td width="40%">
                                    <asp:RadioButtonList ID="rbl_shenhe" runat="server" RepeatColumns="6" TextAlign="Right"
                                        AutoPostBack="true" OnSelectedIndexChanged="btn_search1_click">
                                    </asp:RadioButtonList>
                                </td>
                                <td align="center">
                                    <asp:Button runat="server" ID="btnDelete" Text="ɾ��" OnClick="btnDelete_OnClick" />
                                </td>
                                <td align="center">                                    
                                    <input id="ToStore" type="button" value="�����" onclick="ToStore()" runat="server" />&nbsp;&nbsp;
                                    <asp:Button ID="btnExport" runat="server" Text="����" OnClick="btnExport_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table>
                            <tr>
                                <td align="right">
                                    ��ͬ�ţ�
                                    <asp:TextBox ID="txtName" runat="server" Width="100px" Text=""></asp:TextBox>
                                </td>
                                <td>
                                    ��Ŀ����:
                                    <asp:TextBox runat="server" Width="100px" ID="txtXMMC"></asp:TextBox>
                                </td>
                                <td>
                                    ҵ����<asp:TextBox runat="server" Width="100px" ID="txtYZ"></asp:TextBox></td>
                                <td>
                                    ����ţ�<asp:TextBox runat="server" Width="100px" ID="txtRWH"></asp:TextBox></td>
                                <td>
                                    ͼ��:<asp:TextBox runat="server" Width="100px" ID="txtTH"></asp:TextBox></td>
                                <td>
                                    �豸���ƣ�<asp:TextBox runat="server" Width="100px" ID="txtSBMC"></asp:TextBox></td>
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
                    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                        border="1">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                            <HeaderTemplate>
                                <tr align="center" class="tableTitle">
                                    <td>
                                        ���
                                    </td>
                                    <td>
                                        ���ⵥ��
                                    </td>
                                    <td>
                                        ��Ŀ����
                                    </td>
                                    <td>
                                        ��ͬ��
                                    </td>
                                    <td>
                                        ���񵥺�
                                    </td>
                                    <td>
                                        <strong>����</strong>
                                    </td>
                                    <td>
                                        ͼ��
                                    </td>
                                    <td>
                                        �豸����
                                    </td>
                                    <td>
                                        ҵ��
                                    </td>
                                    <td>
                                        ��������
                                    </td>
                                    <td>
                                        ������
                                    </td>
                                    <td>
                                        ����ʱ��
                                    </td>
                                    <td>
                                        ��ע
                                    </td>
                                    <td>
                                        <strong>����״̬</strong>
                                    </td>
                                    <td>
                                        <strong>��ӡ</strong>
                                    </td>
                                    <td runat="server" id="hlookup">
                                        <strong>�鿴</strong>
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr align="center">
                                    <td>
                                        <asp:CheckBox runat="server" ID="cbxXuHao" />
                                        <asp:Label ID="xuhao" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label>
                                        <asp:Label ID="ID" runat="server" Text='<%#Eval("ID")%>' Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TFO_DOCNUM" runat="server" Text='<%#Eval("TFO_DOCNUM")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("proj")%>
                                    </td>
                                    <td>
                                        <%#Eval("contr")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="TSA_ID" runat="server" Text='<%#Eval("TSA_ID")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="TFO_ZONGXU" runat="server" Text='<%#Eval("TFO_ZONGXU")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("TFO_MAP")%>
                                    </td>
                                    <td width="160px">
                                        <asp:TextBox runat="server" ID="TFO_ENGNAME" TextMode="MultiLine" Rows="2" Text='<%#Eval("TFO_ENGNAME")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <%#Eval("CM_CUSNAME")%>
                                    </td>
                                    <td>
                                        <%#Eval("TFO_CKNUM")%>
                                    </td>
                                    <td>
                                        <%#Eval("CM_JHTIME")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblOutDate" runat="server" Text='<%#Eval("OUTDATE")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <%#Eval("NOTE")%>
                                    </td>
                                    <td>
                                        <asp:Label ID="SPZT" runat="server" Text='<%#Eval("SPZT")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="PZTTEXT" runat="server" Text='<%#get_spzt(Eval("SPZT").ToString())%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hplPrint" runat="server" NavigateUrl='<%#"PM_Finished_out_print.aspx?ID="+Eval("TFO_DOCNUM") %>'
                                        Target="_blank" CssClass="link">
                                        <asp:Image ID="imgPrint" runat="server" border="0" hspace="2" align="absmiddle" ImageUrl="~/Assets/images/DaYin.jpg" />
                                        ��ӡ
                                        </asp:HyperLink>
                                    </td>
                                    <td runat="server" id="blookup">
                                        <asp:HyperLink ID="HyperLink_lookup" runat="server" Target="_blank">
                                            <asp:Label ID="PUR_DD" runat="server" Text="�鿴"></asp:Label></asp:HyperLink>
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
</asp:Content>
