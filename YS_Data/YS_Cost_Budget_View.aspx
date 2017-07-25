<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="YS_Cost_Budget_View.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Cost_Budget_View" Title="�ޱ���ҳ" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    Ԥ�����
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    
     <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td align="left">
                            ��ͬ��:
                            <asp:TextBox ID="txt_search" runat="server" Text="ZCZJ.SW.XS." Width="200px"></asp:TextBox><asp:Button
                                ID="btn_search" runat="server" Text="��ѯ" OnClick="btn_search_OnClick" />
                        </td>
                        <td align="center">
                            �Ƶ��ˣ�<asp:DropDownList ID="ddl_addper" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                        </td>
                        <td align="center">
                            ���״̬��<asp:DropDownList ID="ddl_revstate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:CheckBox ID="ckb_time" runat="server" AutoPostBack="true" Text="����δ��ɱ���" OnCheckedChanged="btn_search_OnClick" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td align="left">
                            ��Ŀ����:
                            <asp:DropDownList ID="ddl_project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_project_OnSelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ��������:
                            <asp:DropDownList ID="ddl_engineer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_import" runat="server" Text="��ϸ����" OnClick="btn_import_OnClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btn_orginal" runat="server" OnClick="btn_orginal_OnClick">
                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />�鿴�г���ԭʼָ��</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btnModify" runat="server" OnClick="btnModify_OnClick" Visible="false">
                                <asp:Image ID="ModImahe" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />�޸�Ԥ��</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btnAddMar" runat="server" OnClick="btnAddMar_OnClick">
                                <asp:Image ID="AddImage1" ImageUrl="~/Assets/icons/pcadd.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />����Ԥ��</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="ɾ��" Font-Size="Small" OnClick="btnDelete_OnClick"
                                OnClientClick="return confirm('ɾ���󲻿ɻָ���ȷ��ɾ����');" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="width: 100%; overflow: auto; overflow-x: yes; overflow-y: hidden;">
                <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid nowrap" runat="server"
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                    OnRowDataBound="GridView1_onrowdatabound" 
                    onselectedindexchanged="GridView1_SelectedIndexChanged">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="���" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                <asp:HiddenField ID="hdfMP_ID" runat="server" Value='<%# Eval("YS_CONTRACT_NO") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="��ͬ��" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_YS_CONTRACT_NO" runat="server" Text='<%#Eval("YS_CONTRACT_NO") %>'
                                    ToolTip="˫��������ͬ��Ϣ��"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="��Ŀ����" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_pjname" runat="server" Text='<%#Eval("PCON_PJNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="��������" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_engname" runat="server" Text='<%#Eval("PCON_ENGNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="YS_BUDGET_INCOME" ItemStyle-HorizontalAlign="Center" HeaderText="Ԥ������"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--Ԥ������--%>
                        <asp:BoundField DataField="YS_OUT_LAB_MAR" ItemStyle-HorizontalAlign="Center" HeaderText="������Э"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" HeaderStyle-ForeColor="Black" />
                        <%--������Э--%>
                        <asp:BoundField DataField="YS_FERROUS_METAL" ItemStyle-HorizontalAlign="Center" HeaderText="��ɫ����"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--��ɫ����--%>
                        <asp:BoundField DataField="YS_PURCHASE_PART" ItemStyle-HorizontalAlign="Center" HeaderText="�⹺��"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--�⹺��--%>
                        <asp:BoundField DataField="YS_MACHINING_PART" ItemStyle-HorizontalAlign="Center"
                            HeaderText="�ӹ���" HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--�ӹ���--%>
                        <asp:BoundField DataField="YS_PAINT_COATING" ItemStyle-HorizontalAlign="Center" HeaderText="����Ϳ��"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--����Ϳ��--%>
                        <asp:BoundField DataField="YS_ELECTRICAL" ItemStyle-HorizontalAlign="Center" HeaderText="��������"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--��������--%>
                        <asp:BoundField DataField="YS_OTHERMAT_COST" ItemStyle-HorizontalAlign="Center" HeaderText="�������Ϸ�"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--�������Ϸ�--%>
                        <asp:BoundField DataField="YS_TEAM_CONTRACT" ItemStyle-HorizontalAlign="Center" HeaderText="����а�"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--����а�--%>
                        <asp:BoundField DataField="YS_FAC_CONTRACT" ItemStyle-HorizontalAlign="Center" HeaderText="���ڷְ�"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--���ڷְ�--%>
                        <asp:BoundField DataField="YS_PRODUCT_OUT" ItemStyle-HorizontalAlign="Center" HeaderText="������Э"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--������Э--%>
                        <asp:BoundField DataField="YS_MANU_COST" ItemStyle-HorizontalAlign="Center" HeaderText="�������"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />
                        <%--�������--%>
                        <asp:BoundField DataField="YS_SELL_COST" ItemStyle-HorizontalAlign="Center" HeaderText="���۷���"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />
                        <%--���۷���--%>
                        <asp:BoundField DataField="YS_MANAGE_COST" ItemStyle-HorizontalAlign="Center" HeaderText="�������"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />
                        <%--�������--%>
                        <asp:BoundField DataField="YS_Taxes_Cost" ItemStyle-HorizontalAlign="Center" HeaderText="˰�𼰸���"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />
                        <%--˰�𼰸���--%>
                        <asp:BoundField DataField="YS_TRANS_COST" ItemStyle-HorizontalAlign="Center" HeaderText="�˷�"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />
                        <%--�˷�19--%>
                        <asp:BoundField DataField="YS_MAR_SUM" ItemStyle-HorizontalAlign="Center" HeaderText="���Ϸ�С��"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--���Ϸ�С��--%>
                        <asp:BoundField DataField="YS_FINA_SUM" ItemStyle-HorizontalAlign="Center" HeaderText="��̯��С��"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />
                        <%--�����С��--%>
                        <asp:BoundField DataField="YS_PROFIT" ItemStyle-HorizontalAlign="Center" HeaderText="�����ܶ�"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--�����ܶ�--%>
                        <asp:BoundField DataField="YS_PROFIT_TAX" ItemStyle-HorizontalAlign="Center" HeaderText="������"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--������--%>
                        <asp:BoundField DataField="YS_PROFIT_TAX_RATE" ItemStyle-HorizontalAlign="Center"
                            HeaderText="������" HeaderStyle-Wrap="false" DataFormatString="{0:P}" />
                        <%--������--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="�Ƶ���" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_addper" runat="server" Text='<%#Eval("YS_ADDNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="YS_ADDTIME" HeaderText="�Ƶ�ʱ��" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="YS_TIME" HeaderText="���Ԥ������" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="YS_NOTE" HeaderText="��ע" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="����״̬" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lab_editstate" runat="server" Text='<%# GetEditState(Eval("YS_CONTRACT_NO").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="���״̬" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lab_revstate" runat="server" Text='<%# GetRevState(Eval("YS_REVSTATE").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Ԥ�����" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Button ID="btn_YS_Modify" runat="server" Text="���·���Ԥ��" CommandArgument='<%# Eval("YS_CONTRACT_NO") %>'
                                    OnClientClick="return confirm('Ԥ�������ɾ����ǰ��ͬ������Ԥ�����ݣ������������ŵ����ݣ���������ǰһ�α��Ƶ�Ԥ�����ݿ������Ҳࡰ��Ԥ�����ݡ��в鿴�����������棬��������Ƿ�ȷ�ϵ�����');"
                                    OnClick="btn_YS_Modify_OnClick" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="��Ԥ������" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="Hp_View" runat="Server" ForeColor="Red" NavigateUrl='<%# Get_Old_YS(Eval("YS_CONTRACT_NO").ToString()) %>'>
                                    <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" ForeColor="Red"
                                        hspace="2" align="absmiddle" runat="server" />
                                    <asp:Label ID="check_look" runat="server" Text="�鿴"></asp:Label></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="����" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Button ID="btn_attachment" runat="server" Text="���/�鿴" CommandArgument='<%# Eval("YS_CONTRACT_NO") %>'
                                    OnClick="btn_attachment_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br></br>
            </div>
            <asp:Panel ID="NoDataPanel" runat="server">
                û�м�¼!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    <div>
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="width: 15" align="Left">
                    <img alt="" src="/YS_Data/Sienna.jpg" width="50px" height="15" />
                    <asp:Label ID="Label3" runat="Server" Text="��ʾ���������ϣ�δ�ύ�쵼���" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    &nbsp;&nbsp;&nbsp;
                    <img alt="" src="/YS_Data/Yellow.jpg" width="50px" height="15" />
                    <asp:Label ID="Label2" runat="Server" Text="��ʾ��Ҫ����" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    <img alt="" src="/YS_Data/red.jpg" width="50px" height="15" />
                    <asp:Label ID="Label1" runat="Server" Text="��ʾ�쵼��˲���" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    <img alt="" src="/YS_Data/pink.jpg" width="50px" height="15" />
                    <asp:Label ID="Label4" runat="Server" Text="���ͨ���������·���Ԥ��" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
