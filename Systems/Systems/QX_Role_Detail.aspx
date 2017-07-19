<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" 
AutoEventWireup="true" CodeBehind="QX_Role_Detail.aspx.cs" Inherits="FSGD_PMS.Systems.QX_Role_Detail" %>

<asp:Content ID="Content1"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    权限角色配置页面</asp:Content> 

<asp:Content ID="Content2"  ContentPlaceHolderID="PrimaryContent" runat="server">

    <script language="javascript">
    function postBackByObject()
    {
        var o = window.event.srcElement;
        if (o.tagName == "INPUT" && o.type == "checkbox")
        {
            __doPostBack("","");
        }
    }
function Reset1_onclick() {

}

    </script>

    <%--<div class="RightContentTop"><asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" /><div class="RightContentTitle">
        添加组</div></div>
    <div class=RightContent>--%>
     <div class="box-inner"><div class="box_right"><div class=box-title><table width=100% ><tr><td> 
         添加组(带<span class="red">*</span>号的为必填项)</td></tr></table></div></div></div>
         <div class="box-wrapper">
            <div class="box-outer">

        <table cellpadding="4" cellspacing="1" class="toptable grid" border="1">        
            <tr>
                <td class="tableTitle" width="20%">
                    添加组:
                </td>
                <td width="80%">
                    <asp:TextBox ID="GroupName" runat="server"></asp:TextBox>
                    <asp:Label ID="LBCheckIsSame" runat="server" Text="" Visible="true" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tableTitle" valign="top">
                    权限配置:                 
                </td>
                <td>
                <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>--%>

                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                        <asp:TreeView ID="ConfigPower"
                            runat="server" 
                            Font-Name= "Arial"
                            RootNodeStyle-Font-Bold="true"
                            SelectedNodeStyle-ForeColor="Red"
                            ForeColor="Black"
                            EnableClientScript="true"
                            PopulateNodesFromClsient="true"
                            ShowLines="true"
                            NodeIndent = "20" 
                            ontreenodepopulate="ConfigPower_TreeNodePopulate"
                            OnTreeNodeCheckChanged="ConfigPower_TreeNodeCheckChanged"
                        >
                          
                            <Nodes>
                                <asp:TreeNode Value="0" Text="权限配置" Target="Content" PopulateOnDemand="true" Expanded="false"></asp:TreeNode>
                            </Nodes>
                        </asp:TreeView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="Confirm" runat="server" Text="   确定   " onclick="Confirm_Click" 
                         />
                    <asp:Button ID="Reset1" type="reset" value="   取消   " onclick="Reset1_onclick" />
                </td>
            </tr>
        
        </table>
           
     </div><!--box-outer END -->
   </div> <!--box-wrapper END -->
</div>

</asp:Content>