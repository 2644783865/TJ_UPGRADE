<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="PM_ProjectPlan.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_ProjectPlan" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    项目计划管理</asp:Content>
    
    <asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
 <div  class="box-inner">
        <div class="box_right">
                <table style="width:98%; height:24px">
                <tr>
                     <td style="width:14%;" align="right">项目计划制定状态：</td>
                    <td align="left" style="width:25%;" >
                       <asp:RadioButtonList ID="rblproplan" RepeatColumns="2" runat="server" 
                            AutoPostBack="true" onselectedindexchanged="rblproplan_SelectedIndexChanged" >                
                            <asp:ListItem Text="未制定" value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="已制定" Value="1" ></asp:ListItem>   
                      </asp:RadioButtonList>
                    </td>
                    </tr>
                </table>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
                <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"  OnDataBound="GridView1_DataBound" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        
                    </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="批号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblpid" runat="server" Text='<%# Eval("MS_ID") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="合同名称" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblpj" runat="server" Text='<%# Eval("MS_PJID") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>
                 <asp:TemplateField  HeaderText="任务号" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("MS_ENGID") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField  HeaderText="设备名称" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lbleng" runat="server" Text='<%# Eval("MS_ENGNAME") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目计划"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlTask" CssClass="link" NavigateUrl='<%#"PM_ProjectPlan_Detail.aspx?mnpid="+Eval("MS_ID")+"&Plan="+Eval("MS_PLAN")%>'
                          runat="server">
                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                         <asp:Label ID="lblplan" runat="server" ></asp:Label>                               
                        </asp:HyperLink>
                    </ItemTemplate>
                  <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>                 
            </Columns>
            <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />                    
        </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    <div>
        <asp:HiddenField ID="hfpldetail" runat="server" />
        <asp:Label ID="ControlFinder" runat="server" Visible="false" ></asp:Label>
    </div>
    </asp:Content>
