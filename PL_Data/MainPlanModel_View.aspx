<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="MainPlanModel_View.aspx.cs" Inherits="ZCZJ_DPF.PL_Data.MainPlanModel_View" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    主生产计划模板
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">


    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 30%; height: 24px">
                            <b>主生产计划模板查看</b>
                        </td>
                        <td align="right">
                            <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            <asp:HyperLink ID="hplNew" runat="server" CssClass=";link" NavigateUrl="~/PL_Data/MainPlanModel_Edit.aspx?action=add&id=0">新增模板</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%; overflow: auto;">
            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MODEL_ID" HeaderText="模板编号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MODEL_NAME" HeaderText="模板名称" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MODEL_EDITDATE" HeaderText="最后编制/修改时间" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ST_NAME" HeaderText="最后编制/修改人员" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MODEL_RANGE" HeaderText="适用范围" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MODEL_NOTE" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="修改" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# "~/PL_Data/MainPlanModel_Edit.aspx?action=edit&id="+Eval("MODEL_ID")%>'
                                runat="server">
                                <asp:Image ID="Image11" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />修改模板</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Image ID="Image12" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                hspace="2" align="absmiddle" />
                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_OnClick" runat="server" CommandArgument='<%# Eval("MODEL_ID")%>'
                                CommandName="Del" OnClientClick="return confirm('确认删除吗?')">删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:Panel ID="NoDataPanel" runat="server">
                没有记录!</asp:Panel>
            <uc1:UCPaging ID="UCPaging" runat="server" />
        </div>
    </div>
</asp:Content>
