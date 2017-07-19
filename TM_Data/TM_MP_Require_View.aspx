<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_MP_Require_View.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MP_Require_View" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    材料需用计划表  
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <script type="text/javascript" language="javascript">
     function exportMPExcel()
     {
        var date=new Date();
        var nouse=date.getDate();//window.showModelessDialog
        var obj=new Object();
        window.showModalDialog("TM_MP_ExportExcel.aspx?action=<%=GetTaskID %>&time="+nouse,obj,"dialogHeight:250px;dialogWidth:450px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
     }
    </script>   
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
        <table width="98%">
            <tr>
                 <td align="right" style="width:10%">分类查询:</td>
                 <td align="left">
                    <asp:RadioButtonList ID="rblstate" RepeatColumns="2" runat="server" 
                        AutoPostBack="true" onselectedindexchanged="rblstate_SelectedIndexChanged">                
                    <asp:ListItem Text="正常" value="0" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td align="right" style="width:10%">状态:</td>
                <td align="left">
                    <asp:RadioButtonList ID="rblstatus" RepeatColumns="6" runat="server"  RepeatDirection="Horizontal"
                        AutoPostBack="true" onselectedindexchanged="rblstatus_SelectedIndexChanged">                
                    </asp:RadioButtonList>
                </td>
                <td align="center">
                    排序方式:<asp:DropDownList ID="ddlSort" runat="server"  onselectedindexchanged="rblstatus_SelectedIndexChanged"  AutoPostBack="true">
                   <asp:ListItem Text="批号" Value="MP_ID"></asp:ListItem>
                   <asp:ListItem Text="提交类型" Value="MP_MASHAPE"></asp:ListItem>
                   <asp:ListItem Text="提交日期" Value="MP_SUBMITTM" Selected="True"></asp:ListItem>
                   <asp:ListItem Text="批准日期" Value="MP_ADATE"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlSortOrder" runat="server"  onselectedindexchanged="rblstatus_SelectedIndexChanged"  AutoPostBack="true">
                   <asp:ListItem Text="升序" Value="0" Selected="True"></asp:ListItem>
                   <asp:ListItem Text="降序" Value="1"></asp:ListItem>
            </asp:DropDownList>
                </td>
                <td align="center">
                    <asp:HyperLink ID="HyperLink1" CssClass="hand"  onclick="exportMPExcel();" runat="server"><asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />导出EXCEL</asp:HyperLink>
                </td>
                <td align="right">
                
                  <asp:Image ID="Image3" ToolTip="返回上一页"  CssClass="hand"  Height="16" Width="16" runat="server" onclick="history.go(-1);" ImageUrl="~/Assets/icons/back.png" />
                </td>
               </tr>
        </table>
    </div>
    </div>
    </div>
 <div class="box-wrapper">
        <div class="box-outer">
                <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server" 
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" DataKeyNames="MP_ID">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                     <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="MP_ID" HeaderText="批号" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_MASHAPE" HeaderText="提交类别" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_PJNAME" HeaderText="项目名称" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_ENGNAME" HeaderText="设备名称" ItemStyle-HorizontalAlign="Center" />   
                      <asp:BoundField DataField="MP_MAP" HeaderText="图号" ItemStyle-HorizontalAlign="Center" />                  
                    <asp:BoundField DataField="MP_SUBMITTM" HeaderText="提交日期" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="MP_ADATE" HeaderText="批准日期" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lab_state" runat="server" Text='<%# Eval("MP_STATE").ToString()=="0"||Eval("MP_STATE").ToString()=="1"?"未提交":Eval("MP_STATE").ToString()=="2"?"待审核":Eval("MP_STATE").ToString()=="4"||Eval("MP_STATE").ToString()=="6"?"审核中...":Eval("MP_STATE").ToString()=="8"?"通过":Eval("MP_STATE").ToString()=="9"?"驳回已处理":"驳回" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="审核信息" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlDetail" CssClass="link" 
                         NavigateUrl='<%# "TM_HZY_pur.aspx?mpdetail_id="+Eval("MP_NO") %>' runat="server">
                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" 
                         border="0" hspace="2" align="absmiddle" runat="server" />查看                                
                        </asp:HyperLink>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="详细信息" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlEdit" CssClass="link" 
                         NavigateUrl='<%# "TM_HZY_pur.aspx?mpedit_id="+Eval("MP_NO") %>' runat="server">
                        <asp:Image ID="Image3" ImageUrl="~/Assets/images/res.gif" 
                         border="0" hspace="2" align="absmiddle" runat="server" />查看                                
                        </asp:HyperLink>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="hlDelete" CssClass="link" runat="server" CausesValidation="False" OnClick="hlDelete_OnClick"  CommandName='<%#Eval("MP_ID") %>' OnClientClick="return confirm(&quot;确认删除此批材料计划吗？&quot;)">
                        <asp:Image ID="Image4" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />作废                                
                        </asp:LinkButton>
                    </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:Panel ID="NoDataPanel" runat="server">没有记录!</asp:Panel>
                <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
</asp:Content>
