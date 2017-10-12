<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="QC_TargetAnalyze_List.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_TargetAnalyze_List" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    目标分解
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script type="text/javascript">
        function openLink(url) {
            var returnVlue = window.showModalDialog("QC_TargetAnalyze_Upload.aspx?Id=" + url, '', "dialogHeight:150px;dialogWidth:500px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        }
    </script>

    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td style="width: 30%; height: 24px">
                            <b>中材(天津)重型机械有限公司各部门目标分解</b>
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="lbnNew" runat="server" OnClientClick="return openLink(0)">
                                <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />新增分解任务</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
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
                            <input type="hidden" runat="server" id="hidTargetId" value='<%# Eval("TARGET_ID")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TARGET_NAME" HeaderText="名  称" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="操作" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# "~/QC_Data/QC_TargetAnalyze_Edit.aspx?action=add&tarId="+Eval("TARGET_ID")%>'
                                runat="server">
                                <asp:Image ID="Image11" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />新增目标</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="操作" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink2" NavigateUrl='<%# "~/QC_Data/QC_TargetAnalyze_Edit.aspx?action=edit&tarId="+Eval("TARGET_ID")%>'
                                runat="server">
                                <asp:Image ID="Image16" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />修改目标</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                      <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="查看" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink3" NavigateUrl='<%# "~/QC_Data/QC_TargetAnalyze_Edit.aspx?action=view&tarId="+Eval("TARGET_ID")%>'
                                runat="server">
                                <asp:Image ID="Image17" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />查看</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_OnClick" runat="server" CommandArgument='<%# Eval("TARGET_ID")%>'
                                CommandName="Del" OnClientClick="return confirm('确认删除吗?')">
                                <asp:Image ID="Image12" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                    hspace="2" align="absmiddle" />删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="TARGET_NOTE" HeaderText="备  注" HeaderStyle-Wrap="false"
                        ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
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
