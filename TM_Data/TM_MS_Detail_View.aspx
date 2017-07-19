<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true" CodeBehind="TM_MS_Detail_View.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MS_Detail_View" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    制作明细     
</asp:Content> 
<asp:Content ID="Content1"  ContentPlaceHolderID="PrimaryContent" runat="server">
    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>
    <script type="text/javascript" language="javascript">
     function exportMSExcel()
     {
        var date=new Date();
        var nouse=date.getDate();
        var obj=new Object();
        window.showModalDialog("TM_MS_ExprotExcel.aspx?action=<%=GetTaskID %>&time="+nouse,obj,"dialogHeight:250px;dialogWidth:450px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
     }
     function ExportCheck()
     {
        var table=document.getElementById("<%=GridView1.ClientID %>");
        var rows=table.rows.length;
        var obj_ckb;
        for(var i=1;i<rows;i++)
        {
           obj_ckb=table.rows[i].cells[0].getElementsByTagName("input")[0];
           if(obj_ckb.checked)
           {
              return true;
           }
        }
        alert("请勾选要导出的批号！！！");
        return false;
     }
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
      <asp:PostBackTrigger ControlID="btnExportCheck" />
    </Triggers>
    <ContentTemplate>  
    <div class="box-inner">
    <div class="box_right">
    <div class="box-title">
        <table width="100%">
            <tr>
                <td style="width:10%"><b>制作明细查看</b></td>
                <td align="right">分类查询:</td>
                <td>
                    <asp:RadioButtonList ID="rblstate" RepeatColumns="2" runat="server" 
                        AutoPostBack="true" onselectedindexchanged="rblstate_SelectedIndexChanged">                
                    <asp:ListItem Text="正常" value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="变更" Value="1" ></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td align="right">状态:</td>
                <td>
                    <asp:RadioButtonList ID="rblstatus" RepeatColumns="6" runat="server" 
                        AutoPostBack="true" onselectedindexchanged="rblstatus_SelectedIndexChanged">                
                    </asp:RadioButtonList>
                </td>
                 
                <td align="center">
                    <asp:HyperLink ID="HyperLink1" CssClass="hand"  onclick="exportMSExcel();" runat="server"><asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2" align="absmiddle" runat="server" />导出EXCEL</asp:HyperLink>
                </td>
                <td align="center">
                    <asp:Button ID="btnExportCheck" runat="server" Width="60" OnClick="btnExportCheck_OnClick" OnClientClick="return ExportCheck();" Text="勾选导出" /></td>
                    <td align="center">
                    排序方式:<asp:DropDownList ID="ddlSort" runat="server"  onselectedindexchanged="rblstatus_SelectedIndexChanged"  AutoPostBack="true">
                   <asp:ListItem Text="批号" Value="MS_ID"></asp:ListItem>
                   <asp:ListItem Text="提交日期" Value="MS_SUBMITTM" Selected="True"></asp:ListItem>
                   <asp:ListItem Text="批准日期" Value="MS_ADATE"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlSortOrder" runat="server"  onselectedindexchanged="rblstatus_SelectedIndexChanged"  AutoPostBack="true">
                   <asp:ListItem Text="升序" Value="0" Selected="True"></asp:ListItem>
                   <asp:ListItem Text="降序" Value="1"></asp:ListItem>
            </asp:DropDownList>
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
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                     <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                            <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                            <asp:Label ID="lblMSID" runat="server" Visible="false" Text='<%#Eval("MS_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="MS_ID" HeaderText="批号" ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    <asp:BoundField DataField="MS_PJNAME" HeaderText="项目名称" 
                            ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    <asp:BoundField DataField="MS_ENGNAME" HeaderText="设备名称" 
                            ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="MS_MAP" HeaderText="图号" 
                            ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    <asp:BoundField DataField="MS_SUBMITTM" HeaderText="提交日期" 
                            ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    <asp:BoundField DataField="MS_ADATE" HeaderText="批准日期" 
                            ItemStyle-HorizontalAlign="Center" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lab_state" runat="server" Text='<%# Eval("MS_STATE").ToString()=="0"||Eval("MS_STATE").ToString()=="1"?"未提交":Eval("MS_STATE").ToString()=="2"?"待审核":Eval("MS_STATE").ToString()=="4"||Eval("MS_STATE").ToString()=="6"?"审核中...":Eval("MS_STATE").ToString()=="8"?"通过":Eval("MS_STATE").ToString()=="9"?"驳回已处理":"驳回" %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="详细信息" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlDetail" CssClass="link" 
                         NavigateUrl='<%# "TM_MS_Detail_Audit.aspx?id="+Eval("MS_NO") %>' runat="server">
                        <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif" 
                         border="0" hspace="2" align="absmiddle" runat="server" />查看                                
                        </asp:HyperLink>
                    </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操  作" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="hlDelete1" CssClass="link" runat="server"  CausesValidation="False" OnClick="hlDelete_OnClick" CommandArgument='<%#Eval("MS_ID")%>' CommandName="cancelMS"  
                            OnClientClick="return confirm(&quot;确认取消该批制作明细吗？&quot;)" >
                        <asp:Image ID="Image4" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2" align="absmiddle" runat="server" />取消                                
                        </asp:LinkButton>
                    </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>                   
                     <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="hlDelete2" CssClass="link" runat="server" OnClick="hlDelete_OnClick"
                            CausesValidation="False" CommandName="del"  CommandArgument='<%#Eval("MS_ID") %>'
                            OnClientClick="return confirm(&quot;制作明细是否作废？&quot;)" >
                        <asp:Image ID="Image5" ImageUrl="~/Assets/images/res.gif" 
                         border="0" hspace="2" align="absmiddle" runat="server" />作废                                
                        </asp:LinkButton>
                    </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
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
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

