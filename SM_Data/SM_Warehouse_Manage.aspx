<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="SM_Warehouse_Manage.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_Warehouse_Manage" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">

    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    
    
    
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="LabelMessage" runat="server" ForeColor="Red"></asp:Label>             
            </td>
            <td align="right">
            仓库大类:<asp:DropDownList ID="DDLParent" runat="server" AutoPostBack ="true" 
                    OnSelectedIndexChanged="DDLParent_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;
            <asp:Button ID="Add" runat="server" Text="添加" OnClick="Add_Click" />&nbsp;&nbsp;
            <asp:Button ID="Delete" runat="server" Text="删除" OnClick="Delete_Click" />&nbsp;&nbsp;
            <asp:Button ID="LinkTo" runat="server" Text="仓位管理" OnClick="LinkTo_Click" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>

    
    <div class="box-wrapper">
    <div class="box-outer">
    
    
    
    
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
            <tr align="center" class="tableTitle1">
                <td><strong></strong></td>
                <td><strong></strong></td>                                
                <td><strong>仓库/库区编码</strong></td>
                <td><strong>仓库/库区名称</strong></td>
                <td><strong>填写日期</strong></td>
                <td><strong>维护人</strong></td>
                <td><strong>备注</strong></td>
                <td><strong>修改</strong></td>
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onmouseover="this.className='highlight'" onmouseout="this.className='baseGadget'">
                <td><asp:CheckBox ID="CheckBox1" CssClass="checkBoxCss" runat="server" /><asp:Label ID="LabelID" runat="server" Text='<%#Eval("ID")%>' Visible="False"></asp:Label></td>               
                <td><asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label></td>
                <td><%#Eval("ID")%></td>
                <td><%#Eval("NAME")%></td>
                <td><%#Eval("FILLDATE")%></td>
                <td><%#Eval("CLERK")%></td>
                <td><%#Eval("NOTE")%></td>
                <td><asp:HyperLink ID="HyperLinkModify" NavigateUrl='<%#"SM_Warehouse_Edit.aspx?FLAG=MODI&&ID="+Eval("ID")%>'  runat="server"><asp:Image ID="ImageModify" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />修改</asp:HyperLink></td>   
            </tr>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Panel ID="NoDataPanel" runat="server" Visible="False">没有记录!</asp:Panel>
    </table>
    </div><!--box-outer END -->
    </div> <!--box-wrapper END -->                 
     
</asp:Content>
