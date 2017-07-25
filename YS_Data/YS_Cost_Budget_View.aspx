<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="YS_Cost_Budget_View.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Cost_Budget_View" Title="无标题页" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    预算编制
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    
     <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td align="left">
                            合同号:
                            <asp:TextBox ID="txt_search" runat="server" Text="ZCZJ.SW.XS." Width="200px"></asp:TextBox><asp:Button
                                ID="btn_search" runat="server" Text="查询" OnClick="btn_search_OnClick" />
                        </td>
                        <td align="center">
                            制单人：<asp:DropDownList ID="ddl_addper" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                        </td>
                        <td align="center">
                            审核状态：<asp:DropDownList ID="ddl_revstate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:CheckBox ID="ckb_time" runat="server" AutoPostBack="true" Text="超期未完成编制" OnCheckedChanged="btn_search_OnClick" />
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
                            项目名称:
                            <asp:DropDownList ID="ddl_project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_project_OnSelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 工程名称:
                            <asp:DropDownList ID="ddl_engineer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="btn_import" runat="server" Text="明细导入" OnClick="btn_import_OnClick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btn_orginal" runat="server" OnClick="btn_orginal_OnClick">
                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />查看市场部原始指标</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btnModify" runat="server" OnClick="btnModify_OnClick" Visible="false">
                                <asp:Image ID="ModImahe" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />修改预算</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btnAddMar" runat="server" OnClick="btnAddMar_OnClick">
                                <asp:Image ID="AddImage1" ImageUrl="~/Assets/icons/pcadd.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />新增预算</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="删除" Font-Size="Small" OnClick="btnDelete_OnClick"
                                OnClientClick="return confirm('删除后不可恢复，确认删除吗？');" />
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
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                <asp:HiddenField ID="hdfMP_ID" runat="server" Value='<%# Eval("YS_CONTRACT_NO") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="合同号" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_YS_CONTRACT_NO" runat="server" Text='<%#Eval("YS_CONTRACT_NO") %>'
                                    ToolTip="双击关联合同信息！"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="项目名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_pjname" runat="server" Text='<%#Eval("PCON_PJNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="工程名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_engname" runat="server" Text='<%#Eval("PCON_ENGNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="YS_BUDGET_INCOME" ItemStyle-HorizontalAlign="Center" HeaderText="预算收入"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--预算收入--%>
                        <asp:BoundField DataField="YS_OUT_LAB_MAR" ItemStyle-HorizontalAlign="Center" HeaderText="技术外协"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" HeaderStyle-ForeColor="Black" />
                        <%--技术外协--%>
                        <asp:BoundField DataField="YS_FERROUS_METAL" ItemStyle-HorizontalAlign="Center" HeaderText="黑色金属"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--黑色金属--%>
                        <asp:BoundField DataField="YS_PURCHASE_PART" ItemStyle-HorizontalAlign="Center" HeaderText="外购件"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--外购件--%>
                        <asp:BoundField DataField="YS_MACHINING_PART" ItemStyle-HorizontalAlign="Center"
                            HeaderText="加工件" HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--加工件--%>
                        <asp:BoundField DataField="YS_PAINT_COATING" ItemStyle-HorizontalAlign="Center" HeaderText="油漆涂料"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--油漆涂料--%>
                        <asp:BoundField DataField="YS_ELECTRICAL" ItemStyle-HorizontalAlign="Center" HeaderText="电气电料"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--电气电料--%>
                        <asp:BoundField DataField="YS_OTHERMAT_COST" ItemStyle-HorizontalAlign="Center" HeaderText="其它材料费"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--其它材料费--%>
                        <asp:BoundField DataField="YS_TEAM_CONTRACT" ItemStyle-HorizontalAlign="Center" HeaderText="班组承包"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--班组承包--%>
                        <asp:BoundField DataField="YS_FAC_CONTRACT" ItemStyle-HorizontalAlign="Center" HeaderText="厂内分包"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--厂内分包--%>
                        <asp:BoundField DataField="YS_PRODUCT_OUT" ItemStyle-HorizontalAlign="Center" HeaderText="生产外协"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--生产外协--%>
                        <asp:BoundField DataField="YS_MANU_COST" ItemStyle-HorizontalAlign="Center" HeaderText="制造费用"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />
                        <%--制造费用--%>
                        <asp:BoundField DataField="YS_SELL_COST" ItemStyle-HorizontalAlign="Center" HeaderText="销售费用"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />
                        <%--销售费用--%>
                        <asp:BoundField DataField="YS_MANAGE_COST" ItemStyle-HorizontalAlign="Center" HeaderText="管理费用"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />
                        <%--管理费用--%>
                        <asp:BoundField DataField="YS_Taxes_Cost" ItemStyle-HorizontalAlign="Center" HeaderText="税金及附加"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />
                        <%--税金及附加--%>
                        <asp:BoundField DataField="YS_TRANS_COST" ItemStyle-HorizontalAlign="Center" HeaderText="运费"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />
                        <%--运费19--%>
                        <asp:BoundField DataField="YS_MAR_SUM" ItemStyle-HorizontalAlign="Center" HeaderText="材料费小计"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}" />
                        <%--材料费小计--%>
                        <asp:BoundField DataField="YS_FINA_SUM" ItemStyle-HorizontalAlign="Center" HeaderText="分摊费小计"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}" />
                        <%--财务费小计--%>
                        <asp:BoundField DataField="YS_PROFIT" ItemStyle-HorizontalAlign="Center" HeaderText="利润总额"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--利润总额--%>
                        <asp:BoundField DataField="YS_PROFIT_TAX" ItemStyle-HorizontalAlign="Center" HeaderText="净利润"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <%--净利润--%>
                        <asp:BoundField DataField="YS_PROFIT_TAX_RATE" ItemStyle-HorizontalAlign="Center"
                            HeaderText="净利率" HeaderStyle-Wrap="false" DataFormatString="{0:P}" />
                        <%--净利率--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="制单人" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_addper" runat="server" Text='<%#Eval("YS_ADDNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="YS_ADDTIME" HeaderText="制单时间" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="YS_TIME" HeaderText="完成预算期限" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="YS_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="完善状态" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lab_editstate" runat="server" Text='<%# GetEditState(Eval("YS_CONTRACT_NO").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="审核状态" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lab_revstate" runat="server" Text='<%# GetRevState(Eval("YS_REVSTATE").ToString()) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="预算调整" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Button ID="btn_YS_Modify" runat="server" Text="重新发起预算" CommandArgument='<%# Eval("YS_CONTRACT_NO") %>'
                                    OnClientClick="return confirm('预算调整会删除当前合同的所有预算数据（包括其它部门的数据），调整后，前一次编制的预算数据可以在右侧“旧预算数据”中查看，操作不可逆，请谨慎！是否确认调整？');"
                                    OnClick="btn_YS_Modify_OnClick" Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="旧预算数据" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:HyperLink ID="Hp_View" runat="Server" ForeColor="Red" NavigateUrl='<%# Get_Old_YS(Eval("YS_CONTRACT_NO").ToString()) %>'>
                                    <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" ForeColor="Red"
                                        hspace="2" align="absmiddle" runat="server" />
                                    <asp:Label ID="check_look" runat="server" Text="查看"></asp:Label></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="附件" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Button ID="btn_attachment" runat="server" Text="添加/查看" CommandArgument='<%# Eval("YS_CONTRACT_NO") %>'
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
                没有记录!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    <div>
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="width: 15" align="Left">
                    <img alt="" src="/YS_Data/Sienna.jpg" width="50px" height="15" />
                    <asp:Label ID="Label3" runat="Server" Text="表示部门审核完毕，未提交领导审核" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    &nbsp;&nbsp;&nbsp;
                    <img alt="" src="/YS_Data/Yellow.jpg" width="50px" height="15" />
                    <asp:Label ID="Label2" runat="Server" Text="表示需要完善" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    <img alt="" src="/YS_Data/red.jpg" width="50px" height="15" />
                    <asp:Label ID="Label1" runat="Server" Text="表示领导审核驳回" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    <img alt="" src="/YS_Data/pink.jpg" width="50px" height="15" />
                    <asp:Label ID="Label4" runat="Server" Text="审核通过，可重新发起预算" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
