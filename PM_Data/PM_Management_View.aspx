<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="PM_Management_View.aspx.cs" Inherits="ZCZJ_DPF.PM_Data.PM_Management_View" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    任务分工表</asp:Content>
    
    <asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
           <script type="text/javascript" charset="GB2312">
      javascript:window.history.forward(-1); 
   </script>
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
 <div  class="box-inner">
 <asp:Label ID="ControlFinder" runat="server" Visible="false" ></asp:Label>
        <div class="box_right">
            <div class="box-title">
                <table style="width:98%; height:24px">
                <tr>
                    <td style="width:10%; height:24px"><b>分工登记</b></td>
                     <td>
                       <asp:RadioButtonList ID="rblstatus" RepeatColumns="3" runat="server" 
                            AutoPostBack="true" onselectedindexchanged="rblstatus_SelectedIndexChanged" >                
                            <asp:ListItem Text="待登记" value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="已登记" Value="1" ></asp:ListItem>
                            <asp:ListItem Text="不处理" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                       <td align="right">按类查询:</td>
                       <td align="center">
                       <asp:DropDownList ID="ddl_query" runat="server" AutoPostBack="true" Width="75%" >
                       <asp:ListItem Text="-请选择-" Value="0"></asp:ListItem>
                       <asp:ListItem Text="任务号" Value="1"></asp:ListItem>
                       <asp:ListItem Text="设备名称" Value="2"></asp:ListItem>
                       <asp:ListItem Text="技术负责人" Value="3"></asp:ListItem>
                       <asp:ListItem Text="质量负责人" Value="4"></asp:ListItem>
                       <asp:ListItem Text="调度员" Value="5"></asp:ListItem>
                       <asp:ListItem Text="制作班组" Value="6"></asp:ListItem>
                       <asp:ListItem Text="合同号" Value="7"></asp:ListItem>
                       <asp:ListItem Text="项目名称" Value="8"></asp:ListItem>
                       </asp:DropDownList>
                       </td>
                    <td align="left">
                        <asp:TextBox ID="txtSearch" runat="server" Width="120px"></asp:TextBox>
                     <%--<cc1:AutoCompleteExtender ID="txtSearch_AutoCompleteExtender" runat="server" 
                            DelimiterCharacters="" Enabled="True" ServicePath="~/Ajax.asmx" TargetControlID="txtSearch"
                            UseContextKey="True" ServiceMethod="TcAssignNames" CompletionInterval="1"
                             MinimumPrefixLength="1" CompletionSetCount="10">
                        </cc1:AutoCompleteExtender>--%>
                        <asp:Button ID="btnSearch" runat="server" Text=" 查 询 " onclick="btnSearch_Click" />
                    </td>
                    <td >
                      <asp:Button ID="btnDealWith" runat="server" Text=" 不处理 " onclick="btnDealWith_Click" />
                    </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="overflow:auto">
                <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" DataKeyNames="MTA_ID" OnDataBound="GridView1_DataBound" >
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:CheckBox ID="chtask" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="任务号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("MTA_ID") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>              
                     <asp:TemplateField  HeaderText="设备名称" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lbleng" runat="server" Text='<%# Eval("MTA_ENGNAME") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField  HeaderText="合同号" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblpjid" runat="server" Text='<%# Eval("MTA_PJID") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="项目名称" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblproj" runat="server" Text='<%# Eval("CM_PROJ") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField  HeaderText="技术负责人" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblfuzeren" runat="server" Text='<%# Eval("TSA_TCCLERKNM") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField  HeaderText="质量负责人" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblzlfzr" runat="server" Text='<%# Eval("QSA_QCCLERKNM") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="调度员" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lbldiaoduyuan" runat="server" Text='<%# Eval("MTA_DUY") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="制作班组" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblbanzu" runat="server" Text='<%# Eval("MTA_BANZU") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="预设计划开始" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblpstime" runat="server" Text='<%# Eval("MTA_PSTIME") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="预设计划完成" ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>
                            <asp:Label ID="lblpftime" runat="server" Text='<%# Eval("MTA_PFTIME") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblbeizhu" runat="server" Text='<%# Eval("MTA_NOTE") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField> 
                      <asp:TemplateField  HeaderText="是否入库" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lb_rk" runat="server" Text='<%# Eval("MTA_YNRK") %>'></asp:Label>       
                        </ItemTemplate>
                    </asp:TemplateField>                    
                  <asp:TemplateField HeaderText="分工" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:HyperLink ID="hlTask" CssClass="link" NavigateUrl='<%#"PM_Manut_Managent_DL.aspx?qsaid="+Eval("MTA_ID")+"&state="+Eval("state")%>'
                          runat="server">
                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />
                          <asp:Label ID="lbldengji" runat="server" ></asp:Label>                              
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
    
    </asp:Content>