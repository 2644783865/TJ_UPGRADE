<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/masters/BaseMaster.master" CodeBehind="OnlineMan.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.OnlineMan" %>

<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <div class="RightContentTop"><asp:Image ID="Image1" ImageUrl="~/assets/images/desk_title_left_bg.gif" CssClass="rightTitleLeft_bg" runat="server" /><div class="RightContentTitle">
    
      密码还原 </div></div>
       <div class=RightContent>
        <div class="box-wrapper">
            <div class="box-outer">
               <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
    
    <tr>
    <td>
        当前用户: 
        <asp:Label ID="Label1" runat="server"></asp:Label>
              
       </td>      
       <td>
     <asp:Button ID="btnCom" runat="server" onclick="btnCom_Click" Text="密码还原" />
   
    </td>   
    <td>
    原始密码: 
        <asp:TextBox ID="txtPassword"   runat="server"></asp:TextBox>  
    </td>
    </tr>
    <tr>
    
    <td >加/解密
        <asp:TextBox ID="TextBox1" Width="200px"  runat="server"></asp:TextBox> 
        
            </td>      
       <td>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="加密" /> /<asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="解密" />   
              
       </td>      
       <td>
     
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>   
        
    </td>   
    </tr>
            
    </table>
    
     </div></div></div>
  </asp:Content>
    
