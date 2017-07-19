<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/masters/RightCotentMaster.Master" CodeBehind="enginfo.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.enginfo" %>

<%@ Register src="~/Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

    <asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    工程基本信息管理
    </asp:Content>
    <asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
   <%-- <div class="RightContentTop">        
         工程基本信息管理
    </div>--%>
     <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
     <div class="RightContent">      
      <div class="box-inner"><div class="box_right"><div class=box-title><table width=100%><tr><td>
             修改 / 删除工程</td> 
             <td align="right">查询类别:
             <asp:DropDownList ID="ddlQueryTye" runat="server">
                    <asp:ListItem Text="工程名称" Value="ENG_NAME" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="工程代号" Value="ENG_ID"></asp:ListItem>
                   </asp:DropDownList>                   
             </td>             
               <td>                   
               <asp:TextBox ID="tb_Query" runat="server"></asp:TextBox>&nbsp;
               <asp:Button
                   ID="btn_Query" runat="server" Text="查  询" onclick="btn_Query_Click" />&nbsp&nbsp&nbsp
                   <asp:Button
                       ID="btn_Showall" runat="server" Text="显示全部工程"  OnClick="btn_Showall_Click"/>
              </td>
             <td align="right">
             <asp:HyperLink ID="HyperLink3" NavigateUrl="javascript:window:showModalDialog('enginfoDetail.aspx?action=add','','dialogWidth=800px;dialogHeight=500px');" runat="server">
              <asp:Image ID="ImageTo" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" 
                    align="absmiddle" runat="server" />
             添加工程</asp:HyperLink></td></tr></table></div></div></div>
            <div class="box-wrapper">
            <div class="box-outer">
          <table width=100% align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
    <asp:Repeater ID="Reproject1" runat="server" >
    <HeaderTemplate >    
                 <tr align="center" class="tableTitle">              
                    <td><strong>工程代号</strong></td>
                    <td><strong>工程类型</strong></td>     
                    <td><strong>维护人</strong></td>
                    <td><strong>维护时间</strong></td> 
                    <td><strong>备注</strong></td>
                    <td><strong>修改</strong></td>  
                    <td><strong></strong></td>              
                  </tr>
                </HeaderTemplate>                
                <ItemTemplate>
             <tr class="baseGadget" onMouseOver="this.className='highlight'" onMouseOut="this.className='baseGadget'">                 
               <asp:Label ID="eng_ID"  runat="server" Visible="false" Text='<%#Eval("ENG_ID")%>'></asp:Label>                     
                 <td ><%#Eval("ENG_ID")%>&nbsp;</td> 
                  <td ><%#Eval("ENG_STRTYPE")%>&nbsp;</td> 
               <td ><%#Eval("ST_NAME")%>&nbsp;</td> 
                <td ><%#Eval("ENG_MANDATE")%>&nbsp;</td> 
                 <td><%#Eval("ENG_NOTE")%> &nbsp;</td>                        
                 <td><asp:HyperLink ID="HyperLink1" NavigateUrl='<%# editGc(Eval("ENG_ID").ToString()) %>'  runat="server" ><asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                     修改</asp:HyperLink></td>
                 <td><asp:CheckBox ID="CheckBox1" runat="server" /></td>
                </tr>
            </ItemTemplate> 
    </asp:Repeater>
    <asp:Panel ID="NoDataPanel" runat="server" ForeColor="Red">
                没有记录!</asp:Panel>
    </table>
                <asp:Label ID="lbl_Info" runat="server" Text="Label" Visible="False"></asp:Label>
       <div style=" text-align:right;padding-top:5px;padding-right:15px"> 
           <asp:Button ID="delete" runat="server" Text="删除" onclick="delete_Click" /></div>
       <div><uc1:UCPaging ID="UCPaging1" runat="server" /></div>
  </div> </div> </div>
  </asp:Content>