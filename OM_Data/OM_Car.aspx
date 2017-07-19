<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_Car.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_Car" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    车辆信息管理&nbsp;&nbsp;&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../Assets/main.css" rel="stylesheet" type="text/css" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="box-inner">
                <div class="box_right">
                    <div class="box-title">
                        <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
                        <table style="width: 98%; height: 24px">
                            <tr>
                                <td align="right">
                                    车辆状态：
                                </td>
                                <td align="left">
                                    <asp:RadioButtonList ID="rblstate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblstate_OnSelectedIndexChanged"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Text="全部" Selected="True" Value=""></asp:ListItem>
                                        <asp:ListItem Text="在厂" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="不在厂" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="text-align: right" width="200px">
                                    <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_CarAdd.aspx?action=add"
                                        runat="server">
                                        <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                            ImageAlign="AbsMiddle" runat="server" />
                                        新建车辆信息
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
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="grid_databound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="主键" ItemStyle-HorizontalAlign="Center" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbid" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CARNUM" HeaderText="车牌号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CARTYPE" HeaderText="类型" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CARCAPACITY" HeaderText="载客数" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MILEAGE" HeaderText="里程数（千米）" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="OIL" HeaderText="油量（升）" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="COLOR" HeaderText="颜色" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="是否在厂" ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="status" runat="server" Text='<%# Eval("STATE").ToString()=="0"?"在厂":"不在厂" %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                             <asp:BoundField DataField="IsDel" HeaderText="是否停用" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="保养提示" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                             <asp:BoundField  HeaderText="保险提示" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CARDYE" HeaderText="卡余额" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FZR" HeaderText="负责人" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="NOTE" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="详细信息" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlDetail" CssClass="link" runat="server" NavigateUrl='<%#"OM_CarInfo.aspx?action=look&id="+Eval("CARNUM")%>'>
                                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            ImageAlign="AbsMiddle" runat="server" />
                                        详细
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="车辆回厂" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                    <ItemTemplate>
                    <asp:LinkButton ID="link" runat="server" OnClick="link_change" CommandArgument='<%# Eval("ID")%>'  CommandName="back" OnClientClick="return confirm('确认回厂吗?')">
                    <%--<asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" ImageAlign="AbsMiddle" runat="server" />--%>
                            <%--     回厂
                    </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="修改信息" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlUpdate" CssClass="link" runat="server" NavigateUrl='<%#"OM_CarAdd.aspx?action=update&id="+Eval("ID")%>'>
                                        <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            ImageAlign="AbsMiddle" runat="server" />
                                        修改
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="删除" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Image ID="Image11" ImageUrl="~/Assets/images/res.gif" runat="server" border="0"
                                        hspace="2" align="absmiddle" />
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("ID")%>'
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
                    <asp:Panel ID="NoDataPanel" runat="server">
                        没有记录!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rblstate" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
