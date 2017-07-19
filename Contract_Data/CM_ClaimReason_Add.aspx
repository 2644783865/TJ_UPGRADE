<%@ Page Language="C#" MasterPageFile="~/Masters/PopupBase.Master" AutoEventWireup="true" CodeBehind="CM_ClaimReason_Add.aspx.cs" Inherits="ZCZJ_DPF.Contract_Data.CM_ClaimReason_Add" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="RightContentTitlePlace">索赔原因 
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PrimaryContent">
    <script type="text/javascript" language="javascript">
    window.onload = function()    
     {    
         if( document.body.scrollWidth > (window.screen.availWidth-100) ){     
             window.dialogWidth = (window.screen.availWidth-100).toString() + "px"     
         }else{     
             window.dialogWidth = (document.body.scrollWidth +50).toString() + "px"     
         }         
             
         if( document.body.scrollHeight > (window.screen.availHeight-70) ){     
             window.dialogHeight = (window.screen.availHeight-50).toString() + "px"     
         }else{     
             window.dialogHeight = (document.body.scrollHeight +115).toString() + "px"     
         }         
  
         window.dialogLeft = ((window.screen.availWidth - document.body.clientWidth) / 2).toString() + "px"     
         window.dialogTop = ((window.screen.availHeight - document.body.clientHeight) / 2).toString() + "px"    
     }  

    </script>
    <table width="100%"><tr><td>
    <asp:Label ID="Label2" runat="server" Text="请输入索赔原因:"></asp:Label>&nbsp;&nbsp;
    <asp:TextBox ID="txtClaimReason" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnClaimReasonAdd" runat="server" Text="添 加" 
        onclick="btnClaimReasonAdd_Click" /></td></tr></table>
    <br /><br />
    <asp:GridView ID="grvClaimReason" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
        onrowdatabound="grvClaimReason_RowDataBound" 
        onrowupdating="grvClaimReason_RowUpdating" 
        onrowcancelingedit="grvClaimReason_RowCancelingEdit" 
        onrowdeleting="grvClaimReason_RowDeleting" 
        onrowediting="grvClaimReason_RowEditing"  >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                  <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label> 
                  </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField HeaderText="" DataField="SPR_ID" />--%>
                <asp:BoundField HeaderText="原因描述" DataField="SPR_DESCRIBLE" 
                    ItemStyle-HorizontalAlign="Center" >
                </asp:BoundField>
                 <asp:CommandField ShowEditButton="True" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" 
                            CommandName="Delete" Text="删除"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
             </Columns>
             <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" /> 
    </asp:GridView>
     <asp:Panel ID="palClaimReasonAdd" runat="server" HorizontalAlign="Center"><hr style="width:100%; height:0.1px; color:Blue;" />
         没有记录!</asp:Panel>   
</asp:Content>
