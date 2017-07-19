<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="MainPlan_View.aspx.cs" Inherits="ZCZJ_DPF.PL_Data.MainPlan_View" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    进度查看
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function Show(url) {
            var returnVlue = window.showModalDialog("MainPlan_Detail.aspx?Id=" + url, '', "dialogHeight:450px;dialogWidth:900px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }

    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 30%; height: 24px">
                            <b>中材(天津)重型机械有限公司项目进度查看</b>
                        </td>
                        <td style="width: 20%">
                            <asp:RadioButtonList ID="rblstatus" RepeatColumns="5" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                                <asp:ListItem Text="已生成计划" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="未制定计划" Value="0"></asp:ListItem>
                                <asp:ListItem Text="已完成" Value="2"></asp:ListItem>
                                <asp:ListItem Text="已作废" Value="3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <%-- <td align="right">
                            <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                align="absmiddle" runat="server" />
                            <asp:LinkButton ID="lbnNew" runat="server" CssClass="link" OnClientClick="return openLink(0)">新增分解任务</asp:LinkButton>
                        </td>--%>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%; overflow: auto;">
            <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            <input type="hidden" runat="server" id="hidTargetId" value='<%# Eval("MP_CODE")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MP_CODE" HeaderText="任务号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MP_PJID" HeaderText="合同号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MP_PJNAME" HeaderText="项目名称" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MP_ENGNAME" HeaderText="设备名称" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="技术准备" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="采购周期" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="生产周期" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MP_STARTDATE" HeaderText="下单日期" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MP_DELIVERYDATE" HeaderText="交货时间" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MP_ACTURALDELIVERDATE" HeaderText="实际交货时间" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                      
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状 态" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="tsa_status" runat="server" Text='<%# Eval("MP_STATE").ToString()=="0"?"未制定":Eval("MP_STATE").ToString()=="1"?"进行中":Eval("MP_STATE").ToString()=="2"?"已完成":"已作废" %>'></asp:Label>
                            <input type="hidden" runat="server" id="hidState" value='<%#  Eval("MP_STATE") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="查看" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# "~/PL_Data/MakePlan.aspx?tarId="+Eval("MP_CODE")%>'
                                runat="server">
                                <asp:Image ID="Image11" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />制定计划</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="操作" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Image ID="Image13" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                hspace="2" align="absmiddle" />
                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_OnClick" runat="server" CommandArgument='<%# Eval("MP_CODE")%>'
                                CommandName="Del" OnClientClick="return confirm('确认作废吗?')">作废</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="操作" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Image ID="Image14" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                hspace="2" align="absmiddle" />
                            <asp:LinkButton ID="lnkCom" OnClick="lnkDelete_OnClick" runat="server" CommandArgument='<%# Eval("MP_CODE")%>'
                                CommandName="Com" OnClientClick="return confirm('确认完工吗?')">完工</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="操作" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Image ID="Image15" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                hspace="2" align="absmiddle" />
                            <asp:LinkButton ID="lnkRes" OnClick="lnkDelete_OnClick" runat="server" CommandArgument='<%# Eval("MP_CODE")%>'
                                CommandName="Res" OnClientClick="return confirm('确认重用吗?')">重用</asp:LinkButton>
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
