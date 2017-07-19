<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="TM_Pro_struinfo.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Pro_struinfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    产品模板信息表</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div  class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                    <td style="width:12%"><b>产品名称</b></td>
                     <td style="width:28%" align="center">工程类型:
                        <asp:DropDownList ID="ddlengtype" AutoPostBack="true" runat="server" 
                             onselectedindexchanged="ddlengtype_SelectedIndexChanged">
                            <asp:ListItem Text="回转窑" Value="回转窑" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="球、立磨" Value="球、立磨"></asp:ListItem>
                            <asp:ListItem Text="篦冷机" Value="篦冷机"></asp:ListItem>
                            <asp:ListItem Text="堆取料机" Value="堆取料机"></asp:ListItem>
                            <asp:ListItem Text="钢结构及非标" Value="钢结构及非标"></asp:ListItem>
                            <asp:ListItem Text="电气及其他" Value="电气及其他"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width:48%" align="left">按类查询:
                        <asp:DropDownList ID="ddlSearch" AutoPostBack="true" runat="server" 
                            onselectedindexchanged="ddlSearch_SelectedIndexChanged">
                            <asp:ListItem Text="名称" Value="PDS_NAME"></asp:ListItem>
                            <asp:ListItem Text="编号" Value="PDS_CODE"></asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;
                        <asp:TextBox ID="txtSearch" runat="server" Width="100px"></asp:TextBox>&nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="查询" onclick="btnSearch_Click"/>
                    </td>
                    <td align="right">
                        <asp:HyperLink ID="hlAdd" CssClass="link" NavigateUrl="~/TM_Data/TM_Pro_add.aspx" runat="server">
                         <asp:Image ID="AddImage" ImageUrl="~/Assets/images/Add_new_img.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                            添加产品
                        </asp:HyperLink>
                    </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <%--<table align="center" width="100%" cellpadding="4" cellspacing="1" class="toptable grid"
                border="1">--%>
                <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" DataKeyNames="NodeID" OnRowEditing="GridView1_RowEditing" 
                    onrowdeleting="GridView1_RowDeleting" onrowupdating="GridView1_RowUpdating">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                     <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("NodeID") %>'>&gt;</asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PDS_CODE" HeaderText="编号" ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PDS_NAME" HeaderText="名称" ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="PDS_ENGTYPE" HeaderText="工程类型" ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <%--<asp:BoundField DataField="PDS_ENGNAME" HeaderText="英文名称" ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>--%>
                        <%--<asp:BoundField DataField="ParentNodeID" HeaderText="上级ID" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="PDS_ISYENODE" HeaderText="是否有节点" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="PDS_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="编辑" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbnEdit" runat="server" class="link" CausesValidation="False" CommandName="Edit">
                                    <asp:Image ID="EdImage" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" runat="server" />编辑
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="更新" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbnUpdate" runat="server" class="link" CausesValidation="False" CommandName="Update">
                                    <asp:Image ID="UpImage" ImageUrl="~/Assets/images/yes.gif" border="0" hspace="2" runat="server" />更新
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除" ShowHeader="False" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbnDelete" runat="server" class="link" CausesValidation="False" 
                                    CommandName="Delete" onclientclick="return confirm(&quot;是否删除该记录？&quot;)">
                                <asp:Image ID="DeImage" ImageUrl="~/Assets/images/no.gif" border="0" hspace="2" runat="server" />删除
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
            <%--</table>--%>
            <div class="PageChange">
                <uc1:UCPaging ID="UCPaging1" runat="server" />
            </div>
        </div>
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
