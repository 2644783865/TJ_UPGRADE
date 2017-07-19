<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.Master" AutoEventWireup="true" CodeBehind="QC_ZJXGSH_TOTAL.aspx.cs" Inherits="ZCZJ_DPF.QC_Data.QC_ZJXGSH_TOTAL" Title="Untitled Page" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </asp:ToolkitScriptManager>
<div class="box-inner">
        <div class="box_right">
            <div class="box-title">
            <table>
               <tr>
                  <td>
                     生产制号：
                     <asp:TextBox ID="txtENGID" runat="server"></asp:TextBox>
                  </td>
                  <td>
                     项目名称：
                     <asp:TextBox ID="txtPJNAME" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="xmmc_AutoCompleteExtender" runat="server" 
                    DelimiterCharacters="" Enabled="True" FirstRowSelected="True" 
                    MinimumPrefixLength="1" ServiceMethod="xmmc" ServicePath="~/Ajax.asmx" 
                    TargetControlID="txtPJNAME" UseContextKey="True">
                    </asp:AutoCompleteExtender>
                  </td>
                  <td>
                     工程名称:
                     <asp:TextBox ID="txtENGNAME" runat="server"></asp:TextBox>
                   <asp:AutoCompleteExtender ID="gcmc_AutoCompleteExtender" runat="server" 
                    DelimiterCharacters="" Enabled="True" FirstRowSelected="True" 
                    MinimumPrefixLength="1" ServiceMethod="gcmc" ServicePath="~/Ajax.asmx" 
                    TargetControlID="txtENGNAME" UseContextKey="True"></asp:AutoCompleteExtender>
                  </td>
                  <td>
                  申请人：
                  <asp:TextBox ID="txtapplicant" runat="server"></asp:TextBox> 
                  </td>
                  <td>
                     审批状态：
                      <asp:DropDownList ID="DropDownListSTATE" runat="server" AutoPostBack="true" OnSelectedIndexChanged="STATE_OnSelectedIndexChanged" >
                           <asp:ListItem Text="我的审批任务" Value="0" Selected="True"></asp:ListItem>
                           
                           <asp:ListItem Text="审批中" Value="1" ></asp:ListItem>
                           <asp:ListItem Text="已通过" Value="2" ></asp:ListItem>
                           <asp:ListItem Text="已驳回" Value="3" ></asp:ListItem>
                           <asp:ListItem Text="全部" Value="4" ></asp:ListItem>
                      </asp:DropDownList>
                  </td>
                  <td align="right">
                 <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" />
                  </td>
               </tr>
            </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
         <asp:GridView ID="GridView1" Width="100%" CssClass="toptable" runat="server" AutoGenerateColumns="False"
             CellPadding="4" ForeColor="#333333" DataKeyNames="ID" OnDataBound="GridView1_DataBound"
                        OnRowDataBound="GridView1_RowDataBound" >
             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <RowStyle BackColor="White" />
             <Columns>
              <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false">
                  <ItemTemplate>
                       <%# Convert.ToInt32(Container.DataItemIndex +1) %>
                  </ItemTemplate>
                  <ItemStyle HorizontalAlign="Center" Wrap="false"></ItemStyle>
              </asp:TemplateField>
              
              <asp:TemplateField HeaderText="质检编号" ItemStyle-HorizontalAlign="Center" Visible="false">
                  <ItemTemplate>
                       <asp:Label ID="lbafiid" runat="server" Text='<%# Eval("AFI_ID") %>'></asp:Label>
                       <asp:Label ID="lbid" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="AFI_ENGID" HeaderText="生产制号" HeaderStyle-Wrap="false"
                  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="AFI_PJNAME" HeaderText="项目" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                  ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="AFI_ENGNAME" HeaderText="工程名称" HeaderStyle-Wrap="false"
                  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
              <asp:BoundField DataField="AFI_PARTNAME" HeaderText="部件名称" HeaderStyle-Wrap="false"
                  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" /> 
              <asp:BoundField DataField="AFI_MANCLERK" HeaderText="申请人" HeaderStyle-Wrap="false"
                  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" /> 
              <asp:BoundField DataField="AFI_CLERK_SJ" HeaderText="申请时间" HeaderStyle-Wrap="false"
                  ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />         
             <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="审核状态" >
                    <ItemTemplate>
                        <asp:Label ID="status" runat="server" Text='<%#Eval("AFI_STATUS").ToString()=="0"?"审核中":"已审核"%>'></asp:Label>                       
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
               <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="审核结果" >
                    <ItemTemplate>
                        <asp:HiddenField ID="shjg" runat="server" Value='<%#Eval("AFI_JG")%>' />
                        <asp:Label ID="jg" runat="server" ></asp:Label>                       
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="详细信息" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                       <ItemTemplate>
                            <asp:HyperLink ID="hykview" runat="server" NavigateUrl='<%#"~/QC_Data/QC_ZJXGSH.aspx?ACTION=view&ID="+Eval("ID")+"&AFIID="+Eval("AFI_ID")%>'>
                              <asp:Image ID="Image1" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />    
                                 查看
                            </asp:HyperLink>
                            
                       </ItemTemplate>
              </asp:TemplateField>     
              <asp:TemplateField HeaderText="审核" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                       <ItemTemplate>
                            <asp:HyperLink ID="hykqqsh" runat="server" Enabled="false" NavigateUrl='<%#"~/QC_Data/QC_ZJXGSH.aspx?ACTION=review&ID="+Eval("ID")+"&AFIID="+Eval("AFI_ID")%>'>
                              <asp:Image ID="Image2" ImageUrl="~/assets/images/res.gif" border="0" hspace="2" align="absmiddle"
                                        runat="server" />    
                                 审核
                            </asp:HyperLink>
                            <asp:HiddenField ID="issh" runat="server" Value='<%#Eval("AFI_ISSH")%>' />
                       </ItemTemplate>
               </asp:TemplateField>  
             </Columns>
             <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
             <EditRowStyle BackColor="#2461BF" />
         </asp:GridView>
         <asp:Panel ID="NoDataPanel" runat="server" ForeColor="Red">
                    没有任务!</asp:Panel>
                <div style="text-align: left; padding-top: 5px; padding-left: 15px">
                    </div>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    
</asp:Content>
