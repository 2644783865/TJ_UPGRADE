<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master"
    CodeBehind="YS_Cost_Budget_View.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Cost_Budget_View" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    Ԥ�����
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <style>
        input
        {
            margin: 0;
        }
        td
        {
            height: 21px;
        }
        th
        {
            height: 25px;
        }
    </style>
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <table width="98%">
        <tr>
            <td>
                <asp:CheckBox ID="ckb_time" runat="server" AutoPostBack="true" Text="����δ��ɱ���" OnCheckedChanged="btn_search_OnClick" />
            </td>
        </tr>
        <tr>
            <td align="left">
                ��Ŀ����:
                <asp:DropDownList ID="ddl_project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_project_OnSelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                �豸����:
                <asp:DropDownList ID="ddl_engineer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                </asp:DropDownList>
            </td>
            <td align="left">
                �����:<asp:DropDownList ID="ddl_YS_TSA_ID" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                </asp:DropDownList>
            </td>
            <td align="center">
                Ԥ����ƽ��ȣ�<asp:DropDownList ID="ddl_State" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                </asp:DropDownList>
            </td>
            <td>
                �쵼��˽��ȣ�<asp:DropDownList ID="ddl_YS_REVSTATE" runat="server" AutoPostBack="true"
                    OnSelectedIndexChanged="btn_search_OnClick">
                </asp:DropDownList>
            </td>
            <td align="center">
                ���Ƶ��ˣ�<asp:DropDownList ID="ddl_addper" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <div style="width: 100%; overflow: auto;">
        <asp:GridView ID="GridView1" CssClass="toptable grid nowrap" runat="server" AutoGenerateColumns="False"
            CellPadding="4" ForeColor="#333333">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="���" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="YS_TSA_ID" ItemStyle-HorizontalAlign="center" HeaderText="�����"
                    HeaderStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="YS_CONTRACT_NO" ItemStyle-HorizontalAlign="center" HeaderText="��ͬ��"
                    HeaderStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="YS_PROJECTNAME" ItemStyle-HorizontalAlign="center" HeaderText="��Ŀ����"
                    HeaderStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="YS_ENGINEERNAME" ItemStyle-HorizontalAlign="center" HeaderText="�豸����"
                    HeaderStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="YS_BUDGET_INCOME" ItemStyle-HorizontalAlign="Right" HeaderText="���������"
                    HeaderStyle-Wrap="false" DataFormatString="{0:N2}"></asp:BoundField>
                <asp:BoundField DataField="YS_TOTALCOST_ALL" ItemStyle-HorizontalAlign="Right" HeaderText="����Ԥ���ܶ�"
                    HeaderStyle-Wrap="false" DataFormatString="{0:N2}"></asp:BoundField>
                <asp:BoundField DataField="YS_PROFIT" ItemStyle-HorizontalAlign="Right" HeaderText="Ԥ��ë����"
                    HeaderStyle-Wrap="false" DataFormatString="{0:N2}"></asp:BoundField>
                <asp:BoundField DataField="YS_PROFIT_RATE" ItemStyle-HorizontalAlign="Right" HeaderText="Ԥ��ë����"
                    HeaderStyle-Wrap="false" DataFormatString="{0:P}"></asp:BoundField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="�ɹ�����" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lab_YS_CAIGOU" runat="server" Text='<%# GetFeedBackState(Eval("YS_CAIGOU").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="��������" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lab_YS_SHENGCHAN" runat="server" Text='<%# GetFeedBackState(Eval("YS_SHENGCHAN").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="������������" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lab_YS_SHENGCHAN" runat="server" Text='<%# GetDisRevState(Eval("YS_CAIWU").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Ԥ����ƽ���" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lab_YS_STATE" runat="server" Text='<%# GetState(Eval("YS_STATE").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="�쵼��˽���" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lab_YS_REVSTATE" runat="server" Text='<%# GetRevState( Eval("YS_REVSTATE").ToString() )%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="һ�����" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lab_YS_FIRST_REVSTATE" runat="server" Text='<%# GetDisRevState(Eval("YS_FIRST_REVSTATE").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="�������" HeaderStyle-Wrap="false">
                    <ItemTemplate>
                        <asp:Label ID="lab_SECOND_REVSTATE" runat="server" Text='<%# GetDisRevState(Eval("YS_SECOND_REVSTATE").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="YS_MATERIAL_COST" ItemStyle-HorizontalAlign="Right" HeaderText="���Ϸ�"
                    HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" >
                </asp:BoundField>
                <asp:BoundField DataField="YS_LABOUR_COST" ItemStyle-HorizontalAlign="Right" HeaderText="�˹���"
                    HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" >
                </asp:BoundField>
                <%--<asp:BoundField DataField="YS_FERROUS_METAL" ItemStyle-HorizontalAlign="Right" HeaderText="��ɫ����"
                    HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}">
                </asp:BoundField>
                <asp:BoundField DataField="YS_PURCHASE_PART" ItemStyle-HorizontalAlign="Right" HeaderText="�⹺��"
                    HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}">
                </asp:BoundField>
                <asp:BoundField DataField="YS_MACHINING_PART" ItemStyle-HorizontalAlign="Right" HeaderText="�ӹ���"
                    HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}">
                </asp:BoundField>
                <asp:BoundField DataField="YS_PAINT_COATING" ItemStyle-HorizontalAlign="Right" HeaderText="����Ϳ��"
                    HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}">
                </asp:BoundField>
                <asp:BoundField DataField="YS_ELECTRICAL" ItemStyle-HorizontalAlign="Right" HeaderText="��������"
                    HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}">
                </asp:BoundField>
                <asp:BoundField DataField="YS_OTHERMAT_COST" ItemStyle-HorizontalAlign="Right" HeaderText="�������Ϸ�"
                    HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}">
                </asp:BoundField>
                <asp:BoundField DataField="YS_TEAM_CONTRACT" ItemStyle-HorizontalAlign="Right" HeaderText="ֱ���˹�"
                    HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Salmon" DataFormatString="{0:N2}">
                </asp:BoundField>
                <asp:BoundField DataField="YS_FAC_CONTRACT" ItemStyle-HorizontalAlign="Right" HeaderText="���ڷְ�"
                    HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Salmon" DataFormatString="{0:N2}">
                </asp:BoundField>
                <asp:BoundField DataField="YS_PRODUCT_OUT" ItemStyle-HorizontalAlign="Right" HeaderText="������Э"
                    HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Salmon" DataFormatString="{0:N2}">
                </asp:BoundField>--%>
                <asp:BoundField DataField="YS_TRANS_COST" ItemStyle-HorizontalAlign="Right" HeaderText="�˷�"
                    HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" >
                </asp:BoundField>
                <asp:BoundField DataField="YS_TEC_SUBMIT_NAME" HeaderText="�������ύ��" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="YS_ADDTIME" HeaderText="�������ύʱ��" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="YS_ADDNAME" ItemStyle-HorizontalAlign="Center" HeaderText="�����Ƶ���"
                    HeaderStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="YS_ADDFINISHTIME" HeaderText="�Ƶ��������" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="YS_NOTE" HeaderText="��ע" ItemStyle-HorizontalAlign="Center"
                    HeaderStyle-Wrap="false"></asp:BoundField>
                <asp:TemplateField HeaderText="����" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLink1" Target="_blank" CssClass="link" NavigateUrl='<%#"YS_Cost_Budget_Add_Detail.aspx?tsaId="+Eval("YS_TSA_ID")%>'
                            runat="server">
                            <asp:Image ID="img_look" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            ��ϸ��Ϣ��
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </div>
    <asp:Panel ID="NoDataPanel" runat="server">
        û�м�¼!</asp:Panel>
    <div style="position: absolute; margin-top: 5px;">
        <strong>��λ��</strong>Ԫ
    </div>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
</asp:Content>
