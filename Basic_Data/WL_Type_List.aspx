<%@ Page Language="C#" MasterPageFile="~/Masters/BaseMaster.master" AutoEventWireup="true" CodeBehind="WL_Type_List.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.WL_Type_List" Title="Untitled Page"%>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
 <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
    <table width="100%">
        <tr>
            <td> 物料大类:<asp:DropDownList ID="ddlTclass" runat="server" AutoPostBack ="true" 
                    onselectedindexchanged="ddlTclass_SelectedIndexChanged"></asp:DropDownList>
           </td>
            <td align="right"><asp:HyperLink ID="HyperLinkAdd" NavigateUrl="javascript:window.showModalDialog('WL_Type_Edit.aspx?FLAG=ADD&&ID=NO','','dialogWidth=650px;dialogHeight=400px');" runat="server">
            <asp:Image ID="ImageAdd" ImageUrl="~/assets/images/Add_new_img.gif" border="0" hspace="2" align="absmiddle" runat="server" />添加</asp:HyperLink>&nbsp;&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="删除" onclick="btnDelete_Click" />&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </div>
    </div>
    </div>
    <asp:Label ID="message" runat="server" ForeColor="Red"></asp:Label>
    
    <div class="box-wrapper">
    <div class="box-outer">
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <asp:Repeater ID="Repeater1" runat="server">
            <HeaderTemplate>
            <tr align="center" class="tableTitle">                
                <td><strong></strong></td>
                <td><strong>类别编码</strong></td>
                <td><strong>类别名称</strong></td>
                <td><strong>填写日期</strong></td>
                <td><strong>维护人</strong></td>
                <td><strong>类别状态</strong></td>
                <td><strong>备注</strong></td>
                <td><strong>修改</strong></td>
            </tr>
            </HeaderTemplate>
             <ItemTemplate>
            <tr align="center" class="baseGadget" onMouseOver="this.className='highlight'" onMouseOut="this.className='baseGadget'" style="text-align:left">
                 <td><asp:CheckBox ID="CheckBox1" CssClass="checkBoxCss" runat="server" /><asp:Label ID="LabelID" runat="server" Text='<%#Eval("id")%>' Visible="False"></asp:Label>
                <asp:Label ID="LineNumber" runat="server" Text='<%#Container.ItemIndex+1%>'></asp:Label></td>
                <td><%#Eval("id")%></td>
                <td><%#Eval("name")%></td>
                <td><%#Eval("filldate")%></td>
                <td><%#Eval("person")%></td>
                <td><asp:Label ID="LabelStatus" runat="server" Text='<%#convertStatus((string)Eval("status")) %>'></asp:Label></td>
                <td><%#Eval("comment")%></td>
                <td><asp:HyperLink ID="HyperLinkModify" NavigateUrl='<%# editWl(Eval("id").ToString()) %>' runat="server"><asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />修改</asp:HyperLink></td>   
            </tr>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Panel ID="NoDataPanel" runat="server" Visible="False">没有记录!</asp:Panel>
    </table>
 
    </div><!--box-outer END -->
    </div> <!--box-wrapper END -->                 
     
 </asp:Content>
