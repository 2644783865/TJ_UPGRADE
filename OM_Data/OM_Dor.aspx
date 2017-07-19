<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OM_Dor.aspx.cs" MasterPageFile="~/Masters/RightCotentMaster.Master"
    Inherits="ZCZJ_DPF.OM_Data.OM_Dor" Title="住宿管理" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    住宿管理&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=GB2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <table style="width: 100%; height: 24px">
                            <tr>
                                <td align="right">
                                    状态：
                                </td>
                                <td align="left">
                                    <asp:RadioButtonList ID="rblstate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblstate_OnSelectedIndexChanged"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Text="全部"  Value=""></asp:ListItem>
                                        <asp:ListItem Text="未审核" Value="0"></asp:ListItem>
                                      <asp:ListItem Text="审批中" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="已驳回" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="已退宿" Value="4"></asp:ListItem>
                                           <asp:ListItem Text="未我的审核任务" Value="5" Selected="True"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="text-align: right" width="100px">
                                    <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_DorApply.aspx?action=add"
                                        runat="server">
                                        <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                            ImageAlign="AbsMiddle" runat="server" />
                                        新增住宿申请
                                    </asp:HyperLink>
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
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="住宿单号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDorCode" runat="server" Text='<%#Eval("DORCODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DORNAME" HeaderText="姓名" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false"></asp:BoundField>
                            <asp:BoundField DataField="DORWNO" HeaderText="工号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false"></asp:BoundField>
                            <asp:BoundField DataField="DORDEP" HeaderText="部门" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false"></asp:BoundField>
                            <asp:BoundField DataField="DORPOS" HeaderText="岗位" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false"></asp:BoundField>
                            <asp:BoundField DataField="DORROOM" HeaderText="房间号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false"></asp:BoundField>
                            <asp:BoundField DataField="DORADDTIME" HeaderText="申请时间" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false"></asp:BoundField>
                            <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblState" runat="server" Text='<%#Eval("DORSTATE").ToString()=="0"?"未审核":Eval("DORSTATE").ToString()=="1"?"审批中":Eval("DORSTATE").ToString()=="2"?"已通过":Eval("DORSTATE").ToString()=="3"?"已驳回":"已退宿" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="详细信息" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlDetail" CssClass="link" runat="server" NavigateUrl='<%#"OM_DorApply.aspx?action=look&id="+Eval("DORCODE")%>'>
                                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            ImageAlign="AbsMiddle" runat="server" />
                                        查看
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改信息" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlTask" CssClass="link" runat="server" NavigateUrl='<%#"OM_DorApply.aspx?action=update&id="+Eval("DORCODE")%>'>
                                        <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            ImageAlign="AbsMiddle" runat="server" />
                                        修改
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="审核信息" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplreview" runat="server" CssClass="link" NavigateUrl='<%#"OM_DorApply.aspx?action=review&id="+Eval("DORCODE") %>'>
                                        <asp:Image ID="AddImage3" runat="server" border="0" hspace="2" align="absmiddle"
                                            ImageUrl="~/Assets/images/create.gif" />
                                        审核
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除信息" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Image ID="AddImage4" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                        hspace="2" align="absmiddle" />
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("DORCODE")%>'
                                        OnClick="lnkDelete_OnClick" CommandName="Del" OnClientClick="return confirm('确认删除吗?')">删除</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:Panel ID="NoDataPanel" runat="server" HorizontalAlign="Center" ForeColor="Red">
                        没有记录!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
