<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TM_MS_WaitforCreate.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_MS_WaitforCreate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register src="../Controls/UCPaging.ascx" tagname="UCPaging" tagprefix="uc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<base target="_self" />
<head runat="server">
   <META HTTP-EQUIV="pragma" CONTENT="no-cache"> 
   <META HTTP-EQUIV="Cache-Control" CONTENT="no-cache, must-revalidate"> 
    <META HTTP-EQUIV="expires" CONTENT="Wed, 26 Feb 1997 08:21:57 GMT">
    <title>待提交制作明细(未生成)</title>
     <link href="../Assets/main.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    function openLink(url)
    {
        var date=new Date();
        var time=date.getTime();
        var returnValue=window.showModalDialog("TM_MS_ShowNormalAdjustEdit.aspx?nouse="+time+"&xuhao=" + url,'',"dialogHeight:350px;dialogWidth:550px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
        if(returnValue==1)
        {
            window.location.reload();
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<Triggers>
 <asp:PostBackTrigger ControlID="btnExport" />
</Triggers>
<ContentTemplate>
    <div class="box-inner">
     <div class="box_right">
       <div class="box-title">
           <table width="100%">
             <tr>
               <td style="width:15%">任务号：
                <asp:Label ID="tsaid" runat="server"></asp:Label>
               </td>
               <td style="width:15%">合同号：
                <asp:Label ID="lblContract" runat="server"></asp:Label>
               </td>
             <td style="width:15%">项目名称：
                <asp:Label ID="proname" runat="server"></asp:Label>
               
            </td>
            <td style="width:15%">设备名称：
                <asp:Label ID="engname" runat="server"></asp:Label>
             
            </td>
             </tr>
           </table>
       </div>
     </div>
   </div>
   
    <div class="box-wrapper">
        <div class="box-outer">
        <table width="100%">
        <tr>
                   <%-- <td style="width:10%" align="right">隐藏虚拟部件:</td><td><asp:RadioButtonList ID="rblVirtual" AutoPostBack="true" OnSelectedIndexChanged="rblInMs_OnSelectedIndexChanged" runat="server" RepeatColumns="2">
                   <asp:ListItem Text="是" Selected="True" Value="Y"></asp:ListItem>
                   <asp:ListItem Text="否" Value="N"></asp:ListItem>
                </asp:RadioButtonList>
            </td>--%>
               <td align="right" style="width:10%">体现制作明细:</td>
               <td style="width:15%">
                   <asp:RadioButtonList ID="rblInMs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblInMs_OnSelectedIndexChanged" RepeatColumns="3" RepeatDirection="Horizontal">
                      <asp:ListItem Text="全部" Value="All"></asp:ListItem>
                      <asp:ListItem Text="是" Value="Y" ></asp:ListItem>
                      <asp:ListItem Text="否" Value="N"></asp:ListItem>
                   </asp:RadioButtonList>
               </td>
               <td align="right">部件序号:</td>
               <td style="width:18%; height:42px" align="left" valign="top"><cc1:ComboBox ID="ddlXuhao" runat="server" AutoPostBack="true" Height="15px"
                            AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList"
                            onselectedindexchanged="rblInMs_OnSelectedIndexChanged">
                            </cc1:ComboBox></td>
                           
               <td align="center">
               <asp:DropDownList runat="server" ID="ddlShebei">
                                                </asp:DropDownList>
                   <asp:Button ID="btnExport" runat="server" OnClick="btnExport_OnClick" OnClientClick="return confirm('确认导出吗？\r\r提示:导出的数据为当前调整项下的全部数据!!!');" Text="导出Excel" />
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnBack" runat="server" Text="返回调整" OnClick="btnBack_OnClick" />
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnMSCreate" runat="server" Text="生成制作明细" OnClientClick="return confirm('确认提交制作明细吗？\r\r提示：\r\r提交后将下推审核,制作明细无法修改！\r\r如果确认无误,请单击【确定】,否则单击【取消】！！！');" ToolTip="点击完成后,制作明细无法修改,请确认无误后再点击！" OnClick="btnMSCreate_OnClick" /></td>
        </tr>
        </table>
        <asp:Panel ID="NoDataPanel" runat="server"><div style="text-align:center; font-size:larger;">没有数据！</div>
            </asp:Panel>
       <yyc:SmartGridView ID="grv" runat="server" OnRowDataBound="grv_OnRowDataBound" AutoGenerateColumns="False" CssClass="toptable grid" 
             CellPadding="4" ForeColor="#333333" Width="100%">
        <RowStyle BackColor="#EFF3FB"/>
        <Columns>
        <asp:TemplateField HeaderText="行号">
                <ItemTemplate>
                    <asp:Label ID="lblIndex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="30px" />
            </asp:TemplateField>
           <%--  <asp:BoundField DataField="BM_MSXUHAO" HeaderText="明细序号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>--%>
            <%--<asp:BoundField DataField="BM_XUHAO" HeaderText="序号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>--%>
            <asp:BoundField DataField="BM_TUHAO" HeaderText="图号" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_ZONGXU" HeaderText="总序" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_CHANAME" HeaderText="名称" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAGUIGE" HeaderText="规格" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="BM_MAQUALITY" HeaderText="材质" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="NUMBER" HeaderText="单台数量|总数量" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>   
              
            
            <asp:BoundField DataField="BM_TUUNITWGHT" DataFormatString="{0:N2}" HeaderText="图纸单重(kg)" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
           
            <asp:BoundField DataField="BM_TUTOTALWGHT" DataFormatString="{0:N2}" HeaderText="图纸总重(kg)" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
              <asp:BoundField DataField="BM_MASHAPE" HeaderText="材料种类" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_TECHUNIT" HeaderText="单位" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
              <asp:BoundField DataField="BM_YONGLIANG" HeaderText="材料用量" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MATOTALWGHT" HeaderText="材料总重(kg)" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            
            <asp:BoundField DataField="BM_MALENGTH" HeaderText="长度(mm)" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_MAWIDTH" HeaderText="宽度(mm)" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_NOTE" HeaderText="下料备注" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
           
             <asp:BoundField DataField="BM_XIALIAO" HeaderText="下料方式" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_PROCESS" HeaderText="工艺流程" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
             <asp:BoundField DataField="BM_KU" HeaderText="库" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
           
        
        
            <asp:BoundField DataField="BM_ALLBEIZHU" HeaderText="备注" >
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            
        </Columns>
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
        <FixRowColumn FixRowType="Header,Pager" TableHeight="600px" TableWidth="100%" FixColumns="0,1,2" />        
      </yyc:SmartGridView>
            <uc1:UCPaging ID="UCPagingMS" runat="server" />
            
    </div>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
    <ProgressTemplate>
       <div style="position: absolute; top: 30%; right:40%">
       <table>
       <tr>
       <td align="right"><asp:Image ID="Image1" runat="server" ImageUrl="~/Assets/images/ajax-loader2.gif" /></td>
       <td align="left" style="background-color:Yellow; font-size:medium;">数据处理中，请稍后...</td>
       </tr>
       </table>
       </div>
    </ProgressTemplate>
</asp:UpdateProgress>
    </form>
</body>
</html>
