<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/masters/BaseMaster.master" CodeBehind="PassWordUpdate.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.PassWordUpdate" %>

<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="RightContentTop"><asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" /><div class="RightContentTitle">
    
      密码管理 </div></div>
       <div class=RightContent>
        <div class="box-wrapper">
            <div class="box-outer">
               <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
   <%-- <tr>
    <td >原始密码: 
       <asp:TextBox ID="txtOld" TextMode="Password"   runat="server"></asp:TextBox> 
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtOld" ErrorMessage="输入原密码" 
            Width="150px"  ></asp:RequiredFieldValidator>   
    </td> 
                
    </tr>--%>
     
   <tr>
    <td >新的密码: 
        <asp:TextBox ID="txtNew" TextMode="Password" runat="server"></asp:TextBox> 
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtNew" ErrorMessage="输入新密码" 
            Width="150px"  ></asp:RequiredFieldValidator>  </td>
                
    </tr> 
    
    <tr>
    <td >确认密码: 
        <asp:TextBox ID="txtCofirm"  TextMode="Password" runat="server"></asp:TextBox> 
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtCofirm" ErrorMessage="输入确认密码" 
            Width="150px"  ></asp:RequiredFieldValidator>  
       </td>         
    </tr>
  
  
    
    <tr>
    <td>
    <asp:Button ID="btnCom" runat="server" onclick="btnCom_Click" Text="修改" />
    <asp:Label ID="lblupdate" ForeColor="Red" runat="server" ></asp:Label>
    </td>
    
    </tr>
            
    </table>
    
     </div></div></div>
  </asp:Content>
    