<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master"
    CodeBehind="YS_Cost_Budget_Audit_View.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Cost_Budget_Audit_View" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    预算审批
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <asp:Panel ID="pal" runat="server">
                            <td align="left">
                                任务号:
                                <asp:TextBox ID="txt_search" runat="server" Width="200px"></asp:TextBox><asp:Button
                                    ID="btn_search" runat="server" Text="查询" OnClick="btn_search_OnClick" />
                            </td>
                            <td align="center">
                                制单人：<asp:DropDownList ID="ddl_addper" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                                </asp:DropDownList>
                            </td>
                            <td align="center">
                                审核状态：<asp:DropDownList ID="ddl_State" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                                </asp:DropDownList>
                            </td>
                        </asp:Panel>
                        <td align="right">
                            <asp:CheckBox ID="ckb_user" runat="server" AutoPostBack="true" Text="只显示我的审核" OnCheckedChanged="ckb_user_OnCheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="width: 100%; overflow: auto;">
                <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid nowrap" runat="server"
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <%--序号--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                <asp:HiddenField ID="hdfMP_ID" runat="server" Value='<%# Eval("YS_TSA_ID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--任务号--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="任务号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TSAID" runat="server" Text='<%#Eval("YS_TSA_ID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--合同号--%>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="合同号" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_YS_CONTRACT_NO" runat="server" Text='<%#Eval("YS_CONTRACT_NO") %>'
                                    ToolTip="双击关联合同信息！"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--项目名称--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="项目名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_pjname" runat="server" Text='<%#Eval("CM_PROJ") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--设备名称--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="设备名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TSA_ENGNAME" runat="server" Text='<%#Eval("TSA_ENGNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--预算收入--%>
                        <asp:BoundField DataField="YS_BUDGET_INCOME" ItemStyle-HorizontalAlign="Center" HeaderText="￥ 预算收入（元）"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--预算总额--%>
                        <asp:BoundField DataField="YS_TOTALCOST_ALL" ItemStyle-HorizontalAlign="Center" HeaderText="￥ 预算总额（元）"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}"></asp:BoundField>
                        <%--毛利润--%>
                        <asp:BoundField DataField="YS_PROFIT" ItemStyle-HorizontalAlign="Center" HeaderText="￥ 毛利润（元）"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}"></asp:BoundField>
                        <%--毛利率--%>
                        <asp:BoundField DataField="YS_PROFIT_RATE" ItemStyle-HorizontalAlign="Center" HeaderText="毛利率"
                            HeaderStyle-Wrap="false" DataFormatString="{0:P}"></asp:BoundField>
                        <%--制单人--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="制单人" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_addper" runat="server" Text='<%#Eval("YS_ADDNAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--制单时间--%>
                        <asp:BoundField DataField="YS_ADDTIME" HeaderText="制单时间" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false"></asp:BoundField>
                        <%--进度--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="进度" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lab_State" runat="server" Text='<%# Eval("YS_STATE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--审核--%>
                        <asp:TemplateField HeaderText="审核" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlReview" CssClass="link" NavigateUrl='<%# GetEncodeUrl("audit",Eval("YS_TSA_ID").ToString()) %>'
                                    runat="server">
                                    <asp:Image ID="ImgReview" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                        align="absmiddle" runat="server" />审核
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--查看--%>
                        <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlView" CssClass="link" NavigateUrl='<%# GetEncodeUrl("view",Eval("YS_TSA_ID").ToString()) %>'
                                    runat="server">
                                    <asp:Image ID="imgView" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                        align="absmiddle" runat="server" />查看
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
                没有记录!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        var selectedColor = "#C0FF3E";
        var rowOverColor = "blue";
        var rowColor = "#EFF3FB";
        var selectedRows = new Object();

        function SelectRow(uniqueId, element) {
            if (typeof (selectedRows[uniqueId]) == "undefined")
                selectedRows[uniqueId] = false;
            selectedRows[uniqueId] = !selectedRows[uniqueId];
            element.style.backgroundColor = selectedRows[uniqueId] ? selectedColor : rowColor;
        }
        function ShowContract(id) {
            var autonum = Math.round(10000 * Math.random());
            window.open("../Contract_Data/CM_Contract_SW_Add.aspx?Action=View&autonum=" + autonum + "&condetail_id=" + id);
        }     
    
    </script>

</asp:Content>
