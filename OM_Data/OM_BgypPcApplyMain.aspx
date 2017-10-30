<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_BgypPcApplyMain.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_BgypPcApplyMain" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    �칫��Ʒ�ɹ�����&nbsp;&nbsp;&nbsp;&nbsp;
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

    <script language="javascript" type="text/javascript">
        //    function add()
        //    {
        //        var sRet = window.showModalDialog('OM_CARWXSQ_Detail.aspx?action=add','obj','dialogWidth=900px;dialogHeight=600px');
        //        if (sRet == "refresh") 
        //        {
        //            window.location.href = window.location.href;
        //        }
        //    }
        window.onload = function() {
            $("#<%=GridView1.ClientID %> tr").mouseover(function() {
                $(this).css("background", "#C8F7FF");
            }
 );

            $("#<%=GridView1.ClientID %> tr").mouseout(function() {
                $(this).css("background", "white");
            }
);
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%">
                    <tr>
                        <td align="right">
                            <strong>���״̬:</strong>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblstatus" RepeatColumns="5" runat="server" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:RadioButtonList>
                        </td>
                        <td style="width: 40%" align="left">
                            ����ʱ�䣺<asp:TextBox runat="server" ID="txt_starttime" class="easyui-datebox" editable="false"
                                Width="100px" Height="18px"></asp:TextBox>��
                            <asp:TextBox runat="server" ID="txt_endtime" class="easyui-datebox" editable="false"
                                Width="100px" Height="18px"></asp:TextBox>
                            <asp:Button ID="btnQuery" runat="server" Text="��  ѯ" OnClick="rblstatus_SelectedIndexChanged" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDaochu" runat="server" Text="�� ��" OnClick="btnDaochu_OnClick" />
                        </td>
                        <td>
                            <asp:HyperLink ID="HyperLink1" CssClass="link" NavigateUrl="~/OM_Data/OM_BGYP_PCDETAIL.aspx"
                                runat="server">
                                <asp:Image ID="Image2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                ��ѯ�ɹ���ϸ
                            </asp:HyperLink>
                        </td>
                        <td>
                            <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_BgypPcApply.aspx?action=add"
                                runat="server">
                                <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                    ImageAlign="AbsMiddle" runat="server" />
                                �������
                            </asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 100%; overflow: auto">
            <asp:GridView ID="GridView1" CssClass="toptable grid" runat="server" OnRowDataBound="GridView1_DATABOUND"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" ShowFooter="true">
                <FooterStyle BackColor="#EFF3FB" Font-Bold="True" ForeColor="#1E5C95" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="�к�" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Width="25px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="25px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("PCCODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="JBR" HeaderText="������" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DATE" HeaderText="����">
                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                    </asp:BoundField>
                    <asp:BoundField DataField="JINE" HeaderText="�ܶ�(Ԫ)" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="���//�鿴" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlTask1" CssClass="link" runat="server">
                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />
                                <asp:Label ID="state1" runat="server"></asp:Label>
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="SHRF" HeaderText="һ�������">
                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SHRFDATE" HeaderText="һ������" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="״̬" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="status" runat="server" Text='<%# Eval("STATE").ToString()=="1"?"������":Eval("STATE").ToString()=="2"?"ͨ��":"����" %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����ȡ��" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" Visible="false" runat="server" ToolTip="�Ѳ��زɹ�������ȡ��(ɾ��)��"
                                CssClass="link" OnClick="btnDelete_OnClick" CommandArgument='<%# Eval("PCCODE")%>'
                                OnClientClick="return confirm('���ȷ����ɾ���ò��صĲɹ�����¼����Ӧ�ɹ���ϸ��ȷ��ɾ��?')">
                                <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/erase.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                ����ȡ��
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:Panel ID="NoDataPanel" runat="server">
                û������!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
