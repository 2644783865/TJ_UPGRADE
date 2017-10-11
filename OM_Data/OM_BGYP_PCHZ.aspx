<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master"
    AutoEventWireup="true" CodeBehind="OM_BGYP_PCHZ.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_BGYP_PCHZ" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    办公用品汇总申请
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
   <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%">
                    <tr>
                        <td align="right">
                            <strong>审核状态:</strong>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblState" RepeatColumns="5" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Year_SelectedIndexChanged">
                                <asp:ListItem Text="全部" Value=""></asp:ListItem>
                                <asp:ListItem Text="审核中" Value="1"></asp:ListItem>
                                <asp:ListItem Text="已通过" Value="2"></asp:ListItem>
                                <asp:ListItem Text="驳回" Value="3"></asp:ListItem>
                                <asp:ListItem Text="我的审核任务" Value="4" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="width: 40%" align="left">
                            申请时间：<asp:TextBox runat="server" ID="txt_starttime" class="easyui-datebox" editable="false"
                                Width="100px" Height="18px"></asp:TextBox>到
                            <asp:TextBox runat="server" ID="txt_endtime" class="easyui-datebox" editable="false"
                                Width="100px" Height="18px"></asp:TextBox>
                            <asp:Button ID="btnQuery" runat="server" Text="查  询" />&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/OM_Data/OM_BGYP_PCHZ_Detail.aspx?action=add"
                                runat="server">
                                <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2"
                                    ImageAlign="AbsMiddle" runat="server" />
                                添加申请
                            </asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 100%; overflow: auto">
            <asp:GridView ID="GridView1" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="行号" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Width="25px" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="25px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                        ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("PCCODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="JBR" HeaderText="申请人" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DATE" HeaderText="日期">
                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                    </asp:BoundField>
                  
                    <asp:BoundField DataField="SHRF" HeaderText="二级审核人">
                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SHRFDATE" HeaderText="二级日期" ItemStyle-HorizontalAlign="Center"
                        ItemStyle-Wrap="false">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:Label ID="status" runat="server" Text='<%# Eval("STATE").ToString()=="1"?"待审中":Eval("STATE").ToString()=="2"?"通过":"驳回" %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="审核" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlTask1" CssClass="link" runat="server"  NavigateUrl='<%#"~/OM_Data/OM_BGYP_PCHZ_Detail.aspx?action=audit&id="+Eval("PCCODE")%>'>
                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server" />
                                审核
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlTask2" CssClass="link" runat="server"  NavigateUrl='<%#"~/OM_Data/OM_BGYP_PCHZ_Detail.aspx?action=view&id="+Eval("PCCODE")%>'>
                                <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                    runat="server"  />
                               查看
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    
                    
                </Columns>
                <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <asp:Panel ID="NoDataPanel" runat="server">
                没有任务!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
