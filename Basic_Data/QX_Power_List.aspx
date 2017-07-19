<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="QX_Power_List.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.QX_Power_List" %>

<%@ Register TagPrefix="mb" TagName="Pager" Src="~/controls/ListPager.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    权限页面控件配置页面</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <!--div class="RightContentTop">
        <asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" /><div class="RightContentTitle"><table width=100%><tr><td width="15"><img  class=CloseImage title="关闭左边管理菜单" src="../assets/images/bar_hide.gif" style="CURSOR: hand" onClick="switchBar(this)"  border="0" alt=""></td><td>权限管理</td>
            <td width="15"><img src="../assets/images/bar_up.gif" title="隐藏" style="CURSOR: hand" onClick="switchTop(this)"  border="0" alt=""></td></tr></table></div>
    </div-->
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                权限名称：
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                <asp:Button ID="btn_select" runat="server" Text="查询" OnClick="btn_select_Click" />
                            </td>
                            <td align="right">
                                <asp:HyperLink ID="HyperLink3" NavigateUrl="javascript:void(window.open('QX_Power_Detail.aspx?Action=Add','',''));"
                                    runat="server">添加权限</asp:HyperLink>&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Button
                                        ID="btnExport0" runat="server" Text="权限/角色-导出" OnClick="btnExport0_Click" Visible="false" />
                                <asp:Button ID="btnExport" runat="server" Text="权限/人员-导出" OnClick="btnExport_Click"  />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box-wrapper">
            <div class="box-outer">
                <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid"
                    border="1">
                    <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                        OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging"
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("page_id") %>'>&gt;</asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="page_id" HeaderText="ID" />
                            <asp:BoundField DataField="name" HeaderText="权限名称" />
                            <asp:BoundField DataField="fatherName" HeaderText="父类名称" />
                            <asp:BoundField DataField="page" HeaderText="对应页面" />
                            <asp:BoundField DataField="controlNum" HeaderText="包含控件数量" />
                            <asp:TemplateField HeaderText="修改">
                                <ItemTemplate>
                                    <%-- <asp:LinkButton ID="lbtnattachview" PostBackUrl='<%#Eval("PD_Proj_Info_Detail_URL")%>'
                                     class="link" runat="server" CommandName="attachview">查看</asp:LinkButton>--%>
                                    <asp:HyperLink ID="hlDetail" CssClass="link" Target="_blank" NavigateUrl='<%#Eval("QX_Power_Detail_URL")%>'
                                        runat="server">
                                        <asp:Image ID="Image2" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                            runat="server" />详细信息
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblattachdel" class="link" runat="server" CommandName="del" OnClientClick="return confirm(&quot;是否删除该权限页面？&quot;)">删除</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#EEEEEE" ForeColor="#2461BF" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <PagerTemplate>
                            <table width="100%">
                                <tr>
                                    <td style="text-align: right">
                                        第<asp:Label ID="lblPageIndex" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>' />
                                        /<asp:Label ID="lblPageCount" runat="server" Text='<%# ((GridView)Container.Parent.Parent).PageCount  %>' />
                                        页
                                        <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" CommandArgument="First"
                                            CommandName="Page" Text="首页" />
                                        <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" CommandArgument="Prev"
                                            CommandName="Page" Text="上一页" />
                                        <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" CommandArgument="Next"
                                            CommandName="Page" Text="下一页" />
                                        <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" CommandArgument="Last"
                                            CommandName="Page" Text="尾页" />
                                        <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1  %>' />
                                        <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-1"
                                            CommandName="Page" Text="GO" />
                                        <!-- here set the CommandArgument of the Go Button to '-1' as the flag -->
                                    </td>
                                </tr>
                            </table>
                        </PagerTemplate>
                    </asp:GridView>
                </table>
                <div class="PageChange">
                    <mb:Pager ID="Pager" runat="server" />
                    <%--<asp:Button ID="DelBtn" runat="server" Text="    删除   " onclick="DelBtn_Click" /></div> --%>
                </div>
                <!--box-outer END -->
            </div>
            <!--box-wrapper END -->
        </div>
        <!--RightContent Part END -->
</asp:Content>
